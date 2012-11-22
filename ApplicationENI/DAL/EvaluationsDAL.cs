using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApplicationENI.Modele;
using System.Data.SqlClient;

namespace ApplicationENI.DAL
{
    class EvaluationsDAL
    {
        //TODO Mat
        static String SELECT_EVALUATION = "SELECT * FROM  EVALUATION WHERE idECF=@idECF AND idStagiaire=@idStagiaire AND idCompetence=@idCompetence";
        static String INSERT_EVALUATION = "INSERT INTO EVALUATION (idEvaluation, idECF, idStagiaire, idCompetence, note, version, date) VALUES (@idEvaluation, @idECf, @idStagiaire, @idCompetence, @note, @version, @date)";
        static String DELETE_EVALUATION = "DELETE FROM WHERE FROM  EVALUATION WHERE idECF=@idECF AND idStagiaire=@idStagiaire AND idCompetence=@idCompetence";
        static String UPDATE_EVALUATION = "UPDATE EVALUATION SET note=@note WHERE idEvaluation=@idEvaluation";
        static String SELECT_MAX = "SELECT MAX(idEvaluation) FROM EVALUATION";

        public static List<Evaluation> getListEvaluations()
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

            return DAL.JeuDonnees.GetListEvaluation();
        }

        public static void ajouterEvaluation(Evaluation eval)
        {
            //Récup de l'id max dans la table EVALUATIONS
            SqlConnection connexion = ConnexionSQL.CreationConnexion();
            SqlCommand cmd = new SqlCommand(SELECT_MAX, connexion);
            SqlDataReader reader = cmd.ExecuteReader();
            String idMaxEvaluation = "0";
            if (reader.Read())
            {
                if (reader[0] != DBNull.Value)
                {
                    idMaxEvaluation = reader.GetString(0).Trim();
                }
            }
            eval.Id = (Convert.ToInt32(idMaxEvaluation) + 1).ToString();
            connexion.Close();

            //Création de la competence
            connexion = ConnexionSQL.CreationConnexion();
            cmd = new SqlCommand(INSERT_EVALUATION, connexion);

            cmd.Parameters.AddWithValue("@idEvaluation", eval.Id);
            cmd.Parameters.AddWithValue("@idECF", eval.Ecf.Id);
            cmd.Parameters.AddWithValue("@idStagiaire", eval.Stagiaire._id);
            cmd.Parameters.AddWithValue("@idCompetence", eval.Competence.Id);
            cmd.Parameters.AddWithValue("@note", eval.Note);
            cmd.Parameters.AddWithValue("@version", eval.Version);
            cmd.Parameters.AddWithValue("@date", eval.Date);

            cmd.ExecuteReader();
            connexion.Close();
        }

        public static void modifierEvaluation(Evaluation eval)
        {
            //Création de la competence
            SqlConnection connexion = ConnexionSQL.CreationConnexion();
            SqlCommand cmd = new SqlCommand(UPDATE_EVALUATION, connexion);

            cmd.Parameters.AddWithValue("@idEvaluation", eval.Id);
            cmd.Parameters.AddWithValue("@note", eval.Note);

            cmd.ExecuteReader();
            connexion.Close();
        }

        public static void supprimerEvaluation(Evaluation eval)
        {
            // test de suppression dans la base de données bidon
            /*SqlConnection connexion = ConnexionSQL.CreationConnexion();
            SqlCommand cmd = new SqlCommand(DELETE_OBSERVATION, connexion);
            cmd.Parameters.AddWithValue("@num_observation", o._id);  // il faut modifier tout ça

            cmd.ExecuteReader();
            connexion.Close();*/
        }

        public static Evaluation donneEvaluation(Evaluation pEval)
        {            
            List<Competence> lesCompetences = new List<Competence>();
            SqlConnection connexion = ConnexionSQL.CreationConnexion();
            SqlCommand cmd = new SqlCommand(SELECT_EVALUATION, connexion);

            cmd.Parameters.AddWithValue("@idECF", pEval.Ecf.Id.Trim());
            cmd.Parameters.AddWithValue("@idStagiaire", pEval.Stagiaire._id);
            cmd.Parameters.AddWithValue("@idCompetence", pEval.Competence.Id.Trim());

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                pEval.Id = reader.GetString(reader.GetOrdinal("idEvaluation")).Trim();
                pEval.Note = (float)reader.GetDouble(reader.GetOrdinal("note"));
                pEval.Version = reader.GetInt32(reader.GetOrdinal("version"));
                pEval.Date = reader.GetDateTime(reader.GetOrdinal("date"));
            }
            else
            {
                pEval = null;
            }
            connexion.Close();

            return pEval;
        }
    }
}
