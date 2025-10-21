using System;
using System.Linq;
using System.Text;

namespace OOP_Lab2
{
    /// <summary>
    /// Базовий клас для вектора, який зберігає одновимірний масив елементів.
    /// Містить віртуальні методи для введення, відображення та пошуку максимального елемента.
    /// </summary>
    public class Vector
    {
        // Protected поля, доступні у похідних класах
        protected int size;
        protected double[] elements;

        // Конструктор за замовчуванням
        public Vector() : this(4) { }

        // Конструктор з параметром
        public Vector(int size)
        {
            if (size <= 0) throw new ArgumentException("Розмір має бути додатнім.");
            this.size = size;
            this.elements = new double[size];
            Console.OutputEncoding = Encoding.UTF8; // Встановлення кодування для коректного відображення української мови
        }

        /// <summary>
        /// Віртуальний метод для введення елементів вектора.
        /// </summary>
        public virtual void InputElements()
        {
            Console.WriteLine($"\nВведіть {size} елементів вектора:");
            for (int i = 0; i < size; i++)
            {
                Console.Write($"Елемент [{i}] = ");
                while (!double.TryParse(Console.ReadLine(), out elements[i]))
                {
                    Console.Write("Некоректне значення, повторіть: ");
                }
            }
        }

        /// <summary>
        /// Віртуальний метод для відображення вектора.
        /// </summary>
        public virtual void Display()
        {
            Console.WriteLine("\n--- Вектор ---");
            Console.WriteLine("Елементи: " + string.Join(", ", elements.Select(e => e.ToString("F2"))));
        }

        /// <summary>
        /// Віртуальний метод для знаходження максимального елемента.
        /// </summary>
        public virtual double MaxElement()
        {
            return elements.Length > 0 ? elements.Max() : double.NaN;
        }
    }

    /// <summary>
    /// Похідний клас для матриці, яка успадковує функціонал Vector, 
    /// але перевизначає методи для роботи в 2D-форматі.
    /// </summary>
    public class Matrix : Vector
    {
        private int rows;
        private int cols;

        // Конструктор за замовчуванням
        public Matrix() : this(3, 3) { }

        // Конструктор з параметрами
        public Matrix(int rows, int cols) : base(rows * cols)
        {
            this.rows = rows;
            this.cols = cols;
        }

        /// <summary>
        /// Перевизначений метод для введення елементів матриці.
        /// </summary>
        public override void InputElements()
        {
            Console.WriteLine($"\nВведіть елементи матриці {rows}x{cols}:");
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Console.Write($"Елемент [{i},{j}] = ");
                    int index = i * cols + j;
                    while (!double.TryParse(Console.ReadLine(), out elements[index]))
                    {
                        Console.Write("Некоректне значення, повторіть: ");
                    }
                }
            }
        }

        /// <summary>
        /// Перевизначений метод для відображення матриці у 2D-форматі.
        /// </summary>
        public override void Display()
        {
            Console.WriteLine($"\n--- Матриця {rows}x{cols} ---");
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    // Виведення з вирівнюванням
                    Console.Write($"{elements[i * cols + j],10:F2}");
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Перевизначений метод MaxElement, який використовує базову реалізацію.
        /// </summary>
        public override double MaxElement()
        {
            return base.MaxElement();
        }
    }

    public static class Program
    {
        public static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            
            // 1. Запит вибору користувача
            Console.WriteLine("Оберіть тип об'єкта для роботи:");
            Console.WriteLine("1. Вектор (Vector)");
            Console.WriteLine("2. Матриця (Matrix)");
            Console.Write("Ваш вибір (1 або 2): ");
            string userChoose = Console.ReadLine();

            // 2. Створення посилання на базовий клас (показчик)
            Vector baseObj = null;

            // 3. Динамічне створення об'єкта залежно від вибору
            if (userChoose == "1")
            {
                Console.Write("Введіть розмір вектора (наприклад, 5): ");
                if (int.TryParse(Console.ReadLine(), out int size) && size > 0)
                {
                    baseObj = new Vector(size); // Створюється об'єкт Vector
                }
                else
                {
                    Console.WriteLine("Некоректний розмір. Використовуємо розмір за замовчуванням (4).");
                    baseObj = new Vector();
                }
            }
            else if (userChoose == "2")
            {
                Console.Write("Введіть кількість рядків матриці (наприклад, 3): ");
                if (!int.TryParse(Console.ReadLine(), out int rows) || rows <= 0) rows = 3;
                Console.Write("Введіть кількість стовпців матриці (наприклад, 4): ");
                if (!int.TryParse(Console.ReadLine(), out int cols) || cols <= 0) cols = 4;
                
                baseObj = new Matrix(rows, cols); // Створюється об'єкт Matrix
            }
            else
            {
                Console.WriteLine("\nНекоректний вибір. Програма завершує роботу.");
                return;
            }

            // 4. Виклик віртуальних методів через посилання на базовий клас
            // На етапі компіляції невідомо, чи baseObj є Vector, чи Matrix.
            // Завдяки віртуальним методам (override) система визначає потрібний метод 
            // під час виконання (пізнє зв'язування).
            
            Console.WriteLine("\n--- Виклик методів через базовий клас (Vector) ---");
            Console.WriteLine("Демонстрація поліморфізму (рантайм визначення типу):");
            
            try
            {
                baseObj.InputElements(); // Викликається InputElements класу Vector або Matrix
                baseObj.Display();       // Викликається Display класу Vector або Matrix
                Console.WriteLine($"\nМаксимальний елемент: {baseObj.MaxElement():F2}"); // Викликається MaxElement класу Vector або Matrix
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nВиникла помилка під час роботи: {ex.Message}");
            }
        }
    }
}
