using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApplicationENI.Modele;
using System.Data.SqlClient;

namespace ApplicationENI.DAL
{
    class SessionECFDAL
    {
        static String SELECT_SESSIONSECFS = "SELECT * FROM SESSIONSECF order by date";//, Competences, CompetenceECFS as lien WHERE lien.idECF=ECFs=idECF and lien.idCompetence=Competences.idCompetence order by ECFs.idECF";
        static String SELECT_SESSIONSECF = "SELECT * FROM SESSIONSECF WHERE idECF=@idECF order by date";
        static String INSERT_SESSIONECF = "INSERT INTO SESSIONSECF (id, idECF, date) VALUES (@id, @idECF,@date)";
        static String DELETE_SESSIONECF = "DELETE FROM SESSIONSECF WHERE idECF=@idECF";
        static String SELECT_MAX_SESSIONECF = "SELECT MAX(id) FROM SESSIONSECF";

        static String SELECT_PARTICIPANTS = "SELECT * FROM PARTICIPANTSSESSIONECF WHERE idSessionsECF=@idSessionECF";
        static String INSERT_PARTICIPANT = "INSERT INTO PARTICIPANTSSESSIONECF (id, idSessionECF, idStagiaire) VALUES (@id, @idSessionECF, @idStagiaire)";
        static String DELETE_PARTICIPANTS = "DELETE FROM PARTICIPANTSSESSIONECF WHERE idSessionECF=@idSessionECF";

        public static List<SessionECF> getListSessionsECFs()
        {
            List<SessionECF> lesSessionsECFs = new List<SessionECF>();
            SqlConnection connexion = ConnexionSQL.CreationConnexion();
            SqlCommand cmd = new SqlCommand(SELECT_SESSIONSECFS, connexion);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                SessionECF sessionECFTemp = new SessionECF();
                ECF ecfTemp=new ECF();
                ecfTemp.Id = reader.GetString(reader.GetOrdinal("idECF")).Trim();

                sessionECFTemp.Id = reader.GetString(reader.GetOrdinal("id"));
                sessionECFTemp.Ecf = ECFDAL.getECF(ecfTemp);
                sessionECFTemp.Date = reader.GetDateTime(reader.GetOrdinal("date"));

                //participants
                List<Stagiaire> lesParticipants= new List<Stagiaire>();
                SqlConnection c2= ConnexionSQL.CreationConnexion();
                SqlCommand cmd2 = new SqlCommand(SELECT_PARTICIPANTS, connexion);
                cmd2.Parameters.AddWithValue("@idSessionECF", sessionECFTemp.Id.Trim());
                SqlDataReader reader2 = cmd.ExecuteReader();

                while (reader2.Read())
                {
                    int idStag=reader2.GetInt32(reader2.GetOrdinal("idStagiaire"));
                    Stagiaire participant = StagiairesDAL.getStagiaire(idStag);

                    sessionECFTemp.Participants.Add(participant);
                }
                c2.Close();

                lesSessionsECFs.Add(sessionECFTemp);
            }
            connexion.Close();

            return lesSessionsECFs;
        }

        public static List<SessionECF> getListSessionsECF(ECF pECF)
        {
            List<SessionECF> lesSessionsECFs = new List<SessionECF>();
            SqlConnection connexion = ConnexionSQL.CreationConnexion();
            SqlCommand cmd = new SqlCommand(SELECT_SESSIONSECF, connexion);
            cmd.Parameters.AddWithValue("@idECF", pECF.Id.Trim());
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                SessionECF sessionECFTemp = new SessionECF();
                ECF ecfTemp = new ECF();
                ecfTemp.Id = reader.GetString(reader.GetOrdinal("idECF")).Trim();

                sessionECFTemp.Id = reader.GetString(reader.GetOrdinal("id"));
                sessionECFTemp.Ecf = ECFDAL.getECF(ecfTemp);
                sessionECFTemp.Date = reader.GetDateTime(reader.GetOrdinal("date"));

                //participants
                List<Stagiaire> lesParticipants = new List<Stagiaire>();
                SqlConnection c2 = ConnexionSQL.CreationConnexion();
                SqlCommand cmd2 = new SqlCommand(SELECT_PARTICIPANTS, connexion);
                cmd2.Parameters.AddWithValue("@idSessionECF", sessionECFTemp.Id.Trim());
                SqlDataReader reader2 = cmd.ExecuteReader();

                while (reader2.Read())
                {
                    int idStag = reader2.GetInt32(reader2.GetOrdinal("idStagiaire"));
                    Stagiaire participant = StagiairesDAL.getStagiaire(idStag);

                    sessionECFTemp.Participants.Add(participant);
                }
                c2.Close();

                lesSessionsECFs.Add(sessionECFTemp);
            }
            connexion.Close();

