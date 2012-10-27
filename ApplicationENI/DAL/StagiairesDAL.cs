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
	

    }
}
