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
        
        public SyntheseECF()
        {
            InitializeComponent();

            affichage();
        }

        private void affichage()
        {
            _stagiaireEncours = Parametres.Instance.stagiaire;
            _lesSessionsECFsStag = CtrlGestionECF.getListSessionsECFStagiaire(_stagiaireEncours);

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
                }
            }
            foreach (TreeViewItem item in tvSynthese.Items)
            {
                item.IsExpanded = true;
            }

        }

        private void tvSynthese_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (tvSynthese.SelectedItem.GetType()==typeof(SessionECF))
            {
                btModDate.IsEnabled = true;
                btModNote.IsEnabled = false;
            }
            else if (tvSynthese.SelectedItem.GetType()==typeof(Evaluation))
            {
                btModDate.IsEnabled = false;
                btModNote.IsEnabled = true;
            }
        }

        private void btModDate_Click(object sender, RoutedEventArgs e)
        {
            //TODO
        }

        private void btModNote_Click(object sender, RoutedEventArgs e)
        {
            //TODO
        }


    }
}
