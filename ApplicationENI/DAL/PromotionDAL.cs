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
        static String SELECT_LISTE_STAGIAIRES_PAR_PROMO = "SELECT * FROM PlanningIndividuelFormation pif, Stagiaire s, Promotion p where pif.CodeStagiaire=s.CodeStagiaire AND pif.CodePromotion=p.CodePromotion and p.CodePromotion=@codePromo";
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


        public static List<Stagiaire> listeStagiaires(String pNomPromo)
        {

            SqlConnection connexion = ConnexionSQL.CreationConnexion();
            SqlCommand cmd = new SqlCommand(SELECT_LISTE_STAGIAIRES_PAR_PROMO, connexion);

            cmd.Parameters.AddWithValue("@codePromo", pNomPromo);
            List<Stagiaire> listeStagiaires = new List<Stagiaire>();

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Stagiaire s = new Stagiaire();
                //s._adresse1 = reader.IsDBNull(13) ? reader.GetString(13) : string.Empty;
                s._id = reader.GetInt32(reader.GetOrdinal("CodeStagiaire"));
                s._civilité = reader.GetSqlString(10).IsNull ? string.Empty : reader.GetString(10);
                s._nom = reader.GetSqlString(11).IsNull ? string.Empty : reader.GetString(11);
                s._prenom = reader.GetSqlString(12).IsNull ? string.Empty : reader.GetString(12);
                s._adresse1 = reader.GetSqlString(13).IsNull ? string.Empty : reader.GetString(13);
                s._adresse2 = reader.GetSqlString(14).IsNull ? string.Empty : reader.GetString(14) ;
                s._adresse3 = reader.GetSqlString(15).IsNull ? string.Empty : reader.GetString(15);
                s._cp = reader.GetSqlString(16).IsNull ? string.Empty : reader.GetString(16);
                s._ville = reader.GetSqlString(17).IsNull ? string.Empty : reader.GetString(17);
                s._telephoneFixe = reader.GetSqlString(18).IsNull ? string.Empty : reader.GetString(18);
                s._telephonePortable = reader.GetSqlString(19).IsNull ? string.Empty : reader.GetString(19);
                s._email = reader.GetSqlString(20).IsNull ? string.Empty : reader.GetString(20);
                if(!reader.GetSqlDateTime(21).IsNull){s._dateNaissance = reader.GetDateTime(21);}
                s._codeRegion = reader.GetSqlString(22).IsNull ? string.Empty : reader.GetString(22);
                s._codeNationalité = reader.GetSqlString(23).IsNull ? string.Empty : reader.GetString(23);
                s._codeOrigineMedia = reader.GetSqlString(24).IsNull ? string.Empty : reader.GetString(24);
                if(!reader.GetSqlDateTime(25).IsNull){s._datePremierEnvoiDoc = reader.GetDateTime(25);}
                if(!reader.GetSqlDateTime(26).IsNull){s._dateCreation = reader.GetDateTime(26);}
                s._repertoire = reader.GetSqlString(27).IsNull ? string.Empty : reader.GetString(27);
                if(reader.GetBoolean(28)){s._permis = reader.GetBoolean(28);}
                s._photo = reader.GetSqlString(29).IsNull ? string.Empty : reader.GetString(29);
                if(reader.GetBoolean(30)){s._envoiDocEnCours = reader.GetBoolean(30);}
                s._historique = reader.GetSqlString(31).IsNull ? string.Empty : reader.GetString(31);

                listeStagiaires.Add(s);
            }
            return listeStagiaires;
        }

    }
}
