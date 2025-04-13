using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class Node
{
    public int Data;
    public Node Next;

    public Node(int data)
    {
        Data = data;
        Next = null;
    }
}

class SinglyLinkedList
{
    private Node head;

    public void Add(int data)
    {
        Node newNode = new Node(data);
        if (head == null)
        {
            head = newNode;
        }
        else
        {
            Node temp = head;
            while (temp.Next != null)
            {
                temp = temp.Next;
            }
            temp.Next = newNode;
        }
    }

    public void Remove(int data)
    {
        if (head == null) return;
        if (head.Data == data)
        {
            head = head.Next;
            return;
        }
        Node current = head;
        while (current.Next != null && current.Next.Data != data)
        {
            current = current.Next;
        }
        if (current.Next != null)
        {
            current.Next = current.Next.Next;
        }
    }

    public bool Search(int key)
    {
        Node current = head;
        while (current != null)
        {
            if (current.Data == key)
                return true;
            current = current.Next;
        }
        return false;
    }

    public void Display()
    {
        Node current = head;
        while (current != null)
        {
            Console.Write(current.Data + " ");
            current = current.Next;
        }
        Console.WriteLine();
    }

    public List<int> ToList()
    {
        List<int> list = new List<int>();
        Node current = head;
        while (current != null)
        {
            list.Add(current.Data);
            current = current.Next;
        }
        return list;
    }

    public void Clear()
    {
        head = null;
    }
}

class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;

        Random rand = new Random();
        for (int i = 1; i <= 3; i++)
        {
            SinglyLinkedList listA = new SinglyLinkedList();
            SinglyLinkedList listB = new SinglyLinkedList();

            for (int j = 0; j < 5; j++)
            {
                listA.Add(rand.Next(-50, 51));
                listB.Add(rand.Next(-50, 51));
            }

            Console.WriteLine($"\nПара {i}:");
            Console.Write("Список A: ");
            listA.Display();
            Console.Write("Список B: ");
            listB.Display();

            List<int> combined = listA.ToList();
            combined.AddRange(listB.ToList());

            Console.Write("Об'єднаний: ");
            Console.WriteLine(string.Join(" ", combined));

            combined.Sort();
            Console.Write("Відсортований: ");
            Console.WriteLine(string.Join(" ", combined));

            listA.Clear();
            listB.Clear();
        }

        SinglyLinkedList list1 = new SinglyLinkedList();
        SinglyLinkedList list2 = new SinglyLinkedList();

        while (true)
        {
            Console.WriteLine("\nМеню:");
            Console.WriteLine("1 - Додати елемент до списку 1");
            Console.WriteLine("2 - Видалити елемент зі списку 1");
            Console.WriteLine("3 - Пошук у списку 1");
            Console.WriteLine("4 - Вивести список 1");
            Console.WriteLine("5 - Додати елемент до списку 2");
            Console.WriteLine("6 - Видалити елемент зі списку 2");
            Console.WriteLine("7 - Пошук у списку 2");
            Console.WriteLine("8 - Вивести список 2");
            Console.WriteLine("9 - Об'єднати списки та вивести відсортовано");
            Console.WriteLine("0 - Вихід");

            Console.Write("Ваш вибір: ");
            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    Console.Write("Введіть значення: ");
                    if (int.TryParse(Console.ReadLine(), out int val1))
                        list1.Add(val1);
                    break;
                case "2":
                    Console.Write("Введіть значення для видалення: ");
                    if (int.TryParse(Console.ReadLine(), out int del1))
                        list1.Remove(del1);
                    break;
                case "3":
                    Console.Write("Введіть значення для пошуку: ");
                    if (int.TryParse(Console.ReadLine(), out int search1))
                        Console.WriteLine(list1.Search(search1) ? "Знайдено" : "Не знайдено");
                    break;
                case "4":
                    Console.WriteLine("Список 1:");
                    list1.Display();
                    break;
                case "5":
                    Console.Write("Введіть значення: ");
                    if (int.TryParse(Console.ReadLine(), out int val2))
                        list2.Add(val2);
                    break;
                case "6":
                    Console.Write("Введіть значення для видалення: ");
                    if (int.TryParse(Console.ReadLine(), out int del2))
                        list2.Remove(del2);
                    break;
                case "7":
                    Console.Write("Введіть значення для пошуку: ");
                    if (int.TryParse(Console.ReadLine(), out int search2))
                        Console.WriteLine(list2.Search(search2) ? "Знайдено" : "Не знайдено");
                    break;
                case "8":
                    Console.WriteLine("Список 2:");
                    list2.Display();
                    break;
                case "9":
                    List<int> combined = list1.ToList();
                    combined.AddRange(list2.ToList());
                    combined.Sort();
                    Console.WriteLine("Об'єднаний та відсортований список:");
                    foreach (int val in combined)
                        Console.Write(val + " ");
                    Console.WriteLine();
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

