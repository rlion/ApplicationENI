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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using ApplicationENI.Modele;
using ApplicationENI;
using ApplicationENI.Controleur;

namespace ApplicationENI.Vue
{
    /// <summary>
    /// Logique d'interaction pour SyntheseECF.xaml
    /// </summary>
    public partial class SyntheseECF : UserControl
    {
        private Stagiaire _stagiaireEncours=null;
        private List<SessionECF> _lesSessionsECFsStag=null;
        private SessionECF _sessionSelectionnee = null;

        public SessionECF SessionSelectionnee
        {
            get { return _sessionSelectionnee; }
            set { _sessionSelectionnee = value; }
        }
        private Evaluation _evaluationSelectionnee = null;

        public Evaluation EvaluationSelectionnee
        {
            get { return _evaluationSelectionnee; }
            set { _evaluationSelectionnee = value; }
        }
        
        public SyntheseECF()
        {
            InitializeComponent();

            affichage();
        }

        private void affichage()
        {
            _stagiaireEncours = Parametres.Instance.stagiaire;
            _lesSessionsECFsStag = CtrlGestionECF.getListSessionsECFStagiaire(_stagiaireEncours);

            tvSynthese.Items.Clear();

            if (_lesSessionsECFsStag!=null)
            {
                foreach (SessionECF sess in _lesSessionsECFsStag)
                {
                    TreeViewItem tviSessionECF = new TreeViewItem();
                    tviSessionECF.Header = sess;
                    tvSynthese.Items.Add(tviSessionECF);
                    foreach (Competence comp in sess.Ecf.Competences)
                    {
                        TreeViewItem tviCompetenceNote = new TreeViewItem();
                        Evaluation eval = CtrlGestionECF.donneNote(sess, _stagiaireEncours, comp);

                        tviCompetenceNote.Header = eval;
                        tviSessionECF.Items.Add(tviCompetenceNote);
                        if (eval.Note != -1)
                        {
                            tviSessionECF.IsExpanded = true;
                        }
                    }
                    tvSynthese.Items.Add(null);//espacer les ECFs
                }
                //foreach (TreeViewItem item in tvSynthese.Items)
                //{
                //    item.IsExpanded = true;
                //}
                btModDate.IsEnabled = false;
                btModNote.IsEnabled = false;
            }            
        }

        private void tvSynthese_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (tvSynthese.SelectedItem!=null)
            {
                if (((TreeViewItem)tvSynthese.SelectedItem).Header.GetType() == typeof(SessionECF))
                {
                    if (((SessionECF)((TreeViewItem)tvSynthese.SelectedItem).Header).Date > DateTime.Now)
                    {
                        btModDate.IsEnabled = true;
                        btModNote.IsEnabled = false;
                    }
                    else
                    {
                        btModDate.IsEnabled = false;
                        btModNote.IsEnabled = false;
                    }
                }
                else if (((TreeViewItem)tvSynthese.SelectedItem).Header.GetType() == typeof(Evaluation))
                {
                    if (((Evaluation)((TreeViewItem)tvSynthese.SelectedItem).Header).Date < DateTime.Now)
                    {
                        btModDate.IsEnabled = false;
                        btModNote.IsEnabled = true;
                    }
                    else
                    {
                        btModDate.IsEnabled = false;
                        btModNote.IsEnabled = false;
                    }
                }
            }            
        }

        private void btModDate_Click(object sender, RoutedEventArgs e)
        {
            _sessionSelectionnee = (SessionECF)((TreeViewItem)tvSynthese.SelectedItem).Header;
            PopUp.ModifDateECF popUp = new PopUp.ModifDateECF();
            popUp.ShowDialog();
            _sessionSelectionnee = null;
            affichage();
        }

        private void btModNote_Click(object sender, RoutedEventArgs e)
        {
            _evaluationSelectionnee = (Evaluation)((TreeViewItem)tvSynthese.SelectedItem).Header;
            PopUp.ModifNoteECF popUp = new PopUp.ModifNoteECF();
            popUp.ShowDialog();
            _evaluationSelectionnee = null;
            affichage();
        }

        private void btExporter_Click(object sender, RoutedEventArgs e)
        {
            _stagiaireEncours = Parametres.Instance.stagiaire;

            List<SessionECF> listeSessions = CtrlGestionECF.getListSessionsECFStagiaire(_stagiaireEncours);
            List<Evaluation> listeEvaluations = new List<Evaluation>();

            if (listeSessions != null)
            {
                foreach (SessionECF sess in listeSessions)
                {
                    foreach (Competence comp in sess.Ecf.Competences)
                    {
                        Evaluation eval = CtrlGestionECF.donneNote(sess, _stagiaireEncours, comp);
                        if (eval != null) listeEvaluations.Add(eval);
                    }
                }
            }

            string stagName = _stagiaireEncours._civilité + " " + _stagiaireEncours._nom + " " + _stagiaireEncours._prenom;

            Rapports.SyntheseECF rapport = new Rapports.SyntheseECF(listeSessions, listeEvaluations, stagName);
            rapport.Show();
        }


    }
}
