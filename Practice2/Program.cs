using System;

namespace Practice2
{
    class Program
    {
        static void Main(string[] args)
        {
            //Вариант 0
            Console.WriteLine(('D' + 'A') % 7);

        }
    }


    public class Matrix 
    {
        public readonly double[,] Data;

        //Конструкторы 
        public Matrix(int nRows, int nCols)
        {

        }

        public Matrix(double[,] initData)
        {
            Rows = initData.GetLength(0);
            Cols = initData.GetLength(1);

            IsSquared = false;
            Size = 0;

            if (Rows == Cols)
            {
                Size = Rows;
                IsSquared = true;
            }

            this.Data = new double[Rows, Cols];
           
        }


        public int Rows { get; } //доступ к числу строк
        public int Cols { get; } //доступ к числу столбцов
        public int Size { get; } //доступ к размеру квадратной матрицы
        public bool IsSquared { get; } //// Является ли матрица квадратной
        public bool IsEmpty { get; } // Является ли матрица нулевой 
        public bool IsUnity { get; } // Является ли матрица единичной 
        public bool IsDiagonal { get; } // Является ли матрица диагональной 
        public bool IsSymmetric { get; } // Является ли матрица симметричной
    }
}
