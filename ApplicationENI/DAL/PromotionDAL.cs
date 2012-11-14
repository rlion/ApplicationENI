using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApplicationENI.Modele;
using System.Data.SqlClient;

namespace ApplicationENI.DAL
{
    class PromotionDAL
    {
        static String SELECT_PROMOTIONS = "SELECT * FROM PROMOTION";
       
        
        public static List<Promotion> listePromotions()
        {

            SqlConnection connexion = ConnexionSQL.CreationConnexion();
            SqlCommand cmd = new SqlCommand(SELECT_PROMOTIONS, connexion);
            List<Promotion> listePromotions = new List<Promotion>();

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Promotion p = new Promotion(reader.GetString(reader.GetOrdinal("CodePromotion")), reader.GetString(reader.GetOrdinal("Libelle")));
                listePromotions.Add(p);
            }
            return listePromotions;
        }


        

    }
}
