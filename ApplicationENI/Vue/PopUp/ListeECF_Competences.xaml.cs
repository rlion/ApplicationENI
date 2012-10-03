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
    /// Logique d'interaction pour ListeECF_Competences.xaml
    /// </summary>
    public partial class ListeECF_Competences : Window
    {
        //exemple : http://merill.net/2009/10/wpf-checked-listbox/

        private List<SelectionCompetence> _listeCompetences;
        private List<SelectionCompetence> ListeCompetences
        {
            get { return _listeCompetences; }
            set { _listeCompetences = value; }
        }

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
        }

        public ListeECF_Competences()
        {
            InitializeComponent();

            ActualiseAffichage();
        }

        private void ActualiseAffichage()
        {
            //lbListeCompetences.Items.Clear();
            lbListeCompetences.ItemsSource = null;
            _listeCompetences = new List<SelectionCompetence>();
            foreach (Competence comp in CompetencesDAL.getListCompetences())
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

        private void btAnnuler_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btAjouter_Click(object sender, RoutedEventArgs e)
        {
            AjoutECF_Competence popUp = new AjoutECF_Competence();
            popUp.ShowDialog();

            ActualiseAffichage();
        }

        private void btSupprimer_Click(object sender, RoutedEventArgs e)
        {
            String message = "";

            //TODO confirmer la suppression
            foreach (SelectionCompetence selComp in _listeCompetences)
            {
                if (selComp.IsChecked)
                {
                    message = CompetencesDAL.supprimerCompetence(selComp.Competence);
                }
            }
            if (message.Trim() != "")
            {
                MessageBox.Show(message);
            }
            else
            {
                ActualiseAffichage();
            }
        }

        private void btValider_Click(object sender, RoutedEventArgs e)
        {
            ECFDAL.supprimerLiens(((GestionECF)instanceFenetre.InstanceFenetreEnCours).EcfCourant);
            
            foreach (SelectionCompetence selComp in _listeCompetences)
            {
                if (selComp.IsChecked)
                {
                    ECFDAL.ajouterLien(((GestionECF)instanceFenetre.InstanceFenetreEnCours).EcfCourant, selComp.Competence);
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
        private void refresh()
        {
            lbListeCompetences.ItemsSource = null;
            lbListeCompetences.Items.Clear();
            lbListeCompetences.ItemsSource = _listeCompetences;
        }
    }
}
