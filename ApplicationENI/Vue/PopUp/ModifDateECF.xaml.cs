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
        //SessionECF _sessionECF = null;
        //Stagiaire _stagaire = null;
        CtrlModifDateECF _ctrlModifDateECF = null;

        public ModifDateECF()
        {
            InitializeComponent();

            _ctrlModifDateECF = new CtrlModifDateECF();

            _ctrlModifDateECF.SessionECF = ((SyntheseECF)instanceFenetre.InstanceFenetreEnCours).CtrlSyntheseECF.SessionSelectionnee;
            _ctrlModifDateECF.Stagaire = Parametres.Instance.stagiaire;
            
            dateSel.DisplayDateStart = DateTime.Now;
            dateSel.SelectedDate = _ctrlModifDateECF.SessionECF.Date;

            tbInfo.Text = "Modification session ECF : " + _ctrlModifDateECF.SessionECF.ToString() + "\n" + "Stagiaire : " + _ctrlModifDateECF.Stagaire.ToString();
        }

        private void btValider_Click(object sender, RoutedEventArgs e)
        {
            if (dateSel.SelectedDate != null && _ctrlModifDateECF.SessionECF.Date != (DateTime)dateSel.SelectedDate)
            {
                _ctrlModifDateECF.modifierDateSessionECF_Stagiaire(_ctrlModifDateECF.Stagaire, _ctrlModifDateECF.SessionECF, (DateTime)dateSel.SelectedDate);
                Close();
            }            
        }

        private void btAnnuler_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
