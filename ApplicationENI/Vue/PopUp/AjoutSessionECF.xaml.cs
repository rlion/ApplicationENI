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
        #region proprietes
        private List<ECF> _listeECFs = null;
        private SessionECF _sessionECF = new SessionECF();
        private List<SessionECF> _listeECFPlanif = null;
        private List<Stagiaire> _listeStagiaires = new List<Stagiaire>();
        private List<Stagiaire> _listeParticipants = new List<Stagiaire>();
        private List<DateTime> _planif=new List<DateTime>();
        #endregion

        #region get/set
        public SessionECF SessionECF
        {
            get { return _sessionECF; }
            set { _sessionECF = value; }
        }
        public List<Stagiaire> ListeStagiaires
        {
            get { return _listeStagiaires; }
            set { _listeStagiaires = value; }
        }
        public List<Stagiaire> ListeParticipants
        {
            get { return _listeParticipants; }
            set { _listeParticipants = value; }
        }
        #endregion

        #region constructeur
        public AjoutSessionECF()
        {
            InitializeComponent();

            _listeECFs = CtrlGestionECF.getListECFs();
            cbECF.ItemsSource = _listeECFs;
                        
            //datePicker1.DisplayDateStart = DateTime.Now;
        }
        #endregion

        #region evenement
        private void btAnnuler_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btValider_Click(object sender, RoutedEventArgs e)
        {
            //TODO
            //SessionECF sessionECFTemp = new SessionECF();
            //sessionECFTemp.Ecf = (ECF)cbECF.SelectedItem;
            //sessionECFTemp.Date = (DateTime)datePicker1.SelectedDate;

            //if (sessionECFTemp.Date != DateTime.MinValue && sessionECFTemp.Ecf != null)
            //{
            //    _sessionECF = sessionECFTemp;
            //    CtrlGestionECF.ajouterSessionECF(sessionECFTemp);
            //    Close();
            //}
        }
        private void cbECF_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _sessionECF.Ecf = (ECF)cbECF.SelectedItem;
            _listeECFPlanif = CtrlGestionECF.getListSessionsECF(_sessionECF.Ecf);
            _planif = new List<DateTime>();
            calendrier.SelectedDates.Clear();
            
            foreach (SessionECF sessECF in _listeECFPlanif)
	        {
		        _planif.Add(sessECF.Date);
	        }

            List<Formation> lesForms = new List<Formation>();
            cbFormation.ItemsSource = null;
            foreach (Formation form in _sessionECF.Ecf.Formations)
            {
                lesForms.Add(form);
            }
            cbFormation.ItemsSource = lesForms;

            calendrier.IsEnabled = true;
            gbStagiaires.IsEnabled = true;

            lbStagiaires.ItemsSource = null;
            _listeStagiaires = CtrlGestionECF.getListeStagiaires();
            lbStagiaires.ItemsSource = _listeStagiaires;
        }
        #endregion

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
                foreach (DateTime date in _planif)
                {
                    calendrier.SelectedDates.Add(date);
                }

                //On se réabonne à l'event
                calendrier.SelectedDatesChanged += calendrier_SelectedDatesChanged;

                //L'utilisateur clique sur une date non planifiée
                if (!_planif.Contains((DateTime)dt))
                {
                    _sessionECF.Date = dt;
                    MessageBox.Show("Création de la planification de l'ECF " + _sessionECF.ToString());
                    lbDateSession.Content = _sessionECF.Date.ToShortDateString();

                    
                }
                else //L'utilisateur clique sur une date planifiée
                {
                    _sessionECF.Date = dt;
                    MessageBox.Show("Modification de la planification de l'ECF " + _sessionECF.ToString());
                    lbDateSession.Content = _sessionECF.Date.ToShortDateString();
                }
            }
        }

        private void rbFC_Checked(object sender, RoutedEventArgs e)
        {
            if (rbFC.IsChecked == true) {
                rbFC.IsChecked=false;
                rbCP.IsChecked=false;
            }
        }
        private void rbCP_Checked(object sender, RoutedEventArgs e)
        {
            if (rbCP.IsChecked == true)
            {
                rbFC.IsChecked = false;
                rbCP.IsChecked = false;
            }
        }

        private void cbFormation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btAjouter_Click(object sender, RoutedEventArgs e)
        {
            foreach (Stagiaire stag in lbStagiaires.SelectedItems)
            {
                if(!_listeParticipants.Contains(stag)){
                    _listeParticipants.Add(stag);
                }
            }

            //a tester si mm nom mais prenom different
            //http://www.developerfusion.com/code/5513/sorting-and-searching-using-c-lists/
            //http://blog.rapiddg.com/2009/04/sorting-a-list-of-objects-on-multiple-properties-c/
            _listeParticipants.Sort(delegate(Stagiaire s1, Stagiaire s2) 
            { 
                return s1._nom.CompareTo(s2._nom) != 0
                    ? s1._nom.CompareTo(s2._nom)
                    : s1._prenom.CompareTo(s2._prenom); 
            });
            
            lbParticipants.ItemsSource = null;
            lbParticipants.ItemsSource = _listeParticipants;
            
        }

        private void btEnlever_Click(object sender, RoutedEventArgs e)
        {
            lbParticipants.ItemsSource = null;
            foreach (Stagiaire stag in lbParticipants.SelectedItems)
            {
                _listeParticipants.Remove(stag);
            }
            lbParticipants.ItemsSource = _listeParticipants;
        }


    }        
}
