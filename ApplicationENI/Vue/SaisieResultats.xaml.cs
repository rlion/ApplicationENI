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
using System.IO;
using ApplicationENI.Modele;
using ApplicationENI.DAL;

namespace ApplicationENI.Vue
{
    /// <summary>
    /// Logique d'interaction pour SaisieResultats.xaml
    /// </summary>
    public partial class SaisieResultats : UserControl
    {        
        private List<SessionECF> _listeSessionECFs = null;
        private ECF _ecfCourant = null;
        private List<DateTime> _planif = new List<DateTime>();

        //private class ECFSession
        //{
        //    private ECF _ecf;
        //    private int _version;
        //    private DateTime _dateSession;
        //    private List<StagiaireSession> _lesStagiairesSession;

        //    private class StagiaireSession
        //    {
        //        private Stagiaire _stagiaireSession;
        //        private Competence _competenceSession;
        //        private Resultat _resultat;

        //        private class Resultat
        //        {
        //            private int _note=0;
        //            private bool _notationNumerique = false;
        //        }
        //    }
        //}

        
        public SaisieResultats()
        {
            InitializeComponent();

            _listeSessionECFs = SessionECFDAL.getListSessionsECFs();
            //cbECF.ItemsSource = _listeSessionECFs; redondance
            foreach (SessionECF sessEcf in _listeSessionECFs)
            {
                if (!cbECF.HasItems)
                {
                    cbECF.Items.Add(sessEcf.Ecf);
                }
                else if (!cbECF.Items.Contains(sessEcf.Ecf))
                {
                    cbECF.Items.Add(sessEcf.Ecf);
                }
            }
        }

        private void cbECF_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            calendar1.IsEnabled = true;

            _ecfCourant = (ECF)cbECF.SelectedItem;
            foreach (SessionECF sessEcf in _listeSessionECFs)
            {
                if (sessEcf.Ecf == _ecfCourant)
                {
                    _planif.Add(sessEcf.Date);
                    calendar1.SelectedDates.Add(sessEcf.Date);
                }
            }
        }

        private void btAdd1_Click(object sender, RoutedEventArgs e)
        {
            PopUp.AjoutSessionECF popup = new PopUp.AjoutSessionECF();
            popup.ShowDialog();
        }

        //private void Grid_Loaded(object sender, RoutedEventArgs e)
        //{
        //    StreamReader fileReader = new StreamReader(System.IO.Path.Combine(Environment.CurrentDirectory, @"..\..\ressources\competences.txt"));
        //    String stringReader = "";

        //    while (!(fileReader.EndOfStream))
        //    {
        //        stringReader = fileReader.ReadLine();
        //        listBox1.Items.Add(stringReader);
        //    }
        //    fileReader.Close();

        //    fileReader = new StreamReader(System.IO.Path.Combine(Environment.CurrentDirectory, @"..\..\ressources\stagiaires.txt"));
        //    stringReader = "";

        //    while (!(fileReader.EndOfStream))
        //    {
        //        stringReader = fileReader.ReadLine();
        //        listBox2.Items.Add(stringReader);
        //    }
        //    fileReader.Close();
        //}

        //private void image1_ImageFailed(object sender, RoutedEventArgs e)
        //{
        //    //?
        //}


    }
}
