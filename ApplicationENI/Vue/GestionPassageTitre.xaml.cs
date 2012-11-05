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
        private bool isListJuryChanged;


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

            this.epreuveTitre = null;
        }

        //Affichage d'une épreuve titre à partir du choix du datagrid de liste des passages
        private void DisplayDetailPassage()
        {
            dpPassage.SelectedDate = ((EpreuveTitre)dgDatesPassage.SelectedValue).DateEpreuve;
            cbSalle.SelectedValue = ((EpreuveTitre)dgDatesPassage.SelectedValue).Salle;
            dgJury.ItemsSource = ((EpreuveTitre)dgDatesPassage.SelectedValue).ListeJury;
        }

        //Vidage des données d'une épreuve titre lorsque aucune ligne du datagrid de liste des passages n'est sélectionnée
        private void ResetDetailPassage()
        {
            dgDatesPassage.SelectedIndex = -1;
            cbSalle.SelectedIndex = -1;
            dpPassage.SelectedDate = null;
            dgJury.ItemsSource = null;
            epreuveTitre = null;
        }

        //THIS IS IT boubou!
        //Passage du formulaire en mode "Ajout d'un titre"
        private void btCreerTitre_Click(object sender, RoutedEventArgs e)
        {
            this.cbChoixTitre.SelectedIndex = -1;
            this.cbChoixTitre.IsEnabled = false;
            this.txtCodeTitre.IsEnabled = true;
            ResetDetailPassage();
        }

        //THIS IS IT boubou!
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

        //THIS IS IT boubou!
        private void btSupprTitre_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Etes-vous sûr(e) de vouloir supprimer ce titre?", "Gestion des titres", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                string codeTitre = titre.CodeTitre;
                Controleur.SupprimerTitre(codeTitre);
                InitData();
            }
        }

        //THIS IS IT boubou!
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

        //THIS IS IT boubou!
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
                isListJuryChanged = false;
                this.epreuveTitre = (EpreuveTitre)dgDatesPassage.SelectedValue;
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
            if(titre != null && titre.CodeTitre != null && cbSalle.SelectedIndex != -1 && dpPassage.SelectedDate.HasValue) 
            {
                btSupprPassage.IsEnabled = true;

                if(epreuveTitre.Salle != null) 
                {
                    //Modification

                    EpreuveTitre oldEpreuveTitre = new EpreuveTitre();
                    oldEpreuveTitre.Titre = epreuveTitre.Titre;
                    oldEpreuveTitre.Salle = epreuveTitre.Salle;
                    oldEpreuveTitre.DateEpreuve = epreuveTitre.DateEpreuve;
                    oldEpreuveTitre.ListeJury = epreuveTitre.ListeJury;

                    if(dpPassage.SelectedDate.Value != epreuveTitre.DateEpreuve || ((string)cbSalle.SelectedValue) != epreuveTitre.Salle) 
                    {
                        /* TODO : 
                         * Si datePassage et/ou codeSalle a changé : 
                         *     - On conserve en cache : datePassage, codeTitre, codeSalle, list<Jury>
                         *     - On supprime dans eptitrejury les lignes avec datePassage = ancienne datePassage
                         *     - On supprime dans epreuvetitre la ligne avec datePassage = ancienne datePassage
                         *     - On insert dans epreuvetitre avec les données en cache
                         *     - On insert dans eptitrejury les données en utilisant list<Jury>...
                         */
                    } 

                    if (isListJuryChanged && dpPassage.SelectedDate.HasValue && oldEpreuveTitre.DateEpreuve==epreuveTitre.DateEpreuve)
                    {
                       /* Sinon, si List<Jury> a changé : 
                        *     - On supprime dans eptitrejury les lignes avec datePassage = ancienne datePassage
                        *     - On insert dans eptitrejury les données en utilisant list<Jury>...
                        */
                        Controleur.ModifierListeJuryEpreuve(cbChoixTitre.SelectedIndex, oldEpreuveTitre, epreuveTitre);
                    }

                } 
                else 
                {
                    //Ajout
                    if (dpPassage.SelectedDate.HasValue && cbSalle.SelectedIndex != -1 && cbChoixTitre.SelectedIndex != -1)
                    {
                        EpreuveTitre epTitre;

                        if (dgJury.HasItems)
                        {
                            epTitre = new EpreuveTitre(dpPassage.SelectedDate.Value, (string)cbSalle.SelectedValue,
                                (string)cbChoixTitre.SelectedValue, (List<Jury>)dgJury.ItemsSource);
                        }
                        else
                        {
                            epTitre = new EpreuveTitre(dpPassage.SelectedDate.Value, (string)cbSalle.SelectedValue, (string)cbChoixTitre.SelectedValue);
                        }

                        Controleur.AjouterEpreuveTitre(this.cbChoixTitre.SelectedIndex, epTitre);
                        this.titre.ListeEpreuves.Add(epTitre);
                        this.dgDatesPassage.ItemsSource = titre.ListeEpreuves;
                        this.epreuveTitre = epTitre;
                    }
                }
            }
        }

        //Passage du formulaire en mode "ajout d'une épreuve titre"
        private void btAjoutPassage_Click(object sender, RoutedEventArgs e) 
        {
            if(titre != null && !string.IsNullOrEmpty(titre.CodeTitre)) 
            {
                ResetDetailPassage();
                btSupprPassage.IsEnabled = false;
                this.epreuveTitre = new EpreuveTitre();
            }
        }

        private void btAnnulPassage_Click(object sender, RoutedEventArgs e) {

            btSupprPassage.IsEnabled = true;

            if(this.epreuveTitre != null) 
            {
                dpPassage.SelectedDate = epreuveTitre.DateEpreuve;
                cbSalle.SelectedValue = epreuveTitre.Salle;
                dgJury.ItemsSource = epreuveTitre.ListeJury;
            }
        }

        private void dgJury_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(epreuveTitre != null)
            {
                PopUp.GestionJury gestionJury = new PopUp.GestionJury(epreuveTitre.ListeJury);
                gestionJury.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                gestionJury.ShowDialog();
            }
        }
    }
}
