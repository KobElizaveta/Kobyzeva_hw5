namespace Tumakov_hw5
{
    internal class Program
    {
        static (int, int) CountVowelsConsonants(string stringForCounting)
        {
            int CountVowels = 0;
            int CountConsonants = 0;
            string alfVowels = "eyuioa";
            char[] charArray = stringForCounting.ToCharArray();
            foreach(char c in charArray)
            {
                if (char.IsLetter(c))
                {
                    if(alfVowels.Contains(char.ToLower(c)))
                    {
                        CountVowels++;
                    }
                    else
                    {
                        CountConsonants++;
                    }
                }
            }
            return (CountVowels, CountConsonants);
        }

        static void CountVowelsConsonants(string stringForCounting, List<int> listResult)
        {
            int CountVowels = 0;
            int CountConsonants = 0;
            string alfVowels = "eyuioa";
            List<char> charArray = stringForCounting.ToList();
            foreach (char c in charArray)
            {
                if (char.IsLetter(c))
                {
                    if (alfVowels.Contains(char.ToLower(c)))
                    {
                        CountVowels++;
                    }
                    else
                    {
                        CountConsonants++;
                    }
                }
            }
            listResult[0] = CountVowels;
            listResult[1] = CountConsonants;
        }

        static int[,] MatrixMultiplication(byte[,] matrix1, byte[,] matrix2)
        {
            int[,] matrixResult = new int[matrix1.GetLength(0), matrix1.GetLength(0)];
            for (byte i = 0; i < matrix1.GetLength(0); i++)
            {
                for (byte j = 0; j < matrix1.GetLength(0); j++)
                {
                    for (byte k = 0; k < matrix1.GetLength(0); k++)
                    {
                        matrixResult[i, j] += matrix1[i, k] * matrix2[k, j];
                    }
                }
            }
            return matrixResult;
        }

        static LinkedList<LinkedList<int>> MatrixMultiplicationL(LinkedList<LinkedList<byte>> matrix1, LinkedList<LinkedList<byte>> matrix2)
        {
            var matrixResult = new LinkedList<LinkedList<int>>();
            for (byte i = 0; i < matrix1.Count(); i++)
            {
                LinkedList<int> row = new LinkedList<int>();
                for (int j = 0; j < matrix1.Count(); j++)
                {
                    int sum = 0;
                    for (int k = 0; k < matrix1.Count(); k++)
                        sum += matrix1.ElementAt(i).ElementAt(k) * matrix2.ElementAt(k).ElementAt(j);
                    row.AddLast(sum);
                }
                matrixResult.AddLast(row);
            }
            return matrixResult;
        }

        static void PrintMatrix(int[,] matrix)
        {
            for(byte i = 0; i < matrix.GetLength(0); i++ )
            {
                for (byte j = 0; j < matrix.GetLength(0); j++ )
                {
                    Console.Write(matrix[i,j] + "  ");
                }
                Console.WriteLine();
            }
        }

        static void PrintMatrixL(LinkedList<LinkedList<int>> matrix)
        {
            foreach (LinkedList<int> row in matrix)
            {
                foreach (int value in row)
                {
                    Console.Write($"{value}\t");
                }
                Console.WriteLine();
            }
        }

        static void PrintMatrixL(LinkedList<LinkedList<byte>> matrix)
        {
            foreach (LinkedList<byte> row in matrix)
            {
                foreach (byte value in row)
                {
                    Console.Write($"{value}\t");
                }
                Console.WriteLine();
            }
        }

        static void PrintMatrix(byte[,] matrix)
        {
            for (byte i = 0; i < matrix.GetLength(0); i++)
            {
                for (byte j = 0; j < matrix.GetLength(0); j++)
                {
                    Console.Write(matrix[i, j] + "  ");
                }
                Console.WriteLine();
            }
        }

        static void CountMeanTemperature(sbyte[,] temperature, float[] meanTemperature)
        {
            for (byte i = 0; i < temperature.GetLength(0); i++) 
            {
                float sumTemperatureInMonth = 0;
                for (byte j = 0;j < temperature.GetLength(1); j++)
                {
                    sumTemperatureInMonth += temperature[i, j];
                }
                meanTemperature[i] = MathF.Round(sumTemperatureInMonth / temperature.GetLength(0), 2);
            }
        }

        static void CountMeanTemperatureD(Dictionary<string, sbyte[]> temperature, Dictionary<string, float> meanTemperature)
        {
            foreach (var month in temperature)
            {
                float sumTemperatureInMonth = 0;
                for (byte j = 0; j < month.Value.Length; j++)
                {
                    sumTemperatureInMonth += month.Value[j];
                }
                meanTemperature.Add(month.Key, MathF.Round(sumTemperatureInMonth / month.Value.Length, 2));
            }
        }

        static void Task6_1()
        {
            Console.WriteLine("Упражнение 6.1");
            string stringForCounting = File.ReadAllText("..\\..\\..\\Files\\File61.txt");
            (int CountVowels, int CountConsonants) resultCounting = CountVowelsConsonants(stringForCounting);
            Console.WriteLine($"Строка: {stringForCounting}\nКоличество гласных букв: {resultCounting.CountVowels};согласных: {resultCounting.CountConsonants}");
        }

        static void Task6_2()
        {
            Console.WriteLine("Упражнение 6.2");
            Console.WriteLine("Введите число, указывающее размерность матрицы:");
            bool flag62 = true;
            while(flag62)
            {
                if (!byte.TryParse(Console.ReadLine(), out byte size))
                {
                    Console.WriteLine("Введено не число или слишком большое число (до ночи сидеть как-то не хочется). Попробуйте еще");
                }
                else
                {
                    Random randomNum = new Random();
                    byte[,] matrix1 = new byte[size, size];
                    byte[,] matrix2 = new byte[size, size];
                    for (byte i = 0; i < size; i++)
                    {
                        for (byte j = 0; j < size; j++)
                        {
                            matrix1[i, j] = (byte)randomNum.Next(10);
                            matrix2[i, j] = (byte)randomNum.Next(10);
                        }
                    }
                    Console.WriteLine("Первая матрица:");
                    PrintMatrix(matrix1);
                    Console.WriteLine("Вторая матрица:");
                    PrintMatrix(matrix2);
                    int[,] matrixResult = new int[size, size];

                    Console.WriteLine("Результат умножени матриц:");
                    PrintMatrix(MatrixMultiplication(matrix1, matrix2));
                    flag62 = false;
                }
            }
        }

        static void Task6_3()
        {
            Console.WriteLine("Упражнение 6.2. Город с биполярной температурой");
            Random random = new Random();
            sbyte[,] temperature = new sbyte[12, 30];
            Console.WriteLine("Исходные данные:");
            for (byte i = 0; i < temperature.GetLength(0);i++)
            {
                for (byte j = 0; j < temperature.GetLength(1); j++)
                {
                    temperature[i, j] = (sbyte)random.Next(-30, 30);
                    Console.Write(temperature[i, j] + "  ");
                }
                Console.WriteLine();
            }

            float[] meanTemperature = new float[temperature.GetLength(0)];
            CountMeanTemperature(temperature, meanTemperature);
            Array.Sort(meanTemperature);

            Console.WriteLine($"Сортированное среднее значение температур в месяцах:");
            foreach (float i in meanTemperature)
            {
                Console.Write(i + "   ");
            }

        }

        static void TaskDZ6_1()
        {
            Console.WriteLine("Домашнее задание 6.1");
            string stringForCounting = File.ReadAllText("..\\..\\..\\Files\\File61.txt");
            List<int> listResult = new List<int>() { 0, 0 };
            CountVowelsConsonants(stringForCounting, listResult);
            Console.WriteLine($"Строка: {stringForCounting}\nКоличество гласных букв: {listResult[0]};согласных: {listResult[1]}");
        }

        static void TaskDZ6_2()
        {
            Console.WriteLine("Домашнее задание 6.2");
            Console.WriteLine("Введите число, указывающее размерность матрицы:");
            bool flag62 = true;
            while (flag62)
            {
                if (!byte.TryParse(Console.ReadLine(), out byte size))
                {
                    Console.WriteLine("Введено не число или слишком большое число (до ночи сидеть как-то не хочется). Попробуйте еще");
                }
                else
                {
                    Random randomNum = new Random();
                    var matrix1 = new LinkedList<LinkedList<byte>>();
                    var matrix2 = new LinkedList<LinkedList<byte>>();
                    for (byte i = 0; i < size; i++)
                    {
                        LinkedList<byte> row1 = new LinkedList<byte>();
                        LinkedList<byte> row2 = new LinkedList<byte>();

                        for (byte j = 0; j < size; j++)
                        {
                            row1.AddLast((byte)randomNum.Next(10));
                            row2.AddLast((byte)randomNum.Next(10));
                        }

                        matrix1.AddLast(row1);
                        matrix2.AddLast(row2);
                    }
                    Console.WriteLine("Первая матрица:");
                    PrintMatrixL(matrix1);
                    Console.WriteLine("Вторая матрица:");
                    PrintMatrixL(matrix2);
                    LinkedList<LinkedList<int>> matrixResult = MatrixMultiplicationL(matrix1, matrix2);

                    Console.WriteLine("Результат умножения матриц:");
                    PrintMatrixL(matrixResult);
                    flag62 = false;
                }
            }
        }

        static void TaskDZ6_3()
        {
            Console.WriteLine("Домашнее задание 6.3. Город с биполярной температурой");
            Random random = new Random();

            string[] monthNames = { "Январь", "Февраль", "Март", "Апрель", "Май", "Июнь", "Июль", "Август", "Сентябрь", "Октябрь", "Ноябрь", "Декабрь" };

            Dictionary<string, sbyte[]> temperature = new Dictionary<string, sbyte[]>();

            Console.WriteLine("Исходные данные:");
            foreach (string month in monthNames)
            {
                sbyte[] days = new sbyte[30];
                Console.Write(month + ": ");
                for (byte j = 0; j < 30; j++)
                {
                    days[j] = (sbyte)random.Next(-30, 30);
                    Console.Write(days[j] + "  ");
                }
                Console.WriteLine();
                temperature.Add(month, days);
            }

            Dictionary<string, float> meanTemperature = new Dictionary<string, float>();
            CountMeanTemperatureD(temperature, meanTemperature);

            Console.WriteLine($"Сортированное среднее значение температур в месяцах:");
            float[] temps = new float[meanTemperature.Count];
            meanTemperature.Values.CopyTo(temps, 0);
            Array.Sort(temps);

            for (int i = 0; i < temps.Length; i++)
            {
                Console.Write(temps[i] + "  ");
            }
        }


        static void Main(string[] args)
        {
            Task6_1();
            Task6_2();
            Task6_3();
            TaskDZ6_1();
            TaskDZ6_2();
            TaskDZ6_3();


        }
    }
}
