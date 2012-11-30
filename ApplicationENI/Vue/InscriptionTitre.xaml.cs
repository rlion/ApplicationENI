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
using ApplicationENI.Controleur;
using ApplicationENI.Modele;

namespace ApplicationENI.Vue
{
    /// <summary>
    /// Logique d'interaction pour InscriptionTitre.xaml
    /// </summary>
    public partial class InscriptionTitre : UserControl
    {
        private CtrlInscriptionTitre Controleur;
        private PassageTitre passageTitre;
        private PassageTitre histoPassageT;
        private int codeStagiaire;
        private string codeTitre;

        //Constructeur
        public InscriptionTitre()
        {
            InitializeComponent();

            Controleur = new CtrlInscriptionTitre();
            codeStagiaire = Parametres.Instance.stagiaire._id;

            InitForm();
        }

        private void ReInitPassageTitre()
        {
            passageTitre = new PassageTitre(histoPassageT.CodeTitre, histoPassageT.CodeStagiaire, 
                histoPassageT.DatePassage, histoPassageT.EstObtenu, histoPassageT.EstValide);

            this.dpPassage.SelectedDate = passageTitre.DatePassage;
            this.dpNewPass.SelectedDate = passageTitre.DatePassage;
            this.chkValide.IsChecked = passageTitre.EstValide;
            this.rbNon.IsChecked = !passageTitre.EstObtenu;
            this.rbOui.IsChecked = passageTitre.EstObtenu;
        }

        private void InitForm()
        {
            this.labFormation.Content = Controleur.GetFormationStagiaire(codeStagiaire);
            KeyValuePair<string,string> kvp = Controleur.GetInfosTitre(codeStagiaire);
            codeTitre = kvp.Key;
            this.txtLibTitre.Text = kvp.Value;

            if (Controleur.CheckIfInscrit(codeStagiaire))
                histoPassageT = Controleur.GetPassageTitre(codeStagiaire, codeTitre);
            else
                histoPassageT = new PassageTitre(codeTitre, codeStagiaire);

            ReInitPassageTitre();
        }

        private void btReinit_Click(object sender, RoutedEventArgs e)
        {
            ReInitPassageTitre();
        }

        private void btInscrire_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Etes-vous sûr(e) de vouloir inscrire cette personne au titre? L'inscription est définitive.",
                "Inscription Titre", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                passageTitre.DatePassage = dpPassage.SelectedDate.GetValueOrDefault();
                passageTitre.EstValide = chkValide.IsChecked.GetValueOrDefault();

                int result = Controleur.InscrireStagiaire(passageTitre);

                if (result > 0)
                {
                    histoPassageT.DatePassage = passageTitre.DatePassage;
                    histoPassageT.EstValide = passageTitre.EstValide;
                }
                else
                {
                    MessageBox.Show("Cette personne est déjà inscrite à un titre en cours", 
                        "Inscription Titre", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }

                ReInitPassageTitre();
            }
        }

        private void btModifierP_Click(object sender, RoutedEventArgs e)
        {
            if ((string)this.btModifierP.Content == "Modifier")
            {
                this.dpNewPass.IsEnabled = true;
                this.btModifierP.Content = "Consulter";
            }
            else
            {
                this.dpNewPass.IsEnabled = false;
                this.dpNewPass.SelectedDate = this.passageTitre.DatePassage;
                this.btModifierP.Content = "Modifier";
            }
        }

        private void btAnnuler_Click(object sender, RoutedEventArgs e)
        {
            this.dpNewPass.SelectedDate = passageTitre.DatePassage;
            this.rbNon.IsChecked = !passageTitre.EstObtenu;
            this.rbOui.IsChecked = passageTitre.EstObtenu;
            this.dpNewPass.IsEnabled = false;
            this.btModifierP.Content = "Modifier";
        }

        private void btValider_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Etes-vous sûr(e) de vouloir mettre à jour ces informations?",
                "Inscription Titre", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                passageTitre.DatePassage = dpNewPass.SelectedDate.GetValueOrDefault();
                passageTitre.EstObtenu = rbOui.IsChecked.GetValueOrDefault();

                int result = Controleur.UpdateInscription(passageTitre);

                if (result > 0)
                {
                    histoPassageT.DatePassage = passageTitre.DatePassage;
                    histoPassageT.EstObtenu = passageTitre.EstObtenu;
                }

                ReInitPassageTitre();
                this.dpNewPass.IsEnabled = false;
                this.btModifierP.Content = "Modifier";
            }
        }
    }
}
