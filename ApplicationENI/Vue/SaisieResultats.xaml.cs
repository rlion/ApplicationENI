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
        private ECF _ecfCourant = null;
        private List<DateTime> _planif = null;
        private SessionECF _sessionECFcourant = null;
        #endregion

        #region constructeur
        public SaisieResultats()
        {
            InitializeComponent();

            //cbECF
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
        #endregion

        #region Evenements

        //1 Selection de l'ECF
        private void cbECF_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            calendrier.IsEnabled = true;

            //ECF courant
            _ecfCourant = new ECF();
            _ecfCourant = (ECF)cbECF.SelectedItem;
            //session ECFCourant
            _sessionECFcourant = new SessionECF();
            _sessionECFcourant.Ecf = _ecfCourant;
            //liste de sessions ECF (correspondant à l'ecf)
            _listeSessionECFs = new List<SessionECF>();
            _listeSessionECFs = CtrlGestionECF.getListSessionsECF(_ecfCourant);
            //date planifiées
            _planif = new List<DateTime>();
            foreach (SessionECF sess in _listeSessionECFs)
            {
                _planif.Add(sess.Date);
            }
            //calendrier
            calendrier.SelectedDates.Clear();
            foreach (DateTime date in _planif)
            {
                calendrier.SelectedDates.Add(date);
            }
            //compétences
            lbCompetences.ItemsSource = null;
            lbCompetences.ItemsSource = _ecfCourant.Competences;


            calendrier.Visibility = Visibility.Visible;
        }
        //2 Choix de la date
        private void calendrier_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            lbCompetences.IsEnabled = false;
            lbStagiaires.IsEnabled = false;

            lbStagiaires.ItemsSource = null;
            cbVersions.ItemsSource = null;

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
                    lbDateSession.Content = "";
                    gbProprietes.IsEnabled = false;
                    gbStagiaires.IsEnabled = false;
                    gbCompetences.IsEnabled = false;
                    lbStagiaires.ItemsSource = null;
                    lbCompetences.ItemsSource = null;
                    cbVersions.ItemsSource = null;
                    
                }
                else //L'utilisateur clique sur une date planifiée
                {
                    _sessionECFcourant.Date = dt;
                    List<SessionECF> sessionsECFJour = CtrlGestionECF.donneSessionsECFJour(_sessionECFcourant.Ecf, _sessionECFcourant.Date);
                    //TODO?? modifier aspect date selectionnée
                    lbDateSession.Content = "Epreuve du " + _sessionECFcourant.Date.ToShortDateString();
                    gbProprietes.IsEnabled = true;

                    cbVersions.IsEnabled = true;
                    List<int> versions = new List<int>();
                    foreach (SessionECF sessJ in sessionsECFJour)
                    {
                        versions.Add(sessJ.Version);
                    }
                    cbVersions.ItemsSource = versions;
                    if (cbVersions.Items.Count == 1) cbVersions.SelectedItem = cbVersions.Items[0];
                }

                if (_sessionECFcourant.Date != null && _sessionECFcourant.Version != 0)
                {
                    lbCompetences.ItemsSource = _sessionECFcourant.Ecf.Competences;
                    lbCompetences.IsEnabled = true;
                    lbStagiaires.ItemsSource = CtrlGestionECF.getListParticipants(_sessionECFcourant);
                    lbStagiaires.IsEnabled = true;
                }
            }
        }
        //3 Choix de la version
        private void cbVersions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lbCompetences.IsEnabled = false;
            lbStagiaires.IsEnabled = false;

            if (cbVersions.SelectedItem != null)
            {
                //afficheSessionECF((ECF)cbECF.SelectedItem);

                _sessionECFcourant.Version = (int)cbVersions.SelectedItem;
                _sessionECFcourant.Id = CtrlGestionECF.donneIdSessionECF(_sessionECFcourant.Ecf, _sessionECFcourant.Date, _sessionECFcourant.Version);

                //participants
                _sessionECFcourant.Participants = CtrlGestionECF.getListParticipants(_sessionECFcourant);
                lbStagiaires.ItemsSource = _sessionECFcourant.Participants;
                //TODO afficher reste
            }

            if (_sessionECFcourant.Date != null && _sessionECFcourant.Version != 0)
            {
                lbCompetences.ItemsSource = _sessionECFcourant.Ecf.Competences;
                lbCompetences.IsEnabled = true;
                lbStagiaires.ItemsSource = CtrlGestionECF.getListParticipants(_sessionECFcourant);
                lbStagiaires.IsEnabled = true;
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
                if (_sessionECFcourant.Ecf.NotationNumerique)
                {
                    lbNote.Visibility = Visibility.Visible;
                    tbNote.Visibility = Visibility.Visible;
                    rbAcquis.Visibility = Visibility.Hidden;
                    rbEnCours.Visibility = Visibility.Hidden;
                    rbNonAcquis.Visibility = Visibility.Hidden;
                    gbNote.IsEnabled = true;
                    //lbNote.IsEnabled = true;
                    //tbNote.IsEnabled = true;
                    //rbAcquis.IsEnabled = false;
                    //rbEnCours.IsEnabled = false;
                    //rbNonAcquis.IsEnabled = false;
                    btnEnregistrer.IsEnabled = true;
                }
                else
                {
                    lbNote.Visibility = Visibility.Hidden;
                    tbNote.Visibility = Visibility.Hidden;
                    rbAcquis.Visibility = Visibility.Visible;
                    rbEnCours.Visibility = Visibility.Visible;
                    rbNonAcquis.Visibility = Visibility.Visible;
                    gbNote.IsEnabled = true;
                    //lbNote.IsEnabled = false;
                    //tbNote.IsEnabled = false;
                    //rbAcquis.IsEnabled = true;
                    //rbEnCours.IsEnabled = true;
                    //rbNonAcquis.IsEnabled = true;
                    btnEnregistrer.IsEnabled = true;
                }

                Evaluation eval = CtrlGestionECF.donneEvaluation(new Evaluation(_ecfCourant, (Competence)lbCompetences.SelectedItem, (Stagiaire)lbStagiaires.SelectedItem));
                if (eval != null)
                {
                    if (!_ecfCourant.NotationNumerique)
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
                    }
                }
                else
                {
                    rbAcquis.IsChecked = false;
                    rbEnCours.IsChecked = false;
                    rbNonAcquis.IsChecked = false;
                    tbNote.Text = "";
                }
            }
            else
            {
                lbNote.Visibility = Visibility.Hidden;
                tbNote.Visibility = Visibility.Hidden;
                rbAcquis.Visibility = Visibility.Hidden;
                rbEnCours.Visibility = Visibility.Hidden;
                rbNonAcquis.Visibility = Visibility.Hidden;
                gbNote.IsEnabled = false;
                btnEnregistrer.IsEnabled = false;
            }
        }
        //5 Enregistrer
        private void btnEnregistrer_Click(object sender, RoutedEventArgs e)
        {
            int note = -1;
            //TODO verif valeur numerique entre 0 et 20
            if (!_ecfCourant.NotationNumerique)
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
            Evaluation eval = new Evaluation(_ecfCourant, (Competence)lbCompetences.SelectedItem, (Stagiaire)lbStagiaires.SelectedItem, Convert.ToInt32(cbVersions.SelectedItem), note, _sessionECFcourant.Date);
            CtrlGestionECF.ajouterEvaluation(eval);
        }

        private void btnAjouter_Click(object sender, RoutedEventArgs e)
        {
            PopUp.AjoutSessionECF popup = new PopUp.AjoutSessionECF();
            popup.ShowDialog();

            //if (popup.SessionECF != null)
            //{
            //    _ecfCourant = popup.SessionECF.Ecf;
            //    ActualiseAffichage();
            //    afficheSessionECF(_ecfCourant);
            //}

            //TODO RAZ
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
