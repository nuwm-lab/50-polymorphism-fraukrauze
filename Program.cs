using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OOP_Lab2
{
    public class Vector
    {
        protected const int DEFAULT_SIZE = 4;
        
        protected int _size;
        protected double[] _elements;

        public Vector() : this(DEFAULT_SIZE) { }

        public Vector(int size)
        {
            if (size <= 0) throw new ArgumentException("Size must be positive.");
            this._size = size;
            this._elements = new double[size];
            Console.OutputEncoding = Encoding.UTF8; 
        }

        public virtual void SetElements(double[] values)
        {
            if (values == null || values.Length != _size)
            {
                throw new ArgumentException($"Input array size ({values?.Length ?? 0}) does not match vector size ({_size}).");
            }
            Array.Copy(values, _elements, _size);
            Console.WriteLine($"\nSuccessfully set {_size} vector elements from array.");
        }

        public virtual void SetElementsFromConsole()
        {
            Console.WriteLine($"\nEnter {_size} vector elements:");
            for (int i = 0; i < _size; i++)
            {
                Console.Write($"Element [{i}] = ");
                while (!double.TryParse(Console.ReadLine(), out _elements[i]))
                {
                    Console.Write("Invalid value, please retry: ");
                }
            }
        }

        public virtual void Display()
        {
            Console.WriteLine("\n--- Vector ---");
            Console.WriteLine("Elements: " + string.Join(", ", _elements.Select(e => e.ToString("F2"))));
        }

        public virtual double FindMax()
        {
            return _elements.Length > 0 ? _elements.Max() : double.NaN;
        }
    }

    public class Matrix : Vector
    {
        private int _rows;
        private int _cols;

        public Matrix() : this(DEFAULT_SIZE, DEFAULT_SIZE) { }

        public Matrix(int rows, int cols) : base(rows * cols)
        {
            this._rows = rows;
            this._cols = cols;
        }

        public override void SetElementsFromConsole()
        {
            Console.WriteLine($"\nEnter elements for the {_rows}x{_cols} matrix:");
            for (int i = 0; i < _rows; i++)
            {
                for (int j = 0; j < _cols; j++)
                {
                    Console.Write($"Element [{i},{j}] = ");
                    int index = i * _cols + j;
                    while (!double.TryParse(Console.ReadLine(), out _elements[index]))
                    {
                        Console.Write("Invalid value, please retry: ");
                    }
                }
            }
        }
        
        public override void SetElements(double[] values)
        {
            if (values == null || values.Length != _size)
            {
                throw new ArgumentException($"Input array size ({values?.Length ?? 0}) does not match matrix size ({_rows}x{_cols}, total {_size} elements).");
            }
            Array.Copy(values, _elements, _size);
            Console.WriteLine($"\nSuccessfully set {_size} matrix elements from array.");
        }

        public override void Display()
        {
            Console.WriteLine($"\n--- Matrix {_rows}x{_cols} ---");
            for (int i = 0; i < _rows; i++)
            {
                for (int j = 0; j < _cols; j++)
                {
                    Console.Write($"{_elements[i * _cols + j],10:F2}");
                }
                Console.WriteLine();
            }
        }

        public override double FindMax()
        {
            return base.FindMax();
        }
    }

    public static class Program
    {
        public static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            
            List<Vector> shapes = new List<Vector>();
            
            Console.WriteLine("--- INTERACTIVE VECTOR CREATION (5 elements) ---");
            Vector interactiveVector = new Vector(5);
            try
            {
                interactiveVector.SetElementsFromConsole(); 
                shapes.Add(interactiveVector);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            
            Console.WriteLine("\n--- SetElements DEMO and COLLECTION (Polymorphism) ---");

            double[] matrixData = { 1.1, 2.2, 3.3, 4.4, 5.5, 6.6, 7.7, 8.8, 9.9 };
            Matrix predefinedMatrix = new Matrix(3, 3);
            try
            {
                predefinedMatrix.SetElements(matrixData);
                shapes.Add(predefinedMatrix);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Matrix initialization error: {ex.Message}");
            }

            double[] vectorData = { 10.0, -5.0, 25.5, 0.5 };
            Vector predefinedVector = new Vector(4);
            try
            {
                predefinedVector.SetElements(vectorData);
                shapes.Add(predefinedVector);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Vector initialization error: {ex.Message}");
            }


            Console.WriteLine("\n==========================================");
            Console.WriteLine("CALLING VIRTUAL METHODS VIA VECTOR COLLECTION (Late Binding)");
            Console.WriteLine("==========================================");

            foreach (var obj in shapes)
            {
                Console.WriteLine("------------------------------------------");
                
                obj.Display(); 
                
                Console.WriteLine($"Maximum element: {obj.FindMax():F2}"); 
            }
        }
    }
}
