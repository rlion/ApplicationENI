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
    /// Logique d'interaction pour ModifNoteECF.xaml
    /// </summary>
    public partial class ModifNoteECF : Window
    {
        Evaluation _evaluation = null;
        Stagiaire _stagaire = null;

        public ModifNoteECF()
        {
            InitializeComponent();

            _evaluation = ((SyntheseECF)instanceFenetre.InstanceFenetreEnCours).EvaluationSelectionnee;
            _stagaire=Parametres.Instance.stagiaire;
            
            tbInfo.Text= "Modification note ECF : " + _evaluation.Ecf.ToString() + "\n" + "Compétence : " + _evaluation.Competence.ToString() + "\n" + "Stagiaire : " + _stagaire.ToString();
            if (_evaluation.Ecf.NotationNumerique)
            {
                rbAcquis.Visibility = Visibility.Hidden;
                rbEnCours.Visibility = Visibility.Hidden;
                rbNonAcquis.Visibility = Visibility.Hidden;
                tbNote.Visibility = Visibility.Visible;
                lbSurVingt.Visibility = Visibility.Visible;

                if (_evaluation.Note != -1)
                {
                    tbNote.Text = _evaluation.Note.ToString();
                }
                tbNote.Focus();
            }
            else
            {
                rbAcquis.Visibility = Visibility.Visible;
                rbEnCours.Visibility = Visibility.Visible;
                rbNonAcquis.Visibility = Visibility.Visible;
                tbNote.Visibility = Visibility.Hidden;
                lbSurVingt.Visibility = Visibility.Hidden;

                if (_evaluation.Note != -1)
                {
                    if (_evaluation.Note == Ressources.CONSTANTES.NOTE_ACQUIS)
                    {
                        rbAcquis.IsChecked = true;
                    }
                    else if (_evaluation.Note == Ressources.CONSTANTES.NOTE_ENCOURS_ACQUISITION)
                    {
                        rbEnCours.IsChecked = true;
                    }
                    else if (_evaluation.Note == Ressources.CONSTANTES.NOTE_NON_ACQUIS)
                    {
                        rbNonAcquis.IsChecked = true;
                    }
                }
            }
        }

        private void btValider_Click(object sender, RoutedEventArgs e)
        {
            float note=-1;
            
            if (_evaluation.Ecf.NotationNumerique)
	        {
                float noteSaisie;
                
                tbNote.Text= tbNote.Text.Replace('.', ',');
                bool estNumerique = float.TryParse(tbNote.Text,out noteSaisie);

                if (noteSaisie >= 0 && noteSaisie <= 20 && estNumerique)
                {
                    note = noteSaisie;
                    tbNote.Background = Brushes.White;
                }
                else
                {
                    tbNote.Background=Brushes.Red;
                    tbNote.Focus();
                }
	        }else{
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

            if (note != -1)
            {
                CtrlGestionECF.modifierNoteEvaluation(_evaluation, note);
                Close();
            }    
        }

        private void btAnnuler_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void tbNote_GotFocus(object sender, RoutedEventArgs e)
        {
            tbNote.SelectAll();
        }
    }
}
