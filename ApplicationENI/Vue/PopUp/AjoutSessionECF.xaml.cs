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
using ApplicationENI.DAL;

namespace ApplicationENI.Vue.PopUp
{
    /// <summary>
    /// Logique d'interaction pour AjoutSessionECF.xaml
    /// </summary>
    public partial class AjoutSessionECF : Window
    {
        private List<ECF> _listeECFs = null;
        
        public AjoutSessionECF()
        {
            InitializeComponent();

            _listeECFs = ECFDAL.getListECFs();
            cbECF.ItemsSource = _listeECFs;

            datePicker1.DisplayDateStart = DateTime.Now;
        }

        private void btAnnuler_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btValider_Click(object sender, RoutedEventArgs e)
        {
            SessionECF sessionECFTemp = new SessionECF();
            sessionECFTemp.Ecf = (ECF)cbECF.SelectedItem;
            sessionECFTemp.Date = (DateTime)datePicker1.SelectedDate;

            SessionECFDAL.ajouterSessionECF(sessionECFTemp);
            Close();
        }
    }
}
