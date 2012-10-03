using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApplicationENI.Modele;
using System.Data.SqlClient;

namespace ApplicationENI.DAL
{
    class ECFDAL
    {
        //TODO Mat
        static String SELECT_ECF = "SELECT * FROM ECFS WHERE idECF=@id";//, Competences, CompetenceECFS as lien WHERE lien.idECF=ECFs=idECF and lien.idCompetence=Competences.idCompetence order by ECFs.idECF";
        static String SELECT_ECFS = "SELECT * FROM ECFS order by code, libelle";//, Competences, CompetenceECFS as lien WHERE lien.idECF=ECFs=idECF and lien.idCompetence=Competences.idCompetence order by ECFs.idECF";
        static String SELECT_COMPS = "SELECT COMPETENCES.idCompetence, COMPETENCES.code, COMPETENCES.libelle FROM COMPETENCES, COMPETENCESECF WHERE COMPETENCES.idCompetence=COMPETENCESECF.idCompetence and COMPETENCESECF.idECF=@lienECFComp order by COMPETENCES.code, COMPETENCES.libelle";
        static String SELECT_MAX = "SELECT MAX(idECF) FROM ECFS";
        static String SELECT_CODE = "SELECT * FROM ECFS where code=@code";

        static String INSERT_ECF= "INSERT INTO ECFS (idECF, code, libelle) VALUES (@id, @code, @libelle)";
        static String INSERT_LIEN = "INSERT INTO COMPETENCESECF (idECF, idCompetence) VALUES (@idECF, @idCompetence)";
        
        static String UPDATE_ECF = "UPDATE ECFS SET libelle=@libelleECF,coefficient=@coefficient,typeNotation=@typeNotation,nbreVersions=@nbreVersions,commentaire=@commentaire WHERE idECF=@idECF";
        
        static String DELETE_LIENS = "DELETE FROM COMPETENCESECF WHERE idECF=@idECF";
        static String DELETE_LIEN = "DELETE FROM COMPETENCESECF WHERE idECF=@idECF AND idCompetence=@idCompetence";
        static String DELETE_ECF = "DELETE FROM ECFS WHERE idECF=@id";

        public static ECF getECF(ECF ecf)
        {
            ECF ecfTemp = new ECF();

            SqlConnection connexion = ConnexionSQL.CreationConnexion();
            SqlCommand cmd = new SqlCommand(SELECT_ECF, connexion);
            cmd.Parameters.AddWithValue("@id", ecf.Id.Trim());
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                ecfTemp = new ECF();
                ecfTemp.Id = reader.GetString(reader.GetOrdinal("idECF")).Trim();
                ecfTemp.Code = reader.GetString(reader.GetOrdinal("code")).Trim();
                ecfTemp.Libelle = reader.GetString(reader.GetOrdinal("libelle")).Trim();

                if (reader["coefficient"] != DBNull.Value) ecfTemp.Coefficient = reader.GetDouble(reader.GetOrdinal("coefficient"));
                ecfTemp.NotationNumerique = true;
                if ((reader["typeNotation"] != DBNull.Value) && (reader.GetInt16(reader.GetOrdinal("typeNotation")) == Ressources.CONSTANTES.NOTATION_ACQUISITION))
                {
                    ecfTemp.NotationNumerique = false;
                }

                if (reader["nbreVersions"] != DBNull.Value) ecfTemp.NbreVersion = reader.GetInt32(reader.GetOrdinal("nbreVersions"));
                if (reader["commentaire"] != DBNull.Value) ecfTemp.Commentaire = reader.GetString(reader.GetOrdinal("commentaire")).Trim();

                SqlConnection c2 = ConnexionSQL.CreationConnexion();
                SqlCommand cmd2 = new SqlCommand(SELECT_COMPS, c2);
                //cmd2.Parameters.AddWithValue("@lienECFComp", ecfTemp.Id.Trim());
                cmd2.Parameters.Add(new SqlParameter("@lienECFComp", ecfTemp.Id.Trim()));
                SqlDataReader reader2 = cmd2.ExecuteReader();
                List<Competence> lesComp = new List<Competence>();

                while (reader2.Read())
                {
                    Competence compTemp = new Competence();
                    compTemp.Id = reader2.GetString(reader2.GetOrdinal("idCompetence")).Trim();
                    compTemp.Code = reader2.GetString(reader2.GetOrdinal("code")).Trim();
                    compTemp.Libelle = reader2.GetString(reader2.GetOrdinal("libelle")).Trim();
                    lesComp.Add(compTemp);
                }
                c2.Close();
                ecfTemp.Competences = lesComp;
            }
            connexion.Close();

