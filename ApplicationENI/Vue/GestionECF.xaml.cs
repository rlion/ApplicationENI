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
using ApplicationENI.Controleur;

namespace ApplicationENI.Vue
{
    /// <summary>
    /// Logique d'interaction pour GestionECF.xaml
    /// </summary>
    public partial class GestionECF : UserControl
    {
        #region Propriétés
        //private List<ECF> _listeECF = null; //liste de tous les ECFs
        //private ECF _ecfCourant = null; //ecf selectionne         
        //private bool _ecfAdd; //si true on est en train d'ajouter un ECF sinon une Competence
        private CtrlGestionECF _ctrlGestionECF = null;        
        #endregion

        #region getter/setter
        //public bool EcfAdd
        //{
        //    get { return _ecfAdd; }
        //    set { _ecfAdd = value; }
        //}
        //public ECF EcfCourant
        //{
        //    get { return _ecfCourant; }
        //    set { _ecfCourant = value; }
        //}
        public CtrlGestionECF CtrlGestionECF
        {
            get { return _ctrlGestionECF; }
            set { _ctrlGestionECF = value; }
        }
        #endregion

        #region constructeur
        public GestionECF()
        {
            InitializeComponent();

            _ctrlGestionECF=new CtrlGestionECF();

            ActualiseAffichage(null);
        }
        #endregion

        #region affichage
        private void ActualiseAffichage(ECF pECFCourant){
            //RAZ de la combo
            cbECF.ItemsSource = null;//cbECF.Items.Clear();
            //recup de la liste d'ECF
            _ctrlGestionECF.ListeECF = _ctrlGestionECF.getListECFs();
            //peuplement de la combobox
            cbECF.ItemsSource = _ctrlGestionECF.ListeECF;

            //ECF courant (selectionné)
            if (pECFCourant != null)
            {
                if (_ctrlGestionECF.ListeECF!=null)
                {
                    foreach (ECF ecf in _ctrlGestionECF.ListeECF)
                    {
                        if (ecf.Id == pECFCourant.Id) pECFCourant = ecf;
                    }
                }
                
                cbECF.SelectedItem = pECFCourant;
            }
        }
        private void afficheECF(ECF pECF)
        {
            RAZ();

            //si pas d'ECF selectionné on ne peut pas ajouter de competence
            if (pECF == null) //pECF.Equals(null))
            {
                btAjoutFormation.IsEnabled = false;
                btAjoutCompetence.IsEnabled = false;
                return;
            }
            else
            {
                cbECF.SelectedItem = pECF;
                btAjoutCompetence.IsEnabled = true;
                btAjoutFormation.IsEnabled = true;
            }

            //AFFICHAGE
            //libelle
            tbLibECF.Text = pECF.Libelle;
            //TODO? MAT coeff
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
            lbCompetences.ItemsSource = null;
            lbCompetences.Items.Clear();
            lbCompetences.ItemsSource = pECF.Competences;
            //formations
            lbFormations.ItemsSource = null;
            lbFormations.Items.Clear();
            lbFormations.ItemsSource = pECF.Formations;
        }
        private void RAZ()
        {
            //RAZ
            tbLibECF.Text = "";
            rbNumerique.IsChecked = true;
            slVersion.Value = 1;
            lbCompetences.ItemsSource = null;
            lbFormations.ItemsSource = null;
            tbCommECF.Text = "";
        }
        #endregion

        #region evenements
    
