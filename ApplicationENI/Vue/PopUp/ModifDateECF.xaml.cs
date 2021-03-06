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
using System.Windows.Shapes;
using ApplicationENI.Modele;
using ApplicationENI.Controleur;

namespace ApplicationENI.Vue.PopUp
{
    /// <summary>
    /// Logique d'interaction pour ModifDateECF.xaml
    /// </summary>
    public partial class ModifDateECF : Window
    {
        private CtrlModifDateECF _ctrlModifDateECF = null;
        private SessionECF _sessionCourante = null;

        public ModifDateECF(SessionECF pSessionCourante)
        {
            InitializeComponent();

            _ctrlModifDateECF = new CtrlModifDateECF();
            _sessionCourante = pSessionCourante;

            _ctrlModifDateECF.SessionECF = _sessionCourante;
            _ctrlModifDateECF.Stagaire = Parametres.Instance.stagiaire;
            
            dateSel.DisplayDateStart = DateTime.Now;
            dateSel.SelectedDate = _ctrlModifDateECF.SessionECF.Date;

            tbInfo.Text = "Modification session ECF : " + _ctrlModifDateECF.SessionECF.ToString() + "\n" + "Stagiaire : " + _ctrlModifDateECF.Stagaire.ToString();
        }

        private void btValider_Click(object sender, RoutedEventArgs e)
        {
            if (dateSel.SelectedDate != null && _ctrlModifDateECF.SessionECF.Date != (DateTime)dateSel.SelectedDate)
            {
                if ((DateTime)dateSel.SelectedDate!=_ctrlModifDateECF.SessionECF.Date)
                {
                    //si il existe une note on empeche la modification de la date
                    String reponse = _ctrlModifDateECF.modifierDateSessionECF_Stagiaire(_ctrlModifDateECF.Stagaire, _ctrlModifDateECF.SessionECF, (DateTime)dateSel.SelectedDate);
                    if (reponse!="")
                    {
                        MessageBox.Show(reponse, "Attention!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    }
                    Close();
                }                
            }            
        }

        private void btAnnuler_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
