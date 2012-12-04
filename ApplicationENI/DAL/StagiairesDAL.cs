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
        public static Stagiaire getStagiaire(int pCodeStagiaire)
        {
            try
            {

                SqlConnection connexion = ConnexionSQL.CreationConnexion();
                SqlCommand cmd = new SqlCommand(SELECT_STAGIAIRE, connexion);
                cmd.Parameters.AddWithValue("@id", pCodeStagiaire);

                SqlDataReader reader = cmd.ExecuteReader();

                Stagiaire stgRetour = new Stagiaire();

                if (reader.Read())
                {
                    stgRetour._id = reader.GetInt32(reader.GetOrdinal("CodeStagiaire"));
                    stgRetour._civilité = reader.GetSqlString(1).IsNull ? string.Empty : reader.GetString(1);
                    stgRetour._nom = reader.GetSqlString(2).IsNull ? string.Empty : reader.GetString(2);
                    stgRetour._prenom = reader.GetSqlString(3).IsNull ? string.Empty : reader.GetString(3);
                    stgRetour._adresse1 = reader.GetSqlString(4).IsNull ? string.Empty : reader.GetString(4);
                    stgRetour._adresse2 = reader.GetSqlString(5).IsNull ? string.Empty : reader.GetString(5);
                    stgRetour._adresse3 = reader.GetSqlString(6).IsNull ? string.Empty : reader.GetString(6);
                    stgRetour._cp = reader.GetSqlString(7).IsNull ? string.Empty : reader.GetString(7);
                    stgRetour._ville = reader.GetSqlString(8).IsNull ? string.Empty : reader.GetString(8);
                    stgRetour._telephoneFixe = reader.GetSqlString(9).IsNull ? string.Empty : reader.GetString(9);
                    stgRetour._telephonePortable = reader.GetSqlString(10).IsNull ? string.Empty : reader.GetString(10);
                    stgRetour._email = reader.GetSqlString(11).IsNull ? string.Empty : reader.GetString(11);
                    if (!reader.GetSqlDateTime(12).IsNull) { stgRetour._dateNaissance = reader.GetDateTime(12); }
                    stgRetour._codeRegion = reader.GetSqlString(13).IsNull ? string.Empty : reader.GetString(13);
                    stgRetour._codeNationalité = reader.GetSqlString(14).IsNull ? string.Empty : reader.GetString(14);
                    stgRetour._codeOrigineMedia = reader.GetSqlString(15).IsNull ? string.Empty : reader.GetString(15);
                    if (!reader.GetSqlDateTime(16).IsNull) { stgRetour._datePremierEnvoiDoc = reader.GetDateTime(16); }
                    if (!reader.GetSqlDateTime(17).IsNull) { stgRetour._dateCreation = reader.GetDateTime(17); }
                    stgRetour._repertoire = reader.GetSqlString(18).IsNull ? string.Empty : reader.GetString(18);
                    if (reader.GetBoolean(19)) { stgRetour._permis = reader.GetBoolean(19); }
                    stgRetour._photo = reader.GetSqlString(20).IsNull ? string.Empty : reader.GetString(20);
                    if (reader.GetBoolean(21)) { stgRetour._envoiDocEnCours = reader.GetBoolean(21); }
                    stgRetour._historique = reader.GetSqlString(22).IsNull ? string.Empty : reader.GetString(22);
                }
                connexion.Close();
                return stgRetour;
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show("Impossible d'éxécuter la requête : " + e.Message, "Echec de la requête",
                      System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                return null;
            }
        }

        public static List<Stagiaire> getListeStagiaires()
        {
            try
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
                connexion.Close();
                return listeStagiaires;
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show("Impossible d'éxécuter la requête : " + e.Message, "Echec de la requête",
                      System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                return null;
            }
            
        }

        public static List<Stagiaire> getListeStagiaires(Formation pFormation, int pTypeFormation, String pFiltreNomPrenom)
        {
            String requete = "SELECT Stagiaire.CodeStagiaire FROM Stagiaire, PlanningIndividuelFormation ";
            requete += " WHERE Stagiaire.CodeStagiaire=PlanningIndividuelFormation.CodeStagiaire ";    
            //filtre formation
            if (pFormation != null && (pFormation.Code!="0" && pFormation.Libelle!="Toutes"))
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
                requete += " AND (Stagiaire.Nom like ('%" + pFiltreNomPrenom.Trim() + "%') OR Stagiaire.Prenom like ('%" + pFiltreNomPrenom.Trim() + "%')) ";  
            }
            requete += " ORDER BY Stagiaire.Nom, Stagiaire.Prenom";

            SqlConnection connexion = ConnexionSQL.CreationConnexion();
            SqlCommand cmd = new SqlCommand(requete, connexion);
            if (pFormation != null) cmd.Parameters.AddWithValue("@CodeFormation", pFormation.Code.Trim());
            SqlDataReader reader = cmd.ExecuteReader();

            List<Stagiaire> listeStagiaires = new List<Stagiaire>();
            while (reader.Read())
            {
                Stagiaire s = new Stagiaire();
                s._id = reader.GetInt32(reader.GetOrdinal("CodeStagiaire"));

                s = getStagiaire(s._id);

                listeStagiaires.Add(s);
            }
            return listeStagiaires;
        }

    }
}
