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
            dataGridListeAbsences.ItemsSource = ctrl.getListAbsences();
            dataGridListeAbsences.IsReadOnly = true;
            gbListeAbsenceRetards.Header = this.dataGridListeAbsences.Items.Count + " Absence(s)/Retard(s) répertorié(s)";
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
                txtHeureDeb.Text = abs._dateDebut.ToString("HH");
                txtMinuteDeb.Text = abs._dateDebut.ToString("mm");
                txtHeureFin.Text = abs._dateFin.ToString("HH");
                txtMinuteFin.Text = abs._dateFin.ToString("mm");
                checkBoxValide.IsChecked = abs._valide;
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
                checkBoxValide.IsEnabled = false;
                textBoxCommentaire.IsEnabled = false;
                textBoxRaison.IsEnabled = false;
                radioButtonAbsence.IsEnabled = false;
                radioButtonRetard.IsEnabled = false;
                txtHeureDeb.IsEnabled = false;
                txtHeureFin.IsEnabled = false;
                txtMinuteDeb.IsEnabled = false;
                txtMinuteFin.IsEnabled = false;
            }
        }

        private void btnModifier_Click(object sender, RoutedEventArgs e)
        {
            datePickerDateDebut.IsEnabled = true;
            datePickerDateFin.IsEnabled = true;
            checkBoxValide.IsEnabled = true;
            textBoxCommentaire.IsEnabled = true;
            textBoxRaison.IsEnabled = true;
            radioButtonAbsence.IsEnabled = true;
            radioButtonRetard.IsEnabled = true;
            txtHeureDeb.IsEnabled = true;
            txtHeureFin.IsEnabled = true;
            txtMinuteDeb.IsEnabled = true;
            txtMinuteFin.IsEnabled = true;
            btnEnregistrer.IsEnabled = true;
        }

        private void btnSupprimer_Click(object sender, RoutedEventArgs e)
        {
                if (MessageBox.Show("Etes-vous CERTAIN de vouloir supprimer cette absence ?", "Confirmation avant suppression", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    ctrl.supprimerAbsence((Absence)dataGridListeAbsences.SelectedItem);
                    gbListeAbsenceRetards.Header = this.dataGridListeAbsences.Items.Count + " Absence(s)/Retard(s) répertorié(s)";
                    int compteurAbsences = 0;
                    int compteurRetards = 0;
                    String texteAAfficher = "Total : ";

                    foreach (Absence a in dataGridListeAbsences.Items)
                    {
                        if (a._isAbsence == true)
                        {
                            compteurAbsences++;
                        }
                        else
                        {
                            compteurRetards++;
                        }
                    }

                    if (compteurAbsences > 1)
                    {
                        texteAAfficher += compteurAbsences + " absences, ";
                    }
                    else
                    {
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
                    this.dataGridListeAbsences.Items.Refresh();
                }
        }

        private void btnEnregistrer_Click(object sender, RoutedEventArgs e)
        {
            if (VerificationSaisie())
            {
                // TODO: bug dans la saisie de l'heure due au format des minutes ("7" au lieu de "07") ce qui fait que le nombre concaténé est plus petit que l'autre alors que l'heure est plus tardive...
                String raison, commentaire, dateDebut, dateFin;
                int heureDeb, minuteDeb, heureFin, minuteFin;
                bool valide, absence, retard;
                dateDebut = datePickerDateDebut.Text;
                dateFin = datePickerDateFin.Text;
                raison = textBoxRaison.Text;
                commentaire = textBoxCommentaire.Text;
                heureDeb = int.Parse(txtHeureDeb.Text);
                heureFin = int.Parse(txtHeureFin.Text);
                minuteDeb = int.Parse(txtMinuteDeb.Text);
                minuteFin = int.Parse(txtMinuteFin.Text);
                valide = checkBoxValide.IsChecked.Value;
                absence = radioButtonAbsence.IsChecked.Value;
                retard = radioButtonRetard.IsChecked.Value;

                ctrl.modifierAbsence((Absence)dataGridListeAbsences.SelectedItem, dateDebut, dateFin, heureDeb, minuteDeb, heureFin, minuteFin, raison, commentaire, valide, absence);
                this.dataGridListeAbsences.Items.Refresh();
            }
        }

        private bool VerificationSaisie()
        {
            bool retour = true;


            // vérifications sur la présence des informations
            if ((datePickerDateDebut.Text == null || txtHeureDeb.Text == "") || (txtHeureFin.Text == null || txtHeureFin.Text == "")) 
            {
                MessageBox.Show("Veuillez vérifier les horaires saisis", "Saisie erronée", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if ((datePickerDateDebut.Text == null || datePickerDateDebut.Text == "") || (datePickerDateFin.Text == null || datePickerDateFin.Text == ""))
            {
                MessageBox.Show("Veuillez vérifier les dates saisies", "Saisie erronée", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (textBoxRaison.Text == null || textBoxRaison.Text == "")
            {
                MessageBox.Show("Veuillez saisir une raison.", "Saisie erronée", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }


            // vérification de la cohérence des infos.
            int jour, mois, an;
            jour = int.Parse(datePickerDateDebut.Text.Substring(0, 2));
            mois = int.Parse(datePickerDateDebut.Text.Substring(3, 2));
            an = int.Parse(datePickerDateDebut.Text.Substring(6, 4));
            DateTime dateDebut = new DateTime(an, mois, jour);

            jour = int.Parse(datePickerDateFin.Text.Substring(0, 2));
            mois = int.Parse(datePickerDateFin.Text.Substring(3, 2));
            an = int.Parse(datePickerDateFin.Text.Substring(6, 4));
            DateTime dateFin = new DateTime(an, mois, jour);

            if (dateFin - dateDebut < new TimeSpan(0))
            {
                MessageBox.Show("La date de fin est antérieure à la date de début.", "Saisie erronée", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (!verifierInterval(0, 23, int.Parse(txtHeureDeb.Text)) || !verifierInterval(0, 23, int.Parse(txtHeureFin.Text)))
            {
                MessageBox.Show("Les heures saisies doivent être comprises entre 0 et 23.", "Saisie erronée", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (!verifierInterval(0, 59, int.Parse(txtMinuteDeb.Text)) || !verifierInterval(0, 59, int.Parse(txtMinuteFin.Text)))
            {
                MessageBox.Show("Les minutes saisies doivent être comprises entre 0 et 59.", "Saisie erronée", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            // On vérifie que l'heure de fin est postérieur à l'heure de début uniquement si la date de début est la même que la date de fin.
            if (dateDebut == dateFin)
            {
                if (int.Parse(txtHeureDeb.Text + txtMinuteDeb.Text) >= int.Parse(txtHeureFin.Text + txtMinuteFin.Text))
                {
                    MessageBox.Show("L'heure fin est antérieure à la date de début.", "Saisie erronée", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            return retour;
        }

        private void btnAnnuler_Click(object sender, RoutedEventArgs e)
        {
            this.gbDetailAbsenceRetard.Visibility = Visibility.Visible;
            Absence abs = (Absence)dataGridListeAbsences.SelectedItem;
            datePickerDateDebut.Text = abs._dateDebut.ToString();
            datePickerDateFin.Text = abs._dateFin.ToString();
            txtHeureDeb.Text = abs._dateDebut.ToString("HH");
            txtMinuteDeb.Text = abs._dateDebut.Minute.ToString();
            txtHeureFin.Text = abs._dateFin.Hour.ToString();
            txtMinuteFin.Text = abs._dateFin.Minute.ToString();
            checkBoxValide.IsChecked = abs._valide;
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
        }

        private bool verifierInterval(int pFrom, int pTo, int pNombre) {
            if (pNombre >= pFrom && pNombre <= pTo)
            {
                return true;
            }
            else {
                return false;
            }
        }

    }
}
