﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace ApplicationENI.Vue
{
    /// <summary>
    /// Logique d'interaction pour ModificationDates.xaml
    /// </summary>
    public partial class ModificationDates : UserControl
    {
        public ModificationDates()
        {
            InitializeComponent();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            StreamReader fileReader = new StreamReader(System.IO.Path.Combine(Environment.CurrentDirectory, @"..\..\ressources\dates.txt"));
            String stringReader = "";

            while (!(fileReader.EndOfStream))
            {
                stringReader = fileReader.ReadLine();
                listBox1.Items.Add(stringReader);
            }
            fileReader.Close();
        }

        private void image1_ImageFailed(object sender, RoutedEventArgs e)
        {
            //?
        }
    }
}
