public partial class MainWindow : Window
    {
        Dictionary<draw.Bitmap, string[,]> carpets_list = new Dictionary<draw.Bitmap, string[,]>();
        public MainWindow()
        {
            InitializeComponent();
        }
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            draw.Bitmap pic;
            string[,] hexacode = new string[300, 400]; 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.Filter = "image file (*.jpg; *.png; *.jpeg) | *.jpg; *.png; *.jpeg;";
            dlg.InitialDirectory = @"Desktop";

            pic = new draw.Bitmap(dlg.FileName);
                
            for (int i = 0; i < 300; i++)
            {
                for (int j = 0; j < 400; j++)
                {
                    draw.Color clr = pic.GetPixel(i, j);
                    hexacode[i, j] = System.Drawing.ColorTranslator.ToHtml(clr).Remove(0, 1);
                }
            }
            carpets_list.Add(pic, hexacode);
            
        }
    }
