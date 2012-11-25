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
using ApplicationENI.Vue.PopUp;

namespace ApplicationENI.Vue
{
    /// <summary>
    /// Logique d'interaction pour ProfilAlertesStagiaire.xaml
    /// </summary>
    public partial class ProfilAlertesStagiaire : UserControl
    {
        CtrlProfilAlertesStagiaire ctrlStagiaires = new CtrlProfilAlertesStagiaire();
        Stagiaire stg = Parametres.Instance.stagiaire;
        public ProfilAlertesStagiaire()
        {
            InitializeComponent();
            
            
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

        private void listViewAlerte_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // filtrage sur le type d'évènement pour cerner l'action à appliquer
            if (((ItemAlerte)listViewAlerte.SelectedItem).TYPE.ToString() == "Absences") 
            {
                Grid test = (Grid)this.Parent;
                DockPanel testDP = (DockPanel)test.Parent;
                Grid testG = (Grid)testDP.Parent;
                ((MainWindow)testG.Parent).MainGrid.Children.RemoveAt(0);
                ((MainWindow)testG.Parent).MainGrid.Children.Add(new Vue.HistoriqueAbsencesRetards());
                ((MainWindow)testG.Parent).tviHistorique.IsSelected = true;
            }
        }


        private void dataGridListContacts_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit && dataGridListContacts.SelectedItem != null) {
                if (((Contact)dataGridListContacts.SelectedItem)._telFixe != "" && ((Contact)dataGridListContacts.SelectedItem)._telMobile != "")
                {
                    CtrlProfilAlertesStagiaire ctrlStagiaires = new CtrlProfilAlertesStagiaire();
                    ctrlStagiaires.modifierContact((Contact)dataGridListContacts.SelectedItem);
                }
                else {
                    MessageBox.Show("Veuillez entrer au moins un numéro de téléphone", "Erreur de saisie", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                
            }
        }

        private void dataGridListContacts_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete && dataGridListContacts.SelectedItem!=null)
            {
                if (MessageBox.Show("Etes-vous CERTAIN de vouloir supprimer ce contact ?", "Confirmation avant suppression", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    bool retour = DAL.ContactDAL.supprimerContact(    
                                                    ((Contact) dataGridListContacts.SelectedItem)._codeContact
                                                    );
                    if (retour == true)
                    {
                        MessageBox.Show("Suppression effectuée", "Suppression effectuée", MessageBoxButton.OK, MessageBoxImage.Information);
                        CtrlProfilAlertesStagiaire ctrlStagiaires = new CtrlProfilAlertesStagiaire();
                        dataGridListContacts.ItemsSource = Parametres.Instance.stagiaire.getListeContacts();
                        dataGridListContacts.Items.Refresh();
                    } 
                    
                    
                }
            }
        }

        private void btnAjouterContacts_Click(object sender, RoutedEventArgs e)
        {
            AjoutContact formAjoutContact = new AjoutContact();
            formAjoutContact.ShowDialog();
            dataGridListContacts.ItemsSource = stg.getListeContacts();
        }



        


        

    }
}
