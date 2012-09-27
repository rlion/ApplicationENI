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

        public GestionECF()
        {
            InitializeComponent();
            ActualiseAffichage(null);
        }

        private void ActualiseAffichage(ECF pECFCourant){
            //recup de la liste d'ECF
            _listeECF = ECFDAL.getListECFs();
            //peuplement de la combobox
            cbECF.Items.Clear();
            foreach (ECF ecf in _listeECF)
            {
                cbECF.Items.Add(ecf);
            }
            //affichage de l'ECF en cours
            //afficheECF(pECFCourant);
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
            AjoutECF_Competence popUp = new AjoutECF_Competence();
            popUp.ShowDialog();

            if (popUp.ECF != null)
            {
                _ecfCourant = popUp.ECF; 
                _listeECF.Add(_ecfCourant);
                ////cbECF.ItemsSource = null;
                //cbECF.Items.Clear();
                //foreach (ECF ecf in _listeECF)
                //{
                //    cbECF.Items.Add(ecf);
                //}
                ActualiseAffichage(_ecfCourant);
                cbECF.SelectedItem = _ecfCourant;                               
            }
            if (popUp.Competence != null)
            {
                //if (_ecfCourant.Competences == null) _ecfCourant.Competences = new List<Competence>();
                //_ecfCourant.Competences.Add(popUp.Competence);

                //ActualiseAffichage(null);
                //afficheECF(_ecfCourant);                
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
            _ecfCourant.NotationNumerique = false;
            if (rbNumerique.IsChecked == true)
            {
                _ecfCourant.NotationNumerique = true;
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
            AvailablePresentationObjects liste = new AvailablePresentationObjects();
            liste.ShowDialog();
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
