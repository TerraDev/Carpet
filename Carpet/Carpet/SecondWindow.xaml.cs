using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Diagnostics;

namespace Carpet
{
    /// <summary>
    /// Interaction logic for SecondWindow.xaml
    /// </summary>
    public partial class SecondWindow : Window
    {
        public SecondWindow()
        {
            InitializeComponent();
        }

        public bool clickD = true;
        Graph_Color gc;

        enum Click_U
        {
            off,
            calculate,
        }
        Click_U State= Click_U.off;

        private void Submit(object sender, RoutedEventArgs e)
        {
            if (clickD)
            {
                int a = Convert.ToInt32(Num_TextBox.Text);
                Create_Matrix(a);
                gc = new Graph_Color(a);
            }
            clickD = false;
            State = Click_U.calculate;
            
        }

        internal void Create_Matrix(int size)
        {
            for (int t = 0; t < size; t++)
            {
                RowDefinition RD = new RowDefinition();
                RD.Height = new GridLength(30);
                Matrix.RowDefinitions.Add(RD);
                if (Matrix.RowDefinitions.Count() > 8)
                {
                    Matrix.Height += 30;
                }
            }

            for (int t = 0; t < size; t++)
            {
                ColumnDefinition CD = new ColumnDefinition();
                CD.Width = new GridLength(30);
                Matrix.ColumnDefinitions.Add(CD);
                if (Matrix.ColumnDefinitions.Count() > 29)
                {
                    Matrix.Width += 30;
                }
            }

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    TextBox Description = new TextBox();
                    Description.Background = Brushes.Aqua;
                    Description.Text = "0";
                    Description.TextWrapping = TextWrapping.Wrap;
                    Matrix.Children.Add(Description);
                    Grid.SetRow(Description, i);
                    Grid.SetColumn(Description, j);
                }
            }

          //  Helper_Write("Insert values of SOURCE MATRIX" + '\n' + "Click Refresh button to restart");
        }

        private void Color_Click(object sender, RoutedEventArgs e)
        {
            int i = 0, j = 0;
            int gsize = gc.size;
            bool[,] mat = new bool[gsize, gsize];

            foreach (TextBox box in Matrix.Children)
            {
                mat[i, j] = (box.Text == "1");
                j++;
                if (j >= gsize) { i++; j = 0; }
            }
            gc.graph = mat;

            gc.Calculate();
            Show_max.Text = Convert.ToString(gc.Show_max_Color() + 1);
        }

        private void Go_Next(object sender, RoutedEventArgs e)
        {
            //Process.Start();
            this.Close();
        }
    }
}
