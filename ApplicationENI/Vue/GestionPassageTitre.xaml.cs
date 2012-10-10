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
    /// Logique d'interaction pour GestionPassageTitre.xaml
    /// </summary>
    public partial class GestionPassageTitre : UserControl
    {
        private CtrlGestionPassageTitre Controleur;
        private Titre titre;
        private EpreuveTitre epreuveTitre;

        public GestionPassageTitre()
        {
            InitializeComponent();

            Controleur = new CtrlGestionPassageTitre();

            this.cbChoixTitre.SelectedValuePath = "Key";
            this.cbChoixTitre.DisplayMemberPath = "Value";

            this.cbNiveau.SelectedValuePath = "Key";
            this.cbNiveau.DisplayMemberPath = "Value";

            InitData();
        }

        private void InitData()
        {
            this.groupBoxTitre.DataContext = null;
            this.cbChoixTitre.ItemsSource = null;
            this.cbChoixTitre.ItemsSource = Controleur.GetListeCodeTitre();
            this.cbNiveau.ItemsSource = null;
            this.cbNiveau.ItemsSource = Controleur.GetListeNiveaux();

            this.cbChoixTitre.SelectedIndex = 0;
            titre = Controleur.GetTitre((string)cbChoixTitre.SelectedValue);
            this.groupBoxTitre.DataContext = titre;
        }

        private void DisplayDetailPassage()
        {
            dpPassage.SelectedDate = ((EpreuveTitre)dgDatesPassage.SelectedValue).DateEpreuve;
            txtSalle.Text = ((EpreuveTitre)dgDatesPassage.SelectedValue).Salle;
            dgJury.ItemsSource = ((EpreuveTitre)dgDatesPassage.SelectedValue).ListeJury;
        }

        private void ResetDetailPassage()
        {
            dgDatesPassage.SelectedIndex = -1;
            txtSalle.Text = string.Empty;
            dgJury.ItemsSource = null;
        }

        private void btCreerTitre_Click(object sender, RoutedEventArgs e)
        {
            this.cbChoixTitre.SelectedIndex = -1;
            this.cbChoixTitre.IsEnabled = false;
            this.txtCodeTitre.IsEnabled = true;
        }

        private void btModifTitre_Click(object sender, RoutedEventArgs e)
        {
            if(txtCodeTitre.Text == (string)cbChoixTitre.SelectedValue)
                Controleur.ModifierTitre(titre);
            else
            {
                Controleur.AjouterTitre(titre);
                InitData();
                this.cbChoixTitre.IsEnabled = true;
                this.txtCodeTitre.IsEnabled = false;
            }
        }

        private void btSupprTitre_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Etes-vous sûr(e) de vouloir supprimer ce titre?", "Gestion des titres", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                string codeTitre = titre.CodeTitre;
                Controleur.SupprimerTitre(codeTitre);
                InitData();
            }
        }

        private void btAnnulTitre_Click(object sender, RoutedEventArgs e)
        {
            if(this.cbChoixTitre.SelectedIndex != -1)
                titre = Controleur.HistoTitre;
            else
            {
                cbChoixTitre.SelectedIndex = 0;
                titre = Controleur.GetTitre((string)cbChoixTitre.SelectedValue);
            }
            this.cbChoixTitre.IsEnabled = true;
            this.txtCodeTitre.IsEnabled = false;
            this.groupBoxTitre.DataContext = titre;
        }

        private void cbChoixTitre_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.cbChoixTitre.SelectedIndex != -1)
            {
                titre = Controleur.GetTitre((string)cbChoixTitre.SelectedValue);

                if (titre.ListeEpreuves != null && titre.ListeEpreuves.Count > 0)
                {
                    this.dgDatesPassage.ItemsSource = titre.ListeEpreuves;
                }
                else this.dgDatesPassage.ItemsSource = null;
            }
            else
            {
                titre = new Titre();
                this.dgDatesPassage.ItemsSource = null;
            }

            this.groupBoxTitre.DataContext = titre;
        }

        private void dgDatesPassage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgDatesPassage.Items != null && dgDatesPassage.SelectedIndex != -1)
            {
                DisplayDetailPassage();
                btSupprPassage.IsEnabled = true;
            }
            else ResetDetailPassage();
        }

        private void btSupprPassage_Click(object sender, RoutedEventArgs e) 
        {
            if (cbChoixTitre.SelectedIndex != -1)
            {
                MessageBoxResult mr = MessageBox.Show("Êtes-vous sûr(e) de vouloir supprimer cette épreuve?",
                    "Suppression Epreuve Titre", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (mr == MessageBoxResult.Yes)
                {
                    //TODO: Supprimer cette épreuve si elle existe encore
                    //-> clés : datePassage + codetitre + codesalle. Supprimer avant eptitrejury pour cette date
                    // de passage.
                }
            }
        }

        private void btModifPassage_Click(object sender, RoutedEventArgs e) 
        {
            /*
             * - Si CodeTitre null et CodeSalle null : messagebox rien à enregistrer.
             */
            btSupprPassage.IsEnabled = true;
            
            /* TODO : 
             * Si datePassage et/ou codeSalle a changé : 
             *     - On conserve en cache : datePassage, codeTitre, codeSalle, list<Jury>
             *     - On supprime dans eptitrejury les lignes avec datePassage = ancienne datePassage
             *     - On supprime dans epreuvetitre la ligne avec datePassage = ancienne datePassage
             *     - On insert dans epreuvetitre avec les données en cache
             *     - On insert dans eptitrejury les données en utilisant list<Jury>...
             * Sinon, si List<Jury> a changé : 
             *     - On supprime dans eptitrejury les lignes avec datePassage = ancienne datePassage
             *     - On insert dans eptitrejury les données en utilisant list<Jury>...
             */
        }

        private void dgJury_SelectionChanged(object sender, SelectionChangedEventArgs e) 
        {
            //TODO: ouvrir le gestionnaire de jury sur double clic (voire plus tard clic droit -> menu contextuel).
        }

        private void btAjoutPassage_Click(object sender, RoutedEventArgs e) 
        {
            /*
             * - Si CodeTitre null : messagebox rien à ajouter.
             */

            /*TODO:
             * - Mettre tous les champs du groupbox à vide
             * - Créer une EpreuveTitre vide
            */
            btSupprPassage.IsEnabled = false;
        }

        private void btAnnulPassage_Click(object sender, RoutedEventArgs e) {
            /*
             *  - Mettre les valeurs des champs en fonction de EpreuveTitre en cache (et List<Jury>)
             */
            btSupprPassage.IsEnabled = true;
        }

        private void dgJury_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //TODO: lancer la fenêtre de choix du jury
        }
    }
}
