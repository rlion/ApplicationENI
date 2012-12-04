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
        //private List<SessionECF> _listeSessionECFs = null;
        ////private ECF _ecfCourant = null;
        //private List<DateTime> _planif = null;
        //private SessionECF _sessionECFcourant = null;
        //private Evaluation _evaluationEnCours = null;
        CtrlSaisieResultats _ctrlSaisieResultats = null;
        #endregion

        #region constructeur
        public SaisieResultats()
        {
            InitializeComponent();

            _ctrlSaisieResultats = new CtrlSaisieResultats();

            //cbECF
            List<ECF> lesEpreuves = null;
            _ctrlSaisieResultats.ListeSessionECFs = _ctrlSaisieResultats.getListSessionsECFs();
            if (_ctrlSaisieResultats.ListeSessionECFs!=null)
            {
                foreach (SessionECF sessEcf in _ctrlSaisieResultats.ListeSessionECFs)
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
            _ctrlSaisieResultats.SessionECFcourant = new SessionECF();
            _ctrlSaisieResultats.SessionECFcourant.Ecf = (ECF)cbECF.SelectedItem; //_ecfCourant;
            _ctrlSaisieResultats.SessionECFcourant.Date = DateTime.MinValue;
            _ctrlSaisieResultats.SessionECFcourant.Version = 0;
            //liste de sessions ECF (correspondant à l'ecf)
            _ctrlSaisieResultats.ListeSessionECFs = new List<SessionECF>();
            _ctrlSaisieResultats.ListeSessionECFs = _ctrlSaisieResultats.getListSessionsECF(_ctrlSaisieResultats.SessionECFcourant.Ecf);//_ecfCourant);

            affichageCalendrier(DateTime.MinValue);
            affichage();

            calendrier.Visibility = Visibility.Visible;
        }
        //2 Choix de la date
        private void calendrier_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!calendrier.SelectedDate.HasValue) return;

            _ctrlSaisieResultats.SessionECFcourant.Date = DateTime.MinValue;
            _ctrlSaisieResultats.SessionECFcourant.Version = 0;
            DateTime dateSel = calendrier.SelectedDate.Value;
            
            //On remet les dates planifiées
            affichageCalendrier(dateSel);

            if (!_ctrlSaisieResultats.Planif.Contains(dateSel))
            {
                MessageBox.Show("Cette date n'est pas planifiée pour l'ECF " + _ctrlSaisieResultats.SessionECFcourant.Ecf.ToString());                
            }
            else
            {
                _ctrlSaisieResultats.SessionECFcourant.Date = dateSel;
            }
            affichage();
        }
        //3 Choix de la version
        private void cbVersions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbVersions.SelectedItem != null)
            {
                _ctrlSaisieResultats.SessionECFcourant.Version = (int)cbVersions.SelectedItem;
                _ctrlSaisieResultats.SessionECFcourant.Id = _ctrlSaisieResultats.donneIdSessionECF(_ctrlSaisieResultats.SessionECFcourant.Ecf, _ctrlSaisieResultats.SessionECFcourant.Date, _ctrlSaisieResultats.SessionECFcourant.Version);

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
            btnAnnu.IsEnabled = true;
            
            if (lbCompetences.SelectedItem != null && lbStagiaires.SelectedItem != null)
            {
                gbNote.IsEnabled = true;
                gbNote.Visibility = Visibility.Visible;
                btnEnregistrer.IsEnabled = true;

                if (_ctrlSaisieResultats.SessionECFcourant.Ecf.NotationNumerique)
                {
                    lbNote.Visibility = Visibility.Visible;
                    tbNote.Visibility = Visibility.Visible;
                    lbSurVingt.Visibility = Visibility.Visible;
                    tbNote.Background = Brushes.White;
                    rbAcquis.Visibility = Visibility.Hidden;
                    rbEnCours.Visibility = Visibility.Hidden;
                    rbNonAcquis.Visibility = Visibility.Hidden;
                }
                else
                {                    
                    lbNote.Visibility = Visibility.Hidden;
                    tbNote.Visibility = Visibility.Hidden;
                    lbSurVingt.Visibility = Visibility.Hidden;
                    rbAcquis.Visibility = Visibility.Visible;
                    rbEnCours.Visibility = Visibility.Visible;
                    rbNonAcquis.Visibility = Visibility.Visible;                    
                }

                _ctrlSaisieResultats.EvaluationEnCours = _ctrlSaisieResultats.donneNote(_ctrlSaisieResultats.SessionECFcourant, (Stagiaire)lbStagiaires.SelectedItem, (Competence)lbCompetences.SelectedItem);//Evaluation(new Evaluation(_sessionECFcourant.Ecf, (Competence)lbCompetences.SelectedItem, (Stagiaire)lbStagiaires.SelectedItem));
                if (_ctrlSaisieResultats.EvaluationEnCours != null && _ctrlSaisieResultats.EvaluationEnCours.Note != -1)
                {
                    if (!_ctrlSaisieResultats.SessionECFcourant.Ecf.NotationNumerique)
                    {
                        if (_ctrlSaisieResultats.EvaluationEnCours.Note == Ressources.CONSTANTES.NOTE_ACQUIS)
                        {
                            rbAcquis.IsChecked = true;
                        }
                        else if (_ctrlSaisieResultats.EvaluationEnCours.Note == Ressources.CONSTANTES.NOTE_ENCOURS_ACQUISITION)
                        {
                            rbEnCours.IsChecked = true;
                        }
                        else if (_ctrlSaisieResultats.EvaluationEnCours.Note == Ressources.CONSTANTES.NOTE_NON_ACQUIS)
                        {
                            rbNonAcquis.IsChecked = true;
                        }
                    }
                    else
                    {
                        tbNote.Text = _ctrlSaisieResultats.EvaluationEnCours.Note.ToString();
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
                lbSurVingt.Visibility = Visibility.Hidden;
                rbAcquis.Visibility = Visibility.Hidden;
                rbEnCours.Visibility = Visibility.Hidden;
                rbNonAcquis.Visibility = Visibility.Hidden;                
            }
        }
        //5 Enregistrer
        private void btnEnregistrer_Click(object sender, RoutedEventArgs e)
        {
            if (_ctrlSaisieResultats.SessionECFcourant.Date > DateTime.Now)
            {
                if(MessageBox.Show("L'ECF est planifié dans le futur, souhaitez vous néanmoins saisir une note?", "Attention!", MessageBoxButton.YesNo, MessageBoxImage.Exclamation) == MessageBoxResult.Yes)
                {
                    float note = -1;

                    if (!_ctrlSaisieResultats.SessionECFcourant.Ecf.NotationNumerique)
                    {
                        if(rbAcquis.IsChecked == true)
                        {
                            note = Ressources.CONSTANTES.NOTE_ACQUIS;
                        }
                        else if(rbEnCours.IsChecked == true)
                        {
                            note = Ressources.CONSTANTES.NOTE_ENCOURS_ACQUISITION;
                        }
                        else if(rbNonAcquis.IsChecked == true)
                        {
                            note = Ressources.CONSTANTES.NOTE_NON_ACQUIS;
                        }
                    }
                    else
                    {
                        float noteSaisie;

                        tbNote.Text = tbNote.Text.Replace('.', ',');
                        bool estNumerique = float.TryParse(tbNote.Text, out noteSaisie);

                        if(noteSaisie >= 0 && noteSaisie <= 20 && estNumerique)
                        {
                            note = noteSaisie;
                            tbNote.Background = Brushes.White;
                        }
                        else
                        {
                            tbNote.Background = Brushes.Red;
                            tbNote.Focus();
                        }
                    }
                    if(note != -1)
                    {
                        _ctrlSaisieResultats.EvaluationEnCours.Note = note;
                        _ctrlSaisieResultats.modifierNoteEvaluation(_ctrlSaisieResultats.EvaluationEnCours, _ctrlSaisieResultats.EvaluationEnCours.Note);
                        btnAnnu.IsEnabled = false;
                    }            
                }
            }            
        }
        private void btnAjouter_Click(object sender, RoutedEventArgs e)
        {
            PopUp.AjoutSessionECF popup = new PopUp.AjoutSessionECF();
            popup.ShowDialog();

            _ctrlSaisieResultats.SessionECFcourant = null;
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
            _ctrlSaisieResultats.Planif = new List<DateTime>();
            if (_ctrlSaisieResultats.ListeSessionECFs!=null)
            {
                foreach (SessionECF sess in _ctrlSaisieResultats.ListeSessionECFs)
                {
                    _ctrlSaisieResultats.Planif.Add(sess.Date);
                }
            }
            
            //calendrier
            //Pour éviter les boucles infinies, on se désabonne momentanément à l'event
            calendrier.SelectedDatesChanged -= calendrier_SelectedDatesChanged;
            calendrier.SelectedDates.Clear();
            if (_ctrlSaisieResultats.Planif!=null)
            {
                foreach (DateTime date in _ctrlSaisieResultats.Planif)
                {
                    calendrier.SelectedDates.Add(date);
                }
            }
            
            calendrier.SelectedDates.Add(dateSelectionnee);
            //Pour éviter les boucles infinies, on se désabonne momentanément à l'event
            calendrier.SelectedDatesChanged += calendrier_SelectedDatesChanged;
        }

        private void affichage()
        {
            btnAnnu.IsEnabled = false;
            if (_ctrlSaisieResultats.SessionECFcourant == null)
            {
                //cbECF
                cbECF.SelectionChanged -= cbECF_SelectionChanged;
                //cbECF.Items.Clear();
                List<ECF> lesEpreuves = null;
                _ctrlSaisieResultats.ListeSessionECFs = _ctrlSaisieResultats.getListSessionsECFs();
                if (_ctrlSaisieResultats.ListeSessionECFs!=null)
                {
                    foreach (SessionECF sessEcf in _ctrlSaisieResultats.ListeSessionECFs)
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
            if (_ctrlSaisieResultats.SessionECFcourant.Date != DateTime.MinValue && _ctrlSaisieResultats.SessionECFcourant.Version != 0)
            {
                lbDateSession.Content = "Epreuve du " + _ctrlSaisieResultats.SessionECFcourant.Date.ToShortDateString();
                chargerCompetences();
                chargerStagiaires();
            }
            //La date est choisie (mais pas la version)
            else if (_ctrlSaisieResultats.SessionECFcourant.Date != DateTime.MinValue && _ctrlSaisieResultats.SessionECFcourant.Version == 0)
            {
                chargerVersions();

                lbDateSession.Content = "";
                lbCompetences.ItemsSource = null;
                gbCompetences.IsEnabled = false;
                lbStagiaires.ItemsSource = null;
                gbStagiaires.IsEnabled = false;
            }
            //Ni la date ni la version sont choisies
            else
            {
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
            //_listeSessionECFs = CtrlGestionECF.donneSessionsECFJour(_sessionECFcourant.Ecf, _sessionECFcourant.Date);
            cbVersions.Items.Clear();
            //TODO eviter le double appel donneSECF
            if (_ctrlSaisieResultats.donneSessionsECFJour(_ctrlSaisieResultats.SessionECFcourant.Ecf, _ctrlSaisieResultats.SessionECFcourant.Date)!=null)
            {
                foreach (SessionECF session in _ctrlSaisieResultats.donneSessionsECFJour(_ctrlSaisieResultats.SessionECFcourant.Ecf, _ctrlSaisieResultats.SessionECFcourant.Date))
                {
                    cbVersions.Items.Add(session.Version);
                }
            }
            
        }
        private void chargerCompetences()
        {
            gbCompetences.IsEnabled = true;
            lbCompetences.ItemsSource = _ctrlSaisieResultats.SessionECFcourant.Ecf.Competences;
        }
        private void chargerStagiaires()
        {
            gbStagiaires.IsEnabled = true;
            _ctrlSaisieResultats.SessionECFcourant.Participants = _ctrlSaisieResultats.getListParticipants(_ctrlSaisieResultats.SessionECFcourant);
            lbStagiaires.ItemsSource = _ctrlSaisieResultats.SessionECFcourant.Participants;
        }

        private void tbNote_GotFocus(object sender, RoutedEventArgs e)
        {
            //tbNote.SelectionStart = 0;
            //tbNote.SelectionLength = tbNote.Text.Length;
            tbNote.SelectAll();
        }

        private void btnAnnu_Click(object sender, RoutedEventArgs e)
        {
            tbNote.Background = Brushes.White;
            if (_ctrlSaisieResultats.SessionECFcourant.Ecf!=null && _ctrlSaisieResultats.SessionECFcourant.Date!=DateTime.MinValue && _ctrlSaisieResultats.SessionECFcourant.Version!=0 && (Stagiaire)lbStagiaires.SelectedItem!=null && (Competence)lbCompetences.SelectedItem!=null)
            {
                Evaluation eval = _ctrlSaisieResultats.donneNote(_ctrlSaisieResultats.SessionECFcourant, (Stagiaire)lbStagiaires.SelectedItem, (Competence)lbCompetences.SelectedItem);
                float noteSaisie;
                bool b = float.TryParse(tbNote.Text, out noteSaisie);
                if (eval.Note != noteSaisie)
                {
                    if (!_ctrlSaisieResultats.SessionECFcourant.Ecf.NotationNumerique)
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
            }
            
            //else
            //{
            //    _evaluationEnCours = null;
            //    _listeSessionECFs = null;
            //    _planif = null;
            //    _sessionECFcourant = null;
            //    affichage();
            //}
        }
    }
}
