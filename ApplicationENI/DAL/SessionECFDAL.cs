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
        static String SELECT_SESSIONSECFVERSION = "SELECT * FROM SESSIONSECF WHERE idECF=@idECF and version=@version order by date";
        static String INSERT_SESSIONECF = "INSERT INTO SESSIONSECF (idSessionECF, idECF, date, version) VALUES (@idSessionECF, @idECF,@date, @version)";
        static String DELETE_SESSIONECF = "DELETE FROM SESSIONSECF WHERE idECF=@idECF";
        static String SELECT_MAX_SESSIONECF = "SELECT MAX(idSessionECF) FROM SESSIONSECF";

        static String SELECT_PARTICIPANTS = "SELECT * FROM PARTICIPANTSSESSIONECF WHERE idSessionECF=@idSessionECF";
        static String INSERT_PARTICIPANT = "INSERT INTO PARTICIPANTSSESSIONECF (id, idSessionECF, idStagiaire) VALUES (@id, @idSessionECF, @idStagiaire)";
        static String DELETE_PARTICIPANTS = "DELETE FROM PARTICIPANTSSESSIONECF WHERE idSessionECF=@idSessionECF";
        static String SELECT_MAX = "SELECT MAX(id) FROM PARTICIPANTSSESSIONECF";

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

                sessionECFTemp.Id = reader.GetString(reader.GetOrdinal("idSessionECF"));
                sessionECFTemp.Ecf = ECFDAL.getECF(ecfTemp.Id);
                sessionECFTemp.Date = reader.GetDateTime(reader.GetOrdinal("date"));
                sessionECFTemp.Version = reader.GetInt32(reader.GetOrdinal("version"));

                //participants
                List<Stagiaire> lesParticipants= new List<Stagiaire>();
                SqlConnection c2= ConnexionSQL.CreationConnexion();
                SqlCommand cmd2 = new SqlCommand(SELECT_PARTICIPANTS, c2);
                cmd2.Parameters.AddWithValue("@idSessionECF", sessionECFTemp.Id.Trim());
                SqlDataReader reader2 = cmd2.ExecuteReader();

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

        public static List<SessionECF> getListSessionsECFVersion(ECF pECF, int pVersion)
        {
            List<SessionECF> lesSessionsECFs = new List<SessionECF>();
            SqlConnection connexion = ConnexionSQL.CreationConnexion();
            SqlCommand cmd = new SqlCommand(SELECT_SESSIONSECFVERSION, connexion);
            cmd.Parameters.AddWithValue("@idECF", pECF.Id.Trim());
            cmd.Parameters.AddWithValue("@version", pVersion);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                SessionECF sessionECFTemp = new SessionECF();
                ECF ecfTemp = new ECF();
                ecfTemp.Id = reader.GetString(reader.GetOrdinal("idECF")).Trim();

                sessionECFTemp.Id = reader.GetString(reader.GetOrdinal("idSessionECF"));
                sessionECFTemp.Ecf = ECFDAL.getECF(ecfTemp.Id);
                sessionECFTemp.Date = reader.GetDateTime(reader.GetOrdinal("date"));
                sessionECFTemp.Version = reader.GetInt32(reader.GetOrdinal("version"));

                //participants
                List<Stagiaire> lesParticipants = new List<Stagiaire>();
                SqlConnection c2 = ConnexionSQL.CreationConnexion();
                SqlCommand cmd2 = new SqlCommand(SELECT_PARTICIPANTS, c2);
                cmd2.Parameters.AddWithValue("@idSessionECF", sessionECFTemp.Id.Trim());
                SqlDataReader reader2 = cmd2.ExecuteReader();

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

                sessionECFTemp.Id = reader.GetString(reader.GetOrdinal("idSessionECF"));
                sessionECFTemp.Ecf = ECFDAL.getECF(ecfTemp.Id);
                sessionECFTemp.Date = reader.GetDateTime(reader.GetOrdinal("date"));
                sessionECFTemp.Version = reader.GetInt32(reader.GetOrdinal("version"));

                //participants
                List<Stagiaire> lesParticipants = new List<Stagiaire>();
                SqlConnection c2 = ConnexionSQL.CreationConnexion();
                SqlCommand cmd2 = new SqlCommand(SELECT_PARTICIPANTS, c2);
                cmd2.Parameters.AddWithValue("@idSessionECF", sessionECFTemp.Id.Trim());
                SqlDataReader reader2 = cmd2.ExecuteReader();

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

        public static List<SessionECF> getListSessionsECFStagiaire(Stagiaire pStag)
        {
            List<SessionECF> lesECFsPlanifiesStagiaire = null;
            
            //recup la liste des ECFs planifies pour un stagiaire
            String requete = "SELECT * FROM PARTICIPANTSSESSIONECF,SESSIONSECF " +
                " WHERE SESSIONSECF.idSessionECF=PARTICIPANTSSESSIONECF.idSessionECF " +
                " AND PARTICIPANTSSESSIONECF.idStagiaire=@idStagiaire order by SESSIONSECF.date";

            SqlConnection connexion = ConnexionSQL.CreationConnexion();
            SqlCommand cmd = new SqlCommand(requete, connexion);
            cmd.Parameters.AddWithValue("@idStagiaire", pStag._id);
            SqlDataReader reader = cmd.ExecuteReader();

                
            while (reader.Read())
            {
                if (lesECFsPlanifiesStagiaire == null) lesECFsPlanifiesStagiaire = new List<SessionECF>();
                
                SessionECF sessionECFTemp = new SessionECF();
                ECF ecfTemp = new ECF();
                ecfTemp.Id = reader.GetString(reader.GetOrdinal("idECF")).Trim();

                sessionECFTemp.Id = reader.GetString(reader.GetOrdinal("idSessionECF"));
                sessionECFTemp.Ecf = ECFDAL.getECF(ecfTemp.Id);
                sessionECFTemp.Date = reader.GetDateTime(reader.GetOrdinal("date"));
                sessionECFTemp.Version = reader.GetInt32(reader.GetOrdinal("version"));

                lesECFsPlanifiesStagiaire.Add(sessionECFTemp);
            }

            connexion.Close();


            return lesECFsPlanifiesStagiaire;
        }

        public static void ajouterSessionECF(SessionECF sessionEcf)
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

            cmd.Parameters.AddWithValue("@idSessionECF", sessionEcf.Id.Trim());
            cmd.Parameters.AddWithValue("@idECF", sessionEcf.Ecf.Id.Trim());
            cmd.Parameters.AddWithValue("@date", sessionEcf.Date);
            cmd.Parameters.AddWithValue("@version", sessionEcf.Version);

            cmd.ExecuteReader();
            connexion.Close();

            //participants
            if (sessionEcf.Participants!=null)
            {
                ajouterParticipants(sessionEcf);
            }
            
        }

        public static void ajouterParticipants(SessionECF pSessionECF)
        {
            //Suppr des participants
            supprimerParticipants(pSessionECF);

            foreach (Stagiaire stag in pSessionECF.Participants)
            {
                //Récup de l'id max dans la table PARTICIPANTSSESSIONECF
                SqlConnection connexion = ConnexionSQL.CreationConnexion();
                SqlCommand cmd = new SqlCommand(SELECT_MAX, connexion);
                SqlDataReader reader = cmd.ExecuteReader();
                String idMax = "0";
                if (reader.Read())
                {
                    if (reader[0] != DBNull.Value)
                    {
                        idMax = reader.GetString(0).Trim();
                    }
                }
                idMax = (Convert.ToInt32(idMax) + 1).ToString();
                connexion.Close();
                
                
                connexion = ConnexionSQL.CreationConnexion();
                cmd = new SqlCommand(INSERT_PARTICIPANT, connexion);

                //TODO gerer ID? utile,
                cmd.Parameters.AddWithValue("@id", idMax);
                cmd.Parameters.AddWithValue("@idSessionECF", pSessionECF.Id.Trim());
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

            cmd.Parameters.AddWithValue("@idSessionECF", pSessionECF.Id.Trim());

            cmd.ExecuteReader();
            connexion.Close();
        }

        public static String donneIdSessionECF(ECF pEcf, DateTime pDate, int pVersion)
        {
            String requete = "SELECT idSessionECF from SESSIONSECF where idECF=@idECF and date=@date and version=@version";

            SqlConnection connexion = ConnexionSQL.CreationConnexion();
            SqlCommand cmd = new SqlCommand(requete, connexion);
            cmd.Parameters.AddWithValue("@idECF", pEcf.Id);
            cmd.Parameters.AddWithValue("@date", pDate);
            cmd.Parameters.AddWithValue("@version", pVersion);
            SqlDataReader reader = cmd.ExecuteReader();

            String idSessionECF = "0";
            if (reader.Read())
            {
                if (reader[0] != DBNull.Value)
                {
                    idSessionECF = reader.GetString(0).Trim();
                }
            }
            connexion.Close();

            return idSessionECF;
        }
        public static List<SessionECF> donneSessionsECFJour(ECF pECF, DateTime pDate)
        {
            String requete = "SELECT * from SESSIONSECF where idECF=@idECF and date=@date";

            SqlConnection connexion = ConnexionSQL.CreationConnexion();
            SqlCommand cmd = new SqlCommand(requete, connexion);
            cmd.Parameters.AddWithValue("@idECF", pECF.Id);
            cmd.Parameters.AddWithValue("@date", pDate);
            SqlDataReader reader = cmd.ExecuteReader();

            List<SessionECF> lesSessionsECFduJour = new List<SessionECF>();

            while (reader.Read())
            {
                ECF ecfTemp = new ECF();
                ecfTemp.Id= reader.GetString(reader.GetOrdinal("idECF")).Trim();
                SessionECF sess = new SessionECF();

                sess.Id = reader.GetString(reader.GetOrdinal("idSessionECF"));
                sess.Ecf = ECFDAL.getECF(ecfTemp.Id);
                sess.Date = reader.GetDateTime(reader.GetOrdinal("date"));
                sess.Version = reader.GetInt32(reader.GetOrdinal("version"));

                //participants
                List<Stagiaire> lesParticipants = new List<Stagiaire>();
                SqlConnection c2 = ConnexionSQL.CreationConnexion();
                SqlCommand cmd2 = new SqlCommand(SELECT_PARTICIPANTS, c2);
                cmd2.Parameters.AddWithValue("@idSessionECF", sess.Id.Trim());
                SqlDataReader reader2 = cmd2.ExecuteReader();

                while (reader2.Read())
                {
                    int idStag = reader2.GetInt32(reader2.GetOrdinal("idStagiaire"));
                    Stagiaire participant = StagiairesDAL.getStagiaire(idStag);

                    sess.Participants.Add(participant);
                }
                c2.Close();

                lesSessionsECFduJour.Add(sess);
            }
            connexion.Close();

            return lesSessionsECFduJour;
        }

        public static List<Stagiaire> getListParticipants(SessionECF pSessionECF)
        {
            String requete = " SELECT Stagiaire.CodeStagiaire FROM Stagiaire, PARTICIPANTSSESSIONECF " +
                " WHERE Stagiaire.CodeStagiaire=PARTICIPANTSSESSIONECF.idStagiaire AND PARTICIPANTSSESSIONECF.idSessionECF=@idSessionECF";

            SqlConnection connexion = ConnexionSQL.CreationConnexion();
            SqlCommand cmd = new SqlCommand(requete, connexion);
            cmd.Parameters.AddWithValue("@idSessionECF", pSessionECF.Id.Trim());
            //if (pFiltreNomPrenom != "") cmd.Parameters.AddWithValue("@filtreNP", pFiltreNomPrenom.Trim());
            SqlDataReader reader = cmd.ExecuteReader();

            List<Stagiaire> listeStagiaires = new List<Stagiaire>();
            while (reader.Read())
            {
                Stagiaire s = new Stagiaire();
                s._id = reader.GetInt32(reader.GetOrdinal("CodeStagiaire"));

                s = StagiairesDAL.getStagiaire(s._id);

                listeStagiaires.Add(s);
            }
            return listeStagiaires;
        }

        public static bool SessionECFCorrigee(SessionECF pSessionECF, Stagiaire pStag)
        {
            bool b = true;

            foreach (Competence comp in pSessionECF.Ecf.Competences)
	        {
                String requete = "SELECT * FROM EVALUATION " + 
                    "WHERE idECF=@idECF " +
                    "AND idStagiaire=@idStagiaire " +
                    "AND idCompetence=@idCompetence " +
                    "AND date=@date";
                SqlConnection connexion = ConnexionSQL.CreationConnexion();
                SqlCommand cmd = new SqlCommand(requete, connexion);
                cmd.Parameters.AddWithValue("@idECF", pSessionECF.Ecf.Id.Trim());
                cmd.Parameters.AddWithValue("@idStagiaire", pStag._id);
                cmd.Parameters.AddWithValue("@idCompetence", comp.Id.Trim());
                cmd.Parameters.AddWithValue("@date", pSessionECF.Date);
                SqlDataReader reader = cmd.ExecuteReader();

                if (!reader.Read())
                {
                    b = false;
                }
	        } 

            return b;
        }
    }
}
