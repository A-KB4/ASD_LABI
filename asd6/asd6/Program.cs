using System;
using System.Collections.Generic;

class Tree
{
    public int Data;
    public Tree Left;
    public Tree Right;

    public Tree(int data)
    {
        Data = data;
        Left = null;
        Right = null;
    }

    public void Insert(int value)
    {
        if (value < Data)
        {
            if (Left == null) Left = new Tree(value);
            else Left.Insert(value);
        }
        else
        {
            if (Right == null) Right = new Tree(value);
            else Right.Insert(value);
        }
    }

    public Tree Delete(int value)
    {
        if (value < Data)
            Left = Left?.Delete(value);
        else if (value > Data)
            Right = Right?.Delete(value);
        else
        {
            if (Left == null) return Right;
            if (Right == null) return Left;

            Tree minLargerNode = Right;
            while (minLargerNode.Left != null)
                minLargerNode = minLargerNode.Left;

            Data = minLargerNode.Data;
            Right = Right.Delete(minLargerNode.Data);
        }
        return this;
    }

    public void PrePrintTree()
    {
        Console.Write(Data + " ");
        Left?.PrePrintTree();
        Right?.PrePrintTree();
    }

    public void InOrderPrint()
    {
        Left?.InOrderPrint();
        Console.Write(Data + " ");
        Right?.InOrderPrint();
    }

    // Сума всіх елементів - прямий обхід
    public int SumPreOrder()
    {
        int sum = Data;
        if (Left != null) sum += Left.SumPreOrder();
        if (Right != null) sum += Right.SumPreOrder();
        return sum;
    }

    // Виведення листків - прямий обхід
    public void PrintLeavesPreOrder()
    {
        if (Left == null && Right == null)
        {
            Console.Write(Data + " ");
        }
        if (Left != null) Left.PrintLeavesPreOrder();
        if (Right != null) Right.PrintLeavesPreOrder();
    }
}

class Program
{
    static void Main()
    {
        Tree root = null;
        const int v = 1;
        bool running = true;

        while (running)
        {
            Console.WriteLine("\n--- Меню ---");
            Console.WriteLine("1. Створити дерево з випадкових чисел");
            Console.WriteLine("2. Додати елемент");
            Console.WriteLine("3. Видалити елемент");
            Console.WriteLine("4. Вивести дерево");
            Console.WriteLine("5. Вивести вiдсортований вмiст");
            Console.WriteLine("6. Знайти суму всiх елементiв (прямий обхiд)");
            Console.WriteLine("7. Вивести всi листи дерева (прямий обхiд)");
            Console.WriteLine("8. Вийти");
            Console.Write("Ваш вибiр: ");

            string input = Console.ReadLine();
            Console.WriteLine();

            switch (input)
            {
                case "1":
                    Random rand = new Random();
                    HashSet<int> values = new HashSet<int>();
                    int n = 25 + v;

                    while (values.Count < n)
                        values.Add(rand.Next(-50, 51));

                    root = null;
                    foreach (int val in values)
                    {
                        if (root == null) root = new Tree(val);
                        else root.Insert(val);
                    }

                    Console.WriteLine("Дерево створено.");
                    break;

                case "2":
                    Console.Write("Введiть значення для додавання: ");
                    if (int.TryParse(Console.ReadLine(), out int addVal))
                    {
                        if (root == null)
                            root = new Tree(addVal);
                        else
                            root.Insert(addVal);
                        Console.WriteLine("Елемент додано.");
                    }
                    else Console.WriteLine("Некоректне число.");
                    break;

                case "3":
                    Console.Write("Введiть значення для видалення: ");
                    if (int.TryParse(Console.ReadLine(), out int delVal))
                    {
                        if (root != null)
                        {
                            root = root.Delete(delVal);
                            Console.WriteLine("Елемент видалено.");
                        }
                        else Console.WriteLine("Дерево порожнє.");
                    }
                    else Console.WriteLine("Некоректне число.");
                    break;

                case "4":
                    Console.WriteLine("Прямий обхiд дерева:");
                    if (root != null) root.PrePrintTree();
                    else Console.WriteLine("Дерево порожнє.");
                    break;

                case "5":
                    Console.WriteLine("Вiдсортований вмiст:");
                    if (root != null) root.InOrderPrint();
                    else Console.WriteLine("Дерево порожнє.");
                    break;

                case "6":
                    if (root != null)
                        Console.WriteLine("Сума елементiв дерева: " + root.SumPreOrder());
                    else Console.WriteLine("Дерево порожнє.");
                    break;

                case "7":
                    Console.WriteLine("Листя дерева:");
                    if (root != null) root.PrintLeavesPreOrder();
                    else Console.WriteLine("Дерево порожнє.");
                    break;

                case "8":
                    running = false;
                    break;

                default:
                    Console.WriteLine("Невiрний вибiр.");
                    break;
            }
        }
    }
}
