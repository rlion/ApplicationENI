using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApplicationENI.Modele;
using System.Data.SqlClient;

namespace ApplicationENI.DAL
{
    class CoursDAL
    {

        static String SELECT_COURS = "SELECT * FROM COURS";
        //static String SELECT_COURS = "SELECT * FROM COURS WHERE BLABLA";

        public static List<Cours> listeCours(Formation pF)
        {
            SqlConnection connexion = ConnexionSQL.CreationConnexion();
            SqlCommand cmd = new SqlCommand(SELECT_COURS, connexion);
            List<Cours> listeCours = new List<Cours>();

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Cours c = new Cours();
                c.IdCours = reader.GetInt32(reader.GetOrdinal("IdCours"));
                c.LibelleCours = reader.GetString(reader.GetOrdinal("LibelleCours"));
                listeCours.Add(c);
            }
            return listeCours;
        }
    }
}
