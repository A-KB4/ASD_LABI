using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class Program
{
    static Stack<int> intStack = new Stack<int>();
    static Queue<double> doubleQueue = new Queue<double>();

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
                    if (intStack.Count > 0)
                        Console.WriteLine($"Видалено: {intStack.Pop()}");
                    else
                        Console.WriteLine("Стек порожній.");
                    break;

                case "3":
                    Console.WriteLine("Стек:");
                    foreach (int val in intStack)
                        Console.WriteLine(val);
                    break;

                case "4":
                    if (intStack.Count > 0)
                        Console.WriteLine($"Верхівка стеку: {intStack.Peek()}");
                    else
                        Console.WriteLine("Стек порожній.");
                    break;

                case "5":
                    
                    Console.WriteLine("Генерується стек з 10 випадкових цілих чисел...");
                    intStack.Clear();
                    Random rand1 = new Random();
                    for (int i = 0; i < 10; i++)
                        intStack.Push(rand1.Next(-20, 21));

                    Console.WriteLine("Згенерований стек:");
                    foreach (int val in intStack)
                        Console.Write(val + " ");
                    Console.WriteLine();

                    long product = 1;
                    bool hasOdd = false;
                    foreach (int val in intStack)
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
                    if (doubleQueue.Count > 0)
                        Console.WriteLine($"Видалено: {doubleQueue.Dequeue()}");
                    else
                        Console.WriteLine("Черга порожня.");
                    break;

                case "8":
                    Console.WriteLine("Черга:");
                    foreach (double val in doubleQueue)
                        Console.WriteLine(val);
                    break;

                case "9":
                    if (doubleQueue.Count > 0)
                        Console.WriteLine($"Перший елемент черги: {doubleQueue.Peek()}");
                    else
                        Console.WriteLine("Черга порожня.");
                    break;

                case "10":
                    
                    Console.WriteLine("Генерується черга з 10 випадкових дійсних чисел...");
                    doubleQueue.Clear();
                    Random rand2 = new Random();
                    for (int i = 0; i < 10; i++)
                        doubleQueue.Enqueue(Math.Round(rand2.NextDouble() * 20 - 10, 2)); 

                    Console.WriteLine("Згенерована черга:");
                    foreach (double val in doubleQueue)
                        Console.Write(val + " ");
                    Console.WriteLine();

                    int count = doubleQueue.Count(x => x > 0);
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
