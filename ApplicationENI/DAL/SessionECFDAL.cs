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
        static String INSERT_SESSIONECF = "INSERT INTO SESSIONSECF (idECF, date) VALUES (@id,@date)";
        static String DELETE_SESSIONECF = "DELETE FROM SESSIONSECF WHERE idECF=@id";
        
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

                sessionECFTemp.Ecf = ECFDAL.getECF(ecfTemp);
                sessionECFTemp.Date = reader.GetDateTime(reader.GetOrdinal("date"));

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

            //Création de l'ECF
            SqlConnection connexion = ConnexionSQL.CreationConnexion();
            SqlCommand cmd = new SqlCommand(INSERT_SESSIONECF, connexion);

            cmd.Parameters.AddWithValue("@id", sessionEcf.Ecf.Id.Trim());
            cmd.Parameters.AddWithValue("@date", sessionEcf.Date);

            cmd.ExecuteReader();
            connexion.Close();
        }

        public static void supprimerSessionECF(SessionECF sessionEcf)
        {
            //TODO Suppr des liens ECF-Competences
            //supprimerLiens(ecf);

            //Suppr d'un ECF
            SqlConnection connexion = ConnexionSQL.CreationConnexion();
            SqlCommand cmd = new SqlCommand(DELETE_SESSIONECF, connexion);

            cmd.Parameters.AddWithValue("@id", sessionEcf.Ecf.Id.Trim());

            cmd.ExecuteReader();
            connexion.Close();
        }
    }
}
