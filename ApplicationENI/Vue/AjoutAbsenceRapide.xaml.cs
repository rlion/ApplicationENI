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
    /// Logique d'interaction pour AjoutAbsenceRapide.xaml
    /// </summary>
    public partial class AjoutAbsenceRapide : UserControl
    {
        CtrlGestionAbsences Controleur = new CtrlGestionAbsences();
        private bool isInitAutoCompBox;
        public AjoutAbsenceRapide()
        {
            InitializeComponent();
            if (!isInitAutoCompBox) { 
                acbNomPrenom.ItemsSource = Controleur.GetListeStagiaires();
            }
            isInitAutoCompBox = true;
        }

        private void acbNomPrenom_GotFocus(object sender, RoutedEventArgs e)
        {
            if (isInitAutoCompBox)
            {
                acbNomPrenom.Text = string.Empty;
                isInitAutoCompBox = false;
            }
        }
        private void acbNomPrenom_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            labelResult.Content = "";
            if (e.Key == System.Windows.Input.Key.Enter) {
                Controleur.AjouterAbsenceTemporaire(((Stagiaire)acbNomPrenom.SelectedItem));
                labelResult.Content = "Retard ajouté";
            }
        }

        private void btValiderAbsenceTemporaire_Click(object sender, RoutedEventArgs e)
        {
            if ((Stagiaire)acbNomPrenom.SelectedItem != null)
            {
                Controleur.AjouterAbsenceTemporaire((Stagiaire)acbNomPrenom.SelectedItem);
                labelResult.Content = "Retard ajouté";
            }
            else {
                MessageBox.Show("Veuillez sélectionner un stagiaire.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
