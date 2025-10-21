using System;
using System.Linq;

namespace OOP_Lab1
{
    // === Базовий клас Vector4 ===
    public class Vector4
    {
        protected const int Size = 4;
        protected double[] Elements { get; set; }

        // Конструктор за замовчуванням (всі нулі)
        public Vector4()
        {
            Elements = new double[Size];
        }

        // Параметризований конструктор
        public Vector4(params double[] elements)
        {
            Elements = new double[Size];
            for (int i = 0; i < Math.Min(Size, elements.Length); i++)
                Elements[i] = elements[i];
        }

        // Віртуальний метод введення
        public virtual void Input()
        {
            Console.WriteLine("Введіть 4 елементи вектора:");
            for (int i = 0; i < Size; i++)
            {
                Console.Write($"Елемент [{i}] = ");
                while (!double.TryParse(Console.ReadLine(), out Elements[i]))
                    Console.Write("Некоректне значення, повторіть: ");
            }
        }

        // Віртуальний метод виводу
        public virtual void Print()
        {
            Console.WriteLine("Вектор4: " + string.Join(", ", Elements));
        }

        // Віртуальний метод пошуку максимуму
        public virtual double GetMaxElement()
        {
            return Elements.Max();
        }
    }

    // === Похідний клас Matrix4x4 ===
    public class Matrix4x4 : Vector4
    {
        private const int Rows = 4;
        private const int Cols = 4;

        // Конструктор за замовчуванням
        public Matrix4x4()
        {
            Elements = new double[Rows * Cols];
        }

        // Параметризований конструктор
        public Matrix4x4(params double[] elements)
        {
            Elements = new double[Rows * Cols];
            for (int i = 0; i < Math.Min(Rows * Cols, elements.Length); i++)
                Elements[i] = elements[i];
        }

        // Перевизначення методу введення
        public override void Input()
        {
            Console.WriteLine("Введіть елементи матриці 4x4:");
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                {
                    int index = i * Cols + j;
                    Console.Write($"Елемент [{i},{j}] = ");
                    while (!double.TryParse(Console.ReadLine(), out Elements[index]))
                        Console.Write("Некоректне значення, повторіть: ");
                }
            }
        }

        // Перевизначення методу виводу
        public override void Print()
        {
            Console.WriteLine("Матриця 4x4:");
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                    Console.Write($"{Elements[i * Cols + j],8}");
                Console.WriteLine();
            }
        }

        // Перевизначення методу пошуку максимуму
        public override double GetMaxElement()
        {
            return Elements.Max();
        }
    }

    // === Точка входу ===
    public static class Program
    {
        public static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // --- Приклади створення окремих об'єктів ---
            var vector = new Vector4(1, 5, 3, 2);
            var matrix = new Matrix4x4(
                1, 2, 3, 4,
                5, 6, 7, 8,
                9, 10, 11, 12,
                13, 14, 15, 16
            );

            Console.WriteLine("=== Звичайні виклики ===");
            vector.Print();
            Console.WriteLine($"Максимум у векторі: {vector.GetMaxElement()}\n");

            matrix.Print();
            Console.WriteLine($"Максимум у матриці: {matrix.GetMaxElement()}\n");

            // --- Демонстрація поліморфізму ---
            Console.WriteLine("=== Демонстрація поліморфізму ===");
            Vector4 refVector;

            Console.Write("Введіть тип об’єкта (1 — Vector4, 2 — Matrix4x4): ");
            string choice = Console.ReadLine();

            if (choice == "1")
                refVector = new Vector4();
            else
                refVector = new Matrix4x4();

            // Виклики віртуальних методів — програма не знає, який тип об'єкта!
            refVector.Input();
            refVector.Print();
            Console.WriteLine($"Максимальний елемент: {refVector.GetMaxElement()}");
        }
    }
}
