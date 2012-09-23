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
        }

        private void dataGridListeAbsences_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Absence abs = (Absence)dataGridListeAbsences.SelectedItem;
            datePickerDateDebut.Text = abs._dateDebut.ToString();
            datePickerDateFin.Text = abs._dateFin.ToString();
            checkBox1.IsChecked = abs._valide;
            //TODO: Il y a un soucis de conception, il n'y a pas d'attribut permettant de déterminer si c'est une abs ou un ret (se fier uniquement aux dates et heures ne semble pas judicieux).
            //radioButtonAbsence.IsChecked = abs.
            textBoxCommentaire.Text = abs._commentaire;
            textBoxRaison.Text = abs._raison;


        }

    }
}
