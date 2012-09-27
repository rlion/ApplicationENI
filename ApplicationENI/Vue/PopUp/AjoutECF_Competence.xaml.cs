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
using System.Windows.Shapes;
using ApplicationENI.Modele;
using ApplicationENI.DAL;

namespace ApplicationENI.Vue.PopUp
{
    /// <summary>
    /// Logique d'interaction pour AjoutECF_Competence.xaml
    /// </summary>
    public partial class AjoutECF_Competence : Window
    {
        private ECF _ECF = null;
        private Competence _competence = null;
        public ECF ECF
        {
            get { return _ECF; }
            set { _ECF = value; }
        }
        public Competence Competence
        {
            get { return _competence; }
            set { _competence = value; }
        }

        public AjoutECF_Competence()
        {
            InitializeComponent();

            if (((GestionECF)instanceFenetre.InstanceFenetreEnCours).cbECF.SelectedItem == null)
            {
                rbAddCompetence.IsEnabled = false;
                rbAddECF.IsChecked = true;
            }
        }

        private void btAnnuler_Click(object sender, RoutedEventArgs e)
        {
            if (rbAddECF.IsChecked == false && rbAddCompetence.IsChecked == false)
            {
                this.Close();
                return;
            }
            
            rbAddCompetence.IsChecked = false;
            rbAddECF.IsChecked = false;
            tbCode.Text = "";
            tbLibelle.Text = "";
            lbCode.IsEnabled = false;
            lbLibelle.IsEnabled = false;
            tbCode.IsEnabled = false;
            tbLibelle.IsEnabled = false;

            if (((GestionECF)instanceFenetre.InstanceFenetreEnCours).cbECF.SelectedItem == null)
            {
                rbAddCompetence.IsEnabled = false;
            }
        }

        private void btValider_Click(object sender, RoutedEventArgs e)
        {
            if (tbCode.Text.Trim() != "" && tbCode.Text.Trim() != "")
            {
                if (rbAddECF.IsChecked == true)
                {
                    _competence = null;
                    _ECF = new ECF((tbCode.Text.Trim()).ToUpper(), tbCode.Text.Trim());
                    ECFDAL.ajouterECF(_ECF);
                }

                if (rbAddCompetence.IsChecked == true)
                {
                    _ECF = null;
                    _competence = new Competence((tbCode.Text.Trim()).ToUpper(), tbCode.Text.Trim());
                    CompetencesDAL.ajouterCompetence(_competence);
                }
                this.Close();
            }
        }

        private void rbAddECF_Checked(object sender, RoutedEventArgs e)
        {
            lbCode.IsEnabled = true;
            lbLibelle.IsEnabled = true;
            tbCode.IsEnabled = true;
            tbLibelle.IsEnabled = true;
        }

        private void rbAddCompetence_Checked(object sender, RoutedEventArgs e)
        {
            lbCode.IsEnabled = true;
            lbLibelle.IsEnabled = true;
            tbCode.IsEnabled = true;
            tbLibelle.IsEnabled = true;

            tbCode.Focus();
        }
    }
}
