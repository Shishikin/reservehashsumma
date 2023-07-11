﻿using Microsoft.Win32;
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
                MessageBox.Show(BitConverter.ToString(md5));                                                                              
                //var md5 = MD5.Create().ComputeHash(File.ReadAllBytes(search));//(File.ReadAllBytes(dialog.FileName));
                                                                              
                //MessageBox.Show(BitConverter.ToString(md5));
            }
            int numberfiles = 4+1;
            string[] searchfiles = new string[numberfiles+1];
            int numberTask = 4;
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
            Task[] task = new Task[numberTask+1];
            MessageBox.Show(numberfiles.ToString());

            for (int i = 0; i < numberfiles-1; i++)
            {
                //               MessageBox.Show(i.ToString());
                task[i] = new Task(() => Hashsumma(searchfiles[i]));
                task[i].Start();
                //               MessageBox.Show(i.ToString());                
            }
        }
    }
}