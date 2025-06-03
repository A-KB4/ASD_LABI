using System;
using System.Text;

class MyStack
{
    private int[] items = new int[100];
    private int top = -1;

    public void Push(int value)
    {
        if (top < items.Length - 1)
            items[++top] = value;
        else
            Console.WriteLine("Стек переповнено.");
    }

    public int Pop()
    {
        if (top >= 0)
            return items[top--];
        throw new InvalidOperationException("Стек порожній.");
    }

    public int Peek()
    {
        if (top >= 0)
            return items[top];
        throw new InvalidOperationException("Стек порожній.");
    }

    public void Clear()
    {
        top = -1;
    }

    public int Count => top + 1;

    public void Display()
    {
        for (int i = top; i >= 0; i--)
            Console.WriteLine(items[i]);
    }

    public int[] ToArray()
    {
        int[] result = new int[top + 1];
        for (int i = 0; i <= top; i++)
            result[i] = items[i];
        return result;
    }
}

class MyQueue
{
    private double[] items = new double[100];
    private int head = 0;
    private int tail = 0;
    private int count = 0;

    public void Enqueue(double value)
    {
        if (count < items.Length)
        {
            items[tail] = value;
            tail = (tail + 1) % items.Length;
            count++;
        }
        else
            Console.WriteLine("Черга переповнена.");
    }

    public double Dequeue()
    {
        if (count > 0)
        {
            double val = items[head];
            head = (head + 1) % items.Length;
            count--;
            return val;
        }
        throw new InvalidOperationException("Черга порожня.");
    }

    public double Peek()
    {
        if (count > 0)
            return items[head];
        throw new InvalidOperationException("Черга порожня.");
    }

    public void Clear()
    {
        head = tail = count = 0;
    }

    public int Count => count;

    public void Display()
    {
        for (int i = 0; i < count; i++)
        {
            int index = (head + i) % items.Length;
            Console.WriteLine(items[index]);
        }
    }

    public double[] ToArray()
    {
        double[] result = new double[count];
        for (int i = 0; i < count; i++)
        {
            int index = (head + i) % items.Length;
            result[i] = items[index];
        }
        return result;
    }
}

class Program
{
    static MyStack intStack = new MyStack();
    static MyQueue doubleQueue = new MyQueue();

    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;

        while (true)
        {
            Console.WriteLine("\nМеню:");
            Console.WriteLine("1 - Додати елемент до стеку");
            Console.WriteLine("2 - Видалити елемент зі стеку");
            Console.WriteLine("3 - Відобразити стек");
            Console.WriteLine("4 - Переглянути верхівку стеку");
            Console.WriteLine("5 - Завдання 1: Добуток непарних елементів стеку");
            Console.WriteLine("6 - Додати елемент до черги");
            Console.WriteLine("7 - Видалити елемент з черги");
            Console.WriteLine("8 - Відобразити чергу");
            Console.WriteLine("9 - Переглянути перший елемент черги");
            Console.WriteLine("10 - Завдання 2: Кількість додатних елементів черги");
            Console.WriteLine("0 - Вихід");
            Console.Write("Ваш вибір: ");
            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    Console.Write("Введіть ціле число для стеку: ");
                    if (int.TryParse(Console.ReadLine(), out int stackVal))
                        intStack.Push(stackVal);
                    else
                        Console.WriteLine("Невірне значення.");
                    break;

                case "2":
                    try
                    {
                        Console.WriteLine($"Видалено: {intStack.Pop()}");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    break;

                case "3":
                    Console.WriteLine("Стек:");
                    intStack.Display();
                    break;

                case "4":
                    try
                    {
                        Console.WriteLine($"Верхівка стеку: {intStack.Peek()}");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    break;

                case "5":
                    Console.WriteLine("Генерується стек з 10 випадкових цілих чисел...");
                    intStack.Clear();
                    Random rand1 = new Random();
                    for (int i = 0; i < 10; i++)
                        intStack.Push(rand1.Next(-20, 21));

                    Console.WriteLine("Згенерований стек:");
                    intStack.Display();

                    long product = 1;
                    bool hasOdd = false;
                    foreach (int val in intStack.ToArray())
                    {
                        if (val % 2 != 0)
                        {
                            product *= val;
                            hasOdd = true;
                        }
                    }
                    Console.WriteLine(hasOdd ? $"Добуток непарних елементів: {product}" : "Немає непарних елементів.");
                    break;

                case "6":
                    Console.Write("Введіть дійсне число для черги: ");
                    if (double.TryParse(Console.ReadLine(), out double queueVal))
                        doubleQueue.Enqueue(queueVal);
                    else
                        Console.WriteLine("Невірне значення.");
                    break;

                case "7":
                    try
                    {
                        Console.WriteLine($"Видалено: {doubleQueue.Dequeue()}");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    break;

                case "8":
                    Console.WriteLine("Черга:");
                    doubleQueue.Display();
                    break;

                case "9":
                    try
                    {
                        Console.WriteLine($"Перший елемент черги: {doubleQueue.Peek()}");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    break;

                case "10":
                    Console.WriteLine("Генерується черга з 10 випадкових дійсних чисел...");
                    doubleQueue.Clear();
                    Random rand2 = new Random();
                    for (int i = 0; i < 10; i++)
                        doubleQueue.Enqueue(Math.Round(rand2.NextDouble() * 20 - 10, 2));

                    Console.WriteLine("Згенерована черга:");
                    doubleQueue.Display();

                    int count = 0;
                    foreach (double val in doubleQueue.ToArray())
                        if (val > 0) count++;

                    Console.WriteLine($"Кількість додатних елементів: {count}");
                    break;

                case "0":
                    return;

                default:
                    Console.WriteLine("Невірний вибір. Спробуйте ще раз.");
                    break;
            }
        }
    }
}
