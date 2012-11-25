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
		static String SELECT_INFOS_OBSERVATIONS = "SELECT * FROM OBSERVATION WHERE ID_STAGIAIRE=@num_stagiaire";
        static String INSERT_OBSERVATION = "INSERT INTO OBSERVATION VALUES (@date, @nom_auteur, @type, @titre, @texte, @num_stagiaire)";
        static String DELETE_OBSERVATION = "DELETE FROM OBSERVATION WHERE ID_OBSERVATION=@num_observation";
        static String UPDATE_OBSERVATION = "UPDATE OBSERVATION SET DATE=@date, AUTEUR=@auteur, TYPE=@type, TITRE=@titre, TEXTE=@texte WHERE ID_OBSERVATION=@num_observation";
        static String GET_NUM_OBSERVATION = "SELECT @@IDENTITY AS IDENT";


        public static List<Observation> getListObservations(Stagiaire pStg) {
            try
            {
                List<Observation> listeDesObservations = new List<Observation>();
                SqlConnection connexion = ConnexionSQL.CreationConnexion();
                SqlCommand cmd = new SqlCommand(SELECT_INFOS_OBSERVATIONS, connexion);
                cmd.Parameters.AddWithValue("@num_stagiaire", pStg._id);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Observation obsTemp = new Observation();
                    obsTemp._id = reader.GetInt32(reader.GetOrdinal("id_observation"));
                    if (reader.GetDateTime(reader.GetOrdinal("date")).ToString().Length > 0) { obsTemp._date = reader.GetDateTime(reader.GetOrdinal("date")); }
                    obsTemp._nomAuteur = reader.GetSqlString(2).IsNull ? string.Empty : reader.GetString(2);
                    obsTemp._type = reader.GetSqlString(3).IsNull ? string.Empty : reader.GetString(3);
                    obsTemp._titre = reader.GetSqlString(4).IsNull ? string.Empty : reader.GetString(4);
                    obsTemp._texte = reader.GetSqlString(5).IsNull ? string.Empty : reader.GetString(5);
                    obsTemp._stagiaire = pStg;
                    listeDesObservations.Add(obsTemp);
                }
                return listeDesObservations;
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show("Impossible d'éxécuter la requête : " + e.Message, "Echec de la requête",
                      System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                return null;
            }
            
        }

        public static void ajouterObservation(Observation o) {
            SqlConnection connexion = ConnexionSQL.CreationConnexion();
            SqlCommand cmd = new SqlCommand(INSERT_OBSERVATION, connexion);  
            cmd.Parameters.AddWithValue("@date", DateTime.Now);
			cmd.Parameters.AddWithValue("@nom_auteur", Parametres.Instance.login);
			cmd.Parameters.AddWithValue("@type", o._type);
			cmd.Parameters.AddWithValue("@titre", o._titre);
			cmd.Parameters.AddWithValue("@texte", o._texte);
			cmd.Parameters.AddWithValue("@num_stagiaire", o._stagiaire._id);

            cmd.ExecuteNonQuery();

            SqlCommand cmd2 = new SqlCommand(GET_NUM_OBSERVATION, connexion);
            int idDernierObservation = Convert.ToInt32(cmd2.ExecuteScalar());
            o._id = Convert.ToInt32(idDernierObservation);
            connexion.Close();
        }

        public static void modifierObservation(Observation o)
        {
            SqlConnection connexion = ConnexionSQL.CreationConnexion();
            SqlCommand cmd = new SqlCommand(UPDATE_OBSERVATION, connexion);
            cmd.Parameters.AddWithValue("@date", DateTime.Now);
			cmd.Parameters.AddWithValue("@auteur", o._nomAuteur);
			cmd.Parameters.AddWithValue("@type", o._type);
			cmd.Parameters.AddWithValue("@titre", o._titre);
			cmd.Parameters.AddWithValue("@texte", o._texte);
			cmd.Parameters.AddWithValue("@num_observation", o._id);

            cmd.ExecuteReader();
            connexion.Close();
        }

        public static void supprimerObservation(Observation o)
        {
            try
            {
                SqlConnection connexion = ConnexionSQL.CreationConnexion();
                SqlCommand cmd = new SqlCommand(DELETE_OBSERVATION, connexion);
                cmd.Parameters.AddWithValue("@num_observation", o._id); 

                cmd.ExecuteReader();
                connexion.Close();
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show("Impossible de supprimer l'observation : " + e.Message, "Echec de la requête",
                      System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
            
        }

    }
}
