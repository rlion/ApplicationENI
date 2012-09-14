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
using ApplicationENI.Controleur;
using ApplicationENI.Modele;

namespace ApplicationENI.Vue
{
    /// <summary>
    /// Logique d'interaction pour Trombinoscope.xaml
    /// </summary>
    public partial class Trombinoscope : UserControl
    {
        public Trombinoscope()
        {
            InitializeComponent();
            CtrlTrombinoscope ctrlStagiaire = new CtrlTrombinoscope();
            List<Stagiaire> listStagiaire = ctrlStagiaire.listeStagiaires();
            Grid tableauImages = new Grid();
            int i = 0;
            int j = 0;
            foreach (Stagiaire s in listStagiaire)
            {
                if(i - 4 == 0) {
                    i = 0;
                    j+=2;
                } 
                
                gridTrombi.ColumnDefinitions.Add(new ColumnDefinition());
                gridTrombi.RowDefinitions.Add(new RowDefinition());
                gridTrombi.Width = 1000;
                //gridTrombi.Height = 1000;

                Image image = new Image();
                image.BeginInit();

                TextBox txt = new TextBox();
                BitmapImage img = new BitmapImage(new Uri(s._photo));
                image.Source = img;
                image.Width = 100;
                image.Height = 120;
                image.Stretch = Stretch.Uniform;
                
                TextBox txtBoxTest = new TextBox();
                txtBoxTest.Background = Brushes.AliceBlue;
                txtBoxTest.TextAlignment = TextAlignment.Center;
                txtBoxTest.BorderThickness = new Thickness(0);

                image.SetValue(Grid.ColumnProperty, i);
                image.SetValue(Grid.RowProperty, j);
                txtBoxTest.Text = s._prenom + " " + s._nom;
                txtBoxTest.SetValue(Grid.ColumnProperty, i);
                txtBoxTest.SetValue(Grid.RowProperty, j+1);
                //image.SetValue(Panel.ZIndexProperty, 1);
                gridTrombi.Children.Add(image);
                gridTrombi.Children.Add(txtBoxTest);

                i += 1;
                //gridTrombi.Children.Add
                //MessageBox.Show(s._photo);
            }
        }
    }
}
