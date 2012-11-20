using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApplicationENI.Modele;
using System.Data.SqlClient;

namespace ApplicationENI.DAL
{
    class EntrepriseDAL
    {
        static String SELECT_LISTE_ENTREPRISES = "SELECT * FROM ENTREPRISE ORDER BY RAISONSOCIALE";
        //TODO: du coup revoir comment est gérée l'entreprise propre au contact...
        public static List<Entreprise> getListeEntreprises()
        {

            SqlConnection connexion = ConnexionSQL.CreationConnexion();
            SqlCommand cmd = new SqlCommand(SELECT_LISTE_ENTREPRISES, connexion);
            List<Entreprise> listeEntreprises = new List<Entreprise>();
            try
            {
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Entreprise eTemp = new Entreprise();
                    eTemp._codeEntreprise = reader.GetInt32(reader.GetOrdinal("CodeEntreprise"));
                    eTemp._codePostal = reader.GetSqlString(5).IsNull ? String.Empty : reader.GetString(5);
                    eTemp._mail = reader.GetSqlString(10).IsNull ? String.Empty : reader.GetString(10);
                    eTemp._raisonSociale = reader.GetSqlString(1).IsNull ? String.Empty : reader.GetString(1);
                    eTemp._tel = reader.GetSqlString(7).IsNull ? String.Empty : reader.GetString(7);
                    eTemp._ville = reader.GetSqlString(8).IsNull ? String.Empty : reader.GetString(8);
                    listeEntreprises.Add(eTemp);
                }
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show("Impossible d'éxécuter la requête : " + e.Message, "Echec de la requête",
                      System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                return null;
            }

            return listeEntreprises;
        }

    }
}
