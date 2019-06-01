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

namespace Carpet
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Strassen S;
        Dictionary<string[,], BitmapImage> Carpets_List = new Dictionary<string[,], BitmapImage>();

        public MainWindow()
        {
            InitializeComponent();
        }

        bool clickD = true;
        private void Click_Dimensions(object sender, RoutedEventArgs e)
        {
            if(clickD)
            {
                int a = Convert.ToInt32( _1st_D.Text);
                Create_Matrix(a);
                DIM.Text = _1st_D.Text;

                S = new Strassen();
                S.size = a;
            }
            clickD = false;
            State = Click_U.source;
        }

        internal void Create_Matrix(int size)
        {
            for(int t=0;t<size;t++)
            {
                RowDefinition RD = new RowDefinition();
                RD.Height = new GridLength(30);
                Matrix.RowDefinitions.Add(RD);
                if(Matrix.RowDefinitions.Count() > 8)
                {
                    Matrix.Height += 30;
                }
            }

            for (int t = 0; t <size; t++)
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

            Helper_Write("Insert values of SOURCE MATRIX" +'\n' + "Click Refresh button to restart");
        }

        private void Click_Refresh(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }

        internal void Helper_Write(string S)
        {
            Helper.Text = S;
        }
    

        enum Click_U
        {
            off,
            source,
            filter
        }
        Click_U State;

        private void Click_Matrix(object sender, RoutedEventArgs e)
        {
            int i = 0, j = 0;
            int DS=S.size;
            int[,] mat= new int[DS, DS];

            foreach (TextBox box in Matrix.Children)
            {
                mat[i,j]=Convert.ToInt32(box.Text);
                j++;
                if (j >= DS) { i++; j = 0; }
            }

            if(State==Click_U.source)
            {
                S.Source = mat;
                foreach (TextBox box in Matrix.Children)
                {
                    box.Text = "0";
                    box.Background = Brushes.Yellow;
                }
                State = Click_U.filter;
                Helper_Write("Insert values of FILTER MATRIX" + '\n' + "Click Refresh button to restart");
            }

            else if(State==Click_U.filter)
            {
                S.Filter = mat;
                S.Calculate();
                State = Click_U.off;

                int k = 0, q = 0;
                foreach (TextBox box in Matrix.Children)
                {
                    box.Text = Convert.ToString(S.Result[k,q]);
                    box.Background = Brushes.Tomato;
                    q++;
                    if (q >= S.size) { q = 0; k++; }
                }
                Helper_Write("STRASSEN MULTIPLICATION!");
            }


        }

        private void Go_next(object sender, RoutedEventArgs e)
        {
            SecondWindow SW = new SecondWindow();
            SW.Show();
            this.Close();
        }

        BitmapImage btm;
        private void Choose_Pic(object sender, RoutedEventArgs e)
        {
            

            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension 
            //dlg.DefaultExt = ".txt";
            dlg.Filter = "ImageFile  (*.jpeg ; *.png; *.bmp) | *.jpg ; *.jpeg ; *.png; *.bmp";

            if (dlg.ShowDialog() == true)
            {
                // Open document 
                btm = new BitmapImage(new Uri(dlg.FileName));
                
            }

            im1.Source = btm;

            //Color c = btm.get;
                
        }
    }
}
