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
        #region propriétés
        private bool _ecfAdd; //si true on est en train d'ajouter un ECF sinon une Competence
        private ECF _ECF = null;
        private Competence _competence = null;
        #endregion

        #region get/set
        public bool ECFAdd1
        {
            get { return _ecfAdd; }
            set { _ecfAdd = value; }
        }       
        public ECF ECF
        {
            get { return _ECF; }
            set { _ECF = value; }
        }
        public Competence Competence
        {
            get { return _competence; }
            set { _competence = value; }
        }
        #endregion

        #region constructeur
        public AjoutECF_Competence()
        {
            InitializeComponent();
            
            //ECF ou compétence?
            if (((GestionECF)instanceFenetre.InstanceFenetreEnCours).EcfAdd)
            {
                _ecfAdd = true;
                Title = "Ajout d'un ECF";
            }
            else
            {
                _ecfAdd = false;
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
                if (_ecfAdd==true)
                {
                    _competence = null;
                    _ECF = new ECF((tbCode.Text.Trim()).ToUpper(), tbLibelle.Text.Trim());
                    message = CtrlGestionECF.ajouterECF(_ECF);                    
                }
                else //ajout d'une compétence
                {
                    _ECF = null;
                    _competence = new Competence((tbCode.Text.Trim()).ToUpper(), tbLibelle.Text.Trim());
                    message = CtrlGestionECF.ajouterCompetence(_competence);
                }

                //Gestion erreur TODO à revoir??
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
