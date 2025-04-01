using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class DynamicArray
{
    private List<double> data;

    public DynamicArray()
    {
        data = new List<double>();
    }

    public void Add(double value, int index = -1)
    {
        if (index == -1 || index >= data.Count)
            data.Add(value);
        else
            data.Insert(index, value);
    }

    public void Remove(int index)
    {
        if (index >= 0 && index < data.Count)
            data.RemoveAt(index);
    }

    public void Clear()
    {
        data.Clear();
        Console.WriteLine("Масив видалено.");
    }

    public double? GetByIndex(int index)
    {
        return (index >= 0 && index < data.Count) ? data[index] : (double?)null;
    }

    public int FindByValue(double value)
    {
        return data.IndexOf(value);
    }

    public void Display()
    {
        if (data.Count == 0)
        {
            Console.WriteLine("Масив порожній.");
            return;
        }
        Console.WriteLine("Масив: " + string.Join(", ", data));
    }

    public double SumNegative()
    {
        return data.Where(x => x < 0).Sum();
    }

    public double ProductBetweenMinMax()
    {
        if (data.Count < 2) return 0;
        int minIndex = data.IndexOf(data.Min());
        int maxIndex = data.IndexOf(data.Max());

        if (minIndex > maxIndex)
            (minIndex, maxIndex) = (maxIndex, minIndex);

        return data.Skip(minIndex + 1).Take(maxIndex - minIndex - 1).Aggregate(1.0, (a, b) => a * b);
    }

    public List<List<double>> FindSequences()
    {
        List<List<double>> sequences = new List<List<double>>();
        int i = 0;
        while (i < data.Count)
        {
            List<double> seq = new List<double> { data[i] };
            int j = i + 1;
            while (j < data.Count && data[j] == data[i])
            {
                seq.Add(data[j]);
                j++;
            }
            if (seq.Count >= 3)
                sequences.Add(seq);
            i = j;
        }
        return sequences;
    }

    public void GenerateRandomArray(int size)
    {
        Random rand = new Random();
        data = Enumerable.Range(0, size).Select(_ => rand.NextDouble() * 200 - 100).ToList();
    }
}

class Program
{
    static void RunTests()
    {
        Console.WriteLine("\n=== Автоматичне тестування з масивом розміром 1000 ===");
        DynamicArray testArray = new DynamicArray();
        testArray.GenerateRandomArray(1000);

        Console.WriteLine("Сума від’ємних елементів: " + testArray.SumNegative());
        Console.WriteLine("Добуток між мін і макс: " + testArray.ProductBetweenMinMax());
        var sequences = testArray.FindSequences();
        Console.WriteLine("Знайдено послідовностей: " + sequences.Count);
    }

    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;

        DynamicArray arr = new DynamicArray();
        RunTests();

        while (true)
        {
            Console.WriteLine("Виберіть операцію:");
            Console.WriteLine("1 - Додати елемент");
            Console.WriteLine("2 - Видалити елемент");
            Console.WriteLine("3 - Вивести масив");
            Console.WriteLine("4 - Обчислити суму від’ємних");
            Console.WriteLine("5 - Обчислити добуток між мін і макс");
            Console.WriteLine("6 - Знайти послідовності");
            Console.WriteLine("7 - Згенерувати масив (100, 500, 1000)");
            Console.WriteLine("8 - Видалити весь масив");
            Console.WriteLine("0 - Вихід");

            if (!int.TryParse(Console.ReadLine(), out int choice))
            {
                Console.WriteLine("Некоректне введення! Введіть число.");
                continue;
            }

            switch (choice)
            {
                case 1:
                    Console.Write("Введіть число: ");
                    if (double.TryParse(Console.ReadLine(), out double val))
                        arr.Add(val);
                    else
                        Console.WriteLine("Некоректне введення!");
                    break;
                case 2:
                    Console.Write("Введіть індекс: ");
                    if (int.TryParse(Console.ReadLine(), out int idx))
                        arr.Remove(idx);
                    else
                        Console.WriteLine("Некоректне введення!");
                    break;
                case 3:
                    arr.Display();
                    break;
                case 4:
                    Console.WriteLine("Сума від’ємних: " + arr.SumNegative());
                    break;
                case 5:
                    Console.WriteLine("Добуток між мін і макс: " + arr.ProductBetweenMinMax());
                    break;
                case 6:
                    var sequences = arr.FindSequences();
                    if (sequences.Count == 0)
                        Console.WriteLine("Послідовностей не знайдено.");
                    else
                        foreach (var seq in sequences)
                            Console.WriteLine("Послідовність: " + string.Join(", ", seq));
                    break;
                case 7:
                    Console.WriteLine("Оберіть розмір (100, 500, 1000): ");
                    if (int.TryParse(Console.ReadLine(), out int size) && (size == 100 || size == 500 || size == 1000))
                    {
                        arr.GenerateRandomArray(size);
                        Console.WriteLine($"Згенеровано масив розміром {size}.");
                    }
                    else
                    {
                        Console.WriteLine("Невірний розмір!");
                    }
                    break;
                case 8:
                    arr.Clear();
                    break;
                case 0:
                    return;
                default:
                    Console.WriteLine("Невірний вибір!");
                    break;
            }
        }
    }
}
