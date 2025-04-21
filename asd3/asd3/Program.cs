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

// Звичайний однозв’язний список
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
                temp = temp.Next;
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
            current.Next = current.Next.Next;
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
}

// Кільцевий список
class CircularLinkedList
{
    private Node head;

    public void Add(int data)
    {
        Node newNode = new Node(data);
        if (head == null)
        {
            head = newNode;
            head.Next = head;
        }
        else
        {
            Node temp = head;
            while (temp.Next != head)
                temp = temp.Next;

            temp.Next = newNode;
            newNode.Next = head;
        }
    }

    public void Display()
    {
        if (head == null)
        {
            Console.WriteLine("(порожній)");
            return;
        }

        Node current = head;
        do
        {
            Console.Write(current.Data + " ");
            current = current.Next;
        } while (current != head);
        Console.WriteLine();
    }

    public List<int> ToList()
    {
        List<int> list = new List<int>();
        if (head == null) return list;

        Node current = head;
        do
        {
            list.Add(current.Data);
            current = current.Next;
        } while (current != head);

        return list;
    }
}

class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;

        Dictionary<int, SinglyLinkedList> lists = new Dictionary<int, SinglyLinkedList>();
        int listCounter = 0;

        while (true)
        {
            Console.WriteLine("\nМеню:");
            Console.WriteLine("1 - Створити новий список");
            Console.WriteLine("2 - Додати елемент до списку");
            Console.WriteLine("3 - Видалити елемент зі списку");
            Console.WriteLine("4 - Пошук елемента у списку");
            Console.WriteLine("5 - Вивести список");
            Console.WriteLine("6 - Задача з кільцевими списками (об'єднання та сортування)");
            Console.WriteLine("0 - Вихід");

            Console.Write("Ваш вибір: ");
            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    listCounter++;
                    lists[listCounter] = new SinglyLinkedList();
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
                    CircularListTask();
                    break;

                case "0":
                    return;

                default:
                    Console.WriteLine("Невірний вибір. Спробуйте ще раз.");
                    break;
            }
        }
    }

    static int SelectList(Dictionary<int, SinglyLinkedList> lists)
    {
        if (lists.Count == 0)
        {
            Console.WriteLine("Списки ще не створено.");
            return -1;
        }

        Console.Write("Введіть номер списку: ");
        if (int.TryParse(Console.ReadLine(), out int id) && lists.ContainsKey(id))
        {
            return id;
        }
        Console.WriteLine("Невірний номер.");
        return -1;
    }

    static void CircularListTask()
    {
        Console.WriteLine("\nЗадача з кільцевими списками:");
        Console.WriteLine("1 - Згенерувати випадкові елементи");
        Console.WriteLine("2 - Ввести елементи вручну");
        Console.Write("Ваш вибір: ");
        string choice = Console.ReadLine();

        CircularLinkedList listA = new CircularLinkedList();
        CircularLinkedList listB = new CircularLinkedList();

        if (choice == "1")
        {
            Random rnd = new Random();
            for (int i = 0; i < 10; i++)
            {
                listA.Add(rnd.Next(-50, 51));
                listB.Add(rnd.Next(-50, 51));
            }
        }
        else if (choice == "2")
        {
            Console.WriteLine("Введіть елементи для списку A (через пробіл):");
            string[] inputA = Console.ReadLine().Split();
            foreach (string s in inputA)
                if (int.TryParse(s, out int num)) listA.Add(num);

            Console.WriteLine("Введіть елементи для списку B (через пробіл):");
            string[] inputB = Console.ReadLine().Split();
            foreach (string s in inputB)
                if (int.TryParse(s, out int num)) listB.Add(num);
        }
        else
        {
            Console.WriteLine("Невірний вибір.");
            return;
        }

        Console.WriteLine("Список A:");
        listA.Display();
        Console.WriteLine("Список B:");
        listB.Display();

        List<int> combined = listA.ToList();
        combined.AddRange(listB.ToList());
        combined.Sort();

        Console.WriteLine("Об’єднаний та відсортований список:");
        Console.WriteLine(string.Join(" ", combined));
    }
}
