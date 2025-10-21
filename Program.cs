using System;
using System.Linq;

namespace OOP_Lab1
{
    public class Vector
    {
        protected int size;
        protected double[] elements;

        public Vector() : this(4) { }

        public Vector(int size)
        {
            this.size = size;
            elements = new double[size];
        }

        public virtual void InputElements()
        {
            Console.WriteLine($"Введіть {size} елементів вектора:");
            for (int i = 0; i < size; i++)
            {
                Console.Write($"Елемент [{i}] = ");
                while (!double.TryParse(Console.ReadLine(), out elements[i]))
                    Console.Write("Некоректне значення, повторіть: ");
            }
        }

        public virtual void Display()
        {
            Console.WriteLine("Вектор: " + string.Join(", ", elements));
        }

        public virtual double MaxElement()
        {
            return elements.Length > 0 ? elements.Max() : double.NaN;
        }
    }

    public class Matrix : Vector
    {
        private int rows;
        private int cols;

        public Matrix() : this(4, 4) { }

        public Matrix(int rows, int cols) : base(rows * cols)
        {
            this.rows = rows;
            this.cols = cols;
        }

        public override void InputElements()
        {
            Console.WriteLine($"Введіть елементи матриці {rows}x{cols}:");
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Console.Write($"Елемент [{i},{j}] = ");
                    int index = i * cols + j;
                    while (!double.TryParse(Console.ReadLine(), out elements[index]))
                        Console.Write("Некоректне значення, повторіть: ");
                }
            }
        }

        public override void Display()
        {
            Console.WriteLine($"Матриця {rows}x{cols}:");
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Console.Write($"{elements[i * cols + j],8}");
                }
                Console.WriteLine();
            }
        }
    }

    public static class Program
    {
        public static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Динамічне створення об’єкта через покажчик на базовий клас
            Vector obj;

            Console.Write("Введіть тип об’єкта (1 - вектор, 2 - матриця): ");
            string choice = Console.ReadLine();

            if (choice == "1")
                obj = new Vector(); // створюємо об’єкт типу Vector
            else
                obj = new Matrix(); // створюємо об’єкт типу Matrix

            // Виклики віртуальних методів — програма не знає наперед, який саме тип!
            obj.InputElements();
            obj.Display();
            Console.WriteLine($"Максимальний елемент: {obj.MaxElement()}");
        }
    }
}
