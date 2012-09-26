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
using System.IO;
using ApplicationENI.Modele;
using ApplicationENI.Vue.PopUp;
using ApplicationENI.DAL;

namespace ApplicationENI.Vue
{
    /// <summary>
    /// Logique d'interaction pour GestionECF.xaml
    /// </summary>
    public partial class GestionECF : UserControl
    {
        private List<ECF> _listeECF = null;
        private ECF _ecfCourant = null;

        public GestionECF()
        {
            InitializeComponent();

            _listeECF = ECFDAL.getListECFs();

            //_listeECF = DAL.JeuDonnees.GetListECF();
            //foreach (ECF ecf in _listeECF)
            //{
            //    cbECF.Items.Add(ecf);
            //}
        }
        
        private void radioButton1_Checked(object sender, RoutedEventArgs e)
        {
            //?
        }

        private void slVersion_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (this.lbNbVersions!=null)
            {
                this.lbNbVersions.Content = slVersion.Value;
            }    
        }

        //private void Grid_Loaded(object sender, RoutedEventArgs e)
        //{
            //StreamReader fileReader = new StreamReader(System.IO.Path.Combine(Environment.CurrentDirectory, @"..\..\ressources\competences.txt"));
            //String stringReader = "";

            //while (!(fileReader.EndOfStream))
            //{
            //    stringReader = fileReader.ReadLine();
            //    lbCompetences.Items.Add(stringReader);
            //}
            //fileReader.Close();        
        //}

        private void image1_ImageFailed(object sender, RoutedEventArgs e)
        {
            //?
        }

        private void cbECF_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _ecfCourant = (ECF)cbECF.SelectedItem;
            afficheECF(_ecfCourant);
        }

        private void afficheECF(ECF pECF)
        {
            //code, libelle, coefficient, notationNumerique, nbreVersion, commentaire, competences
            //cbECF.SelectedIndex=0;
            tbLibECF.Text = "";
            rbNumerique.IsChecked = true;
            slVersion.Value = 1;
            lbCompetences.ItemsSource = null;    
            tbCommECF.Text = "";

            if (pECF == null)
            {
                return;
            }
            else
            {
                btAdd.IsEnabled = true;
            }


            //code
            //if (cbECF.Text != pECF.ToString()) cbECF.Text = pECF.ToString();

            //libelle
            tbLibECF.Text = pECF.Libelle;

            //TODO MAT coeff

            //type de notation
            if (pECF.NotationNumerique)
            {
                rbNumerique.IsChecked = true;
            }
            else
            {
                rbAcquisition.IsChecked = true;
            }

            //Nbre versions
            slVersion.Value = pECF.NbreVersion;

            //commentaire
            tbCommECF.Text = pECF.Commentaire;

            //competences
            lbCompetences.ItemsSource = pECF.Competences;


        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            AjoutECF_Competence popUp = new AjoutECF_Competence();
            popUp.ShowDialog();

            if (popUp.ECF != null)
            {
                _listeECF.Add(popUp.ECF);
                _ecfCourant = popUp.ECF;
                //cbECF.ItemsSource = null;
                cbECF.Items.Clear();
                foreach (ECF ecf in _listeECF)
                {
                    cbECF.Items.Add(ecf);
                }
                cbECF.SelectedItem = popUp.ECF;                               
            }
            if (popUp.Competence != null)
            {
                if (_ecfCourant.Competences == null) _ecfCourant.Competences = new List<Competence>();
                _ecfCourant.Competences.Add(popUp.Competence);
                afficheECF(_ecfCourant);                
            }
        }

        private void btDel_Click(object sender, RoutedEventArgs e)
        {
            foreach  (Competence comp in lbCompetences.SelectedItems)
	        {
		        _ecfCourant.Competences.Remove(comp);
	        }
            afficheECF(_ecfCourant);
        }

        private void lbCompetences_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbCompetences.SelectedItems.Count > 0)
            {
                btDel.IsEnabled = true;
            }
            else
            {
                btDel.IsEnabled = false;
            }
        }
    
    }
}
