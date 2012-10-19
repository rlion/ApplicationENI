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
using ApplicationENI.DAL;

namespace ApplicationENI.Vue
{
    /// <summary>
    /// Logique d'interaction pour SaisieResultats.xaml
    /// </summary>
    public partial class SaisieResultats : UserControl
    {        
        private List<SessionECF> _listeSessionECFs = null;
        private ECF _ecfCourant = null;
        private List<DateTime> _planif = new List<DateTime>();
        bool _datePlanif = false;
        private SessionECF _sessionECFcourant = null;
        
        public SaisieResultats()
        {
            InitializeComponent();

            
            //cbECF.ItemsSource = _listeSessionECFs; redondance

            ActualiseAffichage();

            calendar1.DisplayDateStart = DateTime.Now;        
        }

        private void ActualiseAffichage(){
            cbECF.ItemsSource = null;
            _listeSessionECFs = SessionECFDAL.getListSessionsECFs();
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
                calendar1.SelectedDates.Clear();

                foreach (SessionECF sessEcf in _listeSessionECFs)
                {
                    if (sessEcf.Ecf.Equals(_ecfCourant))
                    {
                        _planif.Add(sessEcf.Date);
                        calendar1.SelectedDates.Add(sessEcf.Date);
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

        private void cbECF_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            calendar1.IsEnabled = true;

            afficheSessionECF((ECF)cbECF.SelectedItem);
            //CalendarDateRange cdr = new CalendarDateRange(DateTime.Now, new DateTime(2030, DateTime.Now.Month, DateTime.Now.Day));
            calendar1.Visibility = Visibility.Visible;
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

        private void calendar1_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            calendar1.SelectedDatesChanged -= calendar1_SelectedDatesChanged;

            DateTime dt = new DateTime();
            Calendar cal=sender as Calendar;

            //
            if (!cal.SelectedDate.HasValue)
            {
                calendar1.SelectedDatesChanged += calendar1_SelectedDatesChanged;
                return;
            }
            
            dt = (DateTime)cal.SelectedDate.Value; 

            //reinit de l affichage des dates planifiees
            calendar1.SelectedDates.Clear();
            foreach (DateTime dateTemp in _planif)
            {
                calendar1.SelectedDates.Add(dateTemp);
            }
            calendar1.SelectedDatesChanged += calendar1_SelectedDatesChanged;

            
            
            //if (dt == DateTime.MinValue)
            //{
            //    calendar1.SelectedDatesChanged += calendar1_SelectedDatesChanged;
            //    return;
            //}
            

            //if (!dt.Equals(null))
            //{
                if (!_planif.Contains((DateTime)dt))
                {
                    MessageBox.Show("Cette date n'est pas planifiée pour l'ECF " + _ecfCourant.ToString());
                    //calendar1.Visibility = Visibility.Hidden;
                    lbCompetences.Visibility = Visibility.Hidden;
                    lbStagiaires.Visibility = Visibility.Hidden;
                    cbVersions.Visibility = Visibility.Hidden;
                    label1.Visibility = Visibility.Hidden;
                    groupBox1.Visibility = Visibility.Hidden;
                    lbDateSession.Visibility = Visibility.Hidden;
                }
                else
                {
                    _datePlanif = true;
                    _sessionECFcourant = new SessionECF(_ecfCourant,dt);
                    //TODO?? modifier aspect date selectionnée
                    lbDateSession.Content = "Epreuve du " + _sessionECFcourant.Date.ToShortDateString();
                    //calendar1.Visibility = Visibility.Visible;
                    lbCompetences.Visibility = Visibility.Visible;
                    lbStagiaires.Visibility = Visibility.Visible;
                    cbVersions.Visibility = Visibility.Visible;
                    label1.Visibility = Visibility.Visible;
                    groupBox1.Visibility = Visibility.Visible;
                    lbDateSession.Visibility = Visibility.Visible;
                }
            //}

            
            
            if (_datePlanif)
            {
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

        //http://stackoverflow.com/questions/5543119/wpf-button-takes-two-clicks-to-fire-click-event
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
