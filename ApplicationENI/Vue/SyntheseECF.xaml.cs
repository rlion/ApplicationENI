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
        CtrlSyntheseECF _ctrlSyntheseECF = null;
        public CtrlSyntheseECF CtrlSyntheseECF
        {
            get { return _ctrlSyntheseECF; }
            set { _ctrlSyntheseECF = value; }
        }
        
        public SyntheseECF()
        {
            InitializeComponent();

            _ctrlSyntheseECF = new CtrlSyntheseECF();

            affichage();
        }

        private void affichage()
        {
            _ctrlSyntheseECF.StagiaireEncours = Parametres.Instance.stagiaire;
            _ctrlSyntheseECF.LesSessionsECFsStag = _ctrlSyntheseECF.getListSessionsECFStagiaire(_ctrlSyntheseECF.StagiaireEncours);

            tvSynthese.Items.Clear();

            if (_ctrlSyntheseECF.LesSessionsECFsStag != null)
            {
                foreach (SessionECF sess in _ctrlSyntheseECF.LesSessionsECFsStag)
                {
                    TreeViewItem tviSessionECF = new TreeViewItem();
                    tviSessionECF.Header = sess;
                    tvSynthese.Items.Add(tviSessionECF);
                    if (sess.Ecf.Competences!=null)
                    {
                        foreach (Competence comp in sess.Ecf.Competences)
                        {
                            TreeViewItem tviCompetenceNote = new TreeViewItem();
                            Evaluation eval = _ctrlSyntheseECF.donneNote(sess, _ctrlSyntheseECF.StagiaireEncours, comp);

                            tviCompetenceNote.Header = eval;
                            tviSessionECF.Items.Add(tviCompetenceNote);
                            if (eval.Note != -1)
                            {
                                tviSessionECF.IsExpanded = true;
                            }
                        }
                        tvSynthese.Items.Add(null);//espacer les Sessions ECF
                    }                    
                }
                btModDate.IsEnabled = false;
                btModNote.IsEnabled = false;
                btExporter.IsEnabled = true;
            }
            else
            {
                btExporter.IsEnabled = false;
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
            _ctrlSyntheseECF.SessionSelectionnee = (SessionECF)((TreeViewItem)tvSynthese.SelectedItem).Header;
            PopUp.ModifDateECF popUp = new PopUp.ModifDateECF(_ctrlSyntheseECF.SessionSelectionnee);
            popUp.ShowDialog();
            _ctrlSyntheseECF.SessionSelectionnee = null;
            affichage();
        }

        private void btModNote_Click(object sender, RoutedEventArgs e)
        {
            _ctrlSyntheseECF.EvaluationSelectionnee = (Evaluation)((TreeViewItem)tvSynthese.SelectedItem).Header;
            PopUp.ModifNoteECF popUp = new PopUp.ModifNoteECF(_ctrlSyntheseECF.EvaluationSelectionnee);
            popUp.ShowDialog();
            _ctrlSyntheseECF.EvaluationSelectionnee = null;
            affichage();
        }

        private void btExporter_Click(object sender, RoutedEventArgs e)
        {
            _ctrlSyntheseECF.StagiaireEncours = Parametres.Instance.stagiaire;

            List<SessionECF> listeSessions = _ctrlSyntheseECF.getListSessionsECFStagiaire(_ctrlSyntheseECF.StagiaireEncours);
            List<Evaluation> listeEvaluations = new List<Evaluation>();

            if (listeSessions != null)
            {
                foreach (SessionECF sess in listeSessions)
                {
                    if (sess.Ecf.Competences!=null)
                    {
                        foreach (Competence comp in sess.Ecf.Competences)
                        {
                            Evaluation eval = _ctrlSyntheseECF.donneNote(sess, _ctrlSyntheseECF.StagiaireEncours, comp);
                            if (eval != null) listeEvaluations.Add(eval);
                        }
                    }                    
                }
            }

            string stagName = _ctrlSyntheseECF.StagiaireEncours._civilité + " " + _ctrlSyntheseECF.StagiaireEncours._nom + " " + _ctrlSyntheseECF.StagiaireEncours._prenom;

            Rapports.SyntheseECF rapport = new Rapports.SyntheseECF(listeSessions, listeEvaluations, stagName);
            rapport.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            rapport.Show();
        }

    }
}
