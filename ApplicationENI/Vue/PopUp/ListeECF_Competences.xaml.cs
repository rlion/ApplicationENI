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
        //private List<SelectionCompetence> _listeCompetences;
        //private List<SelectionCompetence> ListeCompetences
        //{
        //    get { return _listeCompetences; }
        //    set { _listeCompetences = value; }
        //}
        //private bool isInitAutoCompBox;
        private CtrlListeECF_Competences _ctrlListeECF_Competences = null;
        #endregion

        

        #region constructeur
        public ListeECF_Competences()
        {
            InitializeComponent();

            _ctrlListeECF_Competences = new CtrlListeECF_Competences();

            ActualiseAffichage(null);

            acbCompetence.ItemsSource = _ctrlListeECF_Competences.ListeCompetences;
            _ctrlListeECF_Competences.IsInitAutoCompBox = true;
        }
        #endregion

        #region affichage
        private void ActualiseAffichage(CtrlListeECF_Competences.SelectionCompetence pSc)
        {
            //RAZ
            lbListeCompetences.ItemsSource = null;//lbListeCompetences.Items.Clear();
            _ctrlListeECF_Competences.ListeCompetences = new List<CtrlListeECF_Competences.SelectionCompetence>();
            if (pSc == null)
            {
                //MAJ                
                //TODO double appel
                if (_ctrlListeECF_Competences.getListCompetences()!=null)
                {
                    foreach (Competence comp in _ctrlListeECF_Competences.getListCompetences())
                    {
                        CtrlListeECF_Competences.SelectionCompetence uneComp = new CtrlListeECF_Competences.SelectionCompetence();
                        uneComp.Competence = comp;
                        uneComp.IsChecked = false;
                        if (((GestionECF)instanceFenetre.InstanceFenetreEnCours).lbCompetences.Items.Contains(comp))
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
            AjoutECF_Competence ajoutCompetence = new AjoutECF_Competence();
            ajoutCompetence.ShowDialog();

            ActualiseAffichage(null);
        }
        private void btSupprimer_Click(object sender, RoutedEventArgs e)
        {
            String message = "";

            //TODO confirmer la suppression
            if (_ctrlListeECF_Competences.ListeCompetences!=null)
            {
                foreach (CtrlListeECF_Competences.SelectionCompetence selComp in _ctrlListeECF_Competences.ListeCompetences)
                {
                    if (selComp.IsChecked)
                    {
                        message = _ctrlListeECF_Competences.supprimerCompetence(selComp.Competence);
                    }
                }
            }            

            //gestion erreur
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
            _ctrlListeECF_Competences.supprimerLiensCompetences(((GestionECF)instanceFenetre.InstanceFenetreEnCours).CtrlGestionECF.EcfCourant);
            
            //On recréé tous les liens avec les compétences sélectionnées
            if (_ctrlListeECF_Competences.ListeCompetences!=null)
            {
                foreach (CtrlListeECF_Competences.SelectionCompetence selComp in _ctrlListeECF_Competences.ListeCompetences)
                {
                    if (selComp.IsChecked)
                    {
                        _ctrlListeECF_Competences.ajouterLienCompetence(((GestionECF)instanceFenetre.InstanceFenetreEnCours).CtrlGestionECF.EcfCourant, selComp.Competence);
                    }
                }
            }
            
            Close();
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
        #endregion

        private void btFiltre_Click(object sender, RoutedEventArgs e)
        {
            ActualiseAffichage(null);
            refresh();
            btFiltre.IsEnabled = false;
            acbCompetence.IsEnabled = true;
        }
    }
}
