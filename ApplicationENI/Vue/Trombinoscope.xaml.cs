﻿using System;
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
        public Trombinoscope()
        {
            InitializeComponent();
            groupBox2.Visibility = Visibility.Hidden;
            groupBox3.Visibility = Visibility.Hidden;


            CtrlTrombinoscope ctrlStagiaire = new CtrlTrombinoscope();
            List<Promotion> lesPromos = ctrlStagiaire.listePromotion();

            foreach (Promotion p in lesPromos)
            {
                cboFormation.Items.Add(p);
            }
        }

        private void cboFormation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            groupBox2.Visibility = Visibility.Hidden;
            groupBox3.Visibility = Visibility.Hidden;
        }

        private void btnAfficherTrombi_Click(object sender, RoutedEventArgs e)
        {
            if (groupBox2.Visibility == Visibility.Hidden)
            {
                if (cboFormation.SelectedItem != null)
                {
                    CtrlTrombinoscope ctrlTrombi = new CtrlTrombinoscope();

                    
                    List<Stagiaire> listStagiaire = ctrlTrombi.listeStagiaires(cboFormation.Text);
                    if (listStagiaire.Count == 0)
                    {
                        Label lab = new Label();
                        lab.Foreground = new SolidColorBrush(Colors.Red);
                        lab.FontWeight = FontWeights.Bold;
                        lab.Content = "Pas de stagiaires disponibles pour cette promotion.";
                        Grid g = new Grid();
                        g.Children.Add(lab);
                        groupBox3.Visibility = Visibility.Visible;
                        groupBox3.Header = "Résultat";
                        groupBox3.Content = g;
                    }
                    else { 
                        groupBox2.Visibility = Visibility.Visible;
                        groupBox3.Visibility = Visibility.Visible;
                        Grid tableauImages = new Grid();
                        int i = 0;
                        int j = 0;
                        int photo = 1;
                        RowDefinition testRow, testRowNomStagiaire;
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
                    MessageBox.Show("Veuillez sélectionner au préalable une formation ET un cours.", "Veuillez renseigner les paramètres", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        
        }

        private void buttonImprimer_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog pD = new PrintDialog();
            if (pD.ShowDialog() == true)
            {
                pD.PrintVisual(gridTrombi, "Trombinoscope");
            }
            //TODO: améliorer le rendu.
            /*ReportViewer rv = new ReportViewer();
            rv.ProcessingMode = ProcessingMode.Local;
            ReportDataSource rds = new ReportDataSource(
            rv.LocalReport.DataSources.Add();*/
           
            /*pD.PrintVisual(this, "Impression Trombinoscope");
            var fixedDocument = new FixedDocument();
            pD.PrintDocument(fixedDocument.DocumentPaginator, "Impression");*/
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

        private void cboCours_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            groupBox2.Visibility = Visibility.Hidden;
            groupBox3.Visibility = Visibility.Hidden;
        }
    }
}
