using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApplicationENI.Modele;
using System.Data.SqlClient;

namespace ApplicationENI.DAL
{
    class CompetencesDAL
    {
        //TODO Mat
        static String SELECT_COMPETENCES = "SELECT * FROM COMPETENCE order by code, libelle";
        static String SELECT_CODE = "SELECT * FROM COMPETENCE WHERE code=@code";
        
        static String DELETE_COMPETENCE = "DELETE FROM COMPETENCE WHERE idCompetence=@id";
        static String SELECT_MAX = "SELECT MAX(idCompetence) FROM COMPETENCE";
        static String SELECT_LIENS = "SELECT * FROM COMPETENCESECF where idCompetence=@idCompetence";
        //static String UPDATE_COMPETENCE = "UPDATE SET WHERE ";

        static String INSERT_COMP = "INSERT INTO COMPETENCE (idCompetence, code, libelle) VALUES (@id, @code, @libelle)";

        public static List<Competence> getListCompetences()
        {
            List<Competence> lesCompetences = new List<Competence>();

            SqlConnection connexion = ConnexionSQL.CreationConnexion();
            SqlCommand cmd = new SqlCommand(SELECT_COMPETENCES, connexion);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Competence compTemp = new Competence();
                compTemp.Id = reader.GetString(reader.GetOrdinal("idCompetence")).Trim();
                compTemp.Code = reader.GetString(reader.GetOrdinal("code")).Trim();
                compTemp.Libelle = reader.GetString(reader.GetOrdinal("libelle")).Trim();
                               
                lesCompetences.Add(compTemp);
            }
            connexion.Close();

            return lesCompetences;
            //return DAL.JeuDonnees.GetListCompetence();
        }

        public static String ajouterCompetence(Competence comp)
        {
            //Verifier que ce code n'existe pas deja dans la base
            SqlConnection connexion = ConnexionSQL.CreationConnexion();
            SqlCommand cmd = new SqlCommand(SELECT_CODE, connexion);
            cmd.Parameters.AddWithValue("@code", comp.Code.Trim());
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                connexion.Close();
                return "Ce code et déjà utilisé par une autre compétence";
            }
            connexion.Close();

            //Récup de l'id max dans la table COMPETENCES
            connexion = ConnexionSQL.CreationConnexion();
            cmd = new SqlCommand(SELECT_MAX, connexion);
            reader = cmd.ExecuteReader();
            String idMaxCompetence = "0";
            if (reader.Read()) idMaxCompetence = reader.GetString(0).Trim();
            comp.Id=(Convert.ToInt32(idMaxCompetence) + 1).ToString();
            connexion.Close();

            //Création de la competence
            connexion = ConnexionSQL.CreationConnexion();
            cmd = new SqlCommand(INSERT_COMP, connexion);

            cmd.Parameters.AddWithValue("@id", comp.Id);
            cmd.Parameters.AddWithValue("@code", comp.Code);
            cmd.Parameters.AddWithValue("@libelle", comp.Libelle);

            cmd.ExecuteReader();
            connexion.Close();

            return "";
        }

        public static void modifierCompetence(Competence comp)
        {
            //TODO?
        }

        public static String supprimerCompetence(Competence comp)
        {
            //TODO verifier qu il n y a pas de lien existant sinon message d erreur
            SqlConnection connexion = ConnexionSQL.CreationConnexion();
            SqlCommand cmd = new SqlCommand(SELECT_LIENS, connexion);
            cmd.Parameters.AddWithValue("@idCompetence", comp.Id);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                connexion.Close();
                return "Il existe des liens entre cette compétence (" + comp.Code + " - " + comp.Libelle +
                    ") et (au moins) un ECF";
            }
            connexion.Close();

            //Suppr d'une competence
            connexion = ConnexionSQL.CreationConnexion();
            cmd = new SqlCommand(DELETE_COMPETENCE, connexion);

            cmd.Parameters.AddWithValue("@id", comp.Id);

            cmd.ExecuteReader();
            connexion.Close();

            return "";
        }
    }
}
