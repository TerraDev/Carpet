using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp5
{
    public partial class Form1 : Form
    {
        List<int> power_buy = new List<int>();
        Dictionary<Bitmap, string> carpets_list = new Dictionary<Bitmap, string>();
        Dictionary<Bitmap, int> value_carpet_list = new Dictionary<Bitmap, int>();
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void store_carpets_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = true;
            ofd.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg";
            DialogResult dr = ofd.ShowDialog();
            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                foreach (var file in ofd.FileNames)
                {
                    Bitmap tempbitmap = new Bitmap(file);
                    string tempstr = "";
                    for (int i = 0; i < 30; i++)
                    {
                        for (int j = 0; j < 40; j++)
                        {
                            Color color = tempbitmap.GetPixel(i, j);
                            string a = System.Drawing.ColorTranslator.ToHtml(color);
                            tempstr = tempstr.Insert(tempstr.Length,a.Remove(0, 1));
                        }
                    }
                    carpets_list.Add(tempbitmap, tempstr);
                    value_carpet_list.Add(tempbitmap, rnd.Next(1, 20));
                }
            }

        }

        private void sequence_aligment_Click(object sender, EventArgs e)
        {
            Dictionary<Bitmap, int> dic = new Dictionary<Bitmap, int>();
            string pixel = "";
            Bitmap bm;
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg";
            DialogResult dr = ofd.ShowDialog();
            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                bm = new Bitmap(ofd.FileName);
                for (int i = 0; i < 30; i++)
                {
                    for (int j = 0; j < 40; j++)
                    {
                        Color color = bm.GetPixel(i, j);
                        string a = System.Drawing.ColorTranslator.ToHtml(color);
                        pixel = pixel.Insert(pixel.Length, a.Remove(0, 1));

                    }
                }
            }
            foreach (var item in carpets_list)
            {
                int i, j; // intialising variables  

                int m = pixel.Length; // length of gene1  
                int n = item.Value.Length; // length of gene2  

                // table for storing optimal substructure answers  
                int[,] dp = new int[n + m + 1, n + m + 1];
                for (int q = 0; q < n + m + 1; q++)
                    for (int w = 0; w < n + m + 1; w++)
                        dp[q, w] = 0;

                // intialising the table   
                for (i = 0; i <= (n + m); i++)
                {
                    dp[i, 0] = i * 2;
                    dp[0, i] = i * 2;
                }

                // calcuting the minimum penalty  
                for (i = 1; i <= m; i++)
                {
                    for (j = 1; j <= n; j++)
                    {
                        if (pixel[i - 1] == item.Value[j - 1])
                        {
                            dp[i, j] = dp[i - 1, j - 1];
                        }
                        else
                        {
                            dp[i, j] = Math.Min(Math.Min(dp[i - 1, j - 1] + 1,
                                            dp[i - 1, j] + 2),
                                            dp[i, j - 1] + 2);
                        }
                    }
                }
                dic.Add(item.Key, dp[m, n]);
            }
            var items = from pair in dic
                        orderby pair.Value ascending
                        select pair;
            int count = 0;
            foreach (var item in items)
            {
                if (count == 0)
                    pictureBox1.Image = item.Key;
                else if (count == 1)
                    pictureBox2.Image = item.Key;
                else if (count == 2)
                    pictureBox3.Image = item.Key;
                else
                    break;
                count++;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int money = int.Parse(textBox1.Text);
            int sum = 0;
            var items = from pair in value_carpet_list
                        orderby pair.Value ascending
                        select pair;
            foreach (var item in items)
            {
                if(sum + item.Value<=money)
                {
                    sum += item.Value;
                    power_buy.Add(item.Value);
                }
            }

            foreach (var item in power_buy)
            {
                listView1.Items.Add(item.ToString());
            }
        }
        
    }
}