        #region competences
        private void btAjoutCompetence_Click(object sender, RoutedEventArgs e)
        {
            _ctrlGestionECF.EcfAdd = false;// on ne va pas ajouter un ECF (ie on ajoute une Competence)
            //Affichage de l'écran d'ajout
            ListeECF_Competences ajoutCompetence = new ListeECF_Competences(((List<Competence>)lbCompetences.ItemsSource),_ctrlGestionECF.EcfCourant, _ctrlGestionECF.EcfAdd);
            ajoutCompetence.ShowDialog();

            //MAJ de l'affichage sur l'ECF courant
            ActualiseAffichage(_ctrlGestionECF.EcfCourant);
            afficheECF(_ctrlGestionECF.EcfCourant);//cbECF.SelectedItem = _ecfCourant;
        }
        private void btSupprCompetence_Click(object sender, RoutedEventArgs e)
        {
            //Suppression des liens avec les compétences sélectionnées
            if (lbCompetences.SelectedItems!=null)
            {
                foreach (Competence comp in lbCompetences.SelectedItems)
                {                    
                    String reponse = _ctrlGestionECF.supprimerLienCompetence(_ctrlGestionECF.EcfCourant, comp);
                    if (reponse != "")
                    {
                        MessageBox.Show(reponse, "Attention!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    }
                    else
                    {
                        _ctrlGestionECF.EcfCourant.Competences.Remove(comp);
                    }
                }
            }            

            //MAJ de l'affichage de l'ECF courant
            afficheECF(_ctrlGestionECF.EcfCourant);
        }
        private void lbCompetences_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbCompetences.SelectedItems.Count > 0)
            {
                btSupprCompetence.IsEnabled = true;
            }
            else
            {
                btSupprCompetence.IsEnabled = false;
            }
        }       
        #endregion

