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
using MahApps.Metro.Controls;

namespace Hashsumma3
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }        

        /// функция для подсчёта хэшсуммы файла        
        async Task<byte[]> HashsumAsync(string file)
        {
            const int BUFFER_SIZE = 16 * 1024 * 1024;
            using (MD5 md5 = MD5.Create())
            {
                using (FileStream fs = new FileStream
                    (
                    file, FileMode.Open, FileAccess.Read,
                    FileShare.Read, BUFFER_SIZE, FileOptions.Asynchronous
                    )
                    )
                {
                    byte[] buffer = new byte[BUFFER_SIZE];

                    int shift;
                    while ((shift = await fs.ReadAsync(buffer, 0, buffer.Length)) != 0)
                    {
                        md5.TransformBlock(buffer, 0, shift, buffer, 0);
                    }
                    md5.TransformFinalBlock(buffer, 0, shift);

                    return md5.Hash;
                }
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            //число файлов
            int numberfiles = (int) numberfile.Value;
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
            for (var i = 0; i < task.Length; i++)
            {
                var k = searchfiles[i];
                Task.Run(async () =>
                {
                    byte[] hash;
                    MessageBox.Show(k + '\n' + BitConverter.ToString(await HashsumAsync(k)));
                }
                );
            }            
        }

        //HorizontalAlignment="Center" Height="55" Margin="0,263,0,0" VerticalAlignment="Top" Width="120"
    }
}
