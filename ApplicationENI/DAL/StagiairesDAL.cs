using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApplicationENI.Modele;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace ApplicationENI.DAL
{
    class StagiairesDAL
    {
        static String SELECT_LISTE_STAGIAIRES = "SELECT * FROM Stagiaire ORDER BY Nom, Prenom";
        static String SELECT_STAGIAIRE = "SELECT * FROM Stagiaire WHERE CodeStagiaire=@id";
        static String SELECT_INFOS_STAGIAIRE = "SELECT * FROM Stagiaire WHERE Nom=@nom";
        static String DELETE_STAGIAIRE = "DELETE FROM Stagiaire WHERE Nom=@nom";
        static String UPDATE_STAGIAIRE = "UPDATE Stagiaire SET Nom=@nouveauNom WHERE Nom=@nom";
        
        public static void modifierStagiaire(Stagiaire s)
        {
            SqlConnection connexion = ConnexionSQL.CreationConnexion();
            SqlCommand cmd = new SqlCommand(UPDATE_STAGIAIRE, connexion);
            cmd.Parameters.AddWithValue("@nom", s._nom); 
            cmd.Parameters.AddWithValue("@nouveauNom", "lenouveauNom");

            cmd.ExecuteReader();
            connexion.Close();
        }

        public static void supprimerStagiaire(Stagiaire s)
        {
            SqlConnection connexion = ConnexionSQL.CreationConnexion();
            SqlCommand cmd = new SqlCommand(DELETE_STAGIAIRE, connexion);
            cmd.Parameters.AddWithValue("@nom", s._nom);  

            cmd.ExecuteReader();
            connexion.Close();
        }

        //Quel est l'interet de cette fonction?
        public static Stagiaire getInfosStagiaire(String pNom) {
            
            SqlConnection connexion = ConnexionSQL.CreationConnexion();
            SqlCommand cmd = new SqlCommand(SELECT_INFOS_STAGIAIRE, connexion);
            cmd.Parameters.AddWithValue("@nom", pNom);

            SqlDataReader reader = cmd.ExecuteReader();

            Stagiaire stgRetour = new Stagiaire();
            
            if(reader.Read()) 
            {
                String nom = reader.GetSqlString(2).IsNull ? String.Empty : reader.GetString(2);
                stgRetour._nom = nom;
            }
            connexion.Close();
            return stgRetour;
        }

        public static Stagiaire getStagiaire(int pCodeStagiaire)
        {

            SqlConnection connexion = ConnexionSQL.CreationConnexion();
            SqlCommand cmd = new SqlCommand(SELECT_STAGIAIRE, connexion);
            cmd.Parameters.AddWithValue("@id", pCodeStagiaire);

            SqlDataReader reader = cmd.ExecuteReader();

            Stagiaire stgRetour = new Stagiaire();

            if (reader.Read())
            {
                stgRetour._nom = reader.GetSqlString(2).IsNull ? String.Empty : reader.GetString(2);
                stgRetour._prenom = reader.GetSqlString(3).IsNull ? String.Empty : reader.GetString(3);
                //TODO toutes les propriétés
            }
            connexion.Close();
            return stgRetour;
        }

        public static List<Stagiaire> getListeStagiaires()
        {

            SqlConnection connexion = ConnexionSQL.CreationConnexion();
            SqlCommand cmd = new SqlCommand(SELECT_LISTE_STAGIAIRES, connexion);

            List<Stagiaire> listeStagiaires = new List<Stagiaire>();

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Stagiaire s = new Stagiaire();
                s._id = reader.GetInt32(reader.GetOrdinal("CodeStagiaire"));
                s._civilité = reader.GetSqlString(1).IsNull ? string.Empty : reader.GetString(1);
                s._nom = reader.GetSqlString(2).IsNull ? string.Empty : reader.GetString(2);
                s._prenom = reader.GetSqlString(3).IsNull ? string.Empty : reader.GetString(3);
                s._adresse1 = reader.GetSqlString(4).IsNull ? string.Empty : reader.GetString(4);
                s._adresse2 = reader.GetSqlString(5).IsNull ? string.Empty : reader.GetString(5);
                s._adresse3 = reader.GetSqlString(6).IsNull ? string.Empty : reader.GetString(6);
                s._cp = reader.GetSqlString(7).IsNull ? string.Empty : reader.GetString(7);
                s._ville = reader.GetSqlString(8).IsNull ? string.Empty : reader.GetString(8);
                s._telephoneFixe = reader.GetSqlString(9).IsNull ? string.Empty : reader.GetString(9);
                s._telephonePortable = reader.GetSqlString(10).IsNull ? string.Empty : reader.GetString(10);
                s._email = reader.GetSqlString(11).IsNull ? string.Empty : reader.GetString(11);
                if (!reader.GetSqlDateTime(12).IsNull) { s._dateNaissance = reader.GetDateTime(12); }
                s._codeRegion = reader.GetSqlString(13).IsNull ? string.Empty : reader.GetString(13);
                s._codeNationalité = reader.GetSqlString(14).IsNull ? string.Empty : reader.GetString(14);
                s._codeOrigineMedia = reader.GetSqlString(15).IsNull ? string.Empty : reader.GetString(15);
                if (!reader.GetSqlDateTime(16).IsNull) { s._datePremierEnvoiDoc = reader.GetDateTime(16); }
                if (!reader.GetSqlDateTime(17).IsNull) { s._dateCreation = reader.GetDateTime(17); }
                s._repertoire = reader.GetSqlString(18).IsNull ? string.Empty : reader.GetString(18);
                if (reader.GetBoolean(19)) { s._permis = reader.GetBoolean(19); }
                s._photo = reader.GetSqlString(20).IsNull ? string.Empty : reader.GetString(20);
                if (reader.GetBoolean(21)) { s._envoiDocEnCours = reader.GetBoolean(21); }
                s._historique = reader.GetSqlString(22).IsNull ? string.Empty : reader.GetString(22);
                listeStagiaires.Add(s);
            }
            return listeStagiaires;
        }

        public static List<Stagiaire> getListeStagiaires(Formation pFormation, int pTypeFormation, String pFiltreNomPrenom)
        {
            String requete = "SELECT Stagiaire.CodeStagiaire, Stagiaire.Nom, Stagiaire.Prenom FROM Stagiaire, PlanningIndividuelFormation ";
            requete += " WHERE Stagiaire.CodeStagiaire=PlanningIndividuelFormation.CodeStagiaire ";    
            //filtre formation
            if (pFormation != null || (pFormation.Code=="0" && pFormation.Libelle=="Toutes"))
            {
                requete += " AND PlanningIndividuelFormation.CodeFormation=@CodeFormation ";
            }
            //filtre type formation
            if (pTypeFormation != 0)
            {
                if (pTypeFormation==Ressources.CONSTANTES.TYPE_FORMATION_CP)
                {
                    requete += " AND PlanningIndividuelFormation.CodePromotion is null ";
                }
                else if (pTypeFormation==Ressources.CONSTANTES.TYPE_FORMATION_FC)
                {
                    requete += " AND PlanningIndividuelFormation.CodePromotion is not null ";
                }
            }
            //filtre nom/prenom
            if (pFiltreNomPrenom != "") 
            {
                //comment faire avec un parametre??
                //requete += " AND (Stagiaire.Nom like (@filtreNP) OR Stagiaire.Prenom like (@filtreNP)) ";      
                requete += " AND (Stagiaire.Nom like ('%" + pFiltreNomPrenom.Trim() + "%') OR Stagiaire.Prenom like ('%" + pFiltreNomPrenom.Trim() + "%')) ";  
            }
            requete += " ORDER BY Stagiaire.Nom, Stagiaire.Prenom";

            SqlConnection connexion = ConnexionSQL.CreationConnexion();
            SqlCommand cmd = new SqlCommand(requete, connexion);
            if (pFormation != null) cmd.Parameters.AddWithValue("@CodeFormation", pFormation.Code.Trim());
            //if (pFiltreNomPrenom != "") cmd.Parameters.AddWithValue("@filtreNP", pFiltreNomPrenom.Trim());
            SqlDataReader reader = cmd.ExecuteReader();

            List<Stagiaire> listeStagiaires = new List<Stagiaire>();
            while (reader.Read())
            {
                Stagiaire s = new Stagiaire();
                s._id = reader.GetInt32(reader.GetOrdinal("CodeStagiaire"));
                //s._civilité = reader.GetSqlString(1).IsNull ? string.Empty : reader.GetString(1);
                s._nom = reader.GetSqlString(1).IsNull ? string.Empty : reader.GetString(1);
                s._prenom = reader.GetSqlString(2).IsNull ? string.Empty : reader.GetString(2);
                //s._adresse1 = reader.GetSqlString(4).IsNull ? string.Empty : reader.GetString(4);
                //s._adresse2 = reader.GetSqlString(5).IsNull ? string.Empty : reader.GetString(5);
                //s._adresse3 = reader.GetSqlString(6).IsNull ? string.Empty : reader.GetString(6);
                //s._cp = reader.GetSqlString(7).IsNull ? string.Empty : reader.GetString(7);
                //s._ville = reader.GetSqlString(8).IsNull ? string.Empty : reader.GetString(8);
                //s._telephoneFixe = reader.GetSqlString(9).IsNull ? string.Empty : reader.GetString(9);
                //s._telephonePortable = reader.GetSqlString(10).IsNull ? string.Empty : reader.GetString(10);
                //s._email = reader.GetSqlString(11).IsNull ? string.Empty : reader.GetString(11);
                //if (!reader.GetSqlDateTime(12).IsNull) { s._dateNaissance = reader.GetDateTime(12); }
                //s._codeRegion = reader.GetSqlString(13).IsNull ? string.Empty : reader.GetString(13);
                //s._codeNationalité = reader.GetSqlString(14).IsNull ? string.Empty : reader.GetString(14);
                //s._codeOrigineMedia = reader.GetSqlString(15).IsNull ? string.Empty : reader.GetString(15);
                //if (!reader.GetSqlDateTime(16).IsNull) { s._datePremierEnvoiDoc = reader.GetDateTime(16); }
                //if (!reader.GetSqlDateTime(17).IsNull) { s._dateCreation = reader.GetDateTime(17); }
                //s._repertoire = reader.GetSqlString(18).IsNull ? string.Empty : reader.GetString(18);
                //if (reader.GetBoolean(19)) { s._permis = reader.GetBoolean(19); }
                //s._photo = reader.GetSqlString(20).IsNull ? string.Empty : reader.GetString(20);
                //if (reader.GetBoolean(21)) { s._envoiDocEnCours = reader.GetBoolean(21); }
                //s._historique = reader.GetSqlString(22).IsNull ? string.Empty : reader.GetString(22);
                listeStagiaires.Add(s);
            }
            return listeStagiaires;
        }

    }
}
