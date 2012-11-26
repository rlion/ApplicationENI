

using System.Windows;
using ApplicationENI.Modele;
using System.Collections.Generic;
using System.Windows.Media.Effects;
using System;
using System.DirectoryServices;
using ApplicationENI.Controleur;

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

        #region XAML events

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

        //private void tviPlanning_Selected(object sender, RoutedEventArgs e) {
        //    this.expandStagiaire.IsExpanded = true;
        //    this.MainGrid.Children.RemoveAt(0);
        //    this.MainGrid.Children.Add(new Vue.Planning());

        //}

        private void tviAbsencesRapides_Selected(object sender, RoutedEventArgs e)
        {
            this.expandStagiaire.IsExpanded = false;
            this.MainGrid.Children.RemoveAt(0);
            this.MainGrid.Children.Add(new Vue.AjoutAbsenceRapide());

        }

        #endregion

        #region Bandeau Stagiaire

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

        private void rbFormation_Checked(object sender, RoutedEventArgs e)
        {
            labFiltre.Content = "Formation :";
            string filtre = ", Formation f, PlanningIndividuelFormation p where s.CodeStagiaire=p.CodeStagiaire "+
                "and p.CodeFormation=f.CodeFormation";

            acbNomPrenom.ItemsSource = null;
            acbNomPrenom.ItemsSource = Controleur.GetListeStagiaires(filtre);
            cbFiltre.ItemsSource = Controleur.GetListeFormations();
            cbFiltre.IsEnabled = true;
        }

        private void rbPromotion_Checked(object sender, RoutedEventArgs e)
        {
            labFiltre.Content = "Promotion :";
            string filtre = ", Promotion f, PlanningIndividuelFormation p where s.CodeStagiaire=p.CodeStagiaire " +
    "and p.CodePromotion=f.CodePromotion";

            acbNomPrenom.ItemsSource = null;
            acbNomPrenom.ItemsSource = Controleur.GetListeStagiaires(filtre);
            cbFiltre.ItemsSource = Controleur.GetListePromotions();
            cbFiltre.IsEnabled = true;
        }

        private void cbFiltre_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if(this.IsInitialized && cbFiltre.IsEnabled && cbFiltre.SelectedIndex != -1)
            {
                string filtre;

                if(rbFormation.IsChecked.HasValue && rbFormation.IsChecked.Value)
                {
                    filtre = ", Formation f, PlanningIndividuelFormation p where s.CodeStagiaire=p.CodeStagiaire and " +
                        "p.CodeFormation=f.CodeFormation and f.CodeFormation='"+(string)cbFiltre.SelectedValue+"'";
                }
                else
                {
                    filtre = ", Promotion f, PlanningIndividuelFormation p where s.CodeStagiaire=p.CodeStagiaire and " +
    "p.CodePromotion=f.CodePromotion and f.CodePromotion='" + (string)cbFiltre.SelectedValue + "'";
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
            rbFormation.IsChecked = false;
            rbPromotion.IsChecked = false;
            cbFiltre.ItemsSource = null;
            cbFiltre.Items.Clear();
            cbFiltre.IsEnabled = false;

            Parametres.Instance.stagiaire = null;
            tvPersonParam.IsEnabled = false;
            tvPersonParam.IsExpanded = false;
        }

        #endregion
    }
}
