using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApplicationENI.Modele;
using System.Data.SqlClient;

namespace ApplicationENI.DAL
{
    class FormationDAL
    {
        static String SELECT_FORMATIONS = "SELECT * FROM FORMATION";
        public static List<Formation> listeFormations() {

            SqlConnection connexion = ConnexionSQL.CreationConnexion();
            SqlCommand cmd = new SqlCommand(SELECT_FORMATIONS, connexion);
            List<Formation> listeFormations = new List<Formation>();

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read()) {
                Formation f = new Formation(reader.GetString(reader.GetOrdinal("LibelleCourt")));
                listeFormations.Add(f);
            }
            return listeFormations;
        }

    }
}
