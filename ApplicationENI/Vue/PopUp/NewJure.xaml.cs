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
using ApplicationENI.Modele;

namespace ApplicationENI.Vue.PopUp {
    /// <summary>
    /// Logique d'interaction pour NewJure.xaml
    /// </summary>
    public partial class NewJure : Window 
    {

        public NewJure() 
        {
            InitializeComponent();

            btValider.IsEnabled = false;
        }

        private void btAnnuler_Click(object sender, RoutedEventArgs e) 
        {
            this.Close();
        }

        private void btValider_Click(object sender, RoutedEventArgs e) 
        {
            try
            {
                Jury jury = new Jury(999, txtCivilite.Text, txtNom.Text, txtPrenom.Text);
                DAL.TitresDAL.AjouterJury(jury);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Erreur Création Juré", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void txtCivilite_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckIfAllIsFilled();
        }

        private void txtNom_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckIfAllIsFilled();
        }

        private void txtPrenom_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckIfAllIsFilled();
        }

        private void CheckIfAllIsFilled()
        {
            if(this.IsInitialized)
            {
                if(!string.IsNullOrEmpty(txtCivilite.Text) && !string.IsNullOrEmpty(txtNom.Text) && !string.IsNullOrEmpty(txtPrenom.Text))
                    btValider.IsEnabled = true;
                else btValider.IsEnabled = false;
            }
        }
    }
}
