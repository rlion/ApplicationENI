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
        //private ECF _ecfCourant = null;
        private List<DateTime> _planif = null;
        private SessionECF _sessionECFcourant = null;
        #endregion

        #region constructeur
        public SaisieResultats()
        {
            InitializeComponent();

            //cbECF
            List<ECF> lesEpreuves = null;
            _listeSessionECFs = CtrlGestionECF.getListSessionsECFs();
            foreach (SessionECF sessEcf in _listeSessionECFs)
            {
                if (lesEpreuves==null) lesEpreuves = new List<ECF>();
                if (lesEpreuves.Count==0)
                {
                    lesEpreuves.Add(sessEcf.Ecf);
                }
                else if (!lesEpreuves.Contains(sessEcf.Ecf))
                {
                    lesEpreuves.Add(sessEcf.Ecf);
                }
            }
            //a tester si mm nom mais prenom different
            //http://www.developerfusion.com/code/5513/sorting-and-searching-using-c-lists/
            //http://blog.rapiddg.com/2009/04/sorting-a-list-of-objects-on-multiple-properties-c/
            lesEpreuves.Sort(delegate(ECF ecf1, ECF ecf2)
            {
                return ecf1.Code.CompareTo(ecf2.Code);
            });
            cbECF.ItemsSource = lesEpreuves;
        }
        #endregion

        #region Evenements

        //1 Selection de l'ECF
        private void cbECF_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            calendrier.IsEnabled = true;

            //ECF courant
            //_ecfCourant = new ECF();
            //_ecfCourant = (ECF)cbECF.SelectedItem;
            //session ECFCourant
            _sessionECFcourant = new SessionECF();
            _sessionECFcourant.Ecf = (ECF)cbECF.SelectedItem; //_ecfCourant;
            //liste de sessions ECF (correspondant à l'ecf)
            _listeSessionECFs = new List<SessionECF>();
            _listeSessionECFs = CtrlGestionECF.getListSessionsECF(_sessionECFcourant.Ecf);//_ecfCourant);

            affichageCalendrier(DateTime.MinValue);
            affichage();

            calendrier.Visibility = Visibility.Visible;
        }
        //2 Choix de la date
        private void calendrier_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!calendrier.SelectedDate.HasValue) return;

            DateTime dateSel = calendrier.SelectedDate.Value;
            
            //On remet les dates planifiées
            affichageCalendrier(dateSel);

            if (!_planif.Contains(dateSel))
            {
                MessageBox.Show("Cette date n'est pas planifiée pour l'ECF " + _sessionECFcourant.Ecf.ToString());
                _sessionECFcourant.Date = DateTime.MinValue;                
            }
            else
            {
                _sessionECFcourant.Date = dateSel;
            }
            affichage();
        }
        //3 Choix de la version
        private void cbVersions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbVersions.SelectedItem != null)
            {
                _sessionECFcourant.Version = (int)cbVersions.SelectedItem;
                _sessionECFcourant.Id = CtrlGestionECF.donneIdSessionECF(_sessionECFcourant.Ecf, _sessionECFcourant.Date, _sessionECFcourant.Version);

                affichage();
            }
        }
        //4 Choix du stagiaire/de le compétence
        private void lbCompetences_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            noteStagiaire();
        }
        private void lbStagiaires_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            noteStagiaire();
        }
        private void noteStagiaire()
        {
            if (lbCompetences.SelectedItem != null && lbStagiaires.SelectedItem != null)
            {
                gbNote.IsEnabled = true;
                gbNote.Visibility = Visibility.Visible;
                btnEnregistrer.IsEnabled = true;
                
                if (_sessionECFcourant.Ecf.NotationNumerique)
                {
                    lbNote.Visibility = Visibility.Visible;
                    tbNote.Visibility = Visibility.Visible;                    
                    rbAcquis.Visibility = Visibility.Hidden;
                    rbEnCours.Visibility = Visibility.Hidden;
                    rbNonAcquis.Visibility = Visibility.Hidden;
                }
                else
                {                    
                    lbNote.Visibility = Visibility.Hidden;
                    tbNote.Visibility = Visibility.Hidden;
                    rbAcquis.Visibility = Visibility.Visible;
                    rbEnCours.Visibility = Visibility.Visible;
                    rbNonAcquis.Visibility = Visibility.Visible;                    
                }

                Evaluation eval = CtrlGestionECF.donneNote(_sessionECFcourant, (Stagiaire)lbStagiaires.SelectedItem, (Competence)lbCompetences.SelectedItem);//Evaluation(new Evaluation(_sessionECFcourant.Ecf, (Competence)lbCompetences.SelectedItem, (Stagiaire)lbStagiaires.SelectedItem));
                if (eval != null && eval.Note!=-1)
                {
                    if (!_sessionECFcourant.Ecf.NotationNumerique)
                    {
                        if (eval.Note == Ressources.CONSTANTES.NOTE_ACQUIS)
                        {
                            rbAcquis.IsChecked = true;
                        }
                        else if (eval.Note == Ressources.CONSTANTES.NOTE_ENCOURS_ACQUISITION)
                        {
                            rbEnCours.IsChecked = true;
                        }
                        else if (eval.Note == Ressources.CONSTANTES.NOTE_NON_ACQUIS)
                        {
                            rbNonAcquis.IsChecked = true;
                        }
                    }
                    else
                    {
                        tbNote.Text = eval.Note.ToString();
                        tbNote.Focus();
                    }
                }
                else
                {
                    rbAcquis.IsChecked = false;
                    rbEnCours.IsChecked = false;
                    rbNonAcquis.IsChecked = false;
                    tbNote.Text = "";
                    tbNote.Focus();
                }
            }
            else
            {
                gbNote.IsEnabled = false;
                btnEnregistrer.IsEnabled = false;
                gbNote.Visibility = Visibility.Hidden;
                lbNote.Visibility = Visibility.Hidden;
                tbNote.Visibility = Visibility.Hidden;
                rbAcquis.Visibility = Visibility.Hidden;
                rbEnCours.Visibility = Visibility.Hidden;
                rbNonAcquis.Visibility = Visibility.Hidden;                
            }
        }
        //5 Enregistrer
        private void btnEnregistrer_Click(object sender, RoutedEventArgs e)
        {
            int note = -1;
            //TODO verif valeur numerique entre 0 et 20
            if (!_sessionECFcourant.Ecf.NotationNumerique)
            {
                if (rbAcquis.IsChecked == true)
                {
                    note = Ressources.CONSTANTES.NOTE_ACQUIS;
                }
                else if (rbEnCours.IsChecked == true)
                {
                    note = Ressources.CONSTANTES.NOTE_ENCOURS_ACQUISITION;
                }
                else if (rbNonAcquis.IsChecked == true)
                {
                    note = Ressources.CONSTANTES.NOTE_NON_ACQUIS;
                }
            }
            else
            {
                note = Convert.ToInt32(tbNote.Text);
            }
            Evaluation eval = new Evaluation(_sessionECFcourant.Ecf, (Competence)lbCompetences.SelectedItem, (Stagiaire)lbStagiaires.SelectedItem, Convert.ToInt32(cbVersions.SelectedItem), note, _sessionECFcourant.Date);
            CtrlGestionECF.ajouterEvaluation(eval);
        }
        private void btnAjouter_Click(object sender, RoutedEventArgs e)
        {
            PopUp.AjoutSessionECF popup = new PopUp.AjoutSessionECF();
            popup.ShowDialog();

            _sessionECFcourant = null;
            affichage();
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

        private void affichageCalendrier(DateTime dateSelectionnee)
        {
            _planif = new List<DateTime>();
            foreach (SessionECF sess in _listeSessionECFs)
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

        private void affichage()
        {
            if (_sessionECFcourant==null)
            {
                //cbECF
                cbECF.SelectionChanged -= cbECF_SelectionChanged;
                //cbECF.Items.Clear();
                List<ECF> lesEpreuves = null;
                _listeSessionECFs = CtrlGestionECF.getListSessionsECFs();
                foreach (SessionECF sessEcf in _listeSessionECFs)
                {
                    if (lesEpreuves == null) lesEpreuves = new List<ECF>();
                    if (lesEpreuves.Count == 0)
                    {
                        lesEpreuves.Add(sessEcf.Ecf);
                    }
                    else if (!lesEpreuves.Contains(sessEcf.Ecf))
                    {
                        lesEpreuves.Add(sessEcf.Ecf);
                    }
                }
                //a tester si mm nom mais prenom different
                //http://www.developerfusion.com/code/5513/sorting-and-searching-using-c-lists/
                //http://blog.rapiddg.com/2009/04/sorting-a-list-of-objects-on-multiple-properties-c/
                lesEpreuves.Sort(delegate(ECF ecf1, ECF ecf2)
                {
                    return ecf1.Code.CompareTo(ecf2.Code);
                });
                cbECF.ItemsSource = lesEpreuves;
                cbECF.SelectedItem = null;
                cbECF.SelectionChanged += cbECF_SelectionChanged;

                calendrier.IsEnabled = false;
                calendrier.SelectedDates.Clear();
                lbDateSession.Content = "";
                cbVersions.Items.Clear();
                gbProprietes.IsEnabled = false;
                lbCompetences.ItemsSource = null;
                gbCompetences.IsEnabled = false;
                lbStagiaires.ItemsSource = null;
                gbStagiaires.IsEnabled = false;
                return;
            }
            
            //La date et la version sont choisies
            if (_sessionECFcourant.Date != DateTime.MinValue && _sessionECFcourant.Version != 0)
            {
                lbDateSession.Content = "Epreuve du " + _sessionECFcourant.Date.ToShortDateString();
                chargerCompetences();
                chargerStagiaires();
            }
            //La date est choisie (mais pas la version)
            else if (_sessionECFcourant.Date != DateTime.MinValue && _sessionECFcourant.Version == 0)
            {
                chargerVersions();

                _sessionECFcourant.Version = 0;

                lbDateSession.Content = "";
                lbCompetences.ItemsSource = null;
                gbCompetences.IsEnabled = false;
                lbStagiaires.ItemsSource = null;
                gbStagiaires.IsEnabled = false;
            }
            //Ni la date ni la version sont choisies
            else
            {
                _sessionECFcourant.Date = DateTime.MinValue;
                _sessionECFcourant.Version = 0;

                lbDateSession.Content = "";
                cbVersions.Items.Clear();
                gbProprietes.IsEnabled = false;
                lbCompetences.ItemsSource = null;
                gbCompetences.IsEnabled = false;
                lbStagiaires.ItemsSource = null;
                gbStagiaires.IsEnabled = false;
            }
        }

        private void chargerVersions()
        {
            gbProprietes.IsEnabled = true;
            _listeSessionECFs = CtrlGestionECF.donneSessionsECFJour(_sessionECFcourant.Ecf, _sessionECFcourant.Date);
            cbVersions.Items.Clear();
            foreach (SessionECF session in _listeSessionECFs)
            {
                cbVersions.Items.Add(session.Version);
            }
        }
        private void chargerCompetences()
        {
            gbCompetences.IsEnabled = true;
            lbCompetences.ItemsSource = _sessionECFcourant.Ecf.Competences;
        }
        private void chargerStagiaires()
        {
            gbStagiaires.IsEnabled = true;
            _sessionECFcourant.Participants = CtrlGestionECF.getListParticipants(_sessionECFcourant);
            lbStagiaires.ItemsSource = _sessionECFcourant.Participants;
        }

        private void tbNote_GotFocus(object sender, RoutedEventArgs e)
        {
            //tbNote.SelectionStart = 0;
            //tbNote.SelectionLength = tbNote.Text.Length;
            tbNote.SelectAll();
        }
    }
}
