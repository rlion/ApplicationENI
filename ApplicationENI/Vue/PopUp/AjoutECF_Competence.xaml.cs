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
    /// Logique d'interaction pour AjoutECF_Competence.xaml
    /// </summary>
    public partial class AjoutECF_Competence : Window
    {
        private bool _ecfAdd; //si true on est en train d'ajouter un ECF sinon une Competence

        public bool ECFAdd1
        {
            get { return _ecfAdd; }
            set { _ecfAdd = value; }
        }
        
        private ECF _ECF = null;
        private Competence _competence = null;
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

        public AjoutECF_Competence()
        {
            InitializeComponent();
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

            if (tbCode.Text.Trim() != "" && tbCode.Text.Trim() != "")
            {
                if (_ecfAdd==true)
                {
                    _competence = null;
                    _ECF = new ECF((tbCode.Text.Trim()).ToUpper(), tbCode.Text.Trim());
                    message=ECFDAL.ajouterECF(_ECF);                    
                }
                else
                {
                    _ECF = null;
                    _competence = new Competence((tbCode.Text.Trim()).ToUpper(), tbCode.Text.Trim());
                    message = CompetencesDAL.ajouterCompetence(_competence);
                }

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
    }
}
