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
        //static String SELECT_EVALUATION = "SELECT * FROM  EVALUATION WHERE idECF=@idECF AND idStagiaire=@idStagiaire AND idCompetence=@idCompetence";
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
            int idMaxEvaluation = 0;
            if (reader.Read())
            {
                if (reader[0] != DBNull.Value)
                {
                    idMaxEvaluation = reader.GetInt32(0);
                }
            }
            eval.Id = idMaxEvaluation + 1;
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

        //public static Evaluation donneEvaluation(Evaluation pEval)
        //{            
        //    SqlConnection connexion = ConnexionSQL.CreationConnexion();
        //    SqlCommand cmd = new SqlCommand(SELECT_EVALUATION, connexion);

        //    cmd.Parameters.AddWithValue("@idECF", pEval.Ecf.Id.Trim());
        //    cmd.Parameters.AddWithValue("@idStagiaire", pEval.Stagiaire._id);
        //    cmd.Parameters.AddWithValue("@idCompetence", pEval.Competence.Id.Trim());

        //    SqlDataReader reader = cmd.ExecuteReader();

        //    if (reader.Read())
        //    {
        //        pEval.Id = reader.GetString(reader.GetOrdinal("idEvaluation")).Trim();
        //        pEval.Note = (float)reader.GetDouble(reader.GetOrdinal("note"));
        //        pEval.Version = reader.GetInt32(reader.GetOrdinal("version"));
        //        pEval.Date = reader.GetDateTime(reader.GetOrdinal("date"));
        //    }
        //    else
        //    {
        //        pEval = null;
        //    }
        //    connexion.Close();

        //    return pEval;
        //}
        public static Evaluation donneNote(SessionECF pSession, Stagiaire pStag, Competence pComp)
        {
            Evaluation eval = new Evaluation(pSession.Ecf, pComp, pStag, pSession.Version, -1, pSession.Date);

            String requete = "SELECT * from EVALUATION " +
                "WHERE idECF=@idECF AND version=@version AND date=@date AND idStagiaire=@idStagiaire AND idCompetence=@idCompetence";
            SqlConnection connexion = ConnexionSQL.CreationConnexion();
            SqlCommand cmd = new SqlCommand(requete, connexion);

            cmd.Parameters.AddWithValue("@idECF", pSession.Ecf.Id);
            cmd.Parameters.AddWithValue("@version", pSession.Version);
            cmd.Parameters.AddWithValue("@date", pSession.Date);
            cmd.Parameters.AddWithValue("@idStagiaire", pStag._id);
            cmd.Parameters.AddWithValue("@idCompetence", pComp.Id);
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                eval = new Evaluation();

                eval.Id = reader.GetInt32(reader.GetOrdinal("idEvaluation"));
                eval.Ecf = pSession.Ecf;
                eval.Stagiaire = pStag;
                eval.Competence = pComp;
                eval.Note = (float)reader.GetDouble(reader.GetOrdinal("note"));
                eval.Version = reader.GetInt32(reader.GetOrdinal("version"));
                eval.Date = reader.GetDateTime(reader.GetOrdinal("date"));
            }
            connexion.Close();

            return eval;
        }

        public static List<ECF> getListeECFsNonCorriges(Stagiaire pStag)
        {
            //recup la liste des ECF planifies pour un stagiaire
            List<SessionECF> lesSessionsECFStag = SessionECFDAL.getListSessionsECFStagiaire(pStag);

            //dans cette liste on récupère les ECFs déjà passés (date de passage<aujourd'hui)
            List<SessionECF> lesSessionsECFPassees = null;
            foreach (SessionECF sess in lesSessionsECFStag)
            {
                if (sess.Date<DateTime.Now)  
                {
                    if (lesSessionsECFPassees == null) lesSessionsECFPassees = new List<SessionECF>();
                    lesSessionsECFPassees.Add(sess);
                }
            }
            
            //pour ceux dont la date est passée il faut vérifier si toutes les compétences ont été évaluées
            List<ECF> lesECFsNonCorriges = null;
            foreach (SessionECF sessionEcfPassee in lesSessionsECFPassees)
            {
                if (!SessionECFDAL.SessionECFCorrigee(sessionEcfPassee,pStag))
                {
                    if (lesECFsNonCorriges == null) lesECFsNonCorriges = new List<ECF>();
                    lesECFsNonCorriges.Add(sessionEcfPassee.Ecf);
                }
            }

            return lesECFsNonCorriges;
        }
    }
}
