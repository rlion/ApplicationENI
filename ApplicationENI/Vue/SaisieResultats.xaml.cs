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
using System.IO;
using ApplicationENI.Modele;
using ApplicationENI.Controleur;

namespace ApplicationENI.Vue
{
    /// <summary>
    /// Logique d'interaction pour SaisieResultats.xaml
    /// </summary>
    public partial class SaisieResultats : UserControl
    {
        #region Propriétés
        private List<SessionECF> _listeSessionECFs = null;
        private ECF _ecfCourant = null;
        private List<DateTime> _planif = new List<DateTime>();
        private SessionECF _sessionECFcourant = null;
        #endregion

        #region constructeur
        public SaisieResultats()
        {
            InitializeComponent();

            //Affichage
            ActualiseAffichage();
            calendrier.DisplayDateStart = DateTime.Now;        
        }
        #endregion

        #region Affichage
        private void ActualiseAffichage(){
            //RAZ combo
            cbECF.ItemsSource = null;
            
            //MAJ combo
            _listeSessionECFs = CtrlGestionECF.getListSessionsECFs();
            foreach (SessionECF sessEcf in _listeSessionECFs)
            {
                if (!cbECF.HasItems)
                {
                    cbECF.Items.Add(sessEcf.Ecf);
                }
                else if (!cbECF.Items.Contains(sessEcf.Ecf))
                {
                    cbECF.Items.Add(sessEcf.Ecf);
                }
            }
        }
        private void afficheSessionECF(ECF Ecfcourant)
        {
            if (Ecfcourant != null)
            {
                _ecfCourant = Ecfcourant;
                cbECF.SelectedItem = _ecfCourant;

                _planif = new List<DateTime>();
                calendrier.SelectedDates.Clear();

                foreach (SessionECF sessEcf in _listeSessionECFs)
                {
                    if (sessEcf.Ecf.Equals(_ecfCourant))
                    {
                        _planif.Add(sessEcf.Date);
                        calendrier.SelectedDates.Add(sessEcf.Date);
                    }
                }

                lbCompetences.ItemsSource=_ecfCourant.Competences;

                lbCompetences.IsEnabled = true;
                lbStagiaires.IsEnabled = true;
                
            }
            else
            {
                //??TODO
            }
        }
        #endregion

        #region Evenements
        private void cbECF_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            calendrier.IsEnabled = true;

            afficheSessionECF((ECF)cbECF.SelectedItem);
            calendrier.Visibility = Visibility.Visible;
        }
        private void btnAjouter_Click(object sender, RoutedEventArgs e)
        {
            PopUp.AjoutSessionECF popup = new PopUp.AjoutSessionECF();
            popup.ShowDialog();

            if (popup.SessionECF != null)
            {
                _ecfCourant = popup.SessionECF.Ecf;
                ActualiseAffichage();
                afficheSessionECF(_ecfCourant);
            }
        }
        private void calendrier_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime dt = new DateTime();
            Calendar cal = sender as Calendar;
            
            //Pour éviter les boucles infinies, on se désabonne momentanément à l'event
            calendrier.SelectedDatesChanged -= calendrier_SelectedDatesChanged;

            //S'il n'y pas de valeurs, on se réabonne à l'event et on sort
            if (!cal.SelectedDate.HasValue)
            {
                calendrier.SelectedDatesChanged += calendrier_SelectedDatesChanged;
                return;
            }
            else
            {
                dt = (DateTime)cal.SelectedDate.Value;

                //MAJ de l'affichage des dates planifiees
                calendrier.SelectedDates.Clear();
                foreach (DateTime dateTemp in _planif)
                {
                    calendrier.SelectedDates.Add(dateTemp);
                }

                //On se réabonne à l'event
                calendrier.SelectedDatesChanged += calendrier_SelectedDatesChanged;

                //L'utilisateur clique sur une date non planifiée
                if (!_planif.Contains((DateTime)dt))
                {
                    MessageBox.Show("Cette date n'est pas planifiée pour l'ECF " + _ecfCourant.ToString());
                    lbCompetences.Visibility = Visibility.Hidden;
                    lbStagiaires.Visibility = Visibility.Hidden;
                    cbVersions.Visibility = Visibility.Hidden;
                    label1.Visibility = Visibility.Hidden;
                    groupBox1.Visibility = Visibility.Hidden;
                    lbDateSession.Visibility = Visibility.Hidden;
                }
                else //L'utilisateur clique sur une date planifiée
                {
                    _sessionECFcourant = new SessionECF(_ecfCourant, dt);
                    //TODO?? modifier aspect date selectionnée
                    lbDateSession.Content = "Epreuve du " + _sessionECFcourant.Date.ToShortDateString();
                    lbCompetences.Visibility = Visibility.Visible;
                    lbStagiaires.Visibility = Visibility.Visible;
                    cbVersions.Visibility = Visibility.Visible;
                    label1.Visibility = Visibility.Visible;
                    groupBox1.Visibility = Visibility.Visible;
                    lbDateSession.Visibility = Visibility.Visible;

                    cbVersions.IsEnabled = true;
                    List<int> versions = new List<int>();
                    for (int i = 1; i <= _ecfCourant.NbreVersion; i++)
                    {
                        versions.Add(i);
                    }
                    cbVersions.ItemsSource = versions;

                    //TODO afficher reste
                }
            }
        }
        //http://stackoverflow.com/questions/5543119/wpf-button-takes-two-clicks-to-fire-click-event
        //Permet d'éviter d'avoir à cliquer 2 fois alors que le focus était sur le calendrier
        // un clic pour sortir et un réel (cf. combobox)
        protected override void OnPreviewMouseUp(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseUp(e);
            if (Mouse.Captured is Calendar || Mouse.Captured is System.Windows.Controls.Primitives.CalendarItem)
            {
                Mouse.Capture(null);
            }
        }
        #endregion
    }
}
