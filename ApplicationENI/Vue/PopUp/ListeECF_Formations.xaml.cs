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
using ApplicationENI.Controleur;

namespace ApplicationENI.Vue.PopUp
{
    /// <summary>
    /// Logique d'interaction pour ListeECF_Formations.xaml
    /// </summary>
    public partial class ListeECF_Formations : Window
    {

        #region Attributs, proprietes
        private CtrlListeECF_Formations _ctrlListeECF_Formations = null;
        private List<Formation> _lesFormationsSelectionnees = null;
        private ECF _ECFCourant = null;
        #endregion
        
        #region constructeur
        public ListeECF_Formations(List<Formation> pLesFormationsSelectionnees, ECF pEcfCourant)
        {
            InitializeComponent();

            _ctrlListeECF_Formations = new CtrlListeECF_Formations();
            _lesFormationsSelectionnees = pLesFormationsSelectionnees;
            _ECFCourant = pEcfCourant;

            ActualiseAffichage(null);
        }
        #endregion

        #region affichage
        private void ActualiseAffichage(CtrlListeECF_Formations.SelectionFormation pSf)
        {
            //RAZ
            lbListeFormations.ItemsSource = null;
            _ctrlListeECF_Formations.ListeFormations = new List<CtrlListeECF_Formations.SelectionFormation>();
            if (pSf == null)
            {
                //MAJ        
                if (_ctrlListeECF_Formations.getListFormations()!=null)
                {
                    foreach (Formation form in _ctrlListeECF_Formations.getListFormations())
                    {
                        CtrlListeECF_Formations.SelectionFormation uneForm = new CtrlListeECF_Formations.SelectionFormation();
                        uneForm.Formation = form;
                        uneForm.IsChecked = false;
                        if (_lesFormationsSelectionnees.Contains(form))
                        {
                            uneForm.IsChecked = true;
                        }

                        _ctrlListeECF_Formations.ListeFormations.Add(uneForm);
                    }
                }
                
                lbListeFormations.ItemsSource = _ctrlListeECF_Formations.ListeFormations;
            }
            else
            {
                _ctrlListeECF_Formations.ListeFormations.Add(pSf);
                lbListeFormations.ItemsSource = _ctrlListeECF_Formations.ListeFormations;
            }
            
        }
        private void refresh()
        {
            lbListeFormations.ItemsSource = null;
            lbListeFormations.Items.Clear();
            lbListeFormations.ItemsSource = _ctrlListeECF_Formations.ListeFormations;
        }
        #endregion

        #region evenements
        private void btAnnuler_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void btValider_Click(object sender, RoutedEventArgs e)
        {
            //On supprime tous les liens avec l'ECF courant
            _ctrlListeECF_Formations.supprimerLiensFormations(_ECFCourant);
            
            //On recréé tous les liens avec les compétences sélectionnées
            if (_ctrlListeECF_Formations.ListeFormations!=null)
            {
                foreach (CtrlListeECF_Formations.SelectionFormation selForm in _ctrlListeECF_Formations.ListeFormations)
                {
                    if (selForm.IsChecked)
                    {
                        _ctrlListeECF_Formations.ajouterLienFormation(_ECFCourant, selForm.Formation);
                    }
                }
            }
            
            Close();
        }
        private void btSelect_Click(object sender, RoutedEventArgs e)
        {
            if (_ctrlListeECF_Formations.ListeFormations!=null)
            {
                foreach (CtrlListeECF_Formations.SelectionFormation selForm in _ctrlListeECF_Formations.ListeFormations)
                {
                    selForm.IsChecked = true;
                }
            }
            
            refresh();
        }
        private void btDeselect_Click(object sender, RoutedEventArgs e)
        {
            if (_ctrlListeECF_Formations.ListeFormations!=null)
            {
                foreach (CtrlListeECF_Formations.SelectionFormation selForm in _ctrlListeECF_Formations.ListeFormations)
                {
                    selForm.IsChecked = false;
                }
            }
            
            refresh();
        }
        #endregion
    }
}
