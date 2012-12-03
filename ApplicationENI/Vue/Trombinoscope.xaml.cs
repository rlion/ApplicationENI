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
using System.Net.Mail;

namespace ApplicationENI.Vue
{
    /// <summary>
    /// Logique d'interaction pour Trombinoscope.xaml
    /// </summary>
    public partial class Trombinoscope : UserControl
    {
        private static String filtre = "";
        CtrlTrombinoscope ctrlStagiaire = new CtrlTrombinoscope();
        
        public Trombinoscope()
        {
            
            InitializeComponent();
            cbFiltre.SelectedValuePath = "Key";
            cbFiltre.DisplayMemberPath = "Value";
            cbFiltre.SelectedIndex = -1;
            groupBox2.Visibility = Visibility.Hidden;
            groupBox3.Visibility = Visibility.Hidden;
            btnAfficherTrombi.IsEnabled = false;
        }

      

        private void btnAfficherTrombi_Click(object sender, RoutedEventArgs e)
        {
            if (cbFiltre.SelectedItem != null || rbModule.IsChecked.Value == true){
                    CtrlTrombinoscope ctrlTrombi = new CtrlTrombinoscope();
                    
                    List<Stagiaire> listStagiaire = ctrlTrombi.listeStagiaires(filtre);
                    if (listStagiaire.Count == 0)
                    {
                        Label lab = new Label();
                        lab.Foreground = new SolidColorBrush(Colors.Red);
                        lab.FontWeight = FontWeights.Bold;
                        lab.Content = "Pas de stagiaires disponibles.";
                        Grid g = new Grid();
                        g.Children.Add(lab);
                        groupBox2.Visibility = Visibility.Hidden;
                        groupBox3.Visibility = Visibility.Hidden;
                        groupBox4.Visibility = Visibility.Visible;
                        groupBox4.Content = g;
                        
                    }
                    else { 
                        groupBox2.Visibility = Visibility.Visible;
                        groupBox4.Visibility = Visibility.Hidden;
                        groupBox3.Visibility = Visibility.Visible;
                        Grid tableauImages = new Grid();
                        int i = 0;
                        int j = 0;
                        int photo = 1;
                        RowDefinition testRow, testRowNomStagiaire;
                        if (gridTrombi.RowDefinitions.Count > 0)
                        {
                            gridTrombi.RowDefinitions.RemoveRange(0, gridTrombi.RowDefinitions.Count);
                        }
                        if (gridTrombi.ColumnDefinitions.Count > 0)
                        {
                            gridTrombi.ColumnDefinitions.RemoveRange(0, gridTrombi.ColumnDefinitions.Count);
                        }
                        foreach (Stagiaire s in listStagiaire)
                        {
                            if (i - 4 == 0)
                            {
                                i = 0;
                                j += 2;
                            }

                            ColumnDefinition testColumn = new ColumnDefinition();
                            testColumn.Width = new GridLength(140);
                            gridTrombi.ColumnDefinitions.Add(testColumn);

                            if (i == 0)
                            {
                                testRow = new RowDefinition();
                                testRow.Height = new GridLength(120);
                                gridTrombi.RowDefinitions.Add(testRow);
                            }

                            gridTrombi.Width = 1000;

                            Image image = new Image();
                            image.BeginInit();

                            TextBox txt = new TextBox();
                            BitmapImage img;
                            try
                            {
                                img = new BitmapImage(new Uri(s._photo));
                                
                            }
                            catch (Exception)
                            {
                                img = new BitmapImage(new Uri("pack://application:,,,/ApplicationENI;component/Images/portrait-vide.jpg"));
                            }

                            image.Source = img;
                            image.Stretch = Stretch.Uniform;
                            TextBox txtBoxTest = new TextBox();
                            txtBoxTest.Background = Brushes.AliceBlue;
                            txtBoxTest.TextAlignment = TextAlignment.Center;
                            txtBoxTest.BorderThickness = new Thickness(0);

                            image.SetValue(Grid.ColumnProperty, i);
                            image.SetValue(Grid.RowProperty, j);
                            txtBoxTest.Text = s._prenom + " " + s._nom;
                            txtBoxTest.SetValue(Grid.ColumnProperty, i);
                            txtBoxTest.SetValue(Grid.RowProperty, j + 1);
                            gridTrombi.Children.Add(image);

                            // redéfinition de la hauteur de ligne pour le nom du stagiaire
                            if (i == 0)
                            {
                                testRowNomStagiaire = new RowDefinition();
                                testRowNomStagiaire.Height = new GridLength(30);
                                gridTrombi.RowDefinitions.Add(testRowNomStagiaire);
                            }

                            gridTrombi.Children.Add(txtBoxTest);

                            i += 1;
                            photo++;
                        }
                    }
                    
                }
                else {
                    MessageBox.Show("Veuillez au préalable sélectionner un ou plusieurs critère(s).", "Critère(s) manquant(s)", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            
        
        }

        private void buttonImprimer_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog pD = new PrintDialog();
            String nom = "";

            if (pD.ShowDialog() == true)
            {
                if (cbFiltre.SelectedItem != null)
                {
                    nom = cbFiltre.Text;
                }
                pD.PrintVisual(gridTrombi, "Trombinoscope - " + nom);
            }
        }

        private void buttonEnvoiParMail_Click(object sender, RoutedEventArgs e)
        {
            // Portion de code supprimée, pourrait faire partie des évolutions de la 2.0 sur demande du client.
            /*MailMessage msg = new MailMessage();

            msg.From = new MailAddress(Parametres.Instance.login + "@eni-ecole.fr");
            msg.To.Add(new MailAddress("ADDESTINATAIRE", "NOM"));
            msg.Body = "Bonjour\n" +
            "Ci-joint le trombinoscope pour cette semaine. \n" +
            "A bientôt";
           // msg.Attachments.Add(new Attachment(@"c:\fichierjoint.txt"));

            SmtpClient client = new SmtpClient("SERVEUR_SNMP", 587);
            //client.EnableSsl = true;
            // Envoi du mail
            client.Send(msg);*/
        }

        // formation continue
        private void rbFormation_Checked(object sender, RoutedEventArgs e)
        {
            labFiltre.Content = "Formation :";
            filtre = ", Formation f, Promotion p where s.CodeStagiaire=i.CodeStagiaire " +
                "and i.CodeFormation=f.CodeFormation and i.CodePromotion=p.CodePromotion";

            cbFiltre.ItemsSource = ctrlStagiaire.GetListePromotions();
            cbFiltre.IsEnabled = true;
            btnAfficherTrombi.IsEnabled = false;
        }

        // contrat de pro.
        private void rbPromotion_Checked(object sender, RoutedEventArgs e)
        {
            labFiltre.Content = "Promotion :";
            filtre = ", Formation f where s.CodeStagiaire=i.CodeStagiaire " +
                "and i.CodeFormation=f.CodeFormation and i.CodePromotion is null";

            cbFiltre.ItemsSource = ctrlStagiaire.GetListeFormations();
            cbFiltre.IsEnabled = true;
            btnAfficherTrombi.IsEnabled = false;
        }

        private void rbModule_Checked(object sender, RoutedEventArgs e)
        {
            labFiltre.Content = "";
            filtre = " where s.CodeStagiaire=i.CodeStagiaire and i.CodeFormation is null and i.CodePromotion is null";
            cbFiltre.IsEnabled = false;
            cbFiltre.ItemsSource = null;
            btnAfficherTrombi.IsEnabled = true;
        }

        private void cbFiltre_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.IsInitialized && cbFiltre.IsEnabled && cbFiltre.SelectedIndex != -1)
            {
                btnAfficherTrombi.IsEnabled = true;
                if (rbFormation.IsChecked.HasValue && rbFormation.IsChecked.Value)
                {
                    filtre = ", Promotion p where s.CodeStagiaire=i.CodeStagiaire and " +
                        "i.CodePromotion=p.CodePromotion and p.CodePromotion='" + (String)cbFiltre.SelectedValue + "'";
                }
                else
                {
                    filtre = ", Formation f where s.CodeStagiaire=i.CodeStagiaire and " +
                       "i.CodePromotion=f.CodeFormation and f.CodeFormation='" + (String)cbFiltre.SelectedValue + "'";
                }

            }
        }

       
    }
}
