using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using ApplicationENI.Modele;

namespace ApplicationENI.DAL
{
    class FonctionDAL
    {
        static String SELECT_FONCTIONS = "SELECT * FROM FONCTION";
        public static List<Fonction> listeFonctions() {
            SqlConnection connexion = ConnexionSQL.CreationConnexion();
            SqlCommand cmd = new SqlCommand(SELECT_FONCTIONS, connexion);
            List<Fonction> listeFonctions = new List<Fonction>();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                String code = reader.GetSqlString(0).IsNull ? String.Empty : reader.GetString(0);
                String nom = reader.GetSqlString(1).IsNull ? String.Empty : reader.GetString(1);
                Fonction fonc = new Fonction(code, nom);
                listeFonctions.Add(fonc);
            }
            return listeFonctions;
        }
    }
}