            return lesSessionsECFs;
        }

        public static void ajouterSessionECF(SessionECF sessionEcf, List<Stagiaire> lesParticipants)
        {
            //Verifier que ce code n'existe pas deja dans la base
            //SqlConnection connexion = ConnexionSQL.CreationConnexion();
            //SqlCommand cmd = new SqlCommand(SELECT_CODE, connexion);
            //cmd.Parameters.AddWithValue("@code", ecf.Code.Trim());
            //SqlDataReader reader = cmd.ExecuteReader();
            //if (reader.Read())
            //{
            //    connexion.Close();
            //    return "Ce code et déjà utilisé par un autre ECF";
            //}
            //connexion.Close();
            
            //Récup de l'id max dans la table SESSIONSECF
            SqlConnection connexion = ConnexionSQL.CreationConnexion();
            SqlCommand cmd = new SqlCommand(SELECT_MAX_SESSIONECF, connexion);
            SqlDataReader reader = cmd.ExecuteReader();
            String idMaxSessionECF = "0";
            if (reader.Read())
            {
                if (reader[0] != DBNull.Value)
                {
                    idMaxSessionECF = reader.GetString(0).Trim();
                }
            }
            sessionEcf.Id=(Convert.ToInt32(idMaxSessionECF) + 1).ToString();
            connexion.Close();

            //Création de l'ECF
            connexion = ConnexionSQL.CreationConnexion();
            cmd = new SqlCommand(INSERT_SESSIONECF, connexion);

            cmd.Parameters.AddWithValue("@id", sessionEcf.Id.Trim());
            cmd.Parameters.AddWithValue("@idECF", sessionEcf.Ecf.Id.Trim());
            cmd.Parameters.AddWithValue("@date", sessionEcf.Date);

            cmd.ExecuteReader();
            connexion.Close();

            //participants
            ajouterParticipants(sessionEcf, lesParticipants);
        }

        public static void ajouterParticipants(SessionECF pSessionECF, List<Stagiaire> lesParticipants)
        {
            //Suppr des participants
            supprimerParticipants(pSessionECF);

            foreach (Stagiaire stag in lesParticipants)
            {
                SqlConnection connexion = ConnexionSQL.CreationConnexion();
                SqlCommand cmd = new SqlCommand(INSERT_PARTICIPANT, connexion);

                cmd.Parameters.AddWithValue("@id", pSessionECF.Id.Trim());
                cmd.Parameters.AddWithValue("@idSessionECF", pSessionECF.Ecf.Id.Trim());
                cmd.Parameters.AddWithValue("@idStagiaire", stag._id);

                cmd.ExecuteReader();
                connexion.Close();
            }
        }

        public static void supprimerSessionECF(SessionECF pSessionECF)
        {
            //Suppr des participants
            supprimerParticipants(pSessionECF);

            //Suppr d'un ECF
            SqlConnection connexion = ConnexionSQL.CreationConnexion();
            SqlCommand cmd = new SqlCommand(DELETE_SESSIONECF, connexion);

            cmd.Parameters.AddWithValue("@idECF", pSessionECF.Ecf.Id.Trim());

            cmd.ExecuteReader();
            connexion.Close();
        }

        public static void supprimerParticipants(SessionECF pSessionECF)
        {
            SqlConnection connexion = ConnexionSQL.CreationConnexion();
            SqlCommand cmd = new SqlCommand(DELETE_PARTICIPANTS, connexion);

            cmd.Parameters.AddWithValue("@idSessionECF", pSessionECF.Id);

            cmd.ExecuteReader();
            connexion.Close();
        }
    }
}
