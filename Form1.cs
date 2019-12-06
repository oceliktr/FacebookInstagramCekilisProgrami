using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public struct LinkItem
        {
            public string Href;
            public string Text;

            public override string ToString()
            {
                return Href + "\n\t" + Text;
            }
        }
        static class LinkFinder
        {
            public static List<LinkItem> Find(string file,string faceboox,string instagram)
            {
                List<LinkItem> list = new List<LinkItem>();

                // 1.
                // Find all matches in file.
                MatchCollection m1 = Regex.Matches(file, @"(<a.*?>.*?</a>)", RegexOptions.Singleline);

                // 2.
                // Loop over each match.
                foreach (Match m in m1)
                {
                    string value = m.Groups[1].Value;
                    //                  instagram     facebook  isim etiketindeki tag bilgisi
                    if (value.Contains(instagram) || value.Contains(faceboox))  //profileLink
                    {
                        LinkItem i = new LinkItem();

                        // 3.
                        // Get href attribute.
                        Match m2 = Regex.Match(value, @"href=\""(.*?)\""", RegexOptions.Singleline);
                        if (m2.Success)
                        {
                            i.Href = m2.Groups[1].Value;
                        }

                        // 4.
                        // Remove inner tags from text.
                        string t = Regex.Replace(value, @"\s*<.*?>\s*", "", RegexOptions.Singleline);
                        i.Text = t;

                        list.Add(i);
                    }
                }
                return list;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string metin = richTextBox1.Text;
            listBox1.Items.Clear();
            txtSonuclar.Text = "";
            // 2.
            foreach (LinkItem i in LinkFinder.Find(metin,txtFacebook.Text,txtInstagram.Text))
            {
                if (i.Text != "Fan Klüb" && i.Text != "Erzurum" && i.Text != "Erzurum'u Keşfe Çıktık!" &&
                    i.Text != "Osman Çelik" && i.Text != "Said Erzene" && i.Text != "erzurumportali"&&i.Text!="erzurumportakali")
                {
                    listBox1.Items.Add(i.Text);
                }
            }
            string[] arr = new string[listBox1.Items.Count];
            listBox1.Items.CopyTo(arr, 0);

            var arr2 = arr.Distinct();

            listBox1.Items.Clear();
            foreach (string s in arr2)
            {
                listBox1.Items.Add(s); 
          //      txtSonuclar.Text +=  s + Environment.NewLine;
            }

            label1.Text = "Çekilişe Katılan Kişi Sayısı :" + listBox1.Items.Count;
            txtSonuclar.Text += "--------ASILLAR---------" + Environment.NewLine;
            txtSonuclar.Text += Environment.NewLine;

            for (int j = 1; j <= ndAsil.Value; j++)
            {
              Random rnd = new Random();
                int i = rnd.Next(listBox1.Items.Count);
                
                txtSonuclar.Text += j + " - " + listBox1.Items[i] + Environment.NewLine;
                listBox1.Items.Remove(listBox1.Items[i]);
            }
            txtSonuclar.Text += Environment.NewLine;
            txtSonuclar.Text += "--------YEDEKLER---------" + Environment.NewLine;
            txtSonuclar.Text += Environment.NewLine;

            for (int y = 1; y <= ndYedek.Value; y++)
            {
                Random rnd = new Random();
                int i = rnd.Next(listBox1.Items.Count);

                txtSonuclar.Text += y + " - " + listBox1.Items[i] + Environment.NewLine;
                listBox1.Items.Remove(listBox1.Items[i]);
            }
            //Clipboard.SetText(txtSonuclar.Text);

        }

        private void kopyalaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSonuclar.Text))
                Clipboard.SetText(txtSonuclar.Text);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label1.Text = "Çekilişe Katılan Kişi Sayısı :" + listBox1.Items.Count;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtSonuclar.Text = "";
            label1.Text = "Çekilişe Katılan Kişi Sayısı :" + listBox1.Items.Count;
            txtSonuclar.Text += "--------ASILLAR---------" + Environment.NewLine;
            txtSonuclar.Text += Environment.NewLine;

                Random rnd = new Random();
            for (int j = 1; j <= ndAsil.Value; j++)
            {
                int i = rnd.Next(0,listBox1.Items.Count);
          
                txtSonuclar.Text += j + " - "+ listBox1.Items[i] + Environment.NewLine;
                listBox1.Items.Remove(listBox1.Items[i]);
            }
            txtSonuclar.Text += Environment.NewLine;
            txtSonuclar.Text += "--------YEDEKLER---------" + Environment.NewLine;
            txtSonuclar.Text += Environment.NewLine;

            for (int y = 1; y <= ndYedek.Value; y++)
            {
                int i = rnd.Next(0,listBox1.Items.Count);

                txtSonuclar.Text += y + " - " + listBox1.Items[i] + Environment.NewLine;
                listBox1.Items.Remove(listBox1.Items[i]);
            }
         //   Clipboard.SetText(txtSonuclar.Text);
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
