using System;
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
using System.Windows.Shapes;
using System.IO;

namespace ApplicationENI.Vue
{
    /// <summary>
    /// Logique d'interaction pour SaisieResultats.xaml
    /// </summary>
    public partial class SaisieResultats : UserControl
    {
        public SaisieResultats()
        {
            InitializeComponent();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            StreamReader fileReader = new StreamReader(System.IO.Path.Combine(Environment.CurrentDirectory, @"..\..\ressources\competences.txt"));
            String stringReader = "";

            while (!(fileReader.EndOfStream))
            {
                stringReader = fileReader.ReadLine();
                listBox1.Items.Add(stringReader);
            }
            fileReader.Close();

            fileReader = new StreamReader(System.IO.Path.Combine(Environment.CurrentDirectory, @"..\..\ressources\stagiaires.txt"));
            stringReader = "";

            while (!(fileReader.EndOfStream))
            {
                stringReader = fileReader.ReadLine();
                listBox2.Items.Add(stringReader);
            }
            fileReader.Close();
        }

        private void image1_ImageFailed(object sender, RoutedEventArgs e)
        {
            //?
        }


    }
}
