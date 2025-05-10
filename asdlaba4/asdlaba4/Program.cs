using System;
using System.Collections.Generic;
using System.Text;

class DoublyNode<T>
{
    public T Data;
    public DoublyNode<T> Prev;
    public DoublyNode<T> Next;

    public DoublyNode(T data)
    {
        Data = data;
        Prev = null;
        Next = null;
    }
}

class DoublyLinkedList<T>
{
    private DoublyNode<T> head;
    private DoublyNode<T> tail;

    public void Add(T data)
    {
        var newNode = new DoublyNode<T>(data);
        if (head == null)
        {
            head = tail = newNode;
        }
        else
        {
            tail.Next = newNode;
            newNode.Prev = tail;
            tail = newNode;
        }
    }

    public void Remove(T data)
    {
        var current = head;
        while (current != null)
        {
            if (current.Data.Equals(data))
            {
                if (current == head) head = current.Next;
                if (current == tail) tail = current.Prev;
                if (current.Prev != null) current.Prev.Next = current.Next;
                if (current.Next != null) current.Next.Prev = current.Prev;
                return;
            }
            current = current.Next;
        }
    }

    public bool Search(T data)
    {
        var current = head;
        while (current != null)
        {
            if (current.Data.Equals(data))
                return true;
            current = current.Next;
        }
        return false;
    }

    public void Display()
    {
        var current = head;
        while (current != null)
        {
            Console.Write(current.Data + " ");
            current = current.Next;
        }
        Console.WriteLine();
    }

    public void RemoveAfterEach(T target)
    {
        var current = head;
        while (current != null && current.Next != null)
        {
            if (current.Data.Equals(target))
            {
                var toRemove = current.Next;
                current.Next = toRemove.Next;
                if (toRemove.Next != null)
                    toRemove.Next.Prev = current;
                if (toRemove == tail)
                    tail = current;
            }
            current = current.Next;
        }
    }

    public void InsertAfter(Predicate<T> condition, T newValue)
    {
        var current = head;
        while (current != null)
        {
            if (condition(current.Data))
            {
                var newNode = new DoublyNode<T>(newValue);
                newNode.Next = current.Next;
                newNode.Prev = current;

                if (current.Next != null)
                    current.Next.Prev = newNode;
                else
                    tail = newNode;

                current.Next = newNode;
                return;
            }
            current = current.Next;
        }
    }
}

class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;

        Dictionary<int, DoublyLinkedList<int>> lists = new();
        int listCounter = 0;

        while (true)
        {
            Console.WriteLine("\nМеню:");
            Console.WriteLine("1 - Створити новий список");
            Console.WriteLine("2 - Додати елемент до списку");
            Console.WriteLine("3 - Видалити елемент зі списку");
            Console.WriteLine("4 - Пошук елемента у списку");
            Console.WriteLine("5 - Вивести список");
            Console.WriteLine("6 - Задача зі списком символів і '&'");
            Console.WriteLine("7 - Задача з дійсними числами (вставити 13.5)");
            Console.WriteLine("0 - Вихід");
            Console.Write("Ваш вибір: ");

            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    listCounter++;
                    lists[listCounter] = new DoublyLinkedList<int>();
                    Console.WriteLine($"Список #{listCounter} створено.");
                    break;

                case "2":
                    int addId = SelectList(lists);
                    if (addId == -1) break;
                    Console.Write("Введіть значення: ");
                    if (int.TryParse(Console.ReadLine(), out int addVal))
                        lists[addId].Add(addVal);
                    break;

                case "3":
                    int delId = SelectList(lists);
                    if (delId == -1) break;
                    Console.Write("Введіть значення для видалення: ");
                    if (int.TryParse(Console.ReadLine(), out int delVal))
                        lists[delId].Remove(delVal);
                    break;

                case "4":
                    int searchId = SelectList(lists);
                    if (searchId == -1) break;
                    Console.Write("Введіть значення для пошуку: ");
                    if (int.TryParse(Console.ReadLine(), out int searchVal))
                        Console.WriteLine(lists[searchId].Search(searchVal) ? "Знайдено" : "Не знайдено");
                    break;

                case "5":
                    int printId = SelectList(lists);
                    if (printId == -1) break;
                    Console.WriteLine($"Список #{printId}:");
                    lists[printId].Display();
                    break;

                case "6":
                    SymbolTask();
                    break;

                case "7":
                    RealNumberTask();
                    break;

                case "0":
                    return;

                default:
                    Console.WriteLine("Невірний вибір.");
                    break;
            }
        }
    }

    static int SelectList(Dictionary<int, DoublyLinkedList<int>> lists)
    {
        if (lists.Count == 0)
        {
            Console.WriteLine("Списки ще не створено.");
            return -1;
        }

        Console.Write("Введіть номер списку: ");
        if (int.TryParse(Console.ReadLine(), out int id) && lists.ContainsKey(id))
            return id;

        Console.WriteLine("Невірний номер.");
        return -1;
    }

    static void SymbolTask()
    {
        var rnd = new Random();
        var symbols = new List<char>("ABCDEFGHIJKLMNOPQRSTUVWXYZ@%$#".ToCharArray());
        var list = new DoublyLinkedList<char>();

        // Створити список із 20 символів, включаючи 3 символи '&'
        List<char> randomChars = new();
        for (int i = 0; i < 17; i++)
            randomChars.Add(symbols[rnd.Next(symbols.Count)]);
        for (int i = 0; i < 3; i++)
            randomChars.Insert(rnd.Next(randomChars.Count + 1), '&');

        foreach (char c in randomChars)
            list.Add(c);

        Console.WriteLine("\nСписок символів до видалення:");
        list.Display();

        list.RemoveAfterEach('&');

        Console.WriteLine("Після видалення елементів після '&':");
        list.Display();
    }

    static void RealNumberTask()
    {
        var rnd = new Random();
        var list = new DoublyLinkedList<double>();

        for (int i = 0; i < 10; i++)
            list.Add(Math.Round(rnd.NextDouble() * 5, 2));

        Console.WriteLine("\nСписок дійсних чисел:");
        list.Display();

        list.InsertAfter(x => x > 2, 13.5);

        Console.WriteLine("Після вставки 13.5 після першого елемента > 2:");
        list.Display();
    }
}
