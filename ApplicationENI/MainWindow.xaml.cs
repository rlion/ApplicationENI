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
using System.Windows.Media.Effects;
using ApplicationENI.Modele;

namespace ApplicationENI
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //initialisation des paramètres
            Parametres.Instance.login = "jgabillaud";
            Parametres.Instance.password = "";
            Contact tuteur = new Contact("Jones", "Indiana", "0202020202", "0602020202", "0202020202", "indianajones@gmail.com", "il est sympa", "", "Melle");
            Parametres.Instance.stagiaire = new Stagiaire("Mr.", "Denis", "Choniphroa", "36 rue des papillons", "", "", "35000", "Pancé", "0606060606", "0206060606", "toto@toto.fr", DateTime.Now, "", "", "", DateTime.Now, DateTime.Now, "/test/rep", true, "c:/testPhotos/1.jpg", true, "", tuteur);
            Parametres.Instance.listAlertes = new List<ItemAlerte>();
            Parametres.Instance.listAlertes.Add(new ItemAlerte(0, "Alerte", "Examen le 23 juin."));
            Parametres.Instance.listAlertes.Add(new ItemAlerte(3, "Examen", "ECF n° 3 non corrigé."));


            BlurEffect myBlurEffect = new BlurEffect(); 
            myBlurEffect.Radius = 8;
            this.Effect = myBlurEffect;

            this.MainGrid.Children.Add(new Vue.AccueilGeneral());
            this.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            this.Show();
            
            Vue.Login login = new Vue.Login();
            login.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            login.ShowDialog();

            this.Effect = null;
        }

        private void expandStagiaire_Collapsed(object sender, RoutedEventArgs e)
        {
            this.expandStagiaire.Height = 20;
        }

        private void expandStagiaire_Expanded(object sender, RoutedEventArgs e)
        {
            this.expandStagiaire.Height = 100;
        }

        private void expandMenu_Expanded(object sender, RoutedEventArgs e)
        {
            this.expandMenu.Width = 240;
        }

        private void expandMenu_Collapsed(object sender, RoutedEventArgs e)
        {
            this.expandMenu.Width = 25;
        }

        private void btAccueil_Click(object sender, RoutedEventArgs e)
        {
            this.expandStagiaire.IsExpanded = true;
            this.tviGestionECF.IsSelected = false;
            this.MainGrid.Children.RemoveAt(0);
            this.MainGrid.Children.Add(new Vue.AccueilGeneral());
        }

        private void tviGestionECF_Selected(object sender, RoutedEventArgs e)
        {
            this.expandStagiaire.IsExpanded = false;
            this.MainGrid.Children.RemoveAt(0);
            this.MainGrid.Children.Add(new Vue.GestionECF());
        }

        private void tviGestionTitre_Selected(object sender, RoutedEventArgs e)
        {
            this.expandStagiaire.IsExpanded = true;
            this.expandStagiaire.IsExpanded = true; this.MainGrid.Children.RemoveAt(0);
            this.MainGrid.Children.Add(new Vue.InscriptionTitre());
        }

        private void tviExporter_Selected(object sender, RoutedEventArgs e)
        {
            this.expandStagiaire.IsExpanded = true; 
            this.MainGrid.Children.RemoveAt(0);
            this.MainGrid.Children.Add(new Vue.ExportDocuments());
        }

        private void tviHistorique_Selected(object sender, RoutedEventArgs e)
        {
            this.expandStagiaire.IsExpanded = true; 
            this.MainGrid.Children.RemoveAt(0);
            this.MainGrid.Children.Add(new Vue.HistoriqueAbsencesRetards());
        }

        private void tviNouvAbsence_Selected(object sender, RoutedEventArgs e)
        {
            this.expandStagiaire.IsExpanded = true; 
            this.MainGrid.Children.RemoveAt(0);
            this.MainGrid.Children.Add(new Vue.NouvelleAbsence());
        }

        private void tviSuiviAcquis_Selected(object sender, RoutedEventArgs e)
        {
            this.expandStagiaire.IsExpanded = true; 
            this.MainGrid.Children.RemoveAt(0);
            this.MainGrid.Children.Add(new Vue.SuiviAcquis());
        }

        private void tviRemarques_Selected(object sender, RoutedEventArgs e)
        {
            this.expandStagiaire.IsExpanded = true; 
            this.MainGrid.Children.RemoveAt(0);
            this.MainGrid.Children.Add(new Vue.Observations());
        }

        private void tviGestionResultat_Selected(object sender, RoutedEventArgs e)
        {
            this.expandStagiaire.IsExpanded = true; 
            this.MainGrid.Children.RemoveAt(0);
            this.MainGrid.Children.Add(new Vue.GestionResultats());
        }

        private void tviModifDate_Selected(object sender, RoutedEventArgs e)
        {
            this.expandStagiaire.IsExpanded = true; 
            this.MainGrid.Children.RemoveAt(0);
            this.MainGrid.Children.Add(new Vue.ModificationDates());
        }

        private void tviSaisieResultat_Selected(object sender, RoutedEventArgs e)
        {
            this.expandStagiaire.IsExpanded = false; 
            this.MainGrid.Children.RemoveAt(0);
            this.MainGrid.Children.Add(new Vue.SaisieResultats());
        }

        private void tviTrombinoscope_Selected(object sender, RoutedEventArgs e)
        {
            this.expandStagiaire.IsExpanded = false; 
            this.MainGrid.Children.RemoveAt(0);
            this.MainGrid.Children.Add(new Vue.Trombinoscope());
        }

        private void tviProfil_Selected(object sender, RoutedEventArgs e)
        {
            this.expandStagiaire.IsExpanded = true; 
            this.MainGrid.Children.RemoveAt(0);
            this.MainGrid.Children.Add(new Vue.ProfilAlertesStagiaire());
        }

        private void tviGestionPassages_Selected(object sender, RoutedEventArgs e)
        {
            this.expandStagiaire.IsExpanded = false; 
            this.MainGrid.Children.RemoveAt(0);
            this.MainGrid.Children.Add(new Vue.GestionPassageTitre());
        }

        private void tviPlanning_Selected(object sender, RoutedEventArgs e) {
            this.expandStagiaire.IsExpanded = true;
            this.MainGrid.Children.RemoveAt(0);
            this.MainGrid.Children.Add(new Vue.Planning());

        }

    }
}
