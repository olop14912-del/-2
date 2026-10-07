using System;
using System.IO;

namespace LibMas
{
    public class Massiv
    {
        public static int[,] FillMatrix(int rows, int cols, int max)
        {
            Random rnd = new Random();
            int[,] mas = new int[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    mas[i, j] = rnd.Next(max + 1);
                }
            }

            return mas;
        }
        public static int[,] ClearMatrix()
        {
            return null;
        }
        public static void SaveMatrix(int[,] mas, string fileName)
        {
            StreamWriter file = new StreamWriter(fileName);

            int rows = mas.GetLength(0);
            int cols = mas.GetLength(1);

            file.WriteLine(rows);
            file.WriteLine(cols);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    file.WriteLine(mas[i, j]);
                }
            }

            file.Close();
        }

        public static int[,] OpenMatrix(string fileName)
        {
            StreamReader file = new StreamReader(fileName);

            int rows = Convert.ToInt32(file.ReadLine());
            int cols = Convert.ToInt32(file.ReadLine());

            int[,] mas = new int[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    mas[i, j] = Convert.ToInt32(file.ReadLine());
                }
            }

            file.Close();

            return mas;
        }
    }
}