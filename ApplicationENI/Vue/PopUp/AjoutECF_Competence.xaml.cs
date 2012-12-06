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
    /// Logique d'interaction pour AjoutECF_Competence.xaml
    /// </summary>
    public partial class AjoutECF_Competence : Window
    {
        #region Attribut, propriete
        private CtrlAjoutECF_Competence _ctrlAjoutECF_Competence;
        public CtrlAjoutECF_Competence CtrlAjoutECF_Competence
        {
            get { return _ctrlAjoutECF_Competence; }
            set { _ctrlAjoutECF_Competence = value; }
        }
        private bool _ecfAdd = false;
        #endregion

        #region constructeur
        public AjoutECF_Competence(bool pEcfAdd)
        {
            InitializeComponent();

            _ecfAdd = pEcfAdd;

            _ctrlAjoutECF_Competence = new CtrlAjoutECF_Competence();
            
            //ECF ou compétence?
            if (_ecfAdd)
            {
                _ctrlAjoutECF_Competence.ECFAdd = true;
                Title = "Ajout d'un ECF";
            }
            else
            {
                _ctrlAjoutECF_Competence.ECFAdd = false;
                Title = "Ajout d'une compétence";
            }
            tbCode.Focus();
        }
        #endregion

        #region evenements
        private void btAnnuler_Click(object sender, RoutedEventArgs e)
        {
            if (tbCode.Text.Trim() == "" && tbLibelle.Text.Trim() == "")
            {
                this.Close();
                return;
            }            
            tbCode.Text = "";
            tbLibelle.Text = "";
        }

        private void btValider_Click(object sender, RoutedEventArgs e)
        {
            String message = "";

            if (tbCode.Text.Trim() != "" && tbLibelle.Text.Trim() != "")
            {
                //ajout d'un ECF
                if (_ctrlAjoutECF_Competence.ECFAdd == true)
                {
                    _ctrlAjoutECF_Competence.Competence = null;
                    _ctrlAjoutECF_Competence.ECF = new ECF((tbCode.Text.Trim()).ToUpper(), tbLibelle.Text.Trim());
                    message = _ctrlAjoutECF_Competence.ajouterECF(_ctrlAjoutECF_Competence.ECF);                    
                }
                else //ajout d'une compétence
                {
                    _ctrlAjoutECF_Competence.ECF = null;
                    _ctrlAjoutECF_Competence.Competence = new Competence((tbCode.Text.Trim()).ToUpper(), tbLibelle.Text.Trim());
                    message = _ctrlAjoutECF_Competence.ajouterCompetence(_ctrlAjoutECF_Competence.Competence);
                }

                //Gestion erreur
                if (message.Trim() != "")
                {
                    MessageBox.Show(message);
                }
                else
                {
                    this.Close();
                }
            }
        }
        #endregion
    }
}
