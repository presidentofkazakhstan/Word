
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace pool_HomeWork
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string _url;
       

       
        public MainWindow()
        {
            InitializeComponent();

            Random rand = new Random();

            int temp = rand.Next(0000000, 9999999);

            _url = $"File_{temp}.txt";

        }

        private void SaveFile(object path)
        {
            try
            {
                string text = new TextRange(RichTextBox.Document.ContentStart, RichTextBox.Document.ContentEnd).Text;

                if (text.Length <= 0)
                {
                    if ((MessageBox.Show("Файл пустой! Сохранить файл?", "", MessageBoxButton.YesNo)) == MessageBoxResult.Yes)
                    {
                        File.WriteAllText(_url, text);

                    }
                    else
                    {
                        return;
                    }
                }

                File.WriteAllText(_url, text);
            }
            catch (Exception exception)
            {
                MessageBox.Show("Ошибка записи" + exception.Message);
            }
        }

        private void CreateClick(object sender, RoutedEventArgs e)
        {
            if ((MessageBox.Show("Сохранить файл?","" ,MessageBoxButton.YesNo)) == MessageBoxResult.Yes)
            {
                SaveFile(_url);

            }
            else
            {
                return;
            }
            RichTextBox.Document.Blocks.Clear();
            Process.Start(_url);
        }

        private void OpenClick(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog openDialog = new OpenFileDialog();

                bool? result = openDialog.ShowDialog();

                if (result == true)
                {
                    _url = openDialog.FileName;
                }

                RichTextBox.Document.Blocks.Clear();

                

                Process.Start(_url);

                using (StreamReader reader = new StreamReader(_url))
                {
                    RichTextBox.Document.Blocks.Add(new Paragraph(new Run(reader.ReadToEnd())));
                }

            }
            catch (Exception exception)
            {
                MessageBox.Show("Ошибка " + exception.Message);
            }
        }


        private void PrintClick(object sender, RoutedEventArgs e)
        {
            
            new PrintDialog().ShowDialog();
        }

        private void SaveClick(object sender, RoutedEventArgs e)
        {
            SaveFile(_url);
            Process.Start(_url);
        }
    }
}
