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
using ApplicationENI.Modele;

namespace ApplicationENI.Vue
{
    /// <summary>
    /// Logique d'interaction pour AccueilGeneral.xaml
    /// </summary>
    public partial class AccueilGeneral : UserControl
    {
        public AccueilGeneral()
        {
            InitializeComponent();
            this.labUser.Content = Parametres.Instance.utilisateur.prenom + " " + Parametres.Instance.utilisateur.nom;
        }

        private void listViewAlerte_Initialized(object sender, EventArgs e)
        {
            Actualiser();
        }

        private void hlinkRefresh_Click(object sender, RoutedEventArgs e)
        {
            Actualiser();
        }

        private void Actualiser()
        {
            //0:information, 1:avertissement, 2:erreur, 3:interdiction
            this.listViewAlerte.ItemsSource = DAL.AccueilDAL.GetListeAlertes();
        }
    }
}
