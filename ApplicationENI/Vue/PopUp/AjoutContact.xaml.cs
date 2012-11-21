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
using ApplicationENI.Controleur;
using ApplicationENI.Modele;

namespace ApplicationENI.Vue.PopUp
{
    /// <summary>
    /// Logique d'interaction pour AjoutContact.xaml
    /// </summary>
  
    public partial class AjoutContact : Window
    {
        public static int test;
        CtrlProfilAlertesStagiaire ctrl = new CtrlProfilAlertesStagiaire();
        public AjoutContact()
        {
            InitializeComponent();
            
            cboListeEntreprises.ItemsSource = ctrl.listeEntreprises();
            cboFonctions.ItemsSource = ctrl.listeFonctions();
        }

        private void cboListeEntreprises_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnAjouterEntreprise_Click(object sender, RoutedEventArgs e)
        {
            AjoutEntreprise formEntreprise = new AjoutEntreprise();
            formEntreprise.ShowDialog();
            cboListeEntreprises.ItemsSource = ctrl.listeEntreprises();
        }

        private void btnEnregistrerContact_Click(object sender, RoutedEventArgs e)
        {
            Contact con = new Contact();
            con._nom = txtNom.Text;
            con._prenom = txtPrenom.Text;
            con._telFixe = txtTel.Text;
            con._telMobile = txtPortable.Text;
            con._email = txtMail.Text;
            con._codeFonction = ((Fonction)cboFonctions.SelectedItem)._codeFonction;
        }
    }
}
