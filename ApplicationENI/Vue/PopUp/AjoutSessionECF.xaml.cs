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
    /// Logique d'interaction pour AjoutSessionECF.xaml
    /// </summary>
    public partial class AjoutSessionECF : Window
    {
        private SessionECF _sessionECF = null;
        private List<SessionECF> _listeECFPlanif = null;
        private List<DateTime> _planif = null;
        
        public AjoutSessionECF()
        {
            InitializeComponent();

            cbECF.ItemsSource = CtrlGestionECF.getListECFs();
        }

        

        //1 Selection de l'ECF
        private void cbECF_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _sessionECF = new SessionECF();
            _sessionECF.Ecf = (ECF)cbECF.SelectedItem;

            affichage();
        }
        //2 Selection de la version
        private void cbVersions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _sessionECF.Date = DateTime.MinValue;
            _sessionECF.Version = (int)cbVersions.SelectedItem;
            _listeECFPlanif = CtrlGestionECF.getListSessionsECFVersion(_sessionECF.Ecf, _sessionECF.Version);
            affichage();
        }

        private void affichage()
        {
            btValider.IsEnabled = false;
            
            //La date et la version sont choisies
            if (_sessionECF.Version != 0 && _sessionECF.Date != DateTime.MinValue)
            {
                chargerStagiaires();

                if (_sessionECF.Id == 0)
                {
                    lbDateSession.Content = "Création épreuve du " + _sessionECF.Date.ToShortDateString();
                    lbParticipants.ItemsSource = null;
                }
                else
                {
                    lbDateSession.Content = "Modification épreuve du " + _sessionECF.Date.ToShortDateString();
                    chargerParticipants();
                }
                btValider.IsEnabled = true;
                    
            }
            //La version est choisie (mais pas la date)
            else if (_sessionECF.Version != 0 && _sessionECF.Date == DateTime.MinValue)
            {
                affichageCalendrier(DateTime.MinValue);

                _sessionECF.Date = DateTime.MinValue;

                lbDateSession.Content = "";
                lbStagiaires.ItemsSource = null;
                lbParticipants.ItemsSource = null;
                cbFormation.Items.Clear();
                tbNomPrenom.Text = "";
                chkCP.IsChecked = true;
                chkFC.IsChecked = true;
                gbStagiaires.IsEnabled = false;

                
            }
            //Ni la date ni la version sont choisies
            else
            {
                chargerVersions();
                
                _sessionECF.Date = DateTime.MinValue;
                _sessionECF.Version = 0;
                _planif = null;
                _listeECFPlanif = null;
                calendrier.IsEnabled = false;
                calendrier.SelectedDates.Clear();

                lbDateSession.Content = "";
                lbStagiaires.ItemsSource = null;
                lbParticipants.ItemsSource = null;
                cbFormation.Items.Clear();
                tbNomPrenom.Text = "";
                chkCP.IsChecked = true;
                chkFC.IsChecked = true;
                gbStagiaires.IsEnabled = false;                
            }
        }

        private void chargerStagiaires()
        {
            gbStagiaires.IsEnabled = true;
            tbNomPrenom.Text = "";
            chkCP.IsChecked = true;
            chkFC.IsChecked = true;
            cbFormation.Items.Clear();
            cbFormation.Items.Add(new Formation("0", "Toutes"));
            foreach (Formation form in _sessionECF.Ecf.Formations)
            {
                cbFormation.Items.Add(form);
            }
            lbStagiaires.ItemsSource = CtrlGestionECF.getListeStagiaires();
        }
        private void affichageCalendrier(DateTime dateSelectionnee)
        {
            calendrier.IsEnabled = true;
            _planif = new List<DateTime>();
            foreach (SessionECF sess in _listeECFPlanif)
            {
                _planif.Add(sess.Date);
            }
            //calendrier
            //Pour éviter les boucles infinies, on se désabonne momentanément à l'event
            calendrier.SelectedDatesChanged -= calendrier_SelectedDatesChanged;
            calendrier.SelectedDates.Clear();
            foreach (DateTime date in _planif)
            {
                calendrier.SelectedDates.Add(date);
            }
            calendrier.SelectedDates.Add(dateSelectionnee);

            //Pour éviter les boucles infinies, on se désabonne momentanément à l'event
            calendrier.SelectedDatesChanged += calendrier_SelectedDatesChanged;
        }
        private void chargerVersions()
        {
            cbVersions.IsEnabled = true;
            cbVersions.SelectionChanged -= cbVersions_SelectionChanged;
            cbVersions.Items.Clear();
            for (int i = 1; i <= _sessionECF.Ecf.NbreVersion; i++)
            {
                cbVersions.Items.Add(i);
            }
            cbVersions.SelectionChanged += cbVersions_SelectionChanged;
        }

        private void calendrier_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!calendrier.SelectedDate.HasValue) return;

            DateTime dateSel = calendrier.SelectedDate.Value;

            //On remet les dates planifiées
            affichageCalendrier(dateSel);

            if (!_planif.Contains(dateSel))
            {
                //création d'une planif
                _sessionECF.Id = 0;
                _sessionECF.Date = dateSel;
            }
            else
            {
                _sessionECF.Date = dateSel;
                _sessionECF.Id = CtrlGestionECF.donneIdSessionECF(_sessionECF.Ecf, _sessionECF.Date, _sessionECF.Version);
            }
            affichage();
        }

        private void chargerParticipants()
        {
            lbParticipants.ItemsSource = CtrlGestionECF.getListParticipants(_sessionECF);
        }

        private void btValider_Click(object sender, RoutedEventArgs e)
        {
            List<Stagiaire> listeParticipants = null;
            
            //Nouvelle sessionECF
            if (_sessionECF.Id == 0)
            {
                if (lbParticipants.HasItems)
                {
                    listeParticipants = new List<Stagiaire>();
                    foreach (Stagiaire stag in lbParticipants.Items)
                    {
                        listeParticipants.Add(stag);
                    }
                    _sessionECF.Participants = listeParticipants;
                }
                CtrlGestionECF.ajouterSessionECF(_sessionECF);
            }
            else
            //Modification sessionECF
            {
                if (lbParticipants.HasItems)
                {
                    listeParticipants = new List<Stagiaire>();
                    foreach (Stagiaire stag in lbParticipants.Items)
                    {
                        listeParticipants.Add(stag);
                    }
                    _sessionECF.Participants = listeParticipants;
                }
                CtrlGestionECF.ajouterParticipants(_sessionECF);
            }
            Close();
        }

        private void btAnnuler_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btRefresh_Click(object sender, RoutedEventArgs e)
        {
            //Si aucun des 2 types de formation n'est coché 
            //on force la coche des 2 (car on ne peut pas etre ni lun ni lautre)
            if (chkCP.IsChecked == false && chkFC.IsChecked == false)
            {
                chkCP.IsChecked = true;
                chkFC.IsChecked = true;
            }

            afficherListeFiltree();
        }

        private void afficherListeFiltree()
        {
            lbStagiaires.ItemsSource = null;
            if (chkCP.IsChecked == true && chkFC.IsChecked == true)
            {
                lbStagiaires.ItemsSource = CtrlGestionECF.getListeStagiaires((Formation)cbFormation.SelectedItem, Ressources.CONSTANTES.TYPE_FORMATION_TOUS, tbNomPrenom.Text);
            }
            else if (chkCP.IsChecked == true && chkFC.IsChecked == false)
            {
                lbStagiaires.ItemsSource = CtrlGestionECF.getListeStagiaires((Formation)cbFormation.SelectedItem, Ressources.CONSTANTES.TYPE_FORMATION_CP, tbNomPrenom.Text);
            }
            else if (chkCP.IsChecked == false && chkFC.IsChecked == true)
            {
                lbStagiaires.ItemsSource = CtrlGestionECF.getListeStagiaires((Formation)cbFormation.SelectedItem, Ressources.CONSTANTES.TYPE_FORMATION_FC, tbNomPrenom.Text);
            }
        }

        private void btAjouter_Click(object sender, RoutedEventArgs e)
        {
            List<Stagiaire> listeParticipants =(List<Stagiaire>)lbParticipants.ItemsSource;
            
            foreach (Stagiaire stag in lbStagiaires.SelectedItems)
            {
                if (listeParticipants == null) listeParticipants = new List<Stagiaire>();

                if(!listeParticipants.Contains(stag)){
                    listeParticipants.Add(stag);
                }
            }

            //a tester si mm nom mais prenom different
            //http://www.developerfusion.com/code/5513/sorting-and-searching-using-c-lists/
            //http://blog.rapiddg.com/2009/04/sorting-a-list-of-objects-on-multiple-properties-c/
            listeParticipants.Sort(delegate(Stagiaire s1, Stagiaire s2) 
            { 
                return s1._nom.CompareTo(s2._nom) != 0
                    ? s1._nom.CompareTo(s2._nom)
                    : s1._prenom.CompareTo(s2._prenom); 
            });
            
            lbParticipants.ItemsSource = null;
            lbParticipants.ItemsSource = listeParticipants;    
        }

        private void btEnlever_Click(object sender, RoutedEventArgs e)
        {
            List<Stagiaire> listeParticipants = (List<Stagiaire>)lbParticipants.ItemsSource; 
            
            foreach (Stagiaire stag in lbParticipants.SelectedItems)
            {
                listeParticipants.Remove(stag);
            }
            lbParticipants.ItemsSource = null;
            lbParticipants.ItemsSource = listeParticipants;
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
    }        
}
