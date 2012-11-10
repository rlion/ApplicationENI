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
        static String SELECT_LISTE_STAGIAIRES = "SELECT * FROM STAGIAIRE ";
        static String SELECT_INFOS_STAGIAIRE = "SELECT * FROM STAGIAIRE WHERE NOM=@nom";
        static String DELETE_STAGIAIRE = "DELETE FROM STAGIAIRE WHERE NOM=@nom";
        static String UPDATE_STAGIAIRE = "UPDATE STAGIAIRE SET NOM=@nouveauNom WHERE NOM=@nom";
        
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

        public static List<Stagiaire> getListeStagiaire()
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
                s._tuteur = ContactDAL.rechercherContact(s._id);
                listeStagiaires.Add(s);
            }
            return listeStagiaires;
        }

    }
}
