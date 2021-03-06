﻿
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
using ApplicationENI.Controleur;

namespace ApplicationENI.Vue
{
    /// <summary>
    /// Logique d'interaction pour Observations.xaml
    /// </summary>
    public partial class Observations : UserControl
    {
        // 0 : rien ou modif
        // 1 : ajout
        static int flag_mode_saisie;
        private CtrlGestionObservations ctrl = new CtrlGestionObservations();

        public Observations()
        {
            InitializeComponent();
            flag_mode_saisie = 1;

            dataGridListAbsences.ItemsSource = ctrl.listeObservation(Parametres.Instance.stagiaire);
            dataGridListAbsences.IsReadOnly = true;
            comboBox1.Items.Add("Pédagogique");
            comboBox1.Items.Add("Entreprise");
            comboBox1.Text = "Pédagogique";
            comboBox1.SelectedValue = "Pédagogique";

        }

        
        private void dataGridListAbsences_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            // on actualise le formulaire avec les données du datagrid
            if (this.dataGridListAbsences.SelectedItem != null)
            {
                flag_mode_saisie = 0;
                Observation obsSelectionne = (Observation)this.dataGridListAbsences.SelectedItem;
                txtBoxTitre.Text = obsSelectionne._titre;
                txtBoxAuteur.Text = obsSelectionne._nomAuteur;
                txtBoxDate.Text = obsSelectionne._date.ToString("dd/MM/yyyy");
                txtBoxTexte.Text = obsSelectionne._texte;
                comboBox1.Items.Clear();
                comboBox1.Items.Add("Pédagogique");
                comboBox1.Items.Add("Entreprise");
                comboBox1.Text = obsSelectionne._type;
                comboBox1.SelectedValue = obsSelectionne._type;
            }

        }

        private void btnAjouter_Click(object sender, RoutedEventArgs e) {
            //désactiver la sélection sur le datagrid.
            dataGridListAbsences.UnselectAll();
            flag_mode_saisie = 1;
            
            txtBoxTitre.Text = "";
            //l'auteur est pas défini à la main, on récupère le nom en session (profil connexion)
            txtBoxAuteur.Text = Parametres.Instance.login;
            txtBoxDate.Text = "";
            txtBoxTexte.Text = "";
            comboBox1.Items.Clear();
            comboBox1.Items.Add("Pédagogique");
            comboBox1.Items.Add("Entreprise");
            comboBox1.Text = "Pédagogique";
            comboBox1.SelectedValue = "Pédagogique";
        }

        private void btnEnregistrer_Click(object sender, RoutedEventArgs e) {
            String typeObs = comboBox1.Text;
                String titre = txtBoxTitre.Text;
                String texte = txtBoxTexte.Text;
                String type = (string)comboBox1.SelectedValue;
            
            if (this.dataGridListAbsences.SelectedItem != null)
            {
                    if(OperationExiste(texte, titre, type))
                    {
                        MessageBox.Show("L'observation est déjà enregistrée en base.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    }
                    else {
                        if (flag_mode_saisie == 0) //si on est en mode modif
                        {
                            Observation obsSelectionne = (Observation)this.dataGridListAbsences.SelectedItem;
                            ctrl.modifierOperation(obsSelectionne, typeObs, titre, texte);

                            dataGridListAbsences.ItemsSource = ctrl.listeObservation(Parametres.Instance.stagiaire);
                            dataGridListAbsences.SelectedItem = dataGridListAbsences.Items[dataGridListAbsences.Items.Count - 1];
                        }            
                    }
                

                
            }
            else {
                if (txtBoxTexte.Text == "" || txtBoxTitre.Text == "")
                {
                    MessageBox.Show("Il n'y a rien à enregistrer", "Erreur", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                else
                {
                    if (OperationExiste(texte, titre, type))
                    {
                        MessageBox.Show("L'observation est déjà enregistrée en base.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    }
                    else
                    {
                        ctrl.ajouterObservation(typeObs, titre, texte, Parametres.Instance.stagiaire);

                        dataGridListAbsences.ItemsSource = ctrl.listeObservation(Parametres.Instance.stagiaire);
                        dataGridListAbsences.SelectedItem = dataGridListAbsences.Items[dataGridListAbsences.Items.Count - 1];
                    }
                }
                
                
            }
            dataGridListAbsences.Items.Refresh();
            // on peut réinitialiser la  saisie à 0
            flag_mode_saisie = 0;
        }

        private void btnSupprimer_Click(object sender, RoutedEventArgs e)
        {
            if (this.dataGridListAbsences.SelectedItem != null)
            {
                if(MessageBox.Show("Etes-vous CERTAIN de vouloir supprimer cette observation ?", "Confirmation de suppression", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes){
                   ctrl.supprimerObservation((Observation)this.dataGridListAbsences.SelectedItem, this.dataGridListAbsences.SelectedIndex);
                 dataGridListAbsences.Items.Refresh();
                 txtBoxTitre.Text = "";
                 txtBoxAuteur.Text = "";
                 txtBoxDate.Text = "";
                 txtBoxTexte.Text = "";
                 comboBox1.Items.Clear();
                 comboBox1.Items.Add("Pédagogique");
                 comboBox1.Items.Add("Entreprise");
                 comboBox1.Text = "Pédagogique";
                 comboBox1.SelectedValue = "Pédagogique";
                 dataGridListAbsences.ItemsSource = ctrl.listeObservation(Parametres.Instance.stagiaire);
                 dataGridListAbsences.Items.Refresh();
                }
                
            }
            else
            {
                 MessageBox.Show("Vous n'avez pas sélectionné d'observation dans la liste ci-dessus", "Erreur", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void btnAnnuler_Click(object sender, RoutedEventArgs e)
        {
            if (flag_mode_saisie == 1) //si on est en mode ajout
            {
                txtBoxTitre.Text = "";
                txtBoxAuteur.Text = "";
                txtBoxDate.Text = "";
                txtBoxTexte.Text = "";
                comboBox1.Items.Clear();
                comboBox1.Items.Add("Pédagogique");
                comboBox1.Items.Add("Entreprise");
                comboBox1.Text = "Pédagogique";
                comboBox1.SelectedValue = "Pédagogique";
            }
            else {
                if (this.dataGridListAbsences.SelectedItem != null)
                {
                    Observation obsSelectionne = (Observation)this.dataGridListAbsences.SelectedItem;
                    txtBoxTitre.Text = obsSelectionne._titre;
                    txtBoxAuteur.Text = obsSelectionne._nomAuteur;
                    txtBoxDate.Text = obsSelectionne._date.ToString("dd/MM/yyyy");
                    txtBoxTexte.Text = obsSelectionne._texte;
                    comboBox1.Items.Clear();
                    comboBox1.Items.Add("Pédagogique");
                    comboBox1.Items.Add("Entreprise");
                    comboBox1.Text = "Pédagogique";
                    comboBox1.SelectedValue = "Pédagogique";
                    dataGridListAbsences.Items.Refresh();
                }
                else {
                    MessageBox.Show("Il n'y a aucune saisie ou modification à annuler.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
        }

        private bool OperationExiste (String pTexte, String pTitre, String pType)
        { 
            bool retour = false;
            foreach (Observation o in ctrl.listeObservation(Parametres.Instance.stagiaire))
            {
                    if (o._texte == pTexte &&
                        o._titre == pTitre &&
                        o._type == pType &&
                        o._nomAuteur == Parametres.Instance.login){
                        retour = true;
                    }
            }
            return retour;
        }


}
}
