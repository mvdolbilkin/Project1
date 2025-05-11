using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Долбилкин Михаил
//группа БПИ 256-2
//Вариант 8
namespace P_1_1_DOP
{
    /// <summary>
    /// Класс, содержащий методы для выполнения операций с массивами и работы с файлами.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Выполняет операции с массивом строк, преобразуя их в массив целых чисел.
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
                    sch++;
                }
                catch
                {
                    continue;
                }
            }
            int[] r = new int[sch];
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
                    Console.WriteLine("Данные неверны");
                    while (true)
                    {
                        try
                        {
                            newInt = int.Parse(Console.ReadLine()); // Ввод от пользователя.
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
                    i--;
                    n--;
                    count++;
                }
            }

            if (sch == 0)
            {
                Console.WriteLine("Корректных данных в файле нет");
                Console.ReadKey();
            }

            return r; // Возврат массива целых чисел.
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

            return p; // Возврат результата
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
                    return File.ReadAllLines(path, Encoding.UTF8); // Чтение всех строк из файла.
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
        /// Записывает массив строк в файл.
        /// </summary>
        /// <param name="path">Путь к файлу.</param>
        /// <param name="value">Массив строк, которые необходимо записать.</param>
        static void write_output(string path, string[] value)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(path))
                {
                    foreach (string s in value)
                    {
                        writer.WriteLine(s); // Запись каждой строки в файл.
                    }
                }
            }
            catch
            {
                Console.WriteLine("Проблемы с записью данных в файл");
            }
            
        }

        /// <summary>
        /// Записывает целое число в файл.
        /// </summary>
        /// <param name="path">Путь к файлу.</param>
        /// <param name="value">Целое число, которое необходимо записать.</param>
        static void write_config(string path, int value)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(path))
                {
                    writer.WriteLine(value); // Запись числа в файл.
                }
            }
            catch
            {
                Console.WriteLine("Проблемы с записью данных в файл");
            }
        }

        /// <summary>
        /// Читает целое число из файла конфигурации. Если файл не существует, создаёт его с начальным значением 0.
        /// </summary>
        /// <param name="path">Путь к файлу конфигурации.</param>
        /// <returns>Целое число из файла конфигурации.</returns>
        static int read_cfg(string path)
        {
            string[] cfgArray;
            int count;
            try
            {
                cfgArray = File.ReadAllLines(path);
                count = int.Parse(cfgArray[0]); // Чтение значения из файла.
            }
            catch
            {
                using (StreamWriter writer = new StreamWriter(path))
                {
                    writer.WriteLine(0); // Создание файла с начальным значением.
                }
                count = 1; // Если файл не найден, возвращаем начальное значение.
            }
            return count; // Возврат прочитанного или созданного значения.
        }

        /// <summary>
        /// Основной метод программы, который управляет потоками выполнения и взаимодействует с пользователем.
        /// </summary>
        /// <param name="args">Аргументы командной строки.</param>
        static void Main(string[] args)
        {
            ConsoleKeyInfo keyToExit; // Клавиша для выхода.
            string[] pArray; // Массив результатов.
            int pCount; // Счетчик результатов.
            int count; // Счетчик.
            string path = Path.GetFullPath(Path. Combine(Directory.GetCurrentDirectory(), @"../../../")); // Путь к Solution.
            string cfg = Path.Combine(path, "config.txt"); // Путь к файлу конфигурации.
            do
            {
                count = read_cfg(cfg); // Чтение конфигурации.
                pCount = 0; // Сброс счетчика.
                string pathInput = Path.Combine(path, "input.txt"); // Путь к входному файлу.
                string pathOut = Path.Combine(path, "output.txt"); // Путь к выходному файлу.
                int[] a; // Первый массив целых чисел.
                int[] b; // Второй массив целых чисел.
                string[] n = ReadLinesFromFile(pathInput); // Чтение строк из входного файла.
                pArray = new string[n.Length / 2]; // Инициализация массива результатов.
                for (int j = 0; j < n.Length - 1; j++)
                {
                    do
                    {
                        string[] readText = ReadLinesFromFile(pathInput); // Чтение текста из файла.
                        a = operation_with_massive(readText[j].Split(' ')); // Обработка первой строки.
                        b = operation_with_massive(readText[j + 1].Split(' ')); // Обработка второй строки.
                    } while (a.Length == 0 || b.Length == 0); // Проверка на корректность массивов.
                    int p;
                    if (a.Length != b.Length)
                    {
                        p = 0; // Если длины массивов не равны, результат 0.
                    }
                    else
                    {
                        p = calculation_P(a, b); // Вычисление скалярного произведения.
                    }
                    pArray[pCount] = p.ToString("F3").Replace(",", "."); // Форматирование результата.
                    pCount++;
                    j += 2; // Увеличение счетчика на 2.
                }
                write_output(pathOut, pArray); // Запись результатов в выходной файл.
                count++;
                write_config(cfg, count); // Обновление конфигурации.

                Console.WriteLine("Для выхода нажмите Escape....");
                keyToExit = Console.ReadKey(); // Ожидание нажатия клавиши.
            } while (keyToExit.Key != ConsoleKey.Escape); // Выход по Escape.
        }
    }
}
