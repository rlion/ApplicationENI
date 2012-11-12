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
    /// Logique d'interaction pour ListeECF_Competences.xaml
    /// </summary>
    public partial class ListeECF_Competences : Window
    {
        //exemple : http://merill.net/2009/10/wpf-checked-listbox/

        #region propriété + get/set
        private List<SelectionCompetence> _listeCompetences;
        private List<SelectionCompetence> ListeCompetences
        {
            get { return _listeCompetences; }
            set { _listeCompetences = value; }
        }
        private bool isInitAutoCompBox;
        #endregion

        #region classe spéciale SelectionCompetence
        //classe listant l'ensemble des compétences (avec coche si elle est liée à l'ECF courant)
        private class SelectionCompetence 
        {
            private Competence _competence;           
            private bool _isChecked;

            public Competence Competence
            {
                get { return _competence; }
                set { _competence = value; }
            }
            public bool IsChecked
            {
                get { return _isChecked; }
                set { _isChecked = value; }
            }

            public override string ToString()
            {
                return _competence.Code + " - " + _competence.Libelle;
            }
        }
        #endregion

        #region constructeur
        public ListeECF_Competences()
        {
            InitializeComponent();

            ActualiseAffichage(null);

            autocbCompetence.ItemsSource = _listeCompetences;
            isInitAutoCompBox = true;
        }
        #endregion

        #region affichage
        private void ActualiseAffichage(SelectionCompetence pSc)
        {
            //RAZ
            lbListeCompetences.ItemsSource = null;//lbListeCompetences.Items.Clear();
            _listeCompetences = new List<SelectionCompetence>();
            if (pSc == null)
            {
                //MAJ                
                foreach (Competence comp in CtrlGestionECF.getListCompetences())
                {
                    SelectionCompetence uneComp = new SelectionCompetence();
                    uneComp.Competence = comp;
                    uneComp.IsChecked = false;
                    if (((GestionECF)instanceFenetre.InstanceFenetreEnCours).lbCompetences.Items.Contains(comp))
                    {
                        uneComp.IsChecked = true;
                    }

                    _listeCompetences.Add(uneComp);
                }
                lbListeCompetences.ItemsSource = _listeCompetences;
            }
            else
            {
                _listeCompetences.Add(pSc);
                lbListeCompetences.ItemsSource = _listeCompetences;
            }
            
        }
        private void refresh()
        {
            lbListeCompetences.ItemsSource = null;
            lbListeCompetences.Items.Clear();
            lbListeCompetences.ItemsSource = _listeCompetences;
        }
        #endregion

        #region evenements
        private void btAnnuler_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void btAjouter_Click(object sender, RoutedEventArgs e)
        {
            AjoutECF_Competence ajoutCompetence = new AjoutECF_Competence();
            ajoutCompetence.ShowDialog();

            ActualiseAffichage(null);
        }
        private void btSupprimer_Click(object sender, RoutedEventArgs e)
        {
            String message = "";

            //TODO confirmer la suppression
            foreach (SelectionCompetence selComp in _listeCompetences)
            {
                if (selComp.IsChecked)
                {
                    message = CtrlGestionECF.supprimerCompetence(selComp.Competence);
                }
            }

            //gestion erreur TODO revoir??
            if (message.Trim() != "")
            {
                MessageBox.Show(message);
            }
            else
            {
                ActualiseAffichage(null);
            }
        }
        private void btValider_Click(object sender, RoutedEventArgs e)
        {
            //On supprime tous les liens avec l'ECF courant
            CtrlGestionECF.supprimerLiensCompetences(((GestionECF)instanceFenetre.InstanceFenetreEnCours).EcfCourant);
            
            //On recréé tous les liens avec les compétences sélectionnées
            foreach (SelectionCompetence selComp in _listeCompetences)
            {
                if (selComp.IsChecked)
                {
                    CtrlGestionECF.ajouterLienCompetence(((GestionECF)instanceFenetre.InstanceFenetreEnCours).EcfCourant, selComp.Competence);
                }
            }
            Close();
        }
        private void btSelect_Click(object sender, RoutedEventArgs e)
        {
            foreach (SelectionCompetence selComp in _listeCompetences)
            {
                selComp.IsChecked = true;
            }
            refresh();
        }
        private void btDeselect_Click(object sender, RoutedEventArgs e)
        {
            foreach (SelectionCompetence selComp in _listeCompetences)
            {
                selComp.IsChecked = false;
            }
            refresh();
        }
        private void autocbCompetence_GotFocus(object sender, RoutedEventArgs e)
        {
            if (isInitAutoCompBox)
            {
                autocbCompetence.Text = string.Empty;
                isInitAutoCompBox = false;
            }
        }
        private void autocbCompetence_MouseUp(object sender, MouseButtonEventArgs e)
        {
            foreach (SelectionCompetence sc in _listeCompetences)
            {
                if (sc.Competence.Code == autocbCompetence.Text.Substring(0, autocbCompetence.Text.IndexOf(" - "))
                    && (sc.Competence.Libelle == autocbCompetence.Text.Substring(autocbCompetence.Text.IndexOf(" - ") + 3)))
                {
                    ActualiseAffichage(sc);
                    refresh();
                }
            }
        }
        #endregion

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            //TODO
        }
    }
}
