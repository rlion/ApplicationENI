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
using ApplicationENI.Controleur;
using ApplicationENI.Modele;

namespace ApplicationENI.Vue
{
    /// <summary>
    /// Logique d'interaction pour HistoriqueAbsencesRetards.xaml
    /// </summary>
    public partial class HistoriqueAbsencesRetards : UserControl
    {
        private CtrlGestionAbsences ctrl = new CtrlGestionAbsences();
        public HistoriqueAbsencesRetards()
        {
            InitializeComponent();
            this.dataGridListeAbsences.ItemsSource = ctrl.getListAbsences();
            //gbListeAbsenceRetards.Header = this.dataGridListeAbsences.Items.Count + " Absence(s)/Retard(s) répertorié(s)";
            int compteurAbsences = 0;
            int compteurRetards = 0;
            String texteAAfficher = "Total : ";

            foreach (Absence a in dataGridListeAbsences.Items) {
                if (a._isAbsence == true)
                {
                    compteurAbsences++;
                }
                else {
                    compteurRetards++;
                }
            }

            if (compteurAbsences >1)
            {
                texteAAfficher += compteurAbsences + " absences, ";
            }
            else {
                texteAAfficher += compteurAbsences + " absence, ";
            }

            if (compteurRetards > 1)
            {
                texteAAfficher += compteurRetards + " retards.";
            }
            else
            {
                texteAAfficher += compteurRetards + " retard.";
            }

            gbListeAbsenceRetards.Header = texteAAfficher;
            this.gbDetailAbsenceRetard.Visibility = Visibility.Hidden;
        }

        private void dataGridListeAbsences_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.dataGridListeAbsences.SelectedItem != null)
            {
                this.gbDetailAbsenceRetard.Visibility = Visibility.Visible;
                Absence abs = (Absence)dataGridListeAbsences.SelectedItem;
                datePickerDateDebut.Text = abs._dateDebut.ToString();
                datePickerDateFin.Text = abs._dateFin.ToString();
                checkBox1.IsChecked = abs._valide;
                //TODO: Il y a un soucis de conception, il n'y a pas d'attribut permettant de déterminer si c'est une abs ou un ret (se fier uniquement aux dates et heures ne semble pas judicieux).
                //radioButtonAbsence.IsChecked = abs.
                textBoxCommentaire.Text = abs._commentaire;
                textBoxRaison.Text = abs._raison;
                if (abs._isAbsence == true)
                {
                    radioButtonAbsence.IsChecked = true;
                }
                else
                {
                    radioButtonRetard.IsChecked = true;
                }

                // on désactive tout les champs, ils seront activés lors de la pression du bouton "Modifier"
                datePickerDateDebut.IsEnabled = false;
                datePickerDateFin.IsEnabled = false;
                checkBox1.IsEnabled = false;
                textBoxCommentaire.IsEnabled = false;
                textBoxRaison.IsEnabled = false;
                radioButtonAbsence.IsEnabled = false;
                radioButtonRetard.IsEnabled = false;
            }
        }

        private void btnModifier_Click(object sender, RoutedEventArgs e)
        {
            datePickerDateDebut.IsEnabled = true;
            datePickerDateFin.IsEnabled = true;
            checkBox1.IsEnabled = true;
            textBoxCommentaire.IsEnabled = true;
            textBoxRaison.IsEnabled = true;
            radioButtonAbsence.IsEnabled = true;
            radioButtonRetard.IsEnabled = true;
        }

        private void btnSupprimer_Click(object sender, RoutedEventArgs e)
        {
            ctrl.supprimerAbsence((Absence)dataGridListeAbsences.SelectedItem);
            this.dataGridListeAbsences.Items.Refresh();
        }

    }
}
