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
            txtEntrepriseTuteur.Text = stg._tuteur._nomEntreprise;
            txtMailTuteur.Text = stg._tuteur._email;
            txtNomTuteur.Text = stg._tuteur._nom;
            txtPortableTuteur.Text = stg._tuteur._telMobile;
            txtPrénomTuteur.Text = stg._tuteur._prenom;
            txtTelTuteur.Text = stg._tuteur._telFixe;

            //TODO: mettre un datagrid pour la liste des alertes ?
            //TODO: s'il y a vraiment beaucoup d'alarmes, on affiche un message du genre "il y a 5 alarmes de type ECF" -> on pourrait cliquer dessus ensuite pour le détail?
            this.listViewAlerte.ItemsSource = ctrlStagiaires.listeAlertes();
            this.listViewAlerte.Items.Refresh();
            //TODO: on pourrait aussi mettre une listbox sous le datagrid pour afficher les alertes pour une catégorie
        }

        private void listViewAlerte_Initialized(object sender, EventArgs e)
        {
            
        }
        private void listViewAlerte_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void imageStagiaire_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {

        }
    }
}
