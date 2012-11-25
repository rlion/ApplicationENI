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
            if (txtNom.Text != "" && cboFonctions.SelectedItem != null && cboListeEntreprises.SelectedItem != null)
            {
                if (txtPortable.Text != "" || txtTel.Text != "")
                {
                    Contact con = new Contact();
                    con._nom = txtNom.Text;
                    con._prenom = txtPrenom.Text;
                    con._telFixe = txtTel.Text;
                    con._telMobile = txtPortable.Text;
                    con._email = txtMail.Text;
                    con._fax = txtFax.Text;
                    con._codeFonction = ((Fonction)cboFonctions.SelectedItem)._codeFonction;
                    con._Entreprise = ((Entreprise)cboListeEntreprises.SelectedItem);
                    DAL.ContactDAL.ajouterContact(con);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Veuillez saisir au moins un numéro de téléphone", "Saisie d'un numéro de téléphone obligatoire", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else {
                MessageBox.Show("Veuillez saisir au minimum un nom, une fonction et une entreprise SVP.", "Erreur de saisie", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
