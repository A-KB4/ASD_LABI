    using System;
    using System.Collections.Generic;

    namespace DeviceHashTableApp
    {
        class Program
        {
            const int hashTableSize = 101; // Просте число для кращого розподілу
            static int[] hashTable = new int[hashTableSize];
            static bool[] used = new bool[hashTableSize];
            static Random rand = new Random();

            static void Main()
            {
                var numbers = GenerateDeviceNumbers(50);
                Console.WriteLine("Inserted device numbers:");
                foreach (var num in numbers)
                {
                    Console.WriteLine(num);
                    Insert(num);
                }

                PrintTable();

                Console.WriteLine("\nНайкращий та найгiрший варiанти:");
                AnalyzeDistribution();
            }

            static List<int> GenerateDeviceNumbers(int count)
            {
                var set = new HashSet<int>();
                while (set.Count < count)
                {
                    int d1 = rand.Next(0, 10);
                    int d2 = rand.Next(0, 10);
                    int d4 = rand.Next(0, 10);
                    int d5 = rand.Next(0, 10);
                    int number = 600000 + d1 * 10000 + d2 * 1000 + 100 + d4 * 10 + d5;
                    set.Add(number);
                }
                return new List<int>(set);
            }

            // Проста, але ефективна хеш-функція
            static int MyHash(int data)
            {
                return data % hashTableSize;
            }

            static int Dist(int a, int b)
            {
                return (b - a + hashTableSize) % hashTableSize;
            }

            static void Insert(int data)
            {
                int bucket = MyHash(data);
                while (used[bucket] && hashTable[bucket] != data)
                    bucket = (bucket + 1) % hashTableSize;

                if (!used[bucket])
                {
                    used[bucket] = true;
                    hashTable[bucket] = data;
                }
            }

            static void PrintTable()
            {
                for (int i = 0; i < hashTableSize; i++)
                {
                    if (used[i])
                        Console.WriteLine($"Index {i}: {hashTable[i]}");
                }
            }

            static void AnalyzeDistribution()
            {
                int maxChain = 0;
                int currentChain = 0;

                for (int i = 0; i < hashTableSize; i++)
                {
                    if (used[i])
                    {
                        currentChain++;
                        if (currentChain > maxChain)
                            maxChain = currentChain;
                    }
                    else
                    {
                        currentChain = 0;
                    }
                }

                Console.WriteLine($"Максимальна послiдовнiсть зайнятих слотiв: {maxChain}");
            }
        }
    }
