namespace Lib_13
{
    public class Calc
    {
        public static int[] CountLessAverage(int[,] mas)
        {
            int rows = mas.GetLength(0);
            int cols = mas.GetLength(1);
            int[] result = new int[rows];
            for (int i = 0; i < rows; i++)
            {
                double sum = 0;
                // сумма строки
                for (int j = 0; j < cols; j++)
                {
                    sum = sum + mas[i, j];
                }
                double average = sum / cols;
                int count = 0;
                for (int j = 0; j < cols; j++)
                {
                    if (mas[i, j] < average)
                    {
                        count++;
                    }
                }

                result[i] = count;
            }

            return result;
        }
    }
}