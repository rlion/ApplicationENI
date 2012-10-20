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
        static String INSERT_STAGIAIRE = "INSERT INTO STAGIAIRE VALUES (@id, @nom)";
        static String DELETE_STAGIAIRE = "DELETE FROM STAGIAIRE WHERE NOM=@nom";
        static String UPDATE_STAGIAIRE = "UPDATE STAGIAIRE SET NOM=@nouveauNom WHERE NOM=@nom";
        //static String LISTE_STAGIAIRE_PAR_COURS_FORMATION_DATE = "SELECT * FROM STAGIAIRE WHERE IDCOURS=@idCours AND IDFORMATION=@idFormation AND DATE=@date"; //il faut masse de jointures, etc... Je m'y risque pas tant que la base est pas faite, ça va être l'enfer sinon.
        
        public static List<Stagiaire> getListeStagiaires()
        {
            // Pour l'instant c'est bidon vu qu'il y a pas de BDD au bout.
            return DAL.JeuDonnees.GetListeStagiaire();
        }

        public static List<Stagiaire> getListeStagiaireParCoursEtDate(int idCours, DateTime dateDebut) {
            return DAL.JeuDonnees.GetListeStagiaire();
        }

        public static void ajouterStagiaire(Stagiaire s) { 
            // test d'ajout dans la base de données bidon
            SqlConnection connexion = ConnexionSQL.CreationConnexion();
            SqlCommand cmd = new SqlCommand(INSERT_STAGIAIRE, connexion);
            cmd.Parameters.AddWithValue("@nom", s._nom);  // il faut modifier tout ça
            cmd.Parameters.AddWithValue("@id", Guid.NewGuid());        // c'est bien bidon, mais le test fonctionne.
   
            cmd.ExecuteReader();
            connexion.Close();
        }

        public static void modifierStagiaire(Stagiaire s)
        {
            // test de modification dans la base de données bidon
            SqlConnection connexion = ConnexionSQL.CreationConnexion();
            SqlCommand cmd = new SqlCommand(UPDATE_STAGIAIRE, connexion);
            cmd.Parameters.AddWithValue("@nom", s._nom);  // il faut modifier tout ça
            cmd.Parameters.AddWithValue("@nouveauNom", "lenouveauNom");

            cmd.ExecuteReader();
            connexion.Close();
        }

        public static void supprimerStagiaire(Stagiaire s)
        {
            // test de suppression dans la base de données bidon
            SqlConnection connexion = ConnexionSQL.CreationConnexion();
            SqlCommand cmd = new SqlCommand(DELETE_STAGIAIRE, connexion);
            cmd.Parameters.AddWithValue("@nom", s._nom);  // il faut modifier tout ça

            cmd.ExecuteReader();
            connexion.Close();
        }

        // ébauche de requête SQL, à modifier et tester une fois qu'on aura la base
        public static Stagiaire getInfosStagiaire(String pNom) {
            
            SqlConnection connexion = ConnexionSQL.CreationConnexion();
            SqlCommand cmd = new SqlCommand(SELECT_INFOS_STAGIAIRE, connexion);
            cmd.Parameters.AddWithValue("@nom", pNom);

            SqlDataReader reader = cmd.ExecuteReader();

            Stagiaire stgRetour = new Stagiaire();
            
            if(reader.Read()) 
            {
                String test = reader.GetString(reader.GetOrdinal("nom"));
                stgRetour._nom = test;
            }
            connexion.Close();
            return stgRetour;
        }
	

    }
}
