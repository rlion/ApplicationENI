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
        }

        private void listViewAlerte_Initialized(object sender, EventArgs e)
        {
            //0:information, 1:avertissement, 2:erreur, 3:interdiction
            List<ItemAlerte> listAlertes = new List<ItemAlerte>();
            listAlertes.Add(new ItemAlerte(0, "Retard", "8 stagiaires sont arrivés en retard aujourd'hui."));
            listAlertes.Add(new ItemAlerte(1, "ECF", "l'ECF DL44 VB.NET du 14/11/2006 n'a toujours pas été corrigé."));
            listAlertes.Add(new ItemAlerte(2, "Planning", "Le planning de Bertrand RENARD n'a pas été entièrement défini."));
            listAlertes.Add(new ItemAlerte(3, "Salle", "La salle 404 n'est pas disponible."));
            this.listViewAlerte.ItemsSource = listAlertes;
        }

        private void listViewAlerte_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
