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
            txtSalle.Text = ((EpreuveTitre)dgDatesPassage.SelectedValue).Salle.CodeSalle;
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
                titre = new Titre();

            this.groupBoxTitre.DataContext = titre;
        }

        private void dgDatesPassage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgDatesPassage.Items != null && dgDatesPassage.SelectedIndex != -1)
            {
                DisplayDetailPassage();
            }
            else ResetDetailPassage();
        }
    }
}
