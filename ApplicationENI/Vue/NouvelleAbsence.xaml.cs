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
    /// Logique d'interaction pour Nouvelle_absence.xaml
    /// </summary>
    public partial class NouvelleAbsence : UserControl
    {
        private CtrlGestionAbsences ctrl = new CtrlGestionAbsences();
        public NouvelleAbsence()
        {
            InitializeComponent();

            //TODO: à virer - ces éléments sont renseignés temporairement pour éviter de tout avoir à saisir pour les milliers de tests.
            txtHeureDeb.Text = "09";
            txtMinuteDeb.Text = "09";
            txtHeureFin.Text = "10";
            txtMinuteFin.Text = "09";
            datePickerDateDebut.Text = DateTime.Now.ToString();
            datePickerDateFin.Text = DateTime.Now.ToString();
            checkBoxValide.IsChecked = true;
            radioButtonAbsence.IsChecked = false;
            radioButtonRetard.IsChecked = true;
            textBoxRaison.Text = "Panne de réveil";
            textBoxCommentaire.Text = "Panne de motivation plutôt...";

        }

        private void datePicker1_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void datePickerDateDebut_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            textBoxRaison.Text = datePickerDateDebut.Text;
        }

        private void datePickerDateFin_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            textBoxRaison.Text = datePickerDateFin.Text;
        }

        private void btnAnnuler_Click(object sender, RoutedEventArgs e)
        {
            txtHeureDeb.Text = "";
            txtMinuteDeb.Text = "";
            txtHeureFin.Text = "";
            txtMinuteFin.Text = "";
            checkBoxValide.IsChecked = false;
            radioButtonAbsence.IsChecked = false;
            radioButtonRetard.IsChecked = true;
            textBoxRaison.Text = "";
            textBoxCommentaire.Text = "";
            datePickerDateDebut.Text = "";
            datePickerDateFin.Text = "";
        }

        private void btnEnregistrer_Click(object sender, RoutedEventArgs e)
        {
            if (VerificationSaisie())
            {
                String raison, commentaire, dateDebut, dateFin;
                int heureDeb, minuteDeb, heureFin, minuteFin;
                bool valide, absence, retard;
                dateDebut = datePickerDateDebut.Text;
                dateFin = datePickerDateDebut.Text;
                raison = textBoxRaison.Text;
                commentaire = textBoxCommentaire.Text;
                heureDeb = int.Parse(txtHeureDeb.Text);
                heureFin = int.Parse(txtHeureFin.Text);
                minuteDeb = int.Parse(txtMinuteDeb.Text);
                minuteFin = int.Parse(txtMinuteFin.Text);
                valide = checkBoxValide.IsChecked.Value;
                absence = radioButtonAbsence.IsChecked.Value;
                retard = radioButtonRetard.IsChecked.Value;

                ctrl.AjouterAbsence(dateDebut, dateFin, heureDeb, minuteDeb, heureFin, minuteFin, raison, commentaire, valide, absence, retard);

            }
        }

        
        private bool VerificationSaisie() {
            bool retour = true;


            // vérifications sur la présence des informations
            if ((txtHeureDeb.Text == null || txtHeureDeb.Text == "") || (txtHeureFin.Text == null || txtHeureFin.Text == ""))
            {
                MessageBox.Show("Veuillez vérifier les horaires saisis", "Saisie erronée", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if ((datePickerDateDebut.Text == null || datePickerDateDebut.Text == "") || (datePickerDateFin.Text == null || datePickerDateFin.Text == ""))
            {
                MessageBox.Show("Veuillez vérifier les dates saisies", "Saisie erronée", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (textBoxRaison.Text == null || textBoxRaison.Text == "") {
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
            
            if(dateFin-dateDebut < new TimeSpan(0)){
                MessageBox.Show("La date de fin est antérieure à la date de début.", "Saisie erronée", MessageBoxButton.OK, MessageBoxImage.Error);
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
    }
}
