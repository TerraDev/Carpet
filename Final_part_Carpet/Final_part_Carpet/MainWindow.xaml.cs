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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Wintellect.PowerCollections;

namespace Final_part_Carpet
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static int[,] matrix;
        public static List<int> Destination = new List<int>();
        public static int Starting_Point;
        public static int size;

        public MainWindow()
        {
            InitializeComponent();
            WindowState = WindowState.Maximized;
            jun_box.IsChecked = false ;
            Dest_box.IsChecked = false ;
            Address_box.IsChecked = false ;
        } 

        private void Submit(object sender, RoutedEventArgs e)
        {

            int a = Convert.ToInt32(Sizer.Text);
            Create_Matrix(a);
            matrix = new int[a, a];
            size = a;

            jun_box.IsChecked = true;

        }

        private void Final_Submit(object sender, RoutedEventArgs e)
        {
            if ((jun_box.IsChecked == true) && (Dest_box.IsChecked == true) && (Address_box.IsChecked == true))
            {
                int i = 0, j = 0;
                foreach (TextBox box in Matrix.Children)
                {
                    matrix[i, j] = Convert.ToInt32(box.Text);
                    if (i == j || j == Starting_Point)
                        matrix[i, j] = -1;
                    j++;
                    if (j >= size) { i++; j = 0; }
                }
                j = 0;

                OrderedSet<Possible_Path> spp = new OrderedSet<Possible_Path>();
                Possible_Path first = new Possible_Path(Starting_Point);
                spp.Add(first);
                bool found = false;
                Possible_Path min = null;

                while (spp.Count != 0)
                {
                    min = spp.RemoveFirst();

                    for(j=0;j<size;j++)
                    {
                        matrix[j,min.ll.Last()] = -1 ;
                    }

                    // if min.ll.last is destination, min is the answere!
                    foreach (int dest in Destination)
                        if (min.ll.Last() == dest)
                        {
                            found = true;
                            break;
                        }

                    if (found) break;

                    for (i = 0; i < size; i++)
                    {
                        if (matrix[min.ll.Last(), i] > 0)
                        {
                            spp.Add(new Possible_Path(min, matrix[min.ll.Last(), i], i));
                        }
                    }
                }

                if (found)
                {
                    Destination_box.Text = min.ToString();
                }
                else
                {
                    Destination_box.Text = "No factories near you :(((";
                }

            }
            else
                Destination_box.Text = "Please enter all required inputs first";
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

        private void Submit_Address(object sender, RoutedEventArgs e)
        {
            Starting_Point = Convert.ToInt32(Starter.Text);
            Address_box.IsChecked = true;
        }

        private void Submit_Destinations(object sender, RoutedEventArgs e)
        {
            string tmp = "";

            Destination = new List<int>();

            foreach (char c in Destination_box.Text)
            {
                try
                {
                    Convert.ToInt32(Convert.ToString(c));
                    tmp += c;
                }
                catch
                {
                    if (tmp != "")
                    {
                        Destination.Add(Convert.ToInt32(Convert.ToString(tmp)));
                        tmp = "";
                    }
                }
            }

            if (tmp != "")
                Destination.Add(Convert.ToInt32(Convert.ToString(tmp)));
            Dest_box.IsChecked = true ;

        }

        private void Reset(object sender, RoutedEventArgs e)
        {
            MainWindow m = new MainWindow();
            m.Show();
            this.Close();
        }
    }
}
