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
        static String SELECT_ECFS = "SELECT * FROM ECFs";//, Competences, CompetenceECFS as lien WHERE lien.idECF=ECFs=idECF and lien.idCompetence=Competences.idCompetence order by ECFs.idECF";
        static String SELECT_COMPS = "SELECT * FROM Competences, CompetenceECFS WHERE Competences.idCompetence=CompetenceECFS.idCompetence and CompetenceECFS.idECF=@lienECFComp";
        
        static String INSERT_ECF= "INSERT INTO  VALUES (@id, @date, @nom_auteur, @type, @titre, @texte, @num_stagiaire)";
        static String DELETE_ECF= "DELETE FROM WHERE ";
        static String UPDATE_ECF= "UPDATE  SET WHERE ";

        public static List<ECF> getListECFs()
        {
            List<ECF> lesECFs = new List<ECF>();

            SqlConnection connexion = ConnexionSQL.CreationConnexion();
            SqlCommand cmd = new SqlCommand(SELECT_ECFS, connexion);
            //cmd.Parameters.AddWithValue("@num_stagiaire", pStg._id);

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                ECF ecfTemp = new ECF();
                ecfTemp.Id = reader.GetString(reader.GetOrdinal("idECF"));
                ecfTemp.Code = reader.GetString(reader.GetOrdinal("code"));
                ecfTemp.Libelle = reader.GetString(reader.GetOrdinal("libelle"));
                //ecfTemp.Coefficient = reader.GetFloat(reader.GetOrdinal("coefficient"));
                ecfTemp.NotationNumerique = false;
                //if (reader.GetInt32(reader.GetOrdinal("typeNotation")) == 0) ecfTemp.NotationNumerique = true;
                ecfTemp.NbreVersion = reader.GetInt32(reader.GetOrdinal("nbreVersions"));
                ecfTemp.Commentaire = reader.GetString(reader.GetOrdinal("commentaire"));

                SqlConnection c2 = ConnexionSQL.CreationConnexion();
                SqlCommand cmd2 = new SqlCommand(SELECT_COMPS, c2);
                cmd2.Parameters.AddWithValue("@lienECFComp", ecfTemp.Id);
                SqlDataReader reader2 = cmd2.ExecuteReader();
                List<Competence> lesComp = new List<Competence>();
                Competence compTemp = new Competence();
                while (reader2.Read())
                {
                    //compTemp.Id = reader2.GetString(reader2.GetOrdinal("Competences.idCompetence"));
                    compTemp.Code = reader2.GetString(reader2.GetOrdinal("code"));
                    compTemp.Libelle = reader2.GetString(reader2.GetOrdinal("libelle"));
                    lesComp.Add(compTemp);
                }

                lesECFs.Add(ecfTemp);
            }

            return lesECFs;
            //return DAL.JeuDonnees.GetListECF();
        }

        public static void ajouterECF(ECF ecf)
        {

            //Parametres.Instance.stagiaire.listeObservations.Add(o);
            // test d'ajout dans la base de données bidon
            /*SqlConnection connexion = ConnexionSQL.CreationConnexion();
            SqlCommand cmd = new SqlCommand(INSERT_OBSERVATION, connexion);
            cmd.Parameters.AddWithValue("@id", Guid.NewGuid());  
            cmd.Parameters.AddWithValue("@date", DateTime.Now);
			cmd.Parameters.AddWithValue("@nom_auteur", o._nomAuteur);
			cmd.Parameters.AddWithValue("@type", o._type);
			cmd.Parameters.AddWithValue("@titre", o._titre);
			cmd.Parameters.AddWithValue("@texte", o._texte);
			cmd.Parameters.AddWithValue("@num_stagiaire", o._stagiaire._id);
	
            cmd.ExecuteReader();
            connexion.Close();*/
        }

        public static void modifierECF(ECF ecf)
        {
            // test de modification dans la base de données bidon
            /*SqlConnection connexion = ConnexionSQL.CreationConnexion();
            SqlCommand cmd = new SqlCommand(UPDATE_OBSERVATION, connexion);
            cmd.Parameters.AddWithValue("@date", DateTime.Now);
			cmd.Parameters.AddWithValue("@nom_auteur", o._nomAuteur);
			cmd.Parameters.AddWithValue("@type", o._type);
			cmd.Parameters.AddWithValue("@titre", o._titre);
			cmd.Parameters.AddWithValue("@texte", o._texte);
			cmd.Parameters.AddWithValue("@num_observation", o._id);

            cmd.ExecuteReader();
            connexion.Close();*/
        }

        public static void supprimerECF(ECF e)
        {
            // test de suppression dans la base de données bidon
            /*SqlConnection connexion = ConnexionSQL.CreationConnexion();
            SqlCommand cmd = new SqlCommand(DELETE_OBSERVATION, connexion);
            cmd.Parameters.AddWithValue("@num_observation", o._id);  // il faut modifier tout ça

            cmd.ExecuteReader();
            connexion.Close();*/
        }
        
    }
}
