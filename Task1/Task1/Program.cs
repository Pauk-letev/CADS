using System;
using System.Linq;
using System.IO;

class Program
{
    static double[,] MatrixMult(double[,] A, double[,] B, int n, int m, int p)
    {
        double[,] C = new double[n, p];
        for(int i = 0; i < n; ++i)
            for(int j = 0; j < p; ++j)
            {
                C[i, j] = 0;
                for (int k = 0; k < m; ++k)
                    C[i, j] += A[i, k] * B[k, j];
            }
        return C;
    }
    static bool Simmetric(double[,]A, int n)
    {
        for (int i = 0; i < n; ++i)
            for (int j = i + 1; j < n; ++j)
                if (A[i, j] != A[j, i])
                    return false;
        return true;
    }
    class Vector
    {
        public int Dim {  get; set; }
        private double[,] x;
        public Vector(int dim, double []vector)
        {
            this.Dim = dim;
            x = new double[1, Dim];
            for (int i = 0; i < Dim; ++i)
                x[0, i] = vector[i];
        }
        private double[,] Transpose()
        {
            double[,] x_T = new double[Dim, 1];
            for (int i = 0; i < Dim; ++i)
                x_T[i, 0] = x[0, i];
            return x_T;
        }
        public double Len(double[,] G)
        {
            double[,] ans = MatrixMult(MatrixMult(x, G, 1, Dim, Dim), this.Transpose(), 1, Dim, 1);
            return Math.Sqrt(ans[0, 0]);
        }
    }
    
    static void Main(string[] args)
    {
        string fileR = @"Task_number_one_input.txt";
        string answer;
        int dim;
        double[,] G; Vector x;
        using (StreamReader sr = File.OpenText(fileR))
        {
            dim = int.Parse(sr.ReadLine());
            G = new double[dim, dim];
            for (int i = 0; i < dim; ++i)
            {
                string[] row = sr.ReadLine().Split();
                for (int j = 0; j < dim; ++j)
                    G[i, j] = double.Parse(row[j]);
            }
            double[] vector = sr.ReadLine().Split().Select(double.Parse).ToArray();
            x = new Vector(dim, vector);
        }
        if (Simmetric(G, dim))
            answer = x.Len(G).ToString();
        else
            answer = "Matrix is not simmetric";
        Console.WriteLine(answer);
    }
}