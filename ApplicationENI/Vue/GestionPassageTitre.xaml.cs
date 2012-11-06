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
        private EpreuveTitre histoEpreuveTitre;


        public GestionPassageTitre()
        {
            InitializeComponent();

            Controleur = new CtrlGestionPassageTitre();

            this.cbChoixTitre.SelectedValuePath = "Key";
            this.cbChoixTitre.DisplayMemberPath = "Value";

            this.cbNiveau.SelectedValuePath = "Key";
            this.cbNiveau.DisplayMemberPath = "Value";

            this.cbSalle.SelectedValuePath = "CodeSalle";
            this.cbSalle.DisplayMemberPath = "Libelle";

            InitData();
        }

        //Initialisation des données du titre (partie haute)
        private void InitData()
        {
            this.groupBoxTitre.DataContext = null;
            this.cbChoixTitre.ItemsSource = null;
            this.cbChoixTitre.ItemsSource = Controleur.GetListeCodeTitre();
            this.cbNiveau.ItemsSource = null;
            this.cbNiveau.ItemsSource = Controleur.GetListeNiveaux();
            this.cbSalle.ItemsSource = null;
            this.cbSalle.ItemsSource = Controleur.GetSalles();

            this.cbChoixTitre.SelectedIndex = 0;
            titre = Controleur.GetTitre((string)cbChoixTitre.SelectedValue);
            this.groupBoxTitre.DataContext = titre;

            ResetDetailPassage();
        }

        //Affichage d'une épreuve titre à partir du choix du datagrid de liste des passages
        private void DisplayDetailPassage()
        {
            dpPassage.SelectedDate = this.epreuveTitre.DateEpreuve;
            cbSalle.SelectedValue = this.epreuveTitre.Salle;
            dgJury.ItemsSource = this.epreuveTitre.ListeJury;
        }

        //Vidage des données d'une épreuve titre lorsque aucune ligne du datagrid de liste des passages n'est sélectionnée
        private void ResetDetailPassage()
        {
            dgDatesPassage.SelectedIndex = -1;
            cbSalle.SelectedIndex = -1;
            dpPassage.SelectedDate = null;
            dgJury.ItemsSource = null;
            epreuveTitre = null;
            histoEpreuveTitre = null;
            
            btSupprPassage.IsEnabled = false;
            dgJury.IsEnabled = false;
            cbSalle.IsEnabled = false;
            dpPassage.IsEnabled = false;
        }

        //Méthode appelée depuis btSupprPassage et btModifPassage
        private void SupprimerEpreuveTitre()
        {
            if(this.histoEpreuveTitre != null)
            {
                Controleur.SupprimerEpreuveTitre(cbChoixTitre.SelectedIndex, this.histoEpreuveTitre, this.epreuveTitre);
            }
        }

        //Initialisation de la variable histo
        private void InitEpreuveTitre()
        {
            this.epreuveTitre = new EpreuveTitre();
            this.epreuveTitre.DateEpreuve = this.histoEpreuveTitre.DateEpreuve;
            this.epreuveTitre.ListeJury = this.histoEpreuveTitre.ListeJury;
            this.epreuveTitre.Salle = this.histoEpreuveTitre.Salle;
            this.epreuveTitre.Titre = this.histoEpreuveTitre.Titre;
        }

        //On vérifie qu'une épreuve existante n'est pas en train d'être recréée
        private bool IsUniqueEpreuve()
        {
            foreach(EpreuveTitre epTitre in (List<EpreuveTitre>)dgDatesPassage.ItemsSource)
            {
                if(epreuveTitre.DateEpreuve == epTitre.DateEpreuve && epreuveTitre.Salle == epTitre.Salle && epreuveTitre.Titre == epTitre.Titre)
                {
                    return false;
                }
            }
            return true;
        }

        #region Partie Gestion de la partie haute (Titre)

        //Passage du formulaire en mode "Ajout d'un titre"
        private void btCreerTitre_Click(object sender, RoutedEventArgs e)
        {
            this.cbChoixTitre.SelectedIndex = -1;
            this.cbChoixTitre.IsEnabled = false;
            this.txtCodeTitre.IsEnabled = true;
            ResetDetailPassage();
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

        #endregion

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
                this.histoEpreuveTitre = (EpreuveTitre)dgDatesPassage.SelectedItem;
                InitEpreuveTitre();
                DisplayDetailPassage();
                
                btSupprPassage.IsEnabled = true;
                dgJury.IsEnabled = true;
                cbSalle.IsEnabled = true;
                dpPassage.IsEnabled = true;
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
                    SupprimerEpreuveTitre();

                    this.dgDatesPassage.ItemsSource = null;
                    this.dgDatesPassage.Items.Clear();
                    titre = Controleur.HistoTitre;
                    this.dgDatesPassage.ItemsSource = titre.ListeEpreuves;

                    ResetDetailPassage();
                }
            }
        }

        //Validation de l'ajout ou d'une modification d'une épreuve
        private void btModifPassage_Click(object sender, RoutedEventArgs e) 
        {
            try
            {
                if(titre != null && titre.CodeTitre != null && cbSalle.SelectedIndex != -1 && dpPassage.SelectedDate.HasValue)
                {
                    btSupprPassage.IsEnabled = true;
                    btAjoutPassage.IsEnabled = true;
                    dgDatesPassage.IsEnabled = true;

                    this.epreuveTitre.DateEpreuve = dpPassage.SelectedDate.Value;
                    this.epreuveTitre.Salle = (string)cbSalle.SelectedValue;

                    if(IsUniqueEpreuve() || (!IsUniqueEpreuve() && (histoEpreuveTitre!=null && histoEpreuveTitre.ListeJury != epreuveTitre.ListeJury)))
                    {
                        if(dgJury.HasItems) this.epreuveTitre.ListeJury = (List<Jury>)dgJury.ItemsSource;

                        //Si on est en modification, on supprime toutes les références de l'ancienne EpreuveTitre
                        if(histoEpreuveTitre != null)
                        {
                            //Suppression de l'ancienne épreuve -> histoEpreuveTitre
                            SupprimerEpreuveTitre();
                        }

                        //Ensuite, dans tous les cas, on ajoute toutes les références de la nouvelle EpreuveTitre

                        Controleur.AjouterEpreuveTitre(this.cbChoixTitre.SelectedIndex, this.epreuveTitre);

                        this.dgDatesPassage.ItemsSource = null;
                        this.dgDatesPassage.Items.Clear();
                        titre = Controleur.HistoTitre;
                        this.dgDatesPassage.ItemsSource = titre.ListeEpreuves;

                        this.dgDatesPassage.SelectedValue = this.epreuveTitre;
                    }
                    else throw new Exception();
                }
            }
            catch(Exception)
            {
                MessageBox.Show("Cette épreuve existe déjà!", "Gestion Epreuve Titre", MessageBoxButton.OK, MessageBoxImage.Error);
                this.btAnnulPassage_Click(sender, e);
            }

        }

        //Passage du formulaire en mode "ajout d'une épreuve titre"
        private void btAjoutPassage_Click(object sender, RoutedEventArgs e) 
        {
            if(titre != null && !string.IsNullOrEmpty(titre.CodeTitre)) 
            {
                ResetDetailPassage();
                btAjoutPassage.IsEnabled = false;
                dgDatesPassage.IsEnabled = false;
                dgJury.IsEnabled = true;
                cbSalle.IsEnabled = true;
                dpPassage.IsEnabled = true;

                this.epreuveTitre = new EpreuveTitre();
                this.epreuveTitre.Titre = (string)cbChoixTitre.SelectedValue;
            }
        }

        private void btAnnulPassage_Click(object sender, RoutedEventArgs e) {

            btSupprPassage.IsEnabled = true;
            btAjoutPassage.IsEnabled = true;
            dgDatesPassage.IsEnabled = true;

            if(this.histoEpreuveTitre != null)
            {
                dpPassage.SelectedDate = this.histoEpreuveTitre.DateEpreuve;
                cbSalle.SelectedValue = this.histoEpreuveTitre.Salle;
                dgJury.ItemsSource = this.histoEpreuveTitre.ListeJury;
            }
            else
            {
                ResetDetailPassage();
            }
        }

        //Gestion du jury
        private void dgJury_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(epreuveTitre != null)
            {

                PopUp.GestionJury gestionJury = new PopUp.GestionJury(epreuveTitre);
                gestionJury.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                gestionJury.ShowDialog();
                epreuveTitre.ListeJury = gestionJury.ListeJury;

                dgJury.ItemsSource = null;
                dgJury.Items.Clear();
                dgJury.ItemsSource = epreuveTitre.ListeJury;
            }
        }
    }
}
