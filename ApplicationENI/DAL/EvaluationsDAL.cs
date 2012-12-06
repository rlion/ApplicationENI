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
        static String INSERT_EVALUATION = "INSERT INTO EVALUATION (idEvaluation, idECF, idStagiaire, idCompetence, note, version, date) VALUES (@idEvaluation, @idECf, @idStagiaire, @idCompetence, @note, @version, @date)";
        static String UPDATE_EVALUATION = "UPDATE EVALUATION SET note=@note WHERE idEvaluation=@idEvaluation";
        static String SELECT_MAX = "SELECT MAX(idEvaluation) FROM EVALUATION";
        static String SELECT_NOTE_STAG = "SELECT * from EVALUATION WHERE idECF=@idECF AND version=@version AND date=@date AND idStagiaire=@idStagiaire AND idCompetence=@idCompetence";

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

        public static void modifierNoteEvaluation(Evaluation pEvaluation, float pNote)
        {
            if (pEvaluation.Id != 0)
            {
                //Modification de la note
                SqlConnection connexion = ConnexionSQL.CreationConnexion();
                SqlCommand cmd = new SqlCommand(UPDATE_EVALUATION, connexion);

                cmd.Parameters.AddWithValue("@idEvaluation", pEvaluation.Id);
                cmd.Parameters.AddWithValue("@note", pNote);

                cmd.ExecuteReader();
                connexion.Close();
            }
            else
            {
                pEvaluation.Note = pNote;
                ajouterEvaluation(pEvaluation);
            }            
        }

        public static Evaluation donneNote(SessionECF pSession, Stagiaire pStag, Competence pComp)
        {
            Evaluation eval = new Evaluation(pSession.Ecf, pComp, pStag, pSession.Version, -1, pSession.Date);

            SqlConnection connexion = ConnexionSQL.CreationConnexion();
            SqlCommand cmd = new SqlCommand(SELECT_NOTE_STAG, connexion);

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
            if (lesSessionsECFStag!=null)
            {
                foreach (SessionECF sess in lesSessionsECFStag)
                {
                    if (sess.Date < DateTime.Now)
                    {
                        if (lesSessionsECFPassees == null) lesSessionsECFPassees = new List<SessionECF>();
                        lesSessionsECFPassees.Add(sess);
                    }
                }
            }            
            
            //pour ceux dont la date est passée il faut vérifier si toutes les compétences ont été évaluées
            List<ECF> lesECFsNonCorriges = null;
            if (lesSessionsECFPassees!=null)
            {
                foreach (SessionECF sessionEcfPassee in lesSessionsECFPassees)
                {
                    if (!SessionECFDAL.SessionECFCorrigee(sessionEcfPassee, pStag))
                    {
                        if (lesECFsNonCorriges == null) lesECFsNonCorriges = new List<ECF>();
                        lesECFsNonCorriges.Add(sessionEcfPassee.Ecf);
                    }
                }
            }            

            return lesECFsNonCorriges;
        }

        public static List<SessionECF> getListeSessionsECFNONCorriges()
        {
            List<SessionECF> lesSessionsECFNonCorrigees=null;
            List<Stagiaire> tousLesStagiaires = StagiairesDAL.getListeStagiaires();

            if(tousLesStagiaires!=null)
            {
                foreach(Stagiaire stag in tousLesStagiaires)
                {
                    //recup la liste des ECF planifies pour un stagiaire
                    List<SessionECF> lesSessionsECFStag = SessionECFDAL.getListSessionsECFStagiaire(stag);

                    //dans cette liste on récupère les ECFs déjà passés (date de passage<aujourd'hui)
                    List<SessionECF> lesSessionsECFPassees = null;
                    if (lesSessionsECFStag!=null)
                    {
                        foreach (SessionECF sess in lesSessionsECFStag)
                        {
                            if (sess.Date < DateTime.Now)
                            {
                                if (lesSessionsECFPassees == null) lesSessionsECFPassees = new List<SessionECF>();
                                lesSessionsECFPassees.Add(sess);
                            }
                        }
                    }
                                
                    //pour ceux dont la date est passée il faut vérifier si toutes les compétences ont été évaluées
                    if (lesSessionsECFPassees!=null)
                    {
                        foreach (SessionECF sessionEcfPassee in lesSessionsECFPassees)
                        {
                            if (!SessionECFDAL.SessionECFCorrigee(sessionEcfPassee, stag))
                            {
                                if (lesSessionsECFNonCorrigees == null) lesSessionsECFNonCorrigees = new List<SessionECF>();
                                if (!lesSessionsECFNonCorrigees.Select(x=>x.Id).Contains(sessionEcfPassee.Id))
                                {
                                    lesSessionsECFNonCorrigees.Add(sessionEcfPassee);
                                }
                            }
                        }
                    }                    
                }                
            }
            return lesSessionsECFNonCorrigees;
        }
    }
}
