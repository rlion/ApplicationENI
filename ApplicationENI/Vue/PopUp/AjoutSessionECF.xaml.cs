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
using ApplicationENI.Controleur;

namespace ApplicationENI.Vue.PopUp
{
    /// <summary>
    /// Logique d'interaction pour AjoutSessionECF.xaml
    /// </summary>
    public partial class AjoutSessionECF : Window
    {
        #region proprietes
        private List<ECF> _listeECFs = null;
        private SessionECF _sessionECF = null;
        #endregion

        #region get/set
        public SessionECF SessionECF
        {
            get { return _sessionECF; }
            set { _sessionECF = value; }
        }
        #endregion

        #region constructeur
        public AjoutSessionECF()
        {
            InitializeComponent();

            _listeECFs = CtrlGestionECF.getListECFs();
            cbECF.ItemsSource = _listeECFs;

            datePicker1.DisplayDateStart = DateTime.Now;
        }
        #endregion

        #region evenement
        private void btAnnuler_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btValider_Click(object sender, RoutedEventArgs e)
        {
            SessionECF sessionECFTemp = new SessionECF();
            sessionECFTemp.Ecf = (ECF)cbECF.SelectedItem;
            sessionECFTemp.Date = (DateTime)datePicker1.SelectedDate;

            if (sessionECFTemp.Date != DateTime.MinValue && sessionECFTemp.Ecf != null)
            {
                _sessionECF = sessionECFTemp;
                CtrlGestionECF.ajouterSessionECF(sessionECFTemp);
                Close();
            }
        }
        #endregion
    }
}
