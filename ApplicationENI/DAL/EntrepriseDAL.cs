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
        static String INSERT_ENTREPRISE = "INSERT INTO ENTREPRISE (RAISONSOCIALE, CODEPOSTAL, VILLE, TELEPHONE, EMAIL) VALUES(@raisonSociale, @cp, @ville, @tel, @mail)";
        static String GET_NUM_ENTREPRISE = "SELECT @@IDENTITY AS IDENT";
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

        //    @raisonSociale, @cp, @ville, @tel, @mail
        public static void ajouterEntreprise(Entreprise pE)
        {
            //try
            //{
                SqlConnection connexion = ConnexionSQL.CreationConnexion();
                SqlCommand cmd = new SqlCommand(INSERT_ENTREPRISE, connexion);
                cmd.Parameters.AddWithValue("@raisonSociale", pE._raisonSociale);
                cmd.Parameters.AddWithValue("@cp", pE._codePostal);
                cmd.Parameters.AddWithValue("@ville", pE._ville);
                cmd.Parameters.AddWithValue("@tel", pE._tel);
                cmd.Parameters.AddWithValue("@mail", pE._mail);
                cmd.ExecuteNonQuery();

                // maintenant il faut mettre à jour l'objet Absence en lui assignant son numéro
                SqlCommand cmd2 = new SqlCommand(GET_NUM_ENTREPRISE, connexion);
                int idDerniereEntreprise = Convert.ToInt32(cmd2.ExecuteScalar());
                pE._codeEntreprise = idDerniereEntreprise;
                connexion.Close();
           /* }
            catch (Exception)
            {
                System.Windows.MessageBox.Show("Cette entreprise ne peut être ajoutée.",
                    "Ajout Entreprise impossible", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Stop);
            }*/

        }
    }
}
