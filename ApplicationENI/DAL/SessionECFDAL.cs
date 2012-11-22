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
        static String SELECT_SESSIONSECF = "SELECT * FROM SESSIONSECF WHERE idECF=@idECF and version=@version order by date";
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
                sessionECFTemp.Ecf = ECFDAL.getECF(ecfTemp);
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

        public static List<SessionECF> getListSessionsECF(ECF pECF, int pVersion)
        {
            List<SessionECF> lesSessionsECFs = new List<SessionECF>();
            SqlConnection connexion = ConnexionSQL.CreationConnexion();
            SqlCommand cmd = new SqlCommand(SELECT_SESSIONSECF, connexion);
            cmd.Parameters.AddWithValue("@idECF", pECF.Id.Trim());
            cmd.Parameters.AddWithValue("@version", pVersion);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                SessionECF sessionECFTemp = new SessionECF();
                ECF ecfTemp = new ECF();
                ecfTemp.Id = reader.GetString(reader.GetOrdinal("idECF")).Trim();

                sessionECFTemp.Id = reader.GetString(reader.GetOrdinal("idSessionECF"));
                sessionECFTemp.Ecf = ECFDAL.getECF(ecfTemp);
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
                sess.Ecf = ECFDAL.getECF(ecfTemp);
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
            String requete = " SELECT Stagiaire.CodeStagiaire, Stagiaire.Nom, Stagiaire.Prenom FROM Stagiaire, PARTICIPANTSSESSIONECF " +
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
                //s._civilité = reader.GetSqlString(1).IsNull ? string.Empty : reader.GetString(1);
                s._nom = reader.GetSqlString(1).IsNull ? string.Empty : reader.GetString(1);
                s._prenom = reader.GetSqlString(2).IsNull ? string.Empty : reader.GetString(2);
                //s._adresse1 = reader.GetSqlString(4).IsNull ? string.Empty : reader.GetString(4);
                //s._adresse2 = reader.GetSqlString(5).IsNull ? string.Empty : reader.GetString(5);
                //s._adresse3 = reader.GetSqlString(6).IsNull ? string.Empty : reader.GetString(6);
                //s._cp = reader.GetSqlString(7).IsNull ? string.Empty : reader.GetString(7);
                //s._ville = reader.GetSqlString(8).IsNull ? string.Empty : reader.GetString(8);
                //s._telephoneFixe = reader.GetSqlString(9).IsNull ? string.Empty : reader.GetString(9);
                //s._telephonePortable = reader.GetSqlString(10).IsNull ? string.Empty : reader.GetString(10);
                //s._email = reader.GetSqlString(11).IsNull ? string.Empty : reader.GetString(11);
                //if (!reader.GetSqlDateTime(12).IsNull) { s._dateNaissance = reader.GetDateTime(12); }
                //s._codeRegion = reader.GetSqlString(13).IsNull ? string.Empty : reader.GetString(13);
                //s._codeNationalité = reader.GetSqlString(14).IsNull ? string.Empty : reader.GetString(14);
                //s._codeOrigineMedia = reader.GetSqlString(15).IsNull ? string.Empty : reader.GetString(15);
                //if (!reader.GetSqlDateTime(16).IsNull) { s._datePremierEnvoiDoc = reader.GetDateTime(16); }
                //if (!reader.GetSqlDateTime(17).IsNull) { s._dateCreation = reader.GetDateTime(17); }
                //s._repertoire = reader.GetSqlString(18).IsNull ? string.Empty : reader.GetString(18);
                //if (reader.GetBoolean(19)) { s._permis = reader.GetBoolean(19); }
                //s._photo = reader.GetSqlString(20).IsNull ? string.Empty : reader.GetString(20);
                //if (reader.GetBoolean(21)) { s._envoiDocEnCours = reader.GetBoolean(21); }
                //s._historique = reader.GetSqlString(22).IsNull ? string.Empty : reader.GetString(22);
                listeStagiaires.Add(s);
            }
            return listeStagiaires;
        }
    }
}
