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
        private List<SelectionFormation> _listeFormations;
        private List<SelectionFormation> ListeFormations
        {
            get { return _listeFormations; }
            set { _listeFormations = value; }
        }
        #endregion

        #region classe spéciale SelectionFormation
        //classe listant l'ensemble des formations (avec coche si elle est liée à l'ECF courant)
        private class SelectionFormation
        {
            private Formation _formation;
            private bool _isChecked;

            public Formation Formation
            {
                get { return _formation; }
                set { _formation = value; }
            }
            public bool IsChecked
            {
                get { return _isChecked; }
                set { _isChecked = value; }
            }

            public override string ToString()
            {
                return _formation.Libelle;// +" - " + _competence.Libelle;
            }
        }
        #endregion

        #region constructeur
        public ListeECF_Formations()
        {
            InitializeComponent();

            ActualiseAffichage(null);

            //autocbCompetence.ItemsSource = _listeCompetences;
            //isInitAutoCompBox = true;
        }
        #endregion

        #region affichage
        private void ActualiseAffichage(SelectionFormation pSf)
        {
            //RAZ
            lbListeFormations.ItemsSource = null;//lbListeCompetences.Items.Clear();
            _listeFormations = new List<SelectionFormation>();
            if (pSf == null)
            {
                //MAJ                
                foreach (Formation form in CtrlGestionECF.getListFormations())
                {
                    SelectionFormation uneForm = new SelectionFormation();
                    uneForm.Formation = form;
                    uneForm.IsChecked = false;
                    if (((GestionECF)instanceFenetre.InstanceFenetreEnCours).lbFormations.Items.Contains(form))
                    {
                        uneForm.IsChecked = true;
                    }

                    _listeFormations.Add(uneForm);
                }
                lbListeFormations.ItemsSource = _listeFormations;
            }
            else
            {
                _listeFormations.Add(pSf);
                lbListeFormations.ItemsSource = _listeFormations;
            }
            
        }
        private void refresh()
        {
            lbListeFormations.ItemsSource = null;
            lbListeFormations.Items.Clear();
            lbListeFormations.ItemsSource = _listeFormations;
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
            CtrlGestionECF.supprimerLiensFormations(((GestionECF)instanceFenetre.InstanceFenetreEnCours).EcfCourant);
            
            //On recréé tous les liens avec les compétences sélectionnées
            foreach (SelectionFormation selForm in _listeFormations)
            {
                if (selForm.IsChecked)
                {
                    CtrlGestionECF.ajouterLienFormation(((GestionECF)instanceFenetre.InstanceFenetreEnCours).EcfCourant, selForm.Formation);
                }
            }
            Close();
        }
        private void btSelect_Click(object sender, RoutedEventArgs e)
        {
            foreach (SelectionFormation selForm in _listeFormations)
            {
                selForm.IsChecked = true;
            }
            refresh();
        }
        private void btDeselect_Click(object sender, RoutedEventArgs e)
        {
            foreach (SelectionFormation selForm in _listeFormations)
            {
                selForm.IsChecked = false;
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
