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

        #region propriété + get/set
        //private List<SelectionFormation> _listeFormations;
        //private List<SelectionFormation> ListeFormations
        //{
        //    get { return _listeFormations; }
        //    set { _listeFormations = value; }
        //}
        CtrlListeECF_Formations _ctrlListeECF_Formations = null;
        #endregion

        

        #region constructeur
        public ListeECF_Formations()
        {
            InitializeComponent();

            _ctrlListeECF_Formations = new CtrlListeECF_Formations();
            ActualiseAffichage(null);

            //autocbCompetence.ItemsSource = _listeCompetences;
            //isInitAutoCompBox = true;
        }
        #endregion

        #region affichage
        private void ActualiseAffichage(CtrlListeECF_Formations.SelectionFormation pSf)
        {
            //RAZ
            lbListeFormations.ItemsSource = null;//lbListeCompetences.Items.Clear();
            _ctrlListeECF_Formations.ListeFormations = new List<CtrlListeECF_Formations.SelectionFormation>();
            if (pSf == null)
            {
                //MAJ        
                //TODO eviter double appel getListForm
                if (_ctrlListeECF_Formations.getListFormations()!=null)
                {
                    foreach (Formation form in _ctrlListeECF_Formations.getListFormations())
                    {
                        CtrlListeECF_Formations.SelectionFormation uneForm = new CtrlListeECF_Formations.SelectionFormation();
                        uneForm.Formation = form;
                        uneForm.IsChecked = false;
                        if (((GestionECF)instanceFenetre.InstanceFenetreEnCours).lbFormations.Items.Contains(form))
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
        //TODO?
        //private void btAjouter_Click(object sender, RoutedEventArgs e)
        //{
        //    AjoutECF_Competence ajoutCompetence = new AjoutECF_Competence();
        //    ajoutCompetence.ShowDialog();

        //    ActualiseAffichage(null);
        //}
        //private void btSupprimer_Click(object sender, RoutedEventArgs e)
        //{
        //    String message = "";

        //    //TODO confirmer la suppression
        //    foreach (SelectionCompetence selComp in _listeCompetences)
        //    {
        //        if (selComp.IsChecked)
        //        {
        //            message = CtrlGestionECF.supprimerCompetence(selComp.Competence);
        //        }
        //    }

        //    if (message.Trim() != "")
        //    {
        //        MessageBox.Show(message);
        //    }
        //    else
        //    {
        //        ActualiseAffichage(null);
        //    }
        //}
        private void btValider_Click(object sender, RoutedEventArgs e)
        {
            //On supprime tous les liens avec l'ECF courant
            _ctrlListeECF_Formations.supprimerLiensFormations(((GestionECF)instanceFenetre.InstanceFenetreEnCours).CtrlGestionECF.EcfCourant);
            
            //On recréé tous les liens avec les compétences sélectionnées
            //TODO eviter double appel
            if (_ctrlListeECF_Formations.ListeFormations!=null)
            {
                foreach (CtrlListeECF_Formations.SelectionFormation selForm in _ctrlListeECF_Formations.ListeFormations)
                {
                    if (selForm.IsChecked)
                    {
                        _ctrlListeECF_Formations.ajouterLienFormation(((GestionECF)instanceFenetre.InstanceFenetreEnCours).CtrlGestionECF.EcfCourant, selForm.Formation);
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

        //private void autocbCompetence_GotFocus(object sender, RoutedEventArgs e)
        //{
        //    if (isInitAutoCompBox)
        //    {
        //        autocbCompetence.Text = string.Empty;
        //        isInitAutoCompBox = false;
        //    }
        //}
        //private void autocbCompetence_MouseUp(object sender, MouseButtonEventArgs e)
        //{
        //    foreach (SelectionCompetence sc in _listeCompetences)
        //    {
        //        if (sc.Competence.Code == autocbCompetence.Text.Substring(0, autocbCompetence.Text.IndexOf(" - "))
        //            && (sc.Competence.Libelle == autocbCompetence.Text.Substring(autocbCompetence.Text.IndexOf(" - ") + 3)))
        //        {
        //            ActualiseAffichage(sc);
        //            refresh();
        //        }
        //    }
        //}
        #endregion
    }
}
