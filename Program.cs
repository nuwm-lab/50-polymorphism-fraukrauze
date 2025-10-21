using System;
using System.Linq;

namespace OOP_Lab1
{
    // === Базовий клас Vector4 ===
    public class Vector4
    {
        // Константа розміру
        private const int Size = 4;

        // Приватне поле
        private double[] _elements;

        // Публічна властивість лише для читання (інкапсуляція)
        public double[] Elements => _elements;

        // Конструктор за замовчуванням — ініціалізує нулями
        public Vector4()
        {
            _elements = new double[Size];
        }

        // Параметризований конструктор
        public Vector4(params double[] elements)
        {
            _elements = new double[Size];
            for (int i = 0; i < Math.Min(Size, elements.Length); i++)
                _elements[i] = elements[i];
        }

        // Віртуальний метод введення елементів
        public virtual void Input()
        {
            Console.WriteLine("Введіть 4 елементи вектора:");
            for (int i = 0; i < Size; i++)
            {
                Console.Write($"Елемент [{i}] = ");
                while (!double.TryParse(Console.ReadLine(), out _elements[i]))
                    Console.Write("Некоректне значення, повторіть: ");
            }
        }

        // Віртуальний метод виводу
        public virtual void Print()
        {
            Console.WriteLine("Вектор4: " + string.Join(", ", _elements));
        }

        // Віртуальний метод знаходження максимального елемента
        public virtual double FindMax()
        {
            return _elements.Max();
        }
    }

    // === Похідний клас Matrix4x4 ===
    public class Matrix4x4 : Vector4
    {
        // Константи для розміру матриці
        private const int Rows = 4;
        private const int Cols = 4;

        // Приватне поле для зберігання елементів (16 значень)
        private double[] _matrixElements;

        // Властивість для читання
        public double[] MatrixElements => _matrixElements;

        // Конструктор за замовчуванням — усі елементи 0
        public Matrix4x4()
        {
            _matrixElements = new double[Rows * Cols];
        }

        // Параметризований конструктор
        public Matrix4x4(params double[] elements)
        {
            _matrixElements = new double[Rows * Cols];
            for (int i = 0; i < Math.Min(Rows * Cols, elements.Length); i++)
                _matrixElements[i] = elements[i];
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
                    while (!double.TryParse(Console.ReadLine(), out _matrixElements[index]))
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
                    Console.Write($"{_matrixElements[i * Cols + j],8}");
                Console.WriteLine();
            }
        }

        // Перевизначення методу пошуку максимуму
        public override double FindMax()
        {
            return _matrixElements.Max();
        }
    }

    // === Точка входу ===
    public static class Program
    {
        public static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Масив базового типу, що містить і базові, і похідні об’єкти
            Vector4[] arr = new Vector4[]
            {
                new Vector4(1, 3, 5, 2),
                new Matrix4x4(
                    1, 2, 3, 4,
                    5, 6, 7, 8,
                    9, 10, 11, 12,
                    13, 14, 15, 16)
            };

            Console.WriteLine("=== Демонстрація поліморфізму через масив базових типів ===");

            foreach (var item in arr)
            {
                item.Print();  // Виклик віртуального методу
                Console.WriteLine($"Максимальний елемент: {item.FindMax()}\n");
            }

            // Додатково: показати, що програма може динамічно створювати об’єкти
            Console.WriteLine("=== Створення об’єкта динамічно ===");
            Vector4 refVector;

            Console.Write("Введіть тип об’єкта (1 — Vector4, 2 — Matrix4x4): ");
            string choice = Console.ReadLine();

            if (choice == "1")
                refVector = new Vector4();
            else
                refVector = new Matrix4x4();

            refVector.Input();
            refVector.Print();
            Console.WriteLine($"Максимальний елемент: {refVector.FindMax()}");
        }
    }
}