            ecf = ecfTemp;
            return ecf;
                    
        }

        public static List<ECF> getListECFs()
        {
            List<ECF> lesECFs = new List<ECF>();

            SqlConnection connexion = ConnexionSQL.CreationConnexion();
            SqlCommand cmd = new SqlCommand(SELECT_ECFS, connexion);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                ECF ecfTemp = new ECF();
                ecfTemp.Id = reader.GetString(reader.GetOrdinal("idECF")).Trim();
                ecfTemp.Code = reader.GetString(reader.GetOrdinal("code")).Trim();
                ecfTemp.Libelle = reader.GetString(reader.GetOrdinal("libelle")).Trim();
                
                if (reader["coefficient"] != DBNull.Value) ecfTemp.Coefficient = reader.GetDouble(reader.GetOrdinal("coefficient"));
                ecfTemp.NotationNumerique = true;
                if ((reader["typeNotation"] != DBNull.Value) && (reader.GetInt16(reader.GetOrdinal("typeNotation")) == Ressources.CONSTANTES.NOTATION_ACQUISITION))
                {
                    ecfTemp.NotationNumerique = false;
                }    
                if (reader["nbreVersions"] != DBNull.Value) ecfTemp.NbreVersion = reader.GetInt32(reader.GetOrdinal("nbreVersions"));
                if (reader["commentaire"] != DBNull.Value) ecfTemp.Commentaire = reader.GetString(reader.GetOrdinal("commentaire")).Trim();

                SqlConnection c2 = ConnexionSQL.CreationConnexion();
                SqlCommand cmd2 = new SqlCommand(SELECT_COMPS, c2);
                //cmd2.Parameters.AddWithValue("@lienECFComp", ecfTemp.Id.Trim());
                cmd2.Parameters.Add(new SqlParameter("@lienECFComp",ecfTemp.Id.Trim()));
                SqlDataReader reader2 = cmd2.ExecuteReader();
                List<Competence> lesComp = new List<Competence>();      
          
                while (reader2.Read())
                {
                    Competence compTemp = new Competence();
                    compTemp.Id = reader2.GetString(reader2.GetOrdinal("idCompetence")).Trim();
                    compTemp.Code = reader2.GetString(reader2.GetOrdinal("code")).Trim();
                    compTemp.Libelle = reader2.GetString(reader2.GetOrdinal("libelle")).Trim();
                    lesComp.Add(compTemp);
                }
                c2.Close();
                ecfTemp.Competences = lesComp;
                lesECFs.Add(ecfTemp);
            }
            connexion.Close();

