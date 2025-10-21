using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OOP_Lab2
{
    /// <summary>
    /// Базовий клас для вектора, що зберігає одновимірний масив елементів.
    /// Реалізує віртуальні методи для поліморфної поведінки.
    /// </summary>
    public class Vector4
    {
        protected const int DEFAULT_SIZE = 4;
        
        protected int _size;
        protected double[] _elements;

        /// <summary>
        /// Ініціалізує новий екземпляр класу <see cref="Vector4"/> з розміром за замовчуванням (4).
        /// </summary>
        public Vector4() : this(DEFAULT_SIZE) { }

        /// <summary>
        /// Ініціалізує новий екземпляр класу <see cref="Vector4"/> з вказаним розміром.
        /// Цей конструктор використовується похідними класами (наприклад, Matrix4x4) для ініціалізації базового масиву.
        /// </summary>
        /// <param name="size">Розмір вектора. Має бути додатнім.</param>
        public Vector4(int size)
        {
            if (size <= 0) throw new ArgumentException("Розмір має бути додатнім.");
            this._size = size;
            this._elements = new double[size];
            Console.OutputEncoding = Encoding.UTF8; 
        }

        /// <summary>
        /// Встановлює елементи вектора з переданого масиву.
        /// </summary>
        /// <param name="values">Масив значень для ініціалізації.</param>
        /// <exception cref="ArgumentException">Виникає, якщо розмір масиву не відповідає розміру вектора.</exception>
        public virtual void SetElements(double[] values)
        {
            if (values == null || values.Length != _size)
            {
                throw new ArgumentException($"Розмір вхідного масиву ({values?.Length ?? 0}) не відповідає розміру вектора ({_size}).");
            }
            Array.Copy(values, _elements, _size);
            Console.WriteLine($"\nУспішно встановлено {_size} елементів вектора з масиву.");
        }

        /// <summary>
        /// Віртуальний метод для введення елементів вектора з консолі.
        /// </summary>
        public virtual void SetElementsFromConsole()
        {
            Console.WriteLine($"\nВведіть {_size} елементів вектора:");
            for (int i = 0; i < _size; i++)
            {
                Console.Write($"Елемент [{i}] = ");
                while (!double.TryParse(Console.ReadLine(), out _elements[i]))
                {
                    Console.Write("Некоректне значення, повторіть: ");
                }
            }
        }

        /// <summary>
        /// Віртуальний метод для відображення вектора в консоль.
        /// </summary>
        public virtual void Display()
        {
            Console.WriteLine(this.ToString());
        }

        /// <summary>
        /// Віртуальний метод для знаходження максимального елемента.
        /// </summary>
        /// <returns>Максимальний елемент вектора, або <see cref="double.NaN"/>, якщо він порожній.</returns>
        public virtual double GetMax()
        {
            return _elements.Length > 0 ? _elements.Max() : double.NaN;
        }

        /// <summary>
        /// Повертає рядкове представлення вектора.
        /// </summary>
        /// <returns>Рядок, що представляє поточний вектор.</returns>
        public override string ToString()
        {
            return $"\n--- Вектор ({_size} ел.) ---\nЕлементи: " + string.Join(", ", _elements.Select(e => e.ToString("F2")));
        }
    }

    /// <summary>
    /// Похідний клас для матриці 4x4, яка успадковує клас <see cref="Vector4"/>, 
    /// зберігаючи елементи в одномірному масиві (довжиною 16), але перевизначає методи для 2D-формату.
    /// </summary>
    public class Matrix4x4 : Vector4
    {
        private const int MATRIX_DIMENSION = 4;
        private int _rows;
        private int _cols;

        /// <summary>
        /// Ініціалізує новий екземпляр класу <see cref="Matrix4x4"/> з розміром 4x4.
        /// </summary>
        public Matrix4x4() : this(MATRIX_DIMENSION, MATRIX_DIMENSION) { }

        /// <summary>
        /// Ініціалізує новий екземпляр класу <see cref="Matrix4x4"/> з вказаною кількістю рядків та стовпців.
        /// Конструктор для гнучкості. Якщо розмір не 4x4, слідкуйте за коректністю індексації.
        /// </summary>
        /// <param name="rows">Кількість рядків.</param>
        /// <param name="cols">Кількість стовпців.</param>
        public Matrix4x4(int rows, int cols) : base(rows * cols)
        {
            this._rows = rows;
            this._cols = cols;
        }

        /// <summary>
        /// Перевизначений метод для введення елементів матриці з консолі.
        /// </summary>
        public override void SetElementsFromConsole()
        {
            Console.WriteLine($"\nВведіть елементи матриці {_rows}x{_cols}:");
            for (int i = 0; i < _rows; i++)
            {
                for (int j = 0; j < _cols; j++)
                {
                    Console.Write($"Елемент [{i},{j}] = ");
                    int index = i * _cols + j;
                    while (!double.TryParse(Console.ReadLine(), out _elements[index]))
                    {
                        Console.Write("Некоректне значення, повторіть: ");
                    }
                }
            }
        }
        
        /// <summary>
        /// Перевизначений метод для встановлення елементів матриці з масиву.
        /// </summary>
        /// <param name="values">Масив значень для ініціалізації.</param>
        /// <exception cref="ArgumentException">Виникає, якщо розмір масиву не відповідає розміру матриці.</exception>
        public override void SetElements(double[] values)
        {
            if (values == null || values.Length != _size)
            {
                throw new ArgumentException($"Розмір вхідного масиву ({values?.Length ?? 0}) не відповідає розміру матриці ({_rows}x{_cols}, загалом {_size} елементів).");
            }
            Array.Copy(values, _elements, _size);
            Console.WriteLine($"\nУспішно встановлено {_size} елементів матриці з масиву.");
        }

        /// <summary>
        /// Перевизначений метод для відображення матриці у 2D-форматі.
        /// </summary>
        public override void Display()
        {
            Console.WriteLine(this.ToString());
        }

        /// <summary>
        /// Перевизначений метод GetMax, який використовує базову реалізацію.
        /// </summary>
        /// <returns>Максимальний елемент матриці.</returns>
        public override double GetMax()
        {
            return base.GetMax();
        }

        /// <summary>
        /// Повертає рядкове представлення матриці у 2D-форматі.
        /// </summary>
        /// <returns>Рядок, що представляє поточну матрицю.</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"\n--- Матриця {_rows}x{_cols} ({_size} ел.) ---");
            for (int i = 0; i < _rows; i++)
            {
                for (int j = 0; j < _cols; j++)
                {
                    sb.AppendFormat("{0,10:F2}", _elements[i * _cols + j]);
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }
    }

    public static class Program
    {
        public static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            
            // Використання базового типу Vector4 для демонстрації поліморфізму
            List<Vector4> shapes = new List<Vector4>();
            
            Console.WriteLine("--- ІНТЕРАКТИВНЕ СТВОРЕННЯ ВЕКТОРА (4 елементи) ---");
            // Створюємо Vector4 - розмір 4
            Vector4 interactiveVector = new Vector4();
            try
            {
                interactiveVector.SetElementsFromConsole(); 
                shapes.Add(interactiveVector);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");
            }
            
            Console.WriteLine("\n--- ДЕМОНСТРАЦІЯ SetElements та КОЛЕКЦІЇ (Поліморфізм) ---");

            // Створюємо Matrix4x4 - розмір 4x4 (16 елементів)
            double[] matrixData = { 1.1, 2.2, 3.3, 4.4, 5.5, 6.6, 7.7, 8.8, 9.9, 10.0, 11.1, 12.2, 13.3, 14.4, 15.5, 16.6 }; // 16 елементів
            Matrix4x4 predefinedMatrix = new Matrix4x4(); // 4x4
            try
            {
                predefinedMatrix.SetElements(matrixData);
                shapes.Add(predefinedMatrix);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Помилка при ініціалізації матриці: {ex.Message}");
            }

            // Створюємо ще один Vector4 - розмір 4
            double[] vectorData = { -10.0, -5.0, 25.5, 0.5 };
            Vector4 predefinedVector = new Vector4();
            try
            {
                predefinedVector.SetElements(vectorData);
                shapes.Add(predefinedVector);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Помилка при ініціалізації вектора: {ex.Message}");
            }


            Console.WriteLine("\n==========================================");
            Console.WriteLine("ВИКЛИК ВІРТУАЛЬНИХ МЕТОДІВ ЧЕРЕЗ КОЛЕКЦІЮ VECTOR4 (Пізнє зв'язування)");
            Console.WriteLine("==========================================");

            foreach (var obj in shapes)
            {
                Console.WriteLine("------------------------------------------");
                
                // Динамічний поліморфізм: викликається Matrix4x4.Display() або Vector4.Display()
                obj.Display(); 
                
                // Динамічний поліморфізм: викликається Matrix4x4.GetMax() або Vector4.GetMax()
                Console.WriteLine($"Максимальний елемент: {obj.GetMax():F2}"); 
            }
        }
    }
}
