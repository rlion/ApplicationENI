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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace ApplicationENI.Vue
{
    /// <summary>
    /// Logique d'interaction pour GestionECF.xaml
    /// </summary>
    public partial class GestionECF : UserControl
    {
        public GestionECF()
        {
            InitializeComponent();
        }
        
        private void radioButton1_Checked(object sender, RoutedEventArgs e)
        {
            //?
        }

        private void slVersion_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (this.lbNbVersions!=null)
            {
                this.lbNbVersions.Content = slVersion.Value;
            }    
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


        }

        private void image1_ImageFailed(object sender, RoutedEventArgs e)
        {
            //?
        }

    
    }
}