        #region formations
        private void btAjoutFormation_Click(object sender, RoutedEventArgs e)
        {
            //_ecfAdd = false;// on ne va pas ajouter un ECF (ie on ajoute une Competence)
            //Affichage de l'écran d'ajout
            ListeECF_Formations ajoutFormation = new ListeECF_Formations(((List<Formation>)lbFormations.ItemsSource),_ctrlGestionECF.EcfCourant);
            ajoutFormation.ShowDialog();

            //MAJ de l'affichage sur l'ECF courant
            ActualiseAffichage(_ctrlGestionECF.EcfCourant);
            afficheECF(_ctrlGestionECF.EcfCourant);//cbECF.SelectedItem = _ecfCourant;
        }
        private void btSupprFormation_Click(object sender, RoutedEventArgs e)
        {
            //Suppression des liens avec les formations sélectionnées
            if (lbFormations.SelectedItems!=null)
            {
                foreach (Formation form in lbFormations.SelectedItems)
                {
                    _ctrlGestionECF.EcfCourant.Formations.Remove(form);
                    _ctrlGestionECF.supprimerLienFormation(_ctrlGestionECF.EcfCourant, form);
                }
            }            

            //MAJ de l'affichage de l'ECF courant
            afficheECF(_ctrlGestionECF.EcfCourant);
        }
        private void lbFormations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbFormations.SelectedItems.Count > 0)
            {
                btSupprFormation.IsEnabled = true;
            }
            else
            {
                btSupprFormation.IsEnabled = false;
            }
        }
        #endregion

        #region ecf
        private void btAjoutECF_Click(object sender, RoutedEventArgs e)
        {
            _ctrlGestionECF.EcfAdd = true;// on va ajouter un ECF
            //Affichage de l'écran d'ajout
            AjoutECF_Competence ajoutECF = new AjoutECF_Competence(_ctrlGestionECF.EcfAdd);
            ajoutECF.ShowDialog();

            //MAJ de l'affichage sur l'ECF créé
            if (ajoutECF.CtrlAjoutECF_Competence.ECF != null)
            {
                _ctrlGestionECF.EcfCourant = ajoutECF.CtrlAjoutECF_Competence.ECF;
                ActualiseAffichage(_ctrlGestionECF.EcfCourant);
                afficheECF(_ctrlGestionECF.EcfCourant);
            }
        }
        private void cbECF_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //MAJ de l'ECF courant
            _ctrlGestionECF.EcfCourant = (ECF)cbECF.SelectedItem;

            if (_ctrlGestionECF.EcfCourant != null)
            {
                afficheECF(_ctrlGestionECF.EcfCourant);
                btSupprECF.IsEnabled = true;
                btSave.IsEnabled = true;
                btCancel.IsEnabled = true;
            }
            else
            {
                btSupprECF.IsEnabled = false;
                btSave.IsEnabled = false;
                btCancel.IsEnabled = false;
            }
        }    
        private void btSave_Click(object sender, RoutedEventArgs e)
        {
            //Récup des propriétés saisies
            _ctrlGestionECF.EcfCourant.Libelle = tbLibECF.Text.Trim();
            _ctrlGestionECF.EcfCourant.NbreVersion = (int)slVersion.Value;
            _ctrlGestionECF.EcfCourant.NotationNumerique = true;
            if (rbAcquisition.IsChecked == true)
            {
                _ctrlGestionECF.EcfCourant.NotationNumerique = false;
            }
            _ctrlGestionECF.EcfCourant.Commentaire = tbCommECF.Text.Trim();

            //Modification de l'ECF
            String reponse = _ctrlGestionECF.modifierECF(_ctrlGestionECF.EcfCourant);

            if(reponse!="")
            {
                MessageBox.Show(reponse, "Attention!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }

            //Actualisation de l'affichage
            RAZ();
            ActualiseAffichage(null);
        }        
        private void btSupprECF_Click(object sender, RoutedEventArgs e)
        {
            //TODO confirmer la suppression
            //Suppression de l'ECF courant
            _ctrlGestionECF.supprimerECF(_ctrlGestionECF.EcfCourant);

            //MAJ de l'affichage
            RAZ();
            ActualiseAffichage(null);
        }
        #endregion

        private void slVersion_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            //ActualiseAffichage dans le label correspondant du nombre de version du slider
            if (this.lbNbVersions!=null)
            {
                this.lbNbVersions.Content = slVersion.Value;
            }    
        }        
        private void btCancel_Click(object sender, RoutedEventArgs e)
        {
            //_ecfCourant = ECFDAL.getECF(_ecfCourant);
            //MAJ de l'affichage
            if (_ctrlGestionECF.EcfCourant != null)
            {
                RAZ();
                ActualiseAffichage(_ctrlGestionECF.EcfCourant);
            }
        }

        #endregion
    }
}
//FORMATIONS :
//-----------------------------------------------------------------------------
//DL: NOTES A NA ou ECA
//Initiation a la programmation         2s
//Programmation Orientée Objet (C#)     2s
//SQL (SQL Server)                      1s
//PL/SQL (Oracle)                       1s
//Appli. client/serveur (VB.Net)        4s      ECF - Base de données
//Mise en pratique –Projet (VB.Net)     2s      ECF - Application client/serveur
//Analyse & conception                  3s      ECF - Modélisation
//Appli. Internet/Intranet (Java EE)    4s
//Introduction à PHP                    2s
//Mise en pratique –Projet (Java)       3s      ECF - Anglais (2ème semaine jeudi) / ECF - Développement Web (2ème semaine vendredi)

//CDI:
//SQL (SQL Server)                      1s
//PL/SQL (Oracle)                       1s
//Appli. client/serveur (VB.Net)        4s      ECF – Base de données
//Mise en pratique –Projet (VB.Net)     2s      ECF – Application client/serveur
//Analyse & conception                  3s      ECF – Modélisation
//Appli. Internet/Intranet (Java EE)    4s
//Introduction à PHP                    2s
//Mise en pratique –Projet (Java)       3s      ECF – Anglais (2ème semaine jeudi) / ECF - Développement Web (2ème semaine vendredi)
//Java EE avancé                        2s      ECF - Développement  multi-tiers
//Administration Tomcat                 1s      ECF - Administration Tomcat
//Gestion de projet                     1s
//MS Project                            1s
//Mise  en pratique - Projet            3s      ECF - Gestion de projet

//ECF – Base de données
    //C04: Manipuler les données avec le langage SQL
    //C13: Mettre en place la base de données
    //C14: Programmer dans le langage du SGBD
//ECF – Développement client-serveur
    //C08: Organiser son temps
    //C20: Développer les composants de la couche présentation (IHM)
    //C02: Programmer les formulaires et les états
    //C06: Installer des composants
    //C05: Développer les composants d'accès aux données
    //C19: Manipuler les données réparties dans une architecture client-serveur x-tiers
//ECF – Modélisation
    //C12: Modéliser les données
//ECF – Développement Web
    //C01: Maquetter l'application
    //C03: Programmer des pages Web
    //C22: Réaliser un test d'intégration
//ECF – Développement multi-tiers
    //C15: Définir l'architecture de l'application
    //C16: Modéliser l'application à développer en utilisant UML
    //C18: Développer les composants métier
    //C21: Développer des composants intégrés à l'informatique nomade
//ECF – Administration Tomcat
    //C23: Déployer l'application
//ECF – Conduite de projet
    //C17: Appliquer une démarche qualité
    //C24: Animer l'équipe de développement
//ECF – Anglais
    //C10: Utiliser l'anglais dans son activité professionnelle en informatique 
//ECF – Exposé
    //C07: Assister les utilisateurs 
    //C09: Communiquer dans un contexte professionnel 
    //C11: Actualiser ses compétences techniques
//-----------------------------------------------------------------------------
//ASR-SSR (?): NOTES SUR 20
//Unité de formation 1 : Gestion du poste de travail    
    //Installation et configuration de Windows 7            1s
    //Utilisation d’un système UNIX                         1s
    //CISCO 1 : Notions de base sur les réseaux             2s      ECF1A Bdr
//Unité de formation 2 : Administration des systèmes informatiques
    //Administration et maintenance d'un environnement 
    //Microsoft Windows Server 2008                         1s
    //TP de synthèse                                        1s      ECF1B Windows/Unix/Elect
    //Administration des systèmes Unix/Linux                1s
    //Administration avancée Unix/Linux                     1s
    //TP de synthèse                                        1s  
    //Configuration et résolution des problèmes des 
    //services de domaine Active Directory Windows 
    //Server 2008                                           1s
    //TP de synthèse                                        1s      ECF2
//Unité de formation 3 : Administration des services réseaux
    //Services Réseaux Windows                              1s
    //TP de Synthèse                                        1s
    //Administration des services réseaux sous Unix/Linux   2s
    //TP de Synthèse                                        1s
    //Automatisation des tâches d’administration:Scripting  1s
    //CISCO 2 : Protocoles et concepts de  routage          1s
    //TP de Synthèse                                        1s      ECF 3A / ECF5T1 Oral
//Unité de formation 4: Exploitation et administration de services transverses
    //Messagerie et communications unifiées                 1s
    //TP de Synthèse                                        1s
    //Supervision, Téléphonie                               1s
    //Sécurité des réseaux                                  1s
    //Organisation d’une DSI, ITIL et gestion de projet     1s      ECF 3B / ECF4A / ECF 5T2 Anglais
//UE5?????????????????????????????????????????????????????????
//Unité de formation 6 : Conception et projets d’infrastructure
    //Cisco 3: Commutation de réseau local                  1s
    //Gestion de projet : Partie 2                          1s
    //Gestion de projet : Partie 3                          1s      ECF3C / (ECF5A)
    //Virtualisation de serveur : conception et réalisation 1s
    //Conception d’un infrastructure réseaux Sécurisée      2s
    //Projet Infrastructure                                 2s      ECF5B / ECF5C / (ECF5A)
//Unité de formation 7 : Bases de données et architecture du système d’information
    //Le langage de requête SQL                             1s
    //Administration d’une base de données SQL Server       1s
    //Administration d’une base de données ORACLE           1s
    //Présentation des architectures applicatives           1s      ECF4B1
    //TP                                                    1s      ECF4B2






















