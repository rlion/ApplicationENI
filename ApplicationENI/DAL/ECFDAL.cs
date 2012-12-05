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
        static String SELECT_ECF = "SELECT * FROM ECF WHERE idECF=@id";//, Competences, CompetenceECFS as lien WHERE lien.idECF=ECFs=idECF and lien.idCompetence=Competences.idCompetence order by ECFs.idECF";
        static String SELECT_ECFS = "SELECT * FROM ECF order by code, libelle";//, Competences, CompetenceECFS as lien WHERE lien.idECF=ECFs=idECF and lien.idCompetence=Competences.idCompetence order by ECFs.idECF";
        static String SELECT_COMPS = "SELECT COMPETENCE.idCompetence, COMPETENCE.code, COMPETENCE.libelle FROM COMPETENCE, COMPETENCESECF WHERE COMPETENCE.idCompetence=COMPETENCESECF.idCompetence and COMPETENCESECF.idECF=@lienECFComp order by COMPETENCE.code, COMPETENCE.libelle";
        static String SELECT_FORMS = "SELECT FORMATION.CodeFormation, FORMATION.LibelleCourt FROM FORMATION, FORMATIONSECF WHERE FORMATION.CodeFormation=FORMATIONSECF.idFormation and FORMATIONSECF.idECF=@lienECFForm order by FORMATION.LibelleCourt, FORMATION.CodeFormation";
        static String SELECT_MAX = "SELECT MAX(idECF) FROM ECF";
        static String SELECT_CODE = "SELECT * FROM ECF where code=@code";

        static String INSERT_ECF= "INSERT INTO ECF (idECF, code, libelle,coefficient,typeNotation,nbreVersions) VALUES (@idECF, @code, @libelle,@coefficient,@typeNotation,@nbreVersions)";
        static String INSERT_LIEN_COMPETENCE = "INSERT INTO COMPETENCESECF (idECF, idCompetence) VALUES (@idECF, @idCompetence)";
        static String INSERT_LIEN_FORMATION = "INSERT INTO FORMATIONSECF (idECF, idFormation) VALUES (@idECF, @codeFormation)";
        
        static String UPDATE_ECF = "UPDATE ECF SET libelle=@libelleECF,coefficient=@coefficient,typeNotation=@typeNotation,nbreVersions=@nbreVersions,commentaire=@commentaire WHERE idECF=@idECF";
        
        static String DELETE_LIENS_COMPETENCES = "DELETE FROM COMPETENCESECF WHERE idECF=@idECF";
        static String DELETE_LIEN_COMPETENCE = "DELETE FROM COMPETENCESECF WHERE idECF=@idECF AND idCompetence=@idCompetence";
        static String DELETE_LIENS_FORMATIONS = "DELETE FROM FORMATIONSECF WHERE idECF=@idECF";
        static String DELETE_LIEN_FORMATION = "DELETE FROM FORMATIONSECF WHERE idECF=@idECF AND idFormation=@codeFormation";
        static String DELETE_ECF = "DELETE FROM ECF WHERE idECF=@id";

        public static ECF getECF(int idECF)
        {
            ECF ecfTemp = new ECF();

            SqlConnection connexion = ConnexionSQL.CreationConnexion();
            SqlCommand cmd = new SqlCommand(SELECT_ECF, connexion);
            cmd.Parameters.AddWithValue("@id", idECF);
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                ecfTemp = new ECF();
                ecfTemp.Id = reader.GetInt32(reader.GetOrdinal("idECF"));
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

                //Competences
                SqlConnection c2 = ConnexionSQL.CreationConnexion();
                SqlCommand cmd2 = new SqlCommand(SELECT_COMPS, c2);
                //cmd2.Parameters.AddWithValue("@lienECFComp", ecfTemp.Id.Trim());
                cmd2.Parameters.Add(new SqlParameter("@lienECFComp", ecfTemp.Id));
                SqlDataReader reader2 = cmd2.ExecuteReader();
                List<Competence> lesComp = new List<Competence>();
                while (reader2.Read())
                {
                    Competence compTemp = new Competence();
                    compTemp.Id = reader2.GetInt32(reader2.GetOrdinal("idCompetence"));
                    compTemp.Code = reader2.GetString(reader2.GetOrdinal("code")).Trim();
                    compTemp.Libelle = reader2.GetString(reader2.GetOrdinal("libelle")).Trim();
                    lesComp.Add(compTemp);
                }
                c2.Close();
                ecfTemp.Competences = lesComp;

                //Formations
                SqlConnection c3 = ConnexionSQL.CreationConnexion();
                SqlCommand cmd3 = new SqlCommand(SELECT_FORMS, c3);
                //cmd2.Parameters.AddWithValue("@lienECFComp", ecfTemp.Id.Trim());
                cmd3.Parameters.Add(new SqlParameter("@lienECFForm", ecfTemp.Id));
                SqlDataReader reader3 = cmd3.ExecuteReader();
                List<Formation> lesFormations = new List<Formation>();
                while (reader3.Read())
                {
                    Formation formTemp = new Formation();
                    formTemp.Code = reader3.GetString(reader3.GetOrdinal("CodeFormation"));
                    formTemp.Libelle = reader3.GetString(reader3.GetOrdinal("LibelleCourt")).Trim();
                    lesFormations.Add(formTemp);
                }
                c3.Close();
                ecfTemp.Formations = lesFormations;
            }
            connexion.Close();

            return ecfTemp;
                    
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
                ecfTemp.Id = reader.GetInt32(reader.GetOrdinal("idECF"));
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

                //Competences
                SqlConnection c2 = ConnexionSQL.CreationConnexion();
                SqlCommand cmd2 = new SqlCommand(SELECT_COMPS, c2);
                //cmd2.Parameters.AddWithValue("@lienECFComp", ecfTemp.Id.Trim());
                cmd2.Parameters.Add(new SqlParameter("@lienECFComp",ecfTemp.Id));
                SqlDataReader reader2 = cmd2.ExecuteReader();
                List<Competence> lesComp = new List<Competence>();      
                while (reader2.Read())
                {
                    Competence compTemp = new Competence();
                    compTemp.Id = reader2.GetInt32(reader2.GetOrdinal("idCompetence"));
                    compTemp.Code = reader2.GetString(reader2.GetOrdinal("code")).Trim();
                    compTemp.Libelle = reader2.GetString(reader2.GetOrdinal("libelle")).Trim();
                    lesComp.Add(compTemp);
                }
                c2.Close();
                ecfTemp.Competences = lesComp;

                //Formations
                SqlConnection c3 = ConnexionSQL.CreationConnexion();
                SqlCommand cmd3 = new SqlCommand(SELECT_FORMS, c3);
                //cmd2.Parameters.AddWithValue("@lienECFComp", ecfTemp.Id.Trim());
                cmd3.Parameters.Add(new SqlParameter("@lienECFForm", ecfTemp.Id));
                SqlDataReader reader3 = cmd3.ExecuteReader();
                List<Formation> lesFormations = new List<Formation>();
                while (reader3.Read())
                {
                    Formation formTemp = new Formation();
                    formTemp.Code = reader3.GetString(reader3.GetOrdinal("CodeFormation"));
                    formTemp.Libelle = reader3.GetString(reader3.GetOrdinal("LibelleCourt")).Trim();
                    lesFormations.Add(formTemp);
                }
                c3.Close();
                ecfTemp.Formations = lesFormations;

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
            
            //Récup de l'id max dans la table ECF
            connexion = ConnexionSQL.CreationConnexion();
            cmd = new SqlCommand(SELECT_MAX, connexion);
            reader = cmd.ExecuteReader();
            int idMaxECF=0;
            if (reader.Read())
            {
                if (reader[0] != DBNull.Value) {
                    idMaxECF = reader.GetInt32(0);
                }
            }
            ecf.Id = idMaxECF + 1;
            connexion.Close();

            //Création de l'ECF
            connexion = ConnexionSQL.CreationConnexion();
            cmd = new SqlCommand(INSERT_ECF, connexion);

            cmd.Parameters.AddWithValue("@idECF", ecf.Id);
            cmd.Parameters.AddWithValue("@code", ecf.Code.Trim());
            cmd.Parameters.AddWithValue("@libelle", ecf.Libelle.Trim());
            cmd.Parameters.AddWithValue("@coefficient", 1);
            cmd.Parameters.AddWithValue("@typeNotation", 0);
            cmd.Parameters.AddWithValue("@nbreVersions", 1);

            cmd.ExecuteReader();
            connexion.Close();
            
            return "";
        }

        public static String modifierECF(ECF ecf)
        {
            String reponse="";
            String requete="";
            SqlConnection connexion;
            SqlCommand cmd;
            SqlDataReader reader;

            ECF AncienECF=getECF(ecf.Id);

            //Si on souhaite changer le type de notation mais qu'il y a deja des notes
            if(AncienECF.NotationNumerique!=ecf.NotationNumerique)
            {
                requete="SELECT idECF FROM EVALUATION WHERE idECF=@idECF";
                connexion = ConnexionSQL.CreationConnexion();
                cmd = new SqlCommand(requete, connexion);
                cmd.Parameters.AddWithValue("@idECF", ecf.Id);
                reader = cmd.ExecuteReader();
                if(reader.Read())
                {
                    reponse = "Modification du type de notation impossible, certains stagiaires ont déjà été évalués selon l'ancien type de notation";
                    return reponse;
                }
            }

            //Si on souhaite reduire le nombre de versions mais qu'il y a deja des notes
            if(AncienECF.NbreVersion>ecf.NbreVersion)
            {
                requete="SELECT idECF FROM EVALUATION WHERE idECF=@idECF and version>@version";
                connexion = ConnexionSQL.CreationConnexion();
                cmd = new SqlCommand(requete, connexion);
                cmd.Parameters.AddWithValue("@idECF", ecf.Id);
                cmd.Parameters.AddWithValue("@version", ecf.NbreVersion);
                reader = cmd.ExecuteReader();
                if(reader.Read())
                {
                    reponse = "Modification du nombre de versions impossible, certains stagiaires ont déjà été évalués selon des versions d'ECF supplémentaires";
                    return reponse;
                }
            }

            //MAJ de l'ECF
            connexion = ConnexionSQL.CreationConnexion();
            cmd = new SqlCommand(UPDATE_ECF, connexion);

            cmd.Parameters.AddWithValue("@idECF", ecf.Id);  
            cmd.Parameters.AddWithValue("@libelleECF", ecf.Libelle);
			cmd.Parameters.AddWithValue("@coefficient", ecf.Coefficient);
            int typeNotation = Ressources.CONSTANTES.NOTATION_ACQUISITION;
            if (ecf.NotationNumerique) typeNotation = Ressources.CONSTANTES.NOTATION_NUMERIQUE;
            cmd.Parameters.AddWithValue("@typeNotation", typeNotation);
            cmd.Parameters.AddWithValue("@nbreVersions", ecf.NbreVersion);
            cmd.Parameters.AddWithValue("@commentaire", ecf.Commentaire);
	
            cmd.ExecuteReader();
            connexion.Close();

            bool memesCompetences = false;
            if (ecf.Competences!=null && AncienECF.Competences!=null && ecf.Competences.Count==AncienECF.Competences.Count)
            {
                foreach (Competence comp in ecf.Competences)
                {
                    int indexAncienneComp = AncienECF.Competences.IndexOf(comp);
                    if (indexAncienneComp == -1)
                    {
                        memesCompetences = false;
                        break;
                    }
                    else
                    {
                        if (comp.Equals(AncienECF.Competences[indexAncienneComp]))
                        {
                            memesCompetences = true;
                        }
                        else
                        {
                            memesCompetences = false;
                            break;
                        }
                    }
                }
            }
            

            if (!memesCompetences)
            {
                //Suppr des liens ECF-Competences si pas deja d'evaluation sur l'ECF
                reponse = supprimerLiensCompetences(ecf);

                if (reponse != "")
                {
                    return reponse;
                }
                else
                {
                    //Création des liens ECF-Competences
                    if (ecf.Competences != null)
                    {
                        foreach (Competence compTemp in ecf.Competences)
                        {
                            ajouterLienCompetence(ecf, compTemp);
                        }
                    }
                }
            }                        

            //Suppr des liens ECF-Formations
            supprimerLiensFormations(ecf);

            //Création des liens ECF-Competences
            if (ecf.Formations!=null)
            {
                foreach (Formation formTemp in ecf.Formations)
                {
                    ajouterLienFormation(ecf, formTemp);
                }
            }            

            return "";
        }

        public static String ajouterLienCompetence(ECF ecf, Competence comp)
        {
            //Verifier que l'ECF n'a pas deja ete evalue
            // Si c'est le cas, on ne peut plus en modifier les competences rattachees
            String requete = "SELECT idECF FROM EVALUATION WHERE idECF=@idECF";
            SqlConnection connexion = ConnexionSQL.CreationConnexion();
            SqlCommand cmd = new SqlCommand(requete, connexion);
            cmd.Parameters.AddWithValue("@idECF", ecf.Id);
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                connexion.Close();

                return "Vous ne pouvez plus modifier les compétences de cet ECF car il a déjà été évalué";
            }
            else
            {
                connexion.Close(); 
                
                connexion = ConnexionSQL.CreationConnexion();
                cmd = new SqlCommand(INSERT_LIEN_COMPETENCE, connexion);

                cmd.Parameters.AddWithValue("@idECF", ecf.Id);
                cmd.Parameters.AddWithValue("@idCompetence", comp.Id);

                cmd.ExecuteReader();
                connexion.Close();

                return "";
            }
        }

        public static void ajouterLienFormation(ECF ecf, Formation form)
        {
            SqlConnection connexion = ConnexionSQL.CreationConnexion();
            SqlCommand cmd = new SqlCommand(INSERT_LIEN_FORMATION, connexion);

            cmd.Parameters.AddWithValue("@idECF", ecf.Id);
            cmd.Parameters.AddWithValue("@codeFormation", form.Code);

            cmd.ExecuteReader();
            connexion.Close();
        }

        public static String supprimerLienCompetence(ECF ecf, Competence comp)
        {
            //Verifier que l'ECF n'a pas deja ete evalue
            // Si c'est le cas, on ne peut plus en modifier les competences rattachees
            String requete = "SELECT idECF FROM EVALUATION WHERE idECF=@idECF";
            SqlConnection connexion = ConnexionSQL.CreationConnexion();
            SqlCommand cmd = new SqlCommand(requete, connexion);
            cmd.Parameters.AddWithValue("@idECF", ecf.Id);
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                connexion.Close();

                return "Vous ne pouvez plus modifier les compétences de cet ECF car il a déjà été évalué";
            }
            else
            {
                connexion.Close();
                
                connexion = ConnexionSQL.CreationConnexion();
                cmd = new SqlCommand(DELETE_LIEN_COMPETENCE, connexion);

                cmd.Parameters.AddWithValue("@idECF", ecf.Id);
                cmd.Parameters.AddWithValue("@idCompetence", comp.Id);

                cmd.ExecuteReader();
                connexion.Close();

                return "";
            }
        }
        public static void supprimerLienFormation(ECF ecf, Formation form)
        {
            SqlConnection connexion = ConnexionSQL.CreationConnexion();
            SqlCommand cmd = new SqlCommand(DELETE_LIEN_FORMATION, connexion);

            cmd.Parameters.AddWithValue("@idECF", ecf.Id);
            cmd.Parameters.AddWithValue("@codeFormation", form.Code);

            cmd.ExecuteReader();
            connexion.Close();
        }

        public static String supprimerLiensCompetences(ECF ecf)
        {
            //Verifier que l'ECF n'a pas deja ete evalue
            // Si c'est le cas, on ne peut plus en modifier les competences rattachees
            String requete = "SELECT idECF FROM EVALUATION WHERE idECF=@idECF";
            SqlConnection connexion = ConnexionSQL.CreationConnexion();
            SqlCommand cmd = new SqlCommand(requete, connexion);
            cmd.Parameters.AddWithValue("@idECF", ecf.Id);
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                connexion.Close();

                return "Vous ne pouvez plus modifier les compétences de cet ECF car il a déjà été évalué";
            }
            else
            {
                connexion = ConnexionSQL.CreationConnexion();
                cmd = new SqlCommand(DELETE_LIENS_COMPETENCES, connexion);

                cmd.Parameters.AddWithValue("@idECF", ecf.Id);

                cmd.ExecuteReader();
                connexion.Close();

                return "";
            }
        }

        public static void supprimerLiensFormations(ECF ecf)
        {
            SqlConnection connexion = ConnexionSQL.CreationConnexion();
            SqlCommand cmd = new SqlCommand(DELETE_LIENS_FORMATIONS, connexion);

            cmd.Parameters.AddWithValue("@idECF", ecf.Id);

            cmd.ExecuteReader();
            connexion.Close();
        }

        public static void supprimerECF(ECF ecf)
        {
            //TODO verif liens ECF-SessionECF ECF-evaluation si oui pas possible de supprimer
            //Suppr des liens ECF-Competences
            supprimerLiensCompetences(ecf);
            //Suppr des liens ECF-Formations
            supprimerLiensFormations(ecf);            

            //Suppr d'un ECF
            SqlConnection connexion = ConnexionSQL.CreationConnexion();
            SqlCommand cmd = new SqlCommand(DELETE_ECF, connexion);

            cmd.Parameters.AddWithValue("@id", ecf.Id);

            cmd.ExecuteReader();
            connexion.Close();
        }
        
    }
}
