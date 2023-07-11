using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
using System.Threading;
using System.Reflection;

namespace Hashsumma3
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            void Hashsumma(string search)
            {
                var md5 = MD5.Create().ComputeHash(File.ReadAllBytes(search));                                                                              
                MessageBox.Show(search + '\n' + BitConverter.ToString(md5));                                                                              
            }
            //число файлов
            int numberfiles = 4;
            // массив путей к каждому файлу
            string[] searchfiles = new string[numberfiles];
            //число потоков равно числу файлов
            int numberTask = numberfiles;
            //заполнение массива путей к файлам
            {
                int counter = 0;
                while (counter < numberfiles)
                {
                    var dialog = new OpenFileDialog();
                    if (dialog.ShowDialog() == true)
                    {
                        searchfiles[counter] = dialog.FileName;
                    }
                    counter++;
                }
            }
            //создание массива потоков
            Task[] task = new Task[numberTask];
//                                                   MessageBox.Show(numberfiles.ToString());            
            for (var i = 0; i < task.Length; i++)
            {
                var k = searchfiles[i];
                task[i] = new Task(() =>
                {
                    Hashsumma(k);
                });
            }
 //                                                   MessageBox.Show("hello");
            //запуск потоков
            for (var i = 0; i < task.Length; i++)
            {
                task[i].Start();
            }
            //           MessageBox.Show("good");
        }
    }
}
