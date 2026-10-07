using Lib_13;
using LibMas;
using Microsoft.Win32;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace Практ_2
{
    public partial class MainWindow : Window
    {
        int[,] mas;       // исходная матрица
        DataTable table;  // таблица для вывода матрицы в DataGrid

        public MainWindow()
        {
            InitializeComponent();
        }
        void ShowMatrix()
        {
            table = new DataTable();

            if (mas != null)
            {
                int rows = mas.GetLength(0);
                int cols = mas.GetLength(1);

                for (int j = 0; j < cols; j++)
                {
                    table.Columns.Add("C" + (j + 1), typeof(int));
                }

                for (int i = 0; i < rows; i++)
                {
                    DataRow row = table.NewRow();

                    for (int j = 0; j < cols; j++)
                    {
                        row[j] = mas[i, j];
                    }

                    table.Rows.Add(row);
                }
            }

            dataGrid.ItemsSource = table.DefaultView;
        }
        bool ReadMatrix()
        {
            try
            {
                dataGrid.CommitEdit();

                if (dataGrid.ItemsSource == null)
                {
                    MessageBox.Show("Матрица пустая!");
                    return false;
                }

                DataView view = (DataView)dataGrid.ItemsSource;
                table = view.Table;

                int rows = table.Rows.Count;
                int cols = table.Columns.Count;

                if (rows == 0 || cols == 0)
                {
                    MessageBox.Show("Матрица пустая!");
                    return false;
                }

                mas = new int[rows, cols];

                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        mas[i, j] = Convert.ToInt32(table.Rows[i][j]);
                    }
                }

                return true;
            }
            catch
            {
                MessageBox.Show("В матрице должны быть только целые числа");
                return false;
            }
        }

        void ShowResult(int[] result)
        {
            DataTable resultTable = new DataTable();

            resultTable.Columns.Add("Строка", typeof(int));
            resultTable.Columns.Add("Количество", typeof(int));

            for (int i = 0; i < result.Length; i++)
            {
                resultTable.Rows.Add(i + 1, result[i]);
            }

            resultGrid.ItemsSource = resultTable.DefaultView;
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            int rows = Convert.ToInt32(tbRows.Text);
            int cols = Convert.ToInt32(tbCols.Text);

            mas = new int[rows, cols];

            ShowMatrix();

            resultGrid.ItemsSource = null;
        }
        private void Fill_Click(object sender, RoutedEventArgs e)
        {
            int rows = Convert.ToInt32(tbRows.Text);
            int cols = Convert.ToInt32(tbCols.Text);
            int max = Convert.ToInt32(tbMax.Text);

            mas = Massiv.FillMatrix(rows, cols, max);

            ShowMatrix();

            resultGrid.ItemsSource = null;
        }
        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            mas = Massiv.ClearMatrix();

            ShowMatrix();

            resultGrid.ItemsSource = null;
        }
        private void Calc_Click(object sender, RoutedEventArgs e)
        {
            if (!ReadMatrix())
            {
                return;
            }

            int[] result = Calc.CountLessAverage(mas);

            ShowResult(result);
        }
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (!ReadMatrix())
            {
                return;
            }

            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Текстовые файлы|*.txt";

            if (dlg.ShowDialog() == true)
            {
                Massiv.SaveMatrix(mas, dlg.FileName);
            }
        }
        private void Open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Текстовые файлы|*.txt";

            if (dlg.ShowDialog() == true)
            {
                mas = Massiv.OpenMatrix(dlg.FileName);

                tbRows.Text = mas.GetLength(0).ToString();
                tbCols.Text = mas.GetLength(1).ToString();

                ShowMatrix();

                resultGrid.ItemsSource = null;
            }
        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(
                "Практическая работа №2\n" +
                "Вариант 13\n\n" +
                "Разработал: Клейменов Никита\n" +
                "Группа: ИСП-31",
                "О программе");
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}