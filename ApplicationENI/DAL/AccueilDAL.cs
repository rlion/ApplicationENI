using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApplicationENI.Modele;
using System.Data.SqlClient;

namespace ApplicationENI.DAL
{
    public class AccueilDAL
    {

        public static List<Stagiaire> GetListeStagiaires(string filtre = null)
        {
            try
            {
                List<Stagiaire> listeStagiaires = new List<Stagiaire>();
                if(string.IsNullOrEmpty(filtre)) filtre = " where s.CodeStagiaire = i.CodeStagiaire";

                SqlConnection connexion = ConnexionSQL.CreationConnexion();

                if(connexion != null)
                {
                    string reqStag = "SELECT s.CodeStagiaire, Civilite, Nom, Prenom, Adresse1, Adresse2, Adresse3, " +
                                      "Codepostal, Ville, TelephoneFixe, TelephonePortable, Email, DateNaissance, " +
                                      "CodeRegion, CodeNationalite, CodeOrigineMedia, DateDernierEnvoiDoc, s.DateCreation, " +
                                      "Repertoire, Permis, Photo, EnvoiDocEnCours, Historique FROM Stagiaire s, PlanningIndividuelFormation i" + filtre;

                    SqlCommand commande = connexion.CreateCommand();
                    commande.CommandText = reqStag;
                    SqlDataReader reader = commande.ExecuteReader();

                    while(reader.Read())
                    {
                        int codS = !reader.IsDBNull(0) ? reader.GetInt32(0) : 0;
                        string civ = !reader.IsDBNull(1) ? reader.GetString(1) : string.Empty;
                        string nom = !reader.IsDBNull(2) ? reader.GetString(2) : string.Empty;
                        string pre = !reader.IsDBNull(3) ? reader.GetString(3) : string.Empty;
                        string adr1 = !reader.IsDBNull(4) ? reader.GetString(4) : string.Empty;
                        string adr2 = !reader.IsDBNull(5) ? reader.GetString(5) : string.Empty;
                        string adr3 = !reader.IsDBNull(6) ? reader.GetString(6) : string.Empty;
                        string cp = !reader.IsDBNull(7) ? reader.GetString(7) : string.Empty;
                        string ville = !reader.IsDBNull(8) ? reader.GetString(8) : string.Empty;
                        string telF = !reader.IsDBNull(9) ? reader.GetString(9) : string.Empty;
                        string telP = !reader.IsDBNull(10) ? reader.GetString(10) : string.Empty;
                        string mail = !reader.IsDBNull(11) ? reader.GetString(11) : string.Empty;
                        DateTime dateN = !reader.IsDBNull(12) ? reader.GetDateTime(12) : new DateTime();
                        string codR = !reader.IsDBNull(13) ? reader.GetString(13) : string.Empty;
                        string codN = !reader.IsDBNull(14) ? reader.GetString(14) : string.Empty;
                        string codOM = !reader.IsDBNull(15) ? reader.GetString(15) : string.Empty;
                        DateTime datePEC = !reader.IsDBNull(16) ? reader.GetDateTime(16) : new DateTime();
                        DateTime dateC = !reader.IsDBNull(17) ? reader.GetDateTime(17) : new DateTime();
                        string rep = !reader.IsDBNull(18) ? reader.GetString(18) : string.Empty;
                        bool permis = !reader.IsDBNull(19) ? reader.GetBoolean(19) : false;
                        string photo = !reader.IsDBNull(20) ? reader.GetString(20) : string.Empty;
                        bool envoiDEC = !reader.IsDBNull(21) ? reader.GetBoolean(21) : false;
                        string histo = !reader.IsDBNull(22) ? reader.GetString(22) : string.Empty;


                        listeStagiaires.Add(new Stagiaire(codS, civ, nom, pre, adr1, adr2, adr3, cp, ville, telP, telF, 
                            mail, dateN, codR, codN, codOM, datePEC, dateC, rep, permis, photo, envoiDEC, histo));
                    }
                }

                return listeStagiaires;
            }
            catch(Exception e)
            {
                System.Windows.MessageBox.Show("Impossible de récupérer la liste des stagiaires : " + e.Message, "Accueil général", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                return null;
            }
        }

        public static Dictionary<string,string> GetListeFormations()
        {
            try
            {
                SqlConnection connexion = ConnexionSQL.CreationConnexion();
                Dictionary<string,string> dictFormation = new Dictionary<string, string>();

                if(connexion != null)
                {
                    string reqFormation = "SELECT CodeFormation, LibelleCourt FROM Formation";

                    SqlCommand commande = connexion.CreateCommand();
                    commande.CommandText = reqFormation;
                    SqlDataReader reader = commande.ExecuteReader();

                    while(reader.Read())
                    {
                        string code = !reader.IsDBNull(0) ? reader.GetString(0) : string.Empty;
                        string lib = !reader.IsDBNull(1) ? reader.GetString(1) : string.Empty;
                        dictFormation.Add(code, lib);
                    }
                }

                return dictFormation;
            }
            catch(Exception e)
            {
                System.Windows.MessageBox.Show("Impossible de récupérer la liste des formations : " + e.Message, "Accueil général", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                return null;
            }
        }

        public static Dictionary<string,string> GetListePromotions()
        {
            try
            {
                SqlConnection connexion = ConnexionSQL.CreationConnexion();
                Dictionary<string,string> dictPromotion = new Dictionary<string, string>();

                if(connexion != null)
                {
                    string reqPromotion = "SELECT CodePromotion, Libelle FROM Promotion";

                    SqlCommand commande = connexion.CreateCommand();
                    commande.CommandText = reqPromotion;
                    SqlDataReader reader = commande.ExecuteReader();

                    while(reader.Read())
                    {
                        string code = !reader.IsDBNull(0) ? reader.GetString(0) : string.Empty;
                        string lib = !reader.IsDBNull(1) ? reader.GetString(1) : string.Empty;
                        dictPromotion.Add(code, lib);
                    }
                }

                return dictPromotion;
            }
            catch(Exception e)
            {
                System.Windows.MessageBox.Show("Impossible de récupérer la liste des promotions : " + e.Message, "Accueil général", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                return null;
            }
        }

        //CodeAlerte -> 0:information, 1:avertissement, 2:erreur, 3:interdiction
        public static List<ItemAlerte> GetListeAlertes()
        {
            try
            {
                List<ItemAlerte> listeAlertes = new List<ItemAlerte>();

                SqlConnection connexion = ConnexionSQL.CreationConnexion();
                if(connexion != null)
                {
                    SqlCommand commande = connexion.CreateCommand();

                    //Nombre d'absences
                    string reqAbsence = "SELECT count(*) from ABSENCE where CONVERT(date,dateDebut) = " +
                        "CONVERT(date,GETDATE()) and (CONVERT(date,dateFin) > CONVERT(date,GETDATE())"+
                        " or dateFin is null)";
                    
                    commande.CommandText = reqAbsence;
                    int nbAbsences = (int)commande.ExecuteScalar();
                    if(nbAbsences>0)
                        listeAlertes.Add(new ItemAlerte(0,"Il y a "+nbAbsences+" nouveaux absents aujourd'hui",0));

                    //Nombre de retards
                    string reqRetard = "SELECT count(*) from ABSENCE where CONVERT(date,dateFin) = "+
                                       "CONVERT(date,GETDATE()) and isAbsence=0";

                    commande.CommandText = reqRetard;
                    int nbRetards = (int)commande.ExecuteScalar();
                    if(nbRetards > 0)
                        listeAlertes.Add(new ItemAlerte(0, "Il y a " + nbRetards + " retards enregistrés aujourd'hui", 1));

                    //Liste des ECFS
                    List<SessionECF> listeECF = new List<SessionECF>();
                    listeECF = EvaluationsDAL.getListeSessionsECFNONCorriges();
                    if(listeECF != null)
                    {
                        foreach(SessionECF s in listeECF)
                        {
                            listeAlertes.Add(new ItemAlerte(1, "L'ECF " + s.Ecf.Libelle + " du " + s.Date.ToShortDateString() + " n'a pas encore été corrigé", 2));
                        }
                    }

                    connexion.Close();
                }

                return listeAlertes;
            }
            catch(Exception e)
            {
                System.Windows.MessageBox.Show("Impossible de récupérer la liste des alertes : " + e.Message, 
                    "Accueil général", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                return null;
            }
        }
    }
}
