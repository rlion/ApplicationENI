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
            BitmapImage img;

            //Informations sur le stagiaire
            txtNom.Text = stg._nom;
            txtAddr.Text = stg._adresse1;
            txtPrenom.Text = stg._prenom;
            txtFixe.Text = stg._telephoneFixe;
            txtMail.Text = stg._email;
            txtPortable.Text = stg._telephonePortable;
            txtRep.Text = stg._repertoire;
            txtDateNaiss.Text = stg._dateNaissance.ToString();
            try
            {
                img = new BitmapImage(new Uri(stg._photo));
                imageStagiaire.Source = img;
            }
            catch (Exception)
            {
                img = new BitmapImage(new Uri("pack://application:,,,/ApplicationENI;component/Images/portrait-vide.jpg"));
                imageStagiaire.Source = img;
            }
            


            //informations sur le tuteur du stagiaire
            dataGridListContacts.ItemsSource = stg.getListeContacts();
            this.listViewAlerte.ItemsSource = ctrlStagiaires.listeAlertes();
            this.listViewAlerte.Items.Refresh();
        }

        private void listViewAlerte_Initialized(object sender, EventArgs e)
        {
            
        }
        private void listViewAlerte_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Grid test = (Grid)this.Parent;
            DockPanel testDP = (DockPanel)test.Parent;
            Grid testG = (Grid)testDP.Parent;
            ((MainWindow)testG.Parent).MainGrid.Children.RemoveAt(0);
            ((MainWindow)testG.Parent).MainGrid.Children.Add(new Vue.HistoriqueAbsencesRetards());
            ((MainWindow)testG.Parent).tviHistorique.IsSelected = true;
        }

        private void imageStagiaire_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {

        }

        


        

    }
}
