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

namespace ApplicationENI.Vue.PopUp
{
    /// <summary>
    /// Logique d'interaction pour AjoutEntreprise.xaml
    /// </summary>
    public partial class AjoutEntreprise : Window
    {
        public AjoutEntreprise()
        {
            InitializeComponent();
        }

        private void btnEnregistrer_Click(object sender, RoutedEventArgs e)
        {
            if (txtRaisonSociale.Text != "")
            {
                Controleur.CtrlProfilAlertesStagiaire ctrl = new Controleur.CtrlProfilAlertesStagiaire();
                ctrl.ajouterEntreprise(new Entreprise(txtRaisonSociale.Text, txtCP.Text, txtVille.Text, txtTel.Text, txtMail.Text));
                this.Close();
            }
            else {
                MessageBox.Show("Veuillez entrer une raison sociale.", "Elément manquant", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
