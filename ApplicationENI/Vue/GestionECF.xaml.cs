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
using System.IO;
using ApplicationENI.Modele;
using ApplicationENI.Vue.PopUp;
using ApplicationENI.DAL;

namespace ApplicationENI.Vue
{
    /// <summary>
    /// Logique d'interaction pour GestionECF.xaml
    /// </summary>
    public partial class GestionECF : UserControl
    {
        private List<ECF> _listeECF = null;
        private ECF _ecfCourant = null;        
        private bool _modif = false;
        private bool _ecfAdd; //si true on est en train d'ajouter un ECF sinon une Competence

        public bool EcfAdd
        {
            get { return _ecfAdd; }
            set { _ecfAdd = value; }
        }
        public ECF EcfCourant
        {
            get { return _ecfCourant; }
            set { _ecfCourant = value; }
        }

        public GestionECF()
        {
            InitializeComponent();
            ActualiseAffichage(null);
        }

        private void ActualiseAffichage(ECF pECFCourant){
            cbECF.Items.Clear();
            cbECF.ItemsSource = null;

            //recup de la liste d'ECF
            _listeECF = ECFDAL.getListECFs();
            //peuplement de la combobox
            foreach (ECF ecf in _listeECF)
            {
                cbECF.Items.Add(ecf);
            }

            if (pECFCourant != null)
            {
                foreach (ECF ecf in _listeECF)
                {
                    if (ecf.Id == pECFCourant.Id) pECFCourant = ecf;
                }
                cbECF.SelectedItem = pECFCourant;
            }
        }
        
        private void slVersion_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            //ActualiseAffichage dans le label correspondant du nombre de version du slider
            if (this.lbNbVersions!=null)
            {
                this.lbNbVersions.Content = slVersion.Value;
            }    
        }

        private void cbECF_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _ecfCourant = (ECF)cbECF.SelectedItem;
            afficheECF(_ecfCourant);
        }

        private void RAZ()
        {
            //RAZ
            tbLibECF.Text = "";
            rbNumerique.IsChecked = true;
            slVersion.Value = 1;
            lbCompetences.ItemsSource = null;
            tbCommECF.Text = "";
        }

        private void afficheECF(ECF pECF)
        {
            RAZ();
            
            //si pas d'ECF selectionné on ne peut pas ajouter de competence
            if (pECF == null)
            {
                return;
            }
            else
            {
                cbECF.SelectedItem = pECF;
                btPlus.IsEnabled = true;
            }

            //AFFICHAGE
            //libelle
            tbLibECF.Text = pECF.Libelle;
            //TODO MAT coeff
            //type de notation
            if (pECF.NotationNumerique)
            {
                rbNumerique.IsChecked = true;
            }
            else
            {
                rbAcquisition.IsChecked = true;
            }
            //Nbre versions
            slVersion.Value = pECF.NbreVersion;
            //commentaire
            tbCommECF.Text = pECF.Commentaire;
            //competences
            lbCompetences.ItemsSource = null;
            lbCompetences.Items.Clear();
            lbCompetences.ItemsSource = pECF.Competences;
        }

        private void btAdd_Click(object sender, RoutedEventArgs e)
        {
            _ecfAdd = true; // on va ajouter un ECF
            AjoutECF_Competence popUp = new AjoutECF_Competence();
            popUp.ShowDialog();

            if (popUp.ECF != null)
            {
                _ecfCourant = popUp.ECF;
                //_listeECF.Add(_ecfCourant);
                ActualiseAffichage(_ecfCourant);
                //cbECF.SelectedItem = _ecfCourant;

                //cbECF.SelectedItem.Equals(_ecfCourant);
                afficheECF(_ecfCourant);
            }
        }
        
        private void lbCompetences_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbCompetences.SelectedItems.Count > 0)
            {
                btMoins.IsEnabled = true;
            }
            else
            {
                btMoins.IsEnabled = false;
            }
        }

        private void btSave_Click(object sender, RoutedEventArgs e)
        {
            _ecfCourant.Libelle = tbLibECF.Text.Trim();
            _ecfCourant.NbreVersion = (int)slVersion.Value;
            _ecfCourant.NotationNumerique = true;
            if (rbAcquisition.IsChecked == true)
            {
                _ecfCourant.NotationNumerique = false;
            }
            _ecfCourant.Commentaire = tbCommECF.Text.Trim();

            RAZ();
            ActualiseAffichage(null);

            //List<Competence> lesCompTemp = new List<Competence>();
            //foreach (Competence compTemp in lbCompetences.Items)
            //{
            //    lesCompTemp.Add(compTemp);
            //}
            //_ecfCourant.Competences = lesCompTemp;
            //ECFDAL.modifierECF(_ecfCourant);          
        }

        private void btPlus_Click(object sender, RoutedEventArgs e)
        {
            //TODO Liste avec suppression ou ajout a l ecf
            _ecfAdd = false;
            ListeECF_Competences liste = new ListeECF_Competences();
            liste.ShowDialog();

            ActualiseAffichage(_ecfCourant);
            //cbECF.SelectedItem = _ecfCourant;
            afficheECF(_ecfCourant);
        }

        private void btMoins_Click(object sender, RoutedEventArgs e)
        {
            foreach (Competence comp in lbCompetences.SelectedItems)
            {
                _ecfCourant.Competences.Remove(comp);
            }
            afficheECF(_ecfCourant);
        }

        private void btDelete_Click(object sender, RoutedEventArgs e)
        {
            //TODO confirmer la suppression
            ECFDAL.supprimerECF(_ecfCourant);

            RAZ();
            ActualiseAffichage(null);
        }

        private void btCancel_Click(object sender, RoutedEventArgs e)
        {
            _ecfCourant = ECFDAL.getECF(_ecfCourant);
        }
    }
}
