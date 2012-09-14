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
using ApplicationENI.Modele;
using ApplicationENI.Controleur;

namespace ApplicationENI.Vue
{
    /// <summary>
    /// Logique d'interaction pour ProfilAlertesStagiaire.xaml
    /// </summary>
    public partial class ProfilAlertesStagiaire : UserControl
    {
        public ProfilAlertesStagiaire()
        {
            InitializeComponent();
        }

        private void listViewAlerte_Initialized(object sender, EventArgs e)
        {
            CtrlProfilAlertesStagiaire ctrlStagiaires = new CtrlProfilAlertesStagiaire();
            Stagiaire stg = Parametres.Instance.stagiaire;

            //Informations sur le stagiaire
            txtNom.Text = stg._nom;
            txtAddr.Text = stg._adresse1;
            txtPrenom.Text = stg._prenom;
            //txtEntrepriseTuteur = ?
            txtFixe.Text = stg._telephoneFixe;
            txtMail.Text = stg._email;
            //txtMailTuteur = ?
            //txtNomTuteur = ?
            txtPortable.Text = stg._telephonePortable;
            //txtPortableTuteur = ?
            //txtPrénomTuteur = ?
            txtRep.Text = stg._repertoire;
            //txtTelTuteur = ?
            txtDateNaiss.Text = stg._dateNaissance.ToString();
            
            //informations sur le tuteur du stagiaire
            //txtEntrepriseTuteur = ?
            txtEntrepriseTuteur.Text = "Boucherie Chesnard";
            txtMailTuteur.Text = stg._tuteur._email;
            txtNomTuteur.Text = stg._tuteur._nom;
            txtPortableTuteur.Text = stg._tuteur._telMobile;
            txtPrénomTuteur.Text = stg._tuteur._prenom;
            txtTelTuteur.Text = stg._tuteur._telFixe;

            //test connexion bdd
            // AVANT TOUTE CHOSE, ON EST BIEN D'ACCORD QUE C'EST HYPER CRADE D'APPELER LA DAL DANS LA VUE, MAIS POUR LES TESTS C'EST PRATIQUE
            //txtNom.Text = DAL.StagiairesDAL.getInfosStagiaire("toto")._nom;
            //Stagiaire stg1 = new Stagiaire("testAjout", "tata");
            //DAL.StagiairesDAL.ajouterStagiaire(stg1);
            //DAL.StagiairesDAL.supprimerStagiaire(stg1);
            //DAL.StagiairesDAL.modifierStagiaire(stg1);
            this.listViewAlerte.ItemsSource = Parametres.Instance.listAlertes;
        }
        private void listViewAlerte_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void imageStagiaire_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {

        }
    }
}
