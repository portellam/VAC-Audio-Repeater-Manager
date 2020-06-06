﻿using NAudio.CoreAudioApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace VACARM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string CurrentDirectoryPath
        {
            get
            {
                return Environment.CurrentDirectory;
            }
        }


        public MainWindow()
        {
            InitializeComponent();
            StartEngine();
        }

        private void StartEngine()
        {
            
        }

        private void toolBarSelect_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
