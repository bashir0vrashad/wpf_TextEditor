using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.IO;
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
using Microsoft.Win32;
namespace WpfApp1_TextEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private string copy="";
        private string cut="";
        private bool yoxla = false;
        private int say = 0;

        private void open_click(object sender, RoutedEventArgs e)
        {
           OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Metin Dosyalar (*.txt)|*.txt";
            dialog.Title = "Bir dosya seçin";

            if (dialog.ShowDialog() == true)
            {
                string file = dialog.FileName;
                try
                {
                    string metn = File.ReadAllText(file);
                    file_name.Content = file;
                    text.Text = metn;            
                }
                catch (Exception ex)
                {
                    MessageBox.Show("File Acila Bilmedi: " + ex.Message);
                }
            }

        }

        private void all_click(object sender, RoutedEventArgs e)
        {
            int baslangicIndex = 0; 
            int secilenUzunluk = text.Text.Length;
          
            text.Select(baslangicIndex, secilenUzunluk);
            text.Focus();


        }

        
   
        private void paste_click(object sender, RoutedEventArgs e)
        {
            if (say == 1)
            {
                int cari1 = text.CaretIndex;
                string a = "";
                string b = "";
                for (int i = 0; i < cari1; i++)
                {
                    a += text.Text[i];
                }
                for (int i = cari1; i < text.Text.Length; i++)
                {
                    b += text.Text[i];
                }
                text.Text = a + cut + b;
                say = 0;
            }
            else if(say ==0 && copy.Length!=0)
            {
                int cari = text.CaretIndex;
                string a = "";
                string b = "";
                for (int i = 0; i < cari; i++)
                {
                    a += text.Text[i];
                }
                for (int i = cari; i < text.Text.Length; i++)
                {
                    b += text.Text[i];
                }
                text.Text = a + copy + b;
            }
            
        }

        private void copy_click(object sender, RoutedEventArgs e)
        {
            int bas = text.SelectionStart;
            int son = text.SelectionLength;

            if (bas > 0)
            {
               
                copy = text.Text.Substring(bas, son);
               
            }
            if (say == 1) say = 0;
        }

        private void cut_click(object sender, RoutedEventArgs e)
        {          
            string a = "";           
            int bas = text.SelectionStart;
            int son = text.SelectionLength;

            if (bas > 0)
            {
                
                cut = text.Text.Substring(bas, son);
                text.Text = text.Text.Remove(bas, son);
                say = 1;
               
            }
         
        }

        private void change(object sender, TextChangedEventArgs e)
        {
            if (auto.IsChecked == true) { File.WriteAllText(file_name.Content.ToString(), text.Text); }
        }

        private void save_click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            if (save.ShowDialog() == true)
            {
                string file = save.FileName;
                try
                {
                    File.WriteAllText(file, text.Text);
                    text.Text = "";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("File Tapilmadi: " + ex.Message);
                }
            }
        }
    }
}
