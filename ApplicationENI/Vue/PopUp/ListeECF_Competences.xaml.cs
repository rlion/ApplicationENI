﻿using System;
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
        }
        #endregion

        #region constructeur
        public ListeECF_Competences()
        {
            InitializeComponent();

            ActualiseAffichage();
        }
        #endregion

        #region affichage
        private void ActualiseAffichage()
        {
            //RAZ
            lbListeCompetences.ItemsSource = null;//lbListeCompetences.Items.Clear();
            
            //MAJ
            _listeCompetences = new List<SelectionCompetence>();
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
                ActualiseAffichage();
            }
        }
        private void btValider_Click(object sender, RoutedEventArgs e)
        {
            //On supprime tous les liens avec l'ECF courant
            CtrlGestionECF.supprimerLiens(((GestionECF)instanceFenetre.InstanceFenetreEnCours).EcfCourant);
            
            //On recréé tous les liens avec les compétences sélectionnées
            foreach (SelectionCompetence selComp in _listeCompetences)
            {
                if (selComp.IsChecked)
                {
                    CtrlGestionECF.ajouterLien(((GestionECF)instanceFenetre.InstanceFenetreEnCours).EcfCourant, selComp.Competence);
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
        #endregion
    }
}
