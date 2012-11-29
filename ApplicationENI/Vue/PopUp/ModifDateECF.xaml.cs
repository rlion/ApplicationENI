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
    /// Logique d'interaction pour ModifDateECF.xaml
    /// </summary>
    public partial class ModifDateECF : Window
    {
        SessionECF _sessionECF = null;
        Stagiaire _stagaire = null;

        public ModifDateECF()
        {
            InitializeComponent();

            _sessionECF = ((SyntheseECF)instanceFenetre.InstanceFenetreEnCours).SessionSelectionnee;
            _stagaire=Parametres.Instance.stagiaire;
            
            dateSel.DisplayDateStart = DateTime.Now;
            dateSel.SelectedDate = _sessionECF.Date;
            
            tbInfo.Text= "Modification session ECF : " + _sessionECF.ToString() + "\n" + "Stagiaire : " + _stagaire.ToString();
        }

        private void btValider_Click(object sender, RoutedEventArgs e)
        {
            if (dateSel.SelectedDate != null && _sessionECF.Date != (DateTime)dateSel.SelectedDate)
            {
                CtrlGestionECF.modifierDateSessionECF(_sessionECF,(DateTime)dateSel.SelectedDate);
                Close();
            }            
        }

        private void btAnnuler_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
