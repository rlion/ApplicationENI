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
        static String SELECT_COMPETENCES = "SELECT * FROM COMPETENCES";
        static String DELETE_COMPETENCE = "DELETE FROM COMPETENCES WHERE idCompetence=@id";
        static String SELECT_MAX = "SELECT MAX(idCompetence) FROM COMPETENCES";

        //static String UPDATE_COMPETENCE = "UPDATE SET WHERE ";

        static String INSERT_COMP = "INSERT INTO COMPETENCES (idCompetence, code, libelle) VALUES (@id, @code, @libelle)";

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

        public static void ajouterCompetence(Competence comp)
        {
            //Récup de l'id max dans la table COMPETENCES
            SqlConnection connexion = ConnexionSQL.CreationConnexion();
            SqlCommand cmd = new SqlCommand(SELECT_MAX, connexion);
            SqlDataReader reader = cmd.ExecuteReader();
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
        }

        public static void modifierCompetence(Competence comp)
        {
            //TODO?
        }

        public static void supprimerCompetence(Competence comp)
        {
            //Suppr d'une competence
            SqlConnection connexion = ConnexionSQL.CreationConnexion();
            SqlCommand cmd = new SqlCommand(DELETE_COMPETENCE, connexion);

            cmd.Parameters.AddWithValue("@id", comp.Id);

            cmd.ExecuteReader();
            connexion.Close();
        }
    }
}
