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
            // à reprendre quand il y aura la base...

            try
            {
                SqlConnection connexion = ConnexionSQL.CreationConnexion();
                SqlCommand cmd = new SqlCommand(SELECT_INFOS_OBSERVATIONS, connexion);
                cmd.Parameters.AddWithValue("@num_stagiaire", pStg._id);

                SqlDataReader reader = cmd.ExecuteReader();

//<<<<<<< .mine

//            while (reader.Read())
//            {
//                Observation obsTemp = new Observation();
//                obsTemp._id = reader.GetInt32(reader.GetOrdinal("id_observation")); //et ainsi de suite (attendre que la base soit faire pour avoir les bons noms de paramètres)...
//                obsTemp._date = reader.GetDateTime(reader.GetOrdinal("date"));
//                obsTemp._nomAuteur = reader.GetString(reader.GetOrdinal("auteur"));
//                obsTemp._titre = reader.GetString(reader.GetOrdinal("titre"));
//                obsTemp._type = reader.GetString(reader.GetOrdinal("type"));
//                obsTemp._texte = reader.GetString(reader.GetOrdinal("texte"));
//                obsTemp._stagiaire = pStg; //TODO: tout comme pour absence, vérifier s'il est important que le stagiaire soit contenu dans l'absence
//                pStg.listeObservations.Add(obsTemp);
//            }
//            return pStg.listeObservations;
//            //listeRetour:return DAL.JeuDonnees.GetListeObservation();
//=======


                while (reader.Read())
                {
                    Observation obsTemp = new Observation();
                    obsTemp._id = reader.GetInt32(reader.GetOrdinal("id_observation"));
                    obsTemp._date = reader.GetDateTime(reader.GetOrdinal("date"));
                    obsTemp._nomAuteur = reader.GetString(reader.GetOrdinal("auteur"));
                    obsTemp._titre = reader.GetString(reader.GetOrdinal("titre"));
                    obsTemp._type = reader.GetString(reader.GetOrdinal("type"));
                    obsTemp._texte = reader.GetString(reader.GetOrdinal("texte"));
                    obsTemp._stagiaire = pStg; 
                    pStg.listeObservations.Add(obsTemp);
                }
                return pStg.listeObservations;
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show("Impossible d'éxécuter la requête : " + e.Message, "Echec de la requête",
                      System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                return null;
            }
			
            
//>>>>>>> .r106
        }

        public static void ajouterObservation(Observation o) {

            
            // test d'ajout dans la base de données bidon
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
            Parametres.Instance.stagiaire.listeObservations.Add(o);
        }

        public static void modifierObservation(Observation o)
        {
            // test de modification dans la base de données bidon
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
           // test de suppression dans la base de données bidon
            SqlConnection connexion = ConnexionSQL.CreationConnexion();
            SqlCommand cmd = new SqlCommand(DELETE_OBSERVATION, connexion);
            cmd.Parameters.AddWithValue("@num_observation", o._id);  // il faut modifier tout ça

            cmd.ExecuteReader();
            connexion.Close();
        }

    }
}
