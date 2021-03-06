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
        #region Attributs
        private CtrlListeECF_Competences _ctrlListeECF_Competences = null;
        private List<Competence> _lesCompetencesSelectionnees = null;
        private ECF _EcfCourant = null;
        private bool _EcfAdd = false;
        #endregion           

        #region constructeur
        public ListeECF_Competences(List<Competence> pLesCompetences, ECF pECfCourant, bool pEcfAdd)
        {
            InitializeComponent();

            _ctrlListeECF_Competences = new CtrlListeECF_Competences();
            _lesCompetencesSelectionnees = pLesCompetences;
            _EcfCourant = pECfCourant;
            _EcfAdd = pEcfAdd;

            ActualiseAffichage(null);

            acbCompetence.ItemsSource = _ctrlListeECF_Competences.ListeCompetences;
            _ctrlListeECF_Competences.IsInitAutoCompBox = true;
        }
        #endregion

        #region affichage
        private void ActualiseAffichage(CtrlListeECF_Competences.SelectionCompetence pSc)
        {
            //RAZ
            lbListeCompetences.ItemsSource = null;
            _ctrlListeECF_Competences.ListeCompetences = new List<CtrlListeECF_Competences.SelectionCompetence>();
            if (pSc == null)
            {
                //MAJ                
                if (_ctrlListeECF_Competences.getListCompetences()!=null)
                {
                    foreach (Competence comp in _ctrlListeECF_Competences.getListCompetences())
                    {
                        CtrlListeECF_Competences.SelectionCompetence uneComp = new CtrlListeECF_Competences.SelectionCompetence();
                        uneComp.Competence = comp;
                        uneComp.IsChecked = false;
                        if (_lesCompetencesSelectionnees.Contains(comp))
                        {
                            uneComp.IsChecked = true;
                        }

                        _ctrlListeECF_Competences.ListeCompetences.Add(uneComp);
                    }
                }
                
                lbListeCompetences.ItemsSource = _ctrlListeECF_Competences.ListeCompetences;
            }
            else
            {
                _ctrlListeECF_Competences.ListeCompetences.Add(pSc);
                lbListeCompetences.ItemsSource = _ctrlListeECF_Competences.ListeCompetences;
            }
            
        }
        private void refresh()
        {
            lbListeCompetences.ItemsSource = null;
            lbListeCompetences.Items.Clear();
            lbListeCompetences.ItemsSource = _ctrlListeECF_Competences.ListeCompetences;
        }
        #endregion

        #region evenements
        private void btAnnuler_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void btAjouter_Click(object sender, RoutedEventArgs e)
        {
            AjoutECF_Competence ajoutCompetence = new AjoutECF_Competence(_EcfAdd);
            ajoutCompetence.ShowDialog();

            ActualiseAffichage(null);
        }
        private void btSupprimer_Click(object sender, RoutedEventArgs e)
        {
            String message = "";
            if (_ctrlListeECF_Competences.ListeCompetences!=null)
            {
                foreach (CtrlListeECF_Competences.SelectionCompetence selComp in _ctrlListeECF_Competences.ListeCompetences)
                {
                    if (selComp.IsChecked)
                    {
                        message = _ctrlListeECF_Competences.supprimerCompetence(selComp.Competence);
                        if (message!="")
                        {
                            MessageBox.Show(message, "Attention!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        }
                    }
                }
            }            

            //gestion erreur
            if (message.Trim() == "")
            {
                ActualiseAffichage(null);
            }
        }
        private void btValider_Click(object sender, RoutedEventArgs e)
        {
            //On supprime tous les liens avec l'ECF courant
            String reponse = _ctrlListeECF_Competences.supprimerLiensCompetences(_EcfCourant);
            if (reponse!="")
            {
                MessageBox.Show(reponse, "Attention!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }else{
                //On recréé tous les liens avec les compétences sélectionnées
                if (_ctrlListeECF_Competences.ListeCompetences!=null)
                {
                    foreach (CtrlListeECF_Competences.SelectionCompetence selComp in _ctrlListeECF_Competences.ListeCompetences)
                    {
                        if (selComp.IsChecked)
                        {
                            _ctrlListeECF_Competences.ajouterLienCompetence(_EcfCourant, selComp.Competence);
                        }
                    }
                }            
                Close();
            }
        }
        private void btSelect_Click(object sender, RoutedEventArgs e)
        {
            if (_ctrlListeECF_Competences.ListeCompetences!=null)
            {
                foreach (CtrlListeECF_Competences.SelectionCompetence selComp in _ctrlListeECF_Competences.ListeCompetences)
                {
                    selComp.IsChecked = true;
                }
            }
            
            refresh();
        }
        private void btDeselect_Click(object sender, RoutedEventArgs e)
        {
            if (_ctrlListeECF_Competences.ListeCompetences!=null)
            {
                foreach (CtrlListeECF_Competences.SelectionCompetence selComp in _ctrlListeECF_Competences.ListeCompetences)
                {
                    selComp.IsChecked = false;
                }
            }
            
            refresh();
        }
        private void autocbCompetence_GotFocus(object sender, RoutedEventArgs e)
        {
            if (_ctrlListeECF_Competences.IsInitAutoCompBox)
            {
                acbCompetence.Text = string.Empty;
                _ctrlListeECF_Competences.IsInitAutoCompBox = false;
            }
        }
        private void autocbCompetence_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (_ctrlListeECF_Competences.ListeCompetences!=null)
            {
                foreach (CtrlListeECF_Competences.SelectionCompetence sc in _ctrlListeECF_Competences.ListeCompetences)
                {
                    if (sc.Competence.Code == acbCompetence.Text.Substring(0, acbCompetence.Text.IndexOf(" - "))
                        && (sc.Competence.Libelle == acbCompetence.Text.Substring(acbCompetence.Text.IndexOf(" - ") + 3)))
                    {
                        ActualiseAffichage(sc);
                        refresh();
                    }
                }
            }
            
            acbCompetence.Text = "";
            btFiltre.IsEnabled = true;
            acbCompetence.IsEnabled = false;
        }
        private void btFiltre_Click(object sender, RoutedEventArgs e)
        {
            ActualiseAffichage(null);
            refresh();
            btFiltre.IsEnabled = false;
            acbCompetence.IsEnabled = true;
        }
        #endregion

    }
}
