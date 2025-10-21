using System;
using System.Linq;

namespace OOP_Lab1
{
    /// <summary>
    /// Базовий клас для одномірного вектора з 4 елементів.
    /// Демонструє використання віртуальних методів та інкапсуляції.
    /// </summary>
    public class OneDimensionalVector4
    {
        private const int Size = 4;             // Константа розміру вектора
        private readonly double[] _elements;    // Приватне поле для елементів

        /// <summary>
        /// Створює вектор із 4 елементів, заповнених нулями.
        /// </summary>
        public OneDimensionalVector4()
        {
            _elements = new double[Size];
        }

        /// <summary>
        /// Ініціалізує вектор переданими значеннями (до 4 елементів).
        /// </summary>
        public OneDimensionalVector4(params double[] elements)
        {
            _elements = new double[Size];
            SetElements(elements);
        }

        /// <summary>
        /// Метод для встановлення елементів вектора.
        /// </summary>
        public virtual void SetElements(params double[] elements)
        {
            if (elements == null)
                throw new ArgumentNullException(nameof(elements));

            for (int i = 0; i < Math.Min(Size, elements.Length); i++)
                _elements[i] = elements[i];
        }

        /// <summary>
        /// Віртуальний метод для виводу елементів.
        /// </summary>
        public virtual void Print()
        {
            Console.WriteLine("Вектор4: " + string.Join(", ", _elements));
        }

        /// <summary>
        /// Віртуальний метод для знаходження максимального елемента.
        /// Може бути перевизначений у похідних класах.
        /// </summary>
        public virtual double GetMax()
        {
            if (_elements.Length == 0)
                throw new InvalidOperationException("Вектор порожній.");

            return _elements.Max();
        }
    }

    /// <summary>
    /// Похідний клас для матриці 4x4.
    /// Реалізує власні версії SetElements, Print та GetMax.
    /// </summary>
    public class Matrix4x4 : OneDimensionalVector4
    {
        private const int Rows = 4;
        private const int Cols = 4;
        private readonly double[,] _elements;

        /// <summary>
        /// Створює матрицю 4x4, заповнену нулями.
        /// </summary>
        public Matrix4x4()
        {
            _elements = new double[Rows, Cols];
        }

        /// <summary>
        /// Ініціалізує матрицю переданими елементами (макс. 16 значень).
        /// </summary>
        public Matrix4x4(params double[] elements)
        {
            _elements = new double[Rows, Cols];
            SetElements(elements);
        }

        /// <summary>
        /// Перевизначення методу для встановлення елементів матриці.
        /// </summary>
        public override void SetElements(params double[] elements)
        {
            if (elements == null)
                throw new ArgumentNullException(nameof(elements));

            int count = 0;
            for (int i = 0; i < Rows && count < elements.Length; i++)
            {
                for (int j = 0; j < Cols && count < elements.Length; j++)
                {
                    _elements[i, j] = elements[count++];
                }
            }
        }

        /// <summary>
        /// Перевизначення методу для виводу матриці.
        /// </summary>
        public override void Print()
        {
            Console.WriteLine("Матриця 4x4:");
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                    Console.Write($"{_elements[i, j],8}");
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Перевизначення методу для пошуку максимального елемента в матриці.
        /// </summary>
        public override double GetMax()
        {
            double max = _elements[0, 0];
            foreach (double val in _elements)
            {
                if (val > max)
                    max = val;
            }
            return max;
        }
    }

    public static class Program
    {
        /// <summary>
        /// Метод для тестування поліморфізму.
        /// Приймає базовий тип і викликає GetMax().
        /// </summary>
        private static void TestMax(OneDimensionalVector4 v)
        {
            Console.WriteLine($"Максимум (тип: {v.GetType().Name}) = {v.GetMax()}");
        }

        public static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // === Створення об’єктів базового та похідного класів ===
            var vector = new OneDimensionalVector4(1, 5, 3, 2);
            var matrix = new Matrix4x4(
                1, 2, 3, 4,
                5, 6, 7, 8,
                9, 10, 11, 12,
                13, 14, 15, 16
            );

            // === Вивід інформації ===
            Console.WriteLine("=== Вектор ===");
            vector.Print();
            Console.WriteLine($"Максимальний елемент вектора: {vector.GetMax()}\n");

            Console.WriteLine("=== Матриця ===");
            matrix.Print();
            Console.WriteLine($"Максимальний елемент матриці: {matrix.GetMax()}\n");

            // === Демонстрація поліморфізму через масив базових типів ===
            OneDimensionalVector4[] arr =
            {
                new OneDimensionalVector4(2, 7, 1, 0),
                new Matrix4x4(
                    3, 9, 1, 2,
                    4, 8, 5, 6,
                    7, 11, 10, 12,
                    0, 13, 14, 15)
            };

            Console.WriteLine("=== Демонстрація поліморфізму через масив ===");
            foreach (var item in arr)
            {
                item.Print();
                TestMax(item); // Викликається своя версія GetMax() для кожного типу
                Console.WriteLine();
            }

            // === Додаткова демонстрація ===
            Console.WriteLine("=== Динамічне створення об'єкта ===");
            OneDimensionalVector4 refObj;

            Console.Write("Введіть тип об’єкта (1 — Vector4, 2 — Matrix4x4): ");
            string? choice = Console.ReadLine();

            refObj = choice == "2" ? new Matrix4x4() : new OneDimensionalVector4();

            refObj.SetElements(1, 2, 3, 4);
            refObj.Print();
            Console.WriteLine($"Максимальний елемент (динамічне зв’язування): {refObj.GetMax()}");
        }
    }
}
