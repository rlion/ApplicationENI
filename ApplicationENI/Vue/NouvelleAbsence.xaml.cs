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

            txtHeureDeb.Text = "09";
            txtMinuteDeb.Text = "00";
            txtHeureFin.Text = "09";
            txtMinuteFin.Text = "05";
            datePickerDateDebut.Text = DateTime.Now.ToString();
            datePickerDateFin.Text = DateTime.Now.ToString();
            checkBoxValide.IsChecked = false;
            radioButtonAbsence.IsChecked = true;
            radioButtonRetard.IsChecked = false;
            textBoxRaison.Text = "";
            textBoxCommentaire.Text = "";

        }

        private void datePicker1_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void datePickerDateDebut_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void datePickerDateFin_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void btnAnnuler_Click(object sender, RoutedEventArgs e)
        {
            txtHeureDeb.Text = "";
            txtMinuteDeb.Text = "";
            txtHeureFin.Text = "";
            txtMinuteFin.Text = "";
            checkBoxValide.IsChecked = false;
            radioButtonAbsence.IsChecked = true;
            radioButtonRetard.IsChecked = false;
            textBoxRaison.Text = "";
            textBoxCommentaire.Text = "";
            datePickerDateDebut.Text = "";
            datePickerDateFin.Text = "";
        }

        private void btnEnregistrer_Click(object sender, RoutedEventArgs e)
        {

            if (txtHeureDeb.Text.Length <= 1)
            {
                txtHeureDeb.Text = 0 + txtHeureDeb.Text;
            }
            if (txtMinuteDeb.Text.Length <= 1)
            {
                txtMinuteDeb.Text = 0 + txtMinuteDeb.Text;
            }
            if (txtHeureFin.Text.Length <= 1)
            {
                txtHeureFin.Text = 0 + txtHeureFin.Text;
            }
            if (txtMinuteFin.Text.Length <= 1)
            {
                txtMinuteFin.Text = 0 + txtMinuteFin.Text;
            }
            if (VerificationSaisie())
            {
                String raison, commentaire, dateDebut, dateFin;
                int heureDeb, minuteDeb, heureFin, minuteFin;
                bool valide, absence, retard;
                dateDebut = datePickerDateDebut.Text;
                dateFin = datePickerDateFin.Text;
                raison = textBoxRaison.Text;
                commentaire = textBoxCommentaire.Text;
                valide = checkBoxValide.IsChecked.Value;
                absence = radioButtonAbsence.IsChecked.Value;
                retard = radioButtonRetard.IsChecked.Value;
                
                    heureDeb = int.Parse(txtHeureDeb.Text);
                    heureFin = int.Parse(txtHeureFin.Text);
                    minuteDeb = int.Parse(txtMinuteDeb.Text);
                    minuteFin = int.Parse(txtMinuteFin.Text);
                    ctrl.AjouterAbsence(dateDebut, dateFin, heureDeb, minuteDeb, heureFin, minuteFin, raison, commentaire, valide, absence, retard);
                    MessageBox.Show("Observation ajoutée", "Ajout effectué", MessageBoxButton.OK, MessageBoxImage.Information);               
            }
        }

        
        private bool VerificationSaisie() {
            bool retour = true;


            // vérifications sur la présence des informations
            if ((txtHeureDeb.Text == null || txtHeureDeb.Text == "") || (txtHeureFin.Text == null || txtHeureFin.Text == ""))
            {
                MessageBox.Show("Veuillez vérifier les horaires saisis", "Saisie erronée", MessageBoxButton.OK, MessageBoxImage.Error);
                retour = false;
            }

            if ((datePickerDateDebut.Text == null || datePickerDateDebut.Text == "") || (datePickerDateFin.Text == null || datePickerDateFin.Text == ""))
            {
                MessageBox.Show("Veuillez vérifier les dates saisies", "Saisie erronée", MessageBoxButton.OK, MessageBoxImage.Error);
                retour = false;
            }

            if (textBoxRaison.Text == null || textBoxRaison.Text == "") {
                MessageBox.Show("Veuillez saisir une raison.", "Saisie erronée", MessageBoxButton.OK, MessageBoxImage.Error);
                retour = false;
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
                MessageBox.Show("La date de fin est antérieure ou équivalente à la date de début.", "Saisie erronée", MessageBoxButton.OK, MessageBoxImage.Error);
                retour = false;
            }

            // On vérifie que l'heure de fin est postérieure à l'heure de début uniquement si la date de début est la même que la date de fin.
            if (dateDebut == dateFin)
            {
                try
                {
                    if (int.Parse(txtHeureDeb.Text + txtMinuteDeb.Text) >= int.Parse(txtHeureFin.Text + txtMinuteFin.Text))
                    {
                        MessageBox.Show("L'heure fin est antérieure ou équivalente à la date de début.", "Saisie erronée", MessageBoxButton.OK, MessageBoxImage.Error);
                        retour = false;
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Veillez à saisir des nombres pour les champs concernant les horaires.", "Erreur de saisie", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            return retour;
        }


        private void radioButtonRetard_Checked(object sender, RoutedEventArgs e)
        {
            datePickerDateFin.IsEnabled = false;
            txtHeureDeb.IsEnabled = false;
            txtMinuteDeb.IsEnabled = false;
        }

        private void radioButtonAbsence_Checked(object sender, RoutedEventArgs e)
        {
            datePickerDateFin.IsEnabled = true;
            txtHeureDeb.IsEnabled = true;
            txtMinuteDeb.IsEnabled = true;
        }
    }
}
