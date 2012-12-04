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
            Actualiser();
        }

        private void hlinkRefresh_Click(object sender, RoutedEventArgs e)
        {
            Actualiser();
        }

        private void Actualiser()
        {
            //0:information, 1:avertissement, 2:erreur, 3:interdiction
            //List<ItemAlerte> listAlertes = new List<ItemAlerte>();
            //listAlertes.Add(new ItemAlerte(0, "8 stagiaires sont arrivés en retard aujourd'hui.", 0));
            //listAlertes.Add(new ItemAlerte(1, "l'ECF DL44 VB.NET du 14/11/2006 n'a toujours pas été corrigé.", 1));
            //listAlertes.Add(new ItemAlerte(2, "Le planning de Bertrand RENARD n'a pas été entièrement défini.", 0));
            //listAlertes.Add(new ItemAlerte(3, "La salle 404 n'est pas disponible.", 0));
            this.listViewAlerte.ItemsSource = DAL.AccueilDAL.GetListeAlertes();
        }
    }
}
