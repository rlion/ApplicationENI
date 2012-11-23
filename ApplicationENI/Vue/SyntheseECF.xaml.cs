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
using ApplicationENI;
using ApplicationENI.Controleur;

namespace ApplicationENI.Vue
{
    /// <summary>
    /// Logique d'interaction pour SyntheseECF.xaml
    /// </summary>
    public partial class SyntheseECF : UserControl
    {
        private Stagiaire _stagiaireEncours=null;
        private List<SessionECF> _lesSessionsECFsStag=null;
        
        public SyntheseECF()
        {
            InitializeComponent();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            //StreamReader fileReader = new StreamReader(System.IO.Path.Combine(Environment.CurrentDirectory, @"..\..\ressources\resultats.txt"));
            //String stringReader = "";

            //while (!(fileReader.EndOfStream))
            //{
            //    stringReader = fileReader.ReadLine();
            //    listBox1.Items.Add(stringReader);
            //}
            //fileReader.Close();

            _stagiaireEncours = Parametres.Instance.stagiaire;
            _lesSessionsECFsStag = CtrlGestionECF.getListSessionsECFStagiaire(_stagiaireEncours);

            foreach (SessionECF sess in _lesSessionsECFsStag)
            {
                tvSynthese.Items.Add(sess);
            }
            foreach (TreeViewItem tvi in tvSynthese.Items)
            {
                //SessionECF session = (SessionECF)tvi.Item;

            }
        }


    }
}
