using laba3;
using System;

namespace laba3
{
    internal class Program
    {
        static int arrayActionCount = 0;
        static int chainActionCount = 0;
        static int arrayIndexExceptionCount = 0;
        static int chainIndexExceptionCount = 0;
        static int arrayFileExceptionCount = 0;
        static int chainFileExceptionCount = 0;

        static string filePath = "test.txt";

        static void arrayCounter()
        {
            arrayActionCount++;
        }

        static void chainCounter()
        {
            chainActionCount++;
        }


        public static void Main(string[] args)
        {
            BaseList<int> arrayList = new ArrayList<int>();
            BaseList<int> chainList = new ChainList<int>();
            Random random = new Random();

            arrayList.active += arrayCounter;
            chainList.active += chainCounter;

            int end = 100;
            for (int i = 0; i <= end; i++)
            {
                int factor = random.Next(1, 5);
                int digit = random.Next(0, 1000);
                int index = random.Next(0, end);

                switch (factor)
                {
                    case 1:
                        arrayList.Add(digit);
                        chainList.Add(digit);
                        break;
                    case 2:
                        arrayList.Delete(digit);
                        chainList.Delete(digit);
                        break;
                    case 3:
                        arrayList.Insert(digit, index);
                        chainList.Insert(digit, index);
                        break;
                    case 4:
                        try
                        {
                            arrayList[end] = digit;
                        }
                        catch (Exceptions.BadIndexException)
                        {
                            arrayIndexExceptionCount++;
                        }
                        try
                        {
                            chainList[end] = digit;
                        }
                        catch (Exceptions.BadIndexException)
                        {
                            chainIndexExceptionCount++;
                        }
                        break;
                        /*case 4:
                            arrayList.clear();
                            linkedList.clear();
                            break;*/
                }
            }

            if (arrayList == chainList)
            {
                Console.WriteLine("Accept #1");
            }
            else
            {
                Console.WriteLine("Error #1");
            }

            if (arrayActionCount == chainActionCount)
            {
                Console.WriteLine("Accept #2");
            }
            else
            {
                Console.WriteLine("Error #2");
            }

            if (arrayIndexExceptionCount == chainIndexExceptionCount)
            {
                Console.WriteLine("Accept #3");
            }
            else
            {
                Console.WriteLine("Error #3");
            }

            for (int i = 0; i < 10; i++)
            {
                int factor = random.Next(1, 3);

                switch (factor)
                {
                    case 1:
                        try
                        {
                            arrayList.loadToFile(filePath);
                        }
                        catch (Exceptions.BadFileException)
                        {
                            arrayFileExceptionCount++;
                        }
                        try
                        {
                            chainList.loadToFile(filePath);
                        }
                        catch (Exceptions.BadFileException)
                        {
                            chainIndexExceptionCount++;
                        }
                        break;
                    case 2:
                        arrayList.SaveToFile(filePath);
                        chainList.SaveToFile(filePath);
                        break;
                }
            }

            if (arrayFileExceptionCount == chainFileExceptionCount)
            {
                Console.WriteLine("Accept #4");
            }
            else
            {
                Console.WriteLine(arrayFileExceptionCount + " " + chainIndexExceptionCount);
                Console.WriteLine("Error #4");
            }

            BaseList<int> baseList = arrayList + chainList;
            baseList.Sort();
            foreach (int number in baseList)
            {
                Console.Write(number + " ");
            }
        }
    }
}