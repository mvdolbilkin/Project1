using System;
using System.IO;
//Долбилкин Михаил
//группа БПИ 256-2
//Вариант 8
namespace P_1_1
{
    /// <summary>
    /// Класс, содержащий методы для работы с массивами и файлами.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Преобразует массив строк в массив целых чисел. 
        /// Если строка не может быть преобразована в число, запрашивает ввод от пользователя.
        /// </summary>
        /// <param name="line">Массив строк для обработки.</param>
        /// <returns>Массив целых чисел.</returns>
        static int[] operation_with_massive(string[] line)
        {
            int sch = 0; // Счетчик корректных чисел
            for (int i = 0; i < line.Length; i++)
            {
                try
                {
                    int.Parse(line[i]); // Проверка на преобразование.
                    sch++;
                }
                catch (OverflowException)
                {
                    sch++; // Увеличение счетчика на случай переполнения.
                }
                catch
                {
                    continue; // Игнорировать нечисловые строки.
                }
            }

            int[] r = new int[sch]; // Инициализация массива для результатов.
            int newInt;
            int n = line.Length;
            int count = 0;

            for (int i = 0; i < n; i++)
            {
                try
                {
                    r[i] = int.Parse(line[i + count]);
                }
                catch (OverflowException)
                {
                    Console.WriteLine($"Данные неверны");
                    while (true)
                    {
                        try
                        {
                            newInt = int.Parse(Console.ReadLine()); // Ввод от пользователя при неверных данных.
                            break;
                        }
                        catch
                        {
                            Console.WriteLine("Некорректное значение, введите еще раз");
                        }
                    }
                    r[i] = newInt;
                    i--;
                    n--;
                    count++;
                }
                catch
                {
                    i--; // Уменьшение индекса для повторной обработки.
                    n--;
                    count++;
                }
            }

            if (sch == 0)
            {
                Console.WriteLine("Корректных данных в файле нет");
                Console.ReadKey();
            }

            return r;
        }

        /// <summary>
        /// Вычисляет скалярное произведение двух массивов целых чисел.
        /// </summary>
        /// <param name="a">Первый массив целых чисел.</param>
        /// <param name="b">Второй массив целых чисел.</param>
        /// <returns>Скалярное произведение двух массивов.</returns>
        static int calculation_P(int[] a, int[] b)
        {
            int p = 0; // Скалярное произведение.
            for (int i = 0; i < a.Length; i++)
            {
                p += a[i] * b[i]; // Вычисление скалярного произведения.
            }

            return p;
        }

        /// <summary>
        /// Читает строки из файла и возвращает их в виде массива строк.
        /// </summary>
        /// <param name="path">Путь к файлу.</param>
        /// <returns>Массив строк, прочитанных из файла.</returns>
        static string[] ReadLinesFromFile(string path)
        {
            while (true)
            {
                try
                {
                    return File.ReadAllLines(path); // Чтение всех строк из файла.
                }
                catch (FileNotFoundException)
                {
                    Console.WriteLine("Входной Файл на диске отсутствует");
                    Console.ReadKey();
                }
                catch (FileLoadException)
                {
                    Console.WriteLine("Проблемы с чтением данных из файла");
                    Console.ReadKey();
                }
            }
        }

        /// <summary>
        /// Записывает строку в файл по указанному пути.
        /// </summary>
        /// <param name="path">Путь к файлу.</param>
        /// <param name="value">Строка, которую необходимо записать.</param>
        static void WriteToFile(string path, int value)
        {
            try
            {
                File.WriteAllText(path, value.ToString("F3").Replace(",", ".")); // Запись строки в файл.
            }
            catch
            {
                Console.WriteLine("Проблемы с записью данных в файл");
            }
        }

        /// <summary>
        /// Основной метод программы, который управляет потоком выполнения.
        /// </summary>
        /// <param name="args">Аргументы командной строки.</param>
        static void Main(string[] args)
        {
            ConsoleKeyInfo keyToExit; // Клавиша для выхода.
            int[] a; // Первый массив целых чисел.
            int[] b; // Второй массив целых чисел.
            string[] readText; // Массив строк из файла.

            do
            {
                string path = Path.GetFullPath(Path. Combine(Directory.GetCurrentDirectory(), @"../../../"));; // Путь к Solution.
                string pathInput = Path.Combine(path, "input.txt"); // Путь к входному файлу.
                string pathOut = Path.Combine(path, "output.txt"); // Путь к выходному файлу.
                do
                {
                    readText = ReadLinesFromFile(pathInput); // Чтение строк из файла.
                    a = operation_with_massive(readText[0].Split(' ')); // Обработка первой строки.
                    b = operation_with_massive(readText[1].Split(' ')); // Обработка второй строки.
                } while (a.Length == 0 || b.Length == 0); // Проверка на корректность массивов.
                int P; // Скалярное произведение.
                if (a.Length != b.Length)
                {
                    P = 0; // Если длины массивов не равны, результат 0.
                }
                else
                {
                    P = calculation_P(a, b); // Вычисление скалярного произведения.
                }

                // Запись результата в файл.
                WriteToFile(pathOut, P);
                Console.WriteLine("Для выхода нажмите Escape....");
                keyToExit = Console.ReadKey(); // Ожидание нажатия клавиши.
            } while (keyToExit.Key != ConsoleKey.Escape); // Выход по Escape.
        }
    }
}
