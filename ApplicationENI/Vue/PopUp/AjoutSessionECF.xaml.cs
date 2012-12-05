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
        //private SessionECF _sessionECF = null;
        //private List<SessionECF> _listeECFPlanif = null;
        //private List<DateTime> _planif = null;
        private CtrlAjoutSessionECF _ctrlAjoutSessionECF = null;
        
        public AjoutSessionECF()
        {
            InitializeComponent();
            _ctrlAjoutSessionECF = new CtrlAjoutSessionECF();

            cbECF.ItemsSource = _ctrlAjoutSessionECF.getListECFs();
        }

        

        //1 Selection de l'ECF
        private void cbECF_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _ctrlAjoutSessionECF.SessionECF= new SessionECF();
            _ctrlAjoutSessionECF.SessionECF.Ecf = (ECF)cbECF.SelectedItem;

            affichage();
        }
        //2 Selection de la version
        private void cbVersions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _ctrlAjoutSessionECF.SessionECF.Date = DateTime.MinValue;
            _ctrlAjoutSessionECF.SessionECF.Version = (int)cbVersions.SelectedItem;
            _ctrlAjoutSessionECF.ListeECFPlanif = _ctrlAjoutSessionECF.getListSessionsECFVersion(_ctrlAjoutSessionECF.SessionECF.Ecf, _ctrlAjoutSessionECF.SessionECF.Version);
            affichage();
        }

        private void affichage()
        {
            btValider.IsEnabled = false;
            
            //La date et la version sont choisies
            if (_ctrlAjoutSessionECF.SessionECF.Version != 0 && _ctrlAjoutSessionECF.SessionECF.Date != DateTime.MinValue)
            {
                chargerStagiaires();

                if (_ctrlAjoutSessionECF.SessionECF.Id == 0)
                {
                    lbDateSession.Content = "Création épreuve du " + _ctrlAjoutSessionECF.SessionECF.Date.ToShortDateString();
                    lbParticipants.ItemsSource = null;
                }
                else
                {
                    lbDateSession.Content = "Modification épreuve du " + _ctrlAjoutSessionECF.SessionECF.Date.ToShortDateString();
                    chargerParticipants();
                }
                btValider.IsEnabled = true;
                    
            }
            //La version est choisie (mais pas la date)
            else if (_ctrlAjoutSessionECF.SessionECF.Version != 0 && _ctrlAjoutSessionECF.SessionECF.Date == DateTime.MinValue)
            {
                affichageCalendrier(DateTime.MinValue);

                _ctrlAjoutSessionECF.SessionECF.Date = DateTime.MinValue;

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

                _ctrlAjoutSessionECF.SessionECF.Date = DateTime.MinValue;
                _ctrlAjoutSessionECF.SessionECF.Version = 0;
                _ctrlAjoutSessionECF.Planif = null;
                _ctrlAjoutSessionECF.ListeECFPlanif = null;
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
            if (_ctrlAjoutSessionECF.SessionECF.Ecf.Formations!=null)
            {
                foreach (Formation form in _ctrlAjoutSessionECF.SessionECF.Ecf.Formations)
                {
                    cbFormation.Items.Add(form);
                }
            }
            
            lbStagiaires.ItemsSource = _ctrlAjoutSessionECF.getListeStagiaires();
        }
        private void affichageCalendrier(DateTime dateSelectionnee)
        {
            calendrier.IsEnabled = true;
            _ctrlAjoutSessionECF.Planif = new List<DateTime>();
            if (_ctrlAjoutSessionECF.ListeECFPlanif!=null)
            {
                foreach (SessionECF sess in _ctrlAjoutSessionECF.ListeECFPlanif)
                {
                    _ctrlAjoutSessionECF.Planif.Add(sess.Date);
                }
            }
            
            //calendrier
            //Pour éviter les boucles infinies, on se désabonne momentanément à l'event
            calendrier.SelectedDatesChanged -= calendrier_SelectedDatesChanged;
            calendrier.SelectedDates.Clear();
            if (_ctrlAjoutSessionECF.Planif!=null)
            {
                foreach (DateTime date in _ctrlAjoutSessionECF.Planif)
                {
                    calendrier.SelectedDates.Add(date);
                }
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
            for (int i = 1; i <= _ctrlAjoutSessionECF.SessionECF.Ecf.NbreVersion; i++)
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

            if (!_ctrlAjoutSessionECF.Planif.Contains(dateSel))
            {
                //création d'une planif
                _ctrlAjoutSessionECF.SessionECF.Id = 0;
                _ctrlAjoutSessionECF.SessionECF.Date = dateSel;
            }
            else
            {
                _ctrlAjoutSessionECF.SessionECF.Date = dateSel;
                _ctrlAjoutSessionECF.SessionECF.Id = _ctrlAjoutSessionECF.donneIdSessionECF(_ctrlAjoutSessionECF.SessionECF.Ecf, _ctrlAjoutSessionECF.SessionECF.Date, _ctrlAjoutSessionECF.SessionECF.Version);
            }
            affichage();
        }

        private void chargerParticipants()
        {
            lbParticipants.ItemsSource = _ctrlAjoutSessionECF.getListParticipants(_ctrlAjoutSessionECF.SessionECF);
        }

        private void btValider_Click(object sender, RoutedEventArgs e)
        {
            List<Stagiaire> listeParticipants = null;
            List<Stagiaire> lesParticipantsNonAjoutes = null;
            
            //Nouvelle sessionECF
            if (_ctrlAjoutSessionECF.SessionECF.Id == 0)
            {
                if (lbParticipants.HasItems)
                {
                    listeParticipants = new List<Stagiaire>();
                    if (lbParticipants.Items!=null)
                    {
                        foreach (Stagiaire stag in lbParticipants.Items)
                        {
                            listeParticipants.Add(stag);
                        }
                    }
                    
                    _ctrlAjoutSessionECF.SessionECF.Participants = listeParticipants;
                }
                lesParticipantsNonAjoutes = _ctrlAjoutSessionECF.ajouterSessionECF(_ctrlAjoutSessionECF.SessionECF);
            }
            else
            //Modification sessionECF
            {
                if (lbParticipants.HasItems)
                {
                    listeParticipants = new List<Stagiaire>();
                    if (lbParticipants.Items!=null)
                    {
                        foreach (Stagiaire stag in lbParticipants.Items)
                        {
                            listeParticipants.Add(stag);
                        }
                    }
                    
                    _ctrlAjoutSessionECF.SessionECF.Participants = listeParticipants;
                }
                lesParticipantsNonAjoutes = _ctrlAjoutSessionECF.ajouterParticipants(_ctrlAjoutSessionECF.SessionECF);
            }

            if(lesParticipantsNonAjoutes!=null)
            {
                String reponse="Les stagiaires suivants n'ont pas pu être ajoutés car ils ont déjà effectué cette ECF dans la même version :";
                if (lesParticipantsNonAjoutes!=null)
                {
                    foreach (Stagiaire stagiaireNonAjoute in lesParticipantsNonAjoutes)
                    {
                        if (lbParticipants.Items!=null)
                        {
                            foreach(Stagiaire stag in lbParticipants.Items)
                            {
                                if(stag == stagiaireNonAjoute)
                                {
                                    listeParticipants.Remove(stag);
                                    reponse += "\n" + stagiaireNonAjoute.ToString();
                                }                                
                            }
                            //lbParticipants.Items.Remove(stagiaireNonAjoute);
                            //reponse += "\n" + stagiaireNonAjoute.ToString();
                        }                        
                    }
                }
                
                MessageBox.Show(reponse,"Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
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
                lbStagiaires.ItemsSource = _ctrlAjoutSessionECF.getListeStagiaires((Formation)cbFormation.SelectedItem, Ressources.CONSTANTES.TYPE_FORMATION_TOUS, tbNomPrenom.Text);
            }
            else if (chkCP.IsChecked == true && chkFC.IsChecked == false)
            {
                lbStagiaires.ItemsSource = _ctrlAjoutSessionECF.getListeStagiaires((Formation)cbFormation.SelectedItem, Ressources.CONSTANTES.TYPE_FORMATION_CP, tbNomPrenom.Text);
            }
            else if (chkCP.IsChecked == false && chkFC.IsChecked == true)
            {
                lbStagiaires.ItemsSource = _ctrlAjoutSessionECF.getListeStagiaires((Formation)cbFormation.SelectedItem, Ressources.CONSTANTES.TYPE_FORMATION_FC, tbNomPrenom.Text);
            }
        }

        private void btAjouter_Click(object sender, RoutedEventArgs e)
        {
            List<Stagiaire> listeParticipants =(List<Stagiaire>)lbParticipants.ItemsSource;

            if (lbStagiaires.SelectedItems!=null)
            {
                foreach (Stagiaire stag in lbStagiaires.SelectedItems)
                {
                    if (listeParticipants == null) listeParticipants = new List<Stagiaire>();

                    if (!listeParticipants.Contains(stag))
                    {
                        listeParticipants.Add(stag);
                    }
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

            if (lbParticipants.SelectedItems!=null)
            {
                foreach (Stagiaire stag in lbParticipants.SelectedItems)
                {
                    listeParticipants.Remove(stag);
                }
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