            return lesECFs;
            //return DAL.JeuDonnees.GetListECF();
        }

        public static String ajouterECF(ECF ecf)
        {
            //Verifier que ce code n'existe pas deja dans la base
            SqlConnection connexion = ConnexionSQL.CreationConnexion();
            SqlCommand cmd = new SqlCommand(SELECT_CODE, connexion);
            cmd.Parameters.AddWithValue("@code", ecf.Code.Trim());
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                connexion.Close(); 
                return "Ce code et déjà utilisé par un autre ECF";
            }            
            connexion.Close();
            
            //Récup de l'id max dans la table ECFS
            connexion = ConnexionSQL.CreationConnexion();
            cmd = new SqlCommand(SELECT_MAX, connexion);
            reader = cmd.ExecuteReader();
            String idMaxECF="0";
            if (reader.Read()) idMaxECF = reader.GetString(0).Trim();
            ecf.Id = (Convert.ToInt32(idMaxECF) + 1).ToString().Trim();
            connexion.Close();

            //Création de l'ECF
            connexion = ConnexionSQL.CreationConnexion();
            cmd = new SqlCommand(INSERT_ECF, connexion);

            cmd.Parameters.AddWithValue("@id", ecf.Id.Trim());
            cmd.Parameters.AddWithValue("@code", ecf.Code.Trim());
            cmd.Parameters.AddWithValue("@libelle", ecf.Libelle.Trim());

            cmd.ExecuteReader();
            connexion.Close();
            
            return "";
        }

        public static void modifierECF(ECF ecf)
        {
            //MAJ de l'ECF
            SqlConnection connexion = ConnexionSQL.CreationConnexion();
            SqlCommand cmd = new SqlCommand(UPDATE_ECF, connexion);

            cmd.Parameters.AddWithValue("@idECF", ecf.Id.Trim());  
            cmd.Parameters.AddWithValue("@libelleECF", ecf.Libelle);
			cmd.Parameters.AddWithValue("@coefficient", ecf.Coefficient);
            int typeNotation = Ressources.CONSTANTES.NOTATION_ACQUISITION;
            if (ecf.NotationNumerique) typeNotation = Ressources.CONSTANTES.NOTATION_NUMERIQUE;
            cmd.Parameters.AddWithValue("@typeNotation", typeNotation);
            cmd.Parameters.AddWithValue("@nbreVersions", ecf.NbreVersion);
            cmd.Parameters.AddWithValue("@commentaire", ecf.Commentaire);
	
            cmd.ExecuteReader();
            connexion.Close();

            //Suppr des liens ECF-Competences
            supprimerLiens(ecf);

            //Création des liens ECF-Competences
            foreach (Competence compTemp in ecf.Competences)
            {
                ajouterLien(ecf, compTemp);
            }
        }

        public static void ajouterLien(ECF ecf, Competence comp)
        {
            SqlConnection connexion = ConnexionSQL.CreationConnexion();
            SqlCommand cmd = new SqlCommand(INSERT_LIEN, connexion);

            cmd.Parameters.AddWithValue("@idECF", ecf.Id.Trim());
            cmd.Parameters.AddWithValue("@idCompetence", comp.Id.Trim());

            cmd.ExecuteReader();
            connexion.Close();
        }

        public static void supprimerLien(ECF ecf, Competence comp)
        {
            SqlConnection connexion = ConnexionSQL.CreationConnexion();
            SqlCommand cmd = new SqlCommand(DELETE_LIEN, connexion);

            cmd.Parameters.AddWithValue("@idECF", ecf.Id.Trim());
            cmd.Parameters.AddWithValue("@idCompetence", comp.Id.Trim());

            cmd.ExecuteReader();
            connexion.Close();
        }

        public static void supprimerLiens(ECF ecf)
        {
            SqlConnection connexion = ConnexionSQL.CreationConnexion();
            SqlCommand cmd = new SqlCommand(DELETE_LIENS, connexion);

            cmd.Parameters.AddWithValue("@idECF", ecf.Id.Trim());

            cmd.ExecuteReader();
            connexion.Close();
        }

        public static void supprimerECF(ECF ecf)
        {
            //Suppr des liens ECF-Competences
            supprimerLiens(ecf);
            
            //Suppr d'un ECF
            SqlConnection connexion = ConnexionSQL.CreationConnexion();
            SqlCommand cmd = new SqlCommand(DELETE_ECF, connexion);

            cmd.Parameters.AddWithValue("@id", ecf.Id.Trim());

            cmd.ExecuteReader();
            connexion.Close();
        }
        
    }
}
