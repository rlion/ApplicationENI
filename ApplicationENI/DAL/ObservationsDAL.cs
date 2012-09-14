using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApplicationENI.Modele;
using System.Data.SqlClient;

namespace ApplicationENI.DAL
{
    class ObservationsDAL
    {
		static String SELECT_INFOS_OBSERVATIONS = "SELECT * FROM OBSERVATION WHERE NUM_STAGIAIRE=@num_stagiaire";
        static String INSERT_OBSERVATION = "INSERT INTO OBSERVATION VALUES (@id, @date, @nom_auteur, @type, @titre, @texte, @num_stagiaire)";
        static String DELETE_OBSERVATION = "DELETE FROM OBSERVATION WHERE NUM=@num_observation";
        static String UPDATE_OBSERVATION = "UPDATE OBSERVATION SET DATE=@date, NOMAUTEUR=@nom_auteur, TYPE=@type, TITRE=@titre, TEXTE=@texte WHERE NUM=@num_observation";
	
        public static List<Observation> getListObservations(Stagiaire pStg) {
            // à reprendre quand il y aura la base...
           
			
			/*SqlConnection connexion = ConnexionSQL.CreationConnexion();
            SqlCommand cmd = new SqlCommand(SELECT_INFOS_OBSERVATIONS, connexion);
            cmd.Parameters.AddWithValue("@num_stagiaire", pStg._id);

            SqlDataReader reader = cmd.ExecuteReader();

            
            
            while(reader.Read()) 
            {
				Observation obsTemp = new Observation();
                obsTemp._id = reader.GetGuid(reader.GetOrdinal("id")); //et ainsi de suite (attendre que la base soit faire pour avoir les bons noms de paramètres)...
				obsTemp._date = reader.GetDate(reader.GetOrdinal("date"));
				obsTemp._nomAuteur = reader.GetString(reader.GetOrdinal("nomAuteur"));
				obsTemp._titre = reader.GetString(reader.GetOrdinal("titre"));
				obsTemp._type = reader.GetString(reader.GetOrdinal("type"));
				obsTemp._texte = reader.GetString(reader.GetOrdinal("texte"));
				obsTemp._stagiaire = reader.GetStagiaire(reader.GetOrdinal("stagiaire")); //voir comment faire ici...
				listeObservation.Add(obsTemp);
			}*/

            return DAL.JeuDonnees.GetListeObservation();
        }

        public static void ajouterObservation(Observation o) {

            Parametres.Instance.stagiaire.listeObservations.Add(o);
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

        public static void modifierObservation(Observation o)
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

        public static void supprimerObservation(Observation o)
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
