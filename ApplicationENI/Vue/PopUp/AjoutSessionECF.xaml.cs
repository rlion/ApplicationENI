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
        private SessionECF _sessionECF = null;
        private List<SessionECF> _listeECFPlanif = null;
        private List<Stagiaire> _listeStagiaires = null;
        private List<Stagiaire> _listeParticipants = null;
        private List<DateTime> _planif=null;
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
            //Nouvelle sessionECF
            if (_sessionECF.Id == "")
            {
                //SessionECF sessionECFTemp = new SessionECF((ECF)cbECF.SelectedItem, (DateTime)_sessionECF.Date);
                if (lbParticipants.HasItems)
                {
                    _listeParticipants = new List<Stagiaire>();
                    foreach (Stagiaire stag in lbParticipants.Items)
                    {
                        _listeParticipants.Add(stag);
                    }
                    //sessionECFTemp.Participants = _listeParticipants;
                    _sessionECF.Participants = _listeParticipants;
                }
                CtrlGestionECF.ajouterSessionECF(_sessionECF);
            }
            else
            //Modification sessionECF
            {
                if (lbParticipants.HasItems)
                {
                    _listeParticipants = new List<Stagiaire>();
                    foreach (Stagiaire stag in lbParticipants.Items)
                    {
                        _listeParticipants.Add(stag);
                    }
                    _sessionECF.Participants = _listeParticipants;
                }
                CtrlGestionECF.ajouterParticipants(_sessionECF);
            }
            
            


            Close();
            
        }
        private void cbECF_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _sessionECF = new SessionECF();
            _sessionECF.Ecf = (ECF)cbECF.SelectedItem;

            //Versions
            cbVersions.IsEnabled = true;
            List<int> versions = new List<int>();
            for (int i = 1; i <= _sessionECF.Ecf.NbreVersion; i++)
            {
                versions.Add(i);
            }
            cbVersions.ItemsSource = versions;

            //Filtre Formations (de l'ECF)
            List<Formation> lesForms = new List<Formation>();
            cbFormation.ItemsSource = null;
            lesForms.Add(new Formation("0", "Toutes"));
            foreach (Formation form in _sessionECF.Ecf.Formations)
            {
                lesForms.Add(form);
            }
            cbFormation.ItemsSource = lesForms;
            
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
                lbParticipants.ItemsSource = null;
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

                _sessionECF.Date = dt;
                //L'utilisateur clique sur une date non planifiée
                if (!_planif.Contains((DateTime)dt))
                {
                    //MessageBox.Show("Création de la planification de l'ECF " + _sessionECF.ToString());
                    label3.Content = "Création de la planification de l'ECF";
                }
                else //L'utilisateur clique sur une date planifiée
                {                    
                    //MessageBox.Show("Modification de la planification de l'ECF " + _sessionECF.ToString());
                    label3.Content = "Modification de la planification de l'ECF";
                    _sessionECF.Id = CtrlGestionECF.donneIdSessionECF(_sessionECF.Ecf, _sessionECF.Date, SessionECF.Version);
                    lbParticipants.ItemsSource = null;
                    _listeParticipants = CtrlGestionECF.getListParticipants(_sessionECF);
                    lbParticipants.ItemsSource = _listeParticipants;
                }
                lbDateSession.Content = _sessionECF.Date.ToShortDateString();
                gbStagiaires.IsEnabled = true;
                btValider.IsEnabled = true;

                lbStagiaires.ItemsSource = null;
                _listeStagiaires = CtrlGestionECF.getListeStagiaires();
                lbStagiaires.ItemsSource = _listeStagiaires;
            }
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

        #region Filtres
        private void cbFormation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //afficherListeFiltree();
        }
        private void chkFC_Checked(object sender, RoutedEventArgs e)
        {
            //on force la coche des 2 types de formation car on ne peut pas etre ni lun ni lautre
            //if (chkCP.IsChecked == false && chkFC.IsChecked == false)
            //{
            //    chkCP.IsChecked = true;
            //    chkFC.IsChecked = true;
            //}
        }
        private void chkCP_Checked(object sender, RoutedEventArgs e)
        {
            //on force la coche des 2 types de formation car on ne peut pas etre ni lun ni lautre
            if (chkCP.IsChecked == false && chkFC.IsChecked == false)
            {
                chkCP.IsChecked = true;
                chkFC.IsChecked = true;
            }
        }
        private void btRefresh_Click(object sender, RoutedEventArgs e)
        {
            label4.Content = "Liste (filtrée) :";
            //on force la coche des 2 types de formation car on ne peut pas etre ni lun ni lautre
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
            if(chkCP.IsChecked==true && chkFC.IsChecked==true){
                _listeStagiaires = CtrlGestionECF.getListeStagiaires((Formation)cbFormation.SelectedItem, Ressources.CONSTANTES.TYPE_FORMATION_TOUS, acbNomPrenom.Text);
            }
            else if (chkCP.IsChecked == true && chkFC.IsChecked == false)
            {
                _listeStagiaires = CtrlGestionECF.getListeStagiaires((Formation)cbFormation.SelectedItem, Ressources.CONSTANTES.TYPE_FORMATION_CP, acbNomPrenom.Text);
            }
            else if (chkCP.IsChecked==false && chkFC.IsChecked==true)
            {
                _listeStagiaires = CtrlGestionECF.getListeStagiaires((Formation)cbFormation.SelectedItem, Ressources.CONSTANTES.TYPE_FORMATION_FC, acbNomPrenom.Text);
            }
            //else if (chkCP.IsChecked==false && chkFC.IsChecked==false)
            //{
                
            //    _listeStagiaires = CtrlGestionECF.getListeStagiaires((Formation)cbFormation.SelectedItem, Ressources.CONSTANTES.TYPE_FORMATION_TOUS, acbNomPrenom.Text);
            //}
            
            lbStagiaires.ItemsSource = _listeStagiaires;
        }
        #endregion

        private void cbVersions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _sessionECF.Version = (int)cbVersions.SelectedItem;

            //sessions
            _listeECFPlanif = CtrlGestionECF.getListSessionsECFVersion(_sessionECF.Ecf, _sessionECF.Version);
            _planif = new List<DateTime>();
            calendrier.SelectedDates.Clear();
            lbDateSession.Content = "";
            label3.Content = "";
            //TODO RAZ
            _planif.Clear();
            calendrier.SelectedDates.Clear();
            _listeParticipants=null;
            lbParticipants.ItemsSource = null;
            foreach (SessionECF sessECF in _listeECFPlanif)
            {
                _planif.Add(sessECF.Date);
                calendrier.SelectedDates.Add(sessECF.Date);
            }

            calendrier.IsEnabled = true;
        }     
    }        
}
