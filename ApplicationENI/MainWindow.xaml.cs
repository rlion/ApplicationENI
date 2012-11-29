

using System.Windows;
using ApplicationENI.Modele;
using System.Collections.Generic;
using System.Windows.Media.Effects;
using System;
using System.DirectoryServices;
using ApplicationENI.Controleur;
using System.Windows.Media;

namespace ApplicationENI
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CtrlAccueilGeneral Controleur;
        private bool isInitAutoCompBox;

        public MainWindow()
        {
            InitializeComponent();

            Parametres.Instance.listAlertes = new List<ItemAlerte>();
            Parametres.Instance.listAlertes.Add(new ItemAlerte(0, "Examen le 23 juin.", 5));
            Parametres.Instance.listAlertes.Add(new ItemAlerte(3, "ECF n° 3 non corrigé.", 1));


            BlurEffect myBlurEffect = new BlurEffect();
            myBlurEffect.Radius = 8;
            this.Effect = myBlurEffect;

            this.MainGrid.Children.Add(new Vue.AccueilGeneral());
            this.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            this.Show();

            Vue.Login login = new Vue.Login();
            login.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            login.ShowDialog();

            Controleur = new CtrlAccueilGeneral();
            InitBandeStagiaire();

            this.Effect = null;

            //Modif mat on masque ici sinon on ne voit pas dans l'éditeur graphique...
            tvPersonParam.IsExpanded=false;
            tvPersonParam.IsEnabled = false;
        }

        #region Events bandeau menu général

        private void tviGestionECF_Selected(object sender, RoutedEventArgs e)
        {
            this.expandStagiaire.IsExpanded = false;
            this.MainGrid.Children.RemoveAt(0);
            Vue.GestionECF gestionECF = new Vue.GestionECF();
            instanceFenetre.InstanceFenetreEnCours = gestionECF; 
            this.MainGrid.Children.Add(gestionECF);
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
            this.MainGrid.Children.Add(new Vue.SyntheseECF());
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

        private void tviAbsencesRapides_Selected(object sender, RoutedEventArgs e)
        {
            this.expandStagiaire.IsExpanded = false;
            this.MainGrid.Children.RemoveAt(0);
            this.MainGrid.Children.Add(new Vue.AjoutAbsenceRapide());
        }

        #endregion

        #region Events bandeau stagiaire

        private void InitBandeStagiaire()
        {
            cbFiltre.SelectedValuePath="Key";
            cbFiltre.DisplayMemberPath = "Value";
            cbFiltre.SelectedIndex = -1;

            acbNomPrenom.ItemsSource = Controleur.GetListeStagiaires();
            isInitAutoCompBox = true;
        }

        private void acbNomPrenom_GotFocus(object sender, RoutedEventArgs e)
        {
            if (isInitAutoCompBox)
            {
                acbNomPrenom.Text = string.Empty;
                isInitAutoCompBox = false;
            }
        }

        private void btRechercher_Click(object sender, RoutedEventArgs e) 
        {
            
            if(acbNomPrenom.SelectedItem != null)
            {
                Parametres.Instance.stagiaire = (Stagiaire)acbNomPrenom.SelectedItem;
                tvPersonParam.IsEnabled = true;
                tvPersonParam.IsExpanded = true;
            }
            else MessageBox.Show("Veuillez choisir un stagiaire!");
        }

        //Formation Continue
        private void rbFC_Checked(object sender, RoutedEventArgs e)
        {
            labFiltre.Content = "Promotion :";
            string filtre = ", Formation f, Promotion p where s.CodeStagiaire=i.CodeStagiaire "+
                "and i.CodeFormation=f.CodeFormation and i.CodePromotion=p.CodePromotion";

            acbNomPrenom.ItemsSource = null;
            acbNomPrenom.ItemsSource = Controleur.GetListeStagiaires(filtre);
            cbFiltre.ItemsSource = Controleur.GetListePromotions();
            cbFiltre.IsEnabled = true;
        }

        //Contrat Pro.
        private void rbCP_Checked(object sender, RoutedEventArgs e)
        {
            labFiltre.Content = "Formation :";
            string filtre = ", Formation f where s.CodeStagiaire=i.CodeStagiaire " +
                "and i.CodeFormation=f.CodeFormation and i.CodePromotion is null";

            acbNomPrenom.ItemsSource = null;
            acbNomPrenom.ItemsSource = Controleur.GetListeStagiaires(filtre);
            cbFiltre.ItemsSource = Controleur.GetListeFormations();
            cbFiltre.IsEnabled = true;
        }

        //Module
        private void rbMO_Checked(object sender, RoutedEventArgs e)
        {
            cbFiltre.ItemsSource = null;
            cbFiltre.Items.Clear();
            cbFiltre.IsEnabled = false;
            labFiltre.Content = string.Empty;

            string filtre = " where s.CodeStagiaire=i.CodeStagiaire and i.CodeFormation is null and i.CodePromotion is null";

            acbNomPrenom.ItemsSource = null;
            acbNomPrenom.ItemsSource = Controleur.GetListeStagiaires(filtre);
        }

        private void cbFiltre_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if(this.IsInitialized && cbFiltre.IsEnabled && cbFiltre.SelectedIndex != -1)
            {
                string filtre;

                if(rbFC.IsChecked.HasValue && rbFC.IsChecked.Value)
                {
                    filtre = ", Promotion p where s.CodeStagiaire=i.CodeStagiaire and " +
                        "i.CodePromotion=p.CodePromotion and p.CodePromotion='" + (string)cbFiltre.SelectedValue + "'";
                }
                else
                {
                    filtre = ", Formation f where s.CodeStagiaire=i.CodeStagiaire and " +
                       "i.CodePromotion=f.CodeFormation and f.CodeFormation='" + (string)cbFiltre.SelectedValue + "'";
                }

                acbNomPrenom.ItemsSource = null;
                acbNomPrenom.ItemsSource = Controleur.GetListeStagiaires(filtre);
            }
        }

        private void btAnnuler_Click(object sender, RoutedEventArgs e)
        {
            acbNomPrenom.Text = string.Empty;
            acbNomPrenom.ItemsSource = null;
            acbNomPrenom.ItemsSource = Controleur.GetListeStagiaires();
            rbFC.IsChecked = false;
            rbCP.IsChecked = false;
            rbMO.IsChecked = false;
            cbFiltre.ItemsSource = null;
            cbFiltre.Items.Clear();
            cbFiltre.IsEnabled = false;
            labFiltre.Content = string.Empty;

            Parametres.Instance.stagiaire = null;
            tvPersonParam.IsEnabled = false;
            tvPersonParam.IsExpanded = false;
        }

        #endregion

        #region Events fenêtre principale

        private void btAccueil_Click(object sender, RoutedEventArgs e)
        {
            this.btAccueil.Background = Brushes.Gray;
            this.expandStagiaire.IsExpanded = true;
            this.tviGestionECF.IsSelected = false;
            this.MainGrid.Children.RemoveAt(0);
            this.MainGrid.Children.Add(new Vue.AccueilGeneral());
        }

        private void expandStagiaire_Collapsed(object sender, RoutedEventArgs e)
        {
            this.expandStagiaire.Height = 20;
        }

        private void expandStagiaire_Expanded(object sender, RoutedEventArgs e)
        {
            this.expandStagiaire.Height = 94;
        }

        private void expandMenu_Expanded(object sender, RoutedEventArgs e)
        {
            this.expandMenu.Width = 240;
        }

        private void expandMenu_Collapsed(object sender, RoutedEventArgs e)
        {
            this.expandMenu.Width = 25;
        }

        private void btAccueil_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            this.btAccueil.Background = Brushes.Silver;
        }

        private void btAccueil_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            this.btAccueil.Background = Brushes.LightGray;
        }

        #endregion

    }
}
