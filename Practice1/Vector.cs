using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vector
{
    class Vector
    {
        public Vector(double[] arr)
        {
            _arr = (double[])arr.Clone();
        }


        public int Length
        {
            get { return _arr.Length; }
        }

        public static int RandomNumber()
        {
            Random rand = new Random();
            int a = rand.Next(100);
            return a;
        }
        public static int RandomNumber(int r)
        {
            Random rand = new Random();
            int a = rand.Next(r);
            return a;
        }
        public static int RandomNumber(int r1, int r2)
        {
            Random rand = new Random();
            int a = rand.Next(r1, r2);
            return a;
        }

        public override string ToString()
        {
            return String.Join(" ", _arr);
        }

        public static Vector operator +(Vector op1, Vector op2)
        {
            if (op1.Length != op2.Length)
                throw new ArgumentException();

            double[] arr = new double[op1.Length];
            for (int i = 0; i < op1.Length; ++i)
            {
                arr[i] = op1._arr[i] + op2._arr[i];
            }

            return new Vector(arr);
        }

        public static Vector operator -(Vector op1, Vector op2)
        {
            if (op1.Length != op2.Length)
                throw new ArgumentException();

            double[] arr = new double[op1.Length];
            for (int i = 0; i < op1.Length; ++i)
            {
                arr[i] = op1._arr[i] - op2._arr[i];
            }

            return new Vector(arr);
        }

        double[] _arr;
    }
}
