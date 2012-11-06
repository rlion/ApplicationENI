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

namespace ApplicationENI.Vue.PopUp {
    /// <summary>
    /// Logique d'interaction pour GestionJury.xaml
    /// </summary>
    public partial class GestionJury : Window 
    {
        private List<Jury> listeJury;
        private List<JuryItem> listeJuryItem;
        private bool isModified;//Gestion texte de départ AutoCompleteBox
        private EpreuveTitre epreuveTitre;

        public List<Jury> ListeJury
        {
            get { return listeJury; }
        }

        public GestionJury() 
        {
            InitializeComponent();
        }

        public GestionJury(EpreuveTitre epreuveTitre):this()
        {
            this.epreuveTitre = epreuveTitre;
            this.listeJury = epreuveTitre.ListeJury;
            isModified = false;
            InitData();
        }

        private void InitData()
        {
            listeJuryItem = new List<JuryItem>();

            foreach(Jury j in listeJury) listeJuryItem.Add(new JuryItem(j));
            this.lbListeJures.ItemsSource = listeJuryItem;

            acbNomPrenom.ItemsSource = DAL.TitresDAL.GetListeJury();
        }

        private class JuryItem
        {
            private Jury _jury;
            private bool _isChecked;

            public Jury Jury
            {
                get { return _jury; }
                set { _jury = value; }
            }
            public bool IsChecked
            {
                get { return _isChecked; }
                set { _isChecked = value; }
            }

            public JuryItem(Jury jury, bool isChecked=true)
            {
                this._jury = jury;
                this._isChecked = isChecked;
            }

            public override string ToString()
            {
                return _jury.Civilite + " " + _jury.Nom + " " + _jury.Prenom;
            }
        }

        private void acbNomPrenom_GotFocus(object sender, RoutedEventArgs e)
        {
            if(this.IsInitialized && !isModified && !string.IsNullOrEmpty(this.acbNomPrenom.Text))
            {
                this.acbNomPrenom.Text = string.Empty;
                isModified = true;
            }
        }

        private void btAddJure_Click(object sender, RoutedEventArgs e)
        {
            if(acbNomPrenom.SelectedItem != null && listeJuryItem.Where(x => x.Jury == ((Jury)acbNomPrenom.SelectedItem)).Count() == 0)
            {
                listeJuryItem = (List<JuryItem>)lbListeJures.ItemsSource;
                listeJuryItem.Add(new JuryItem((Jury)acbNomPrenom.SelectedItem));

                lbListeJures.ItemsSource = null;
                lbListeJures.Items.Clear();
                lbListeJures.ItemsSource = listeJuryItem;

                acbNomPrenom.Text = string.Empty;
                acbNomPrenom.SelectedItem = null;
            }
        }

        private void btAnnuler_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btNewJure_Click(object sender, RoutedEventArgs e)
        {
            PopUp.NewJure newjure = new NewJure();
            newjure.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            newjure.ShowDialog();

            //Après fermeture de la Pop-Up
            acbNomPrenom.ItemsSource = null;
            acbNomPrenom.ItemsSource = DAL.TitresDAL.GetListeJury();
        }

        private void btValider_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("Etes vous sûr(e) de valider cette liste de jurés pour cette date de passage du titre?", "Gestion du jury", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                List<Jury> newListeJury = new List<Jury>();
                foreach(JuryItem ji in listeJuryItem.Where(x => x.IsChecked)) newListeJury.Add(ji.Jury);

                listeJury = newListeJury;
                this.Close();
            }
        }

        private void btSupprimer_Click(object sender, RoutedEventArgs e)
        {
            if(acbNomPrenom.SelectedItem != null)
            {

                if(MessageBox.Show("Etes vous sûr(e) de vouloir supprimer définitivement ce juré?", "Gestion du jury", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    //Suppression définitive du juré
                    DAL.TitresDAL.SupprimerJury(((Jury)acbNomPrenom.SelectedItem).IdPersonneJury);

                    //Mise à jour ListBox si besoin
                    if(listeJuryItem.Where(x => x.Jury == ((Jury)acbNomPrenom.SelectedItem)).Count()==1)
                    {
                        listeJuryItem = (List<JuryItem>)lbListeJures.ItemsSource;
                        listeJuryItem.RemoveAll(x => x.Jury == (Jury)acbNomPrenom.SelectedItem);

                        lbListeJures.ItemsSource = null;
                        lbListeJures.Items.Clear();
                        lbListeJures.ItemsSource = listeJuryItem;
                    }

                    //Mise à jour AutoCompleteBox
                    acbNomPrenom.Text = string.Empty;
                    acbNomPrenom.SelectedItem = null;
                    acbNomPrenom.ItemsSource = null;
                    acbNomPrenom.ItemsSource = DAL.TitresDAL.GetListeJury();
                }
            }
        }
    }

}
