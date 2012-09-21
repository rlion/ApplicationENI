using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApplicationENI.Modele;

namespace ApplicationENI.DAL
{
    class CompetencesDAL
    {
        //TODO Mat
        static String SELECT_COMPETENCES = "SELECT * FROM  WHERE ";
        static String INSERT_COMPETENCE = "INSERT INTO  VALUES (@id, @date, @nom_auteur, @type, @titre, @texte, @num_stagiaire)";
        static String DELETE_COMPETENCE = "DELETE FROM WHERE ";
        static String UPDATE_COMPETENCE = "UPDATE  SET WHERE ";

        public static List<Competence> getListCompetences()
        {
            // à reprendre quand il y aura la base...


            /*SqlConnection connexion = ConnexionSQL.CreationConnexion();
            SqlCommand cmd = new SqlCommand(SELECT_ECFs, connexion);
            //cmd.Parameters.AddWithValue("@num_stagiaire", pStg._id);

            SqlDataReader reader = cmd.ExecuteReader();            
            
            while(reader.Read()) 
            {
                ECF ecfTemp = new ECF();
                ecfTemp.Id = reader.GetGuid(reader.GetOrdinal("id"));
                ecfTemp.Libelle = reader.GetGuid(reader.GetOrdinal("libelle"));
                ADD...   
             }*/

            return DAL.JeuDonnees.GetListCompetence();
        }

        public static void ajouterCompetence(Competence comp)
        {

            //Parametres.Instance.stagiaire.listeObservations.Add(o);
            // test d'ajout dans la base de données bidon
            /*SqlConnection connexion = ConnexionSQL.CreationConnexion();
            SqlCommand cmd = new SqlCommand(INSERT_OBSERVATION, connexion);
            cmd.Parameters.AddWithValue("@id", Guid.NewGuid());  
            cmd.Parameters.AddWithValue("@date", DateTime.Now);
			cmd.Parameters.AddWithValue("@nom_auteur", o._nomAuteur);
			cmd.Parameters.AddWithValue("@type", o._type);
			cmd.Parameters.AddWithValue("@titre", o._titre);
			cmd.Parameters.AddWithValue("@texte", o._texte);
			cmd.Parameters.AddWithValue("@num_stagiaire", o._stagiaire._id);
	
            cmd.ExecuteReader();
            connexion.Close();*/
        }

        public static void modifierCompetence(Competence comp)
        {
            // test de modification dans la base de données bidon
            /*SqlConnection connexion = ConnexionSQL.CreationConnexion();
            SqlCommand cmd = new SqlCommand(UPDATE_OBSERVATION, connexion);
            cmd.Parameters.AddWithValue("@date", DateTime.Now);
			cmd.Parameters.AddWithValue("@nom_auteur", o._nomAuteur);
			cmd.Parameters.AddWithValue("@type", o._type);
			cmd.Parameters.AddWithValue("@titre", o._titre);
			cmd.Parameters.AddWithValue("@texte", o._texte);
			cmd.Parameters.AddWithValue("@num_observation", o._id);

            cmd.ExecuteReader();
            connexion.Close();*/
        }

        public static void supprimerCompetence(Competence comp)
        {
            // test de suppression dans la base de données bidon
            /*SqlConnection connexion = ConnexionSQL.CreationConnexion();
            SqlCommand cmd = new SqlCommand(DELETE_OBSERVATION, connexion);
            cmd.Parameters.AddWithValue("@num_observation", o._id);  // il faut modifier tout ça

            cmd.ExecuteReader();
            connexion.Close();*/
        }
    }
}
