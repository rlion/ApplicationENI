using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApplicationENI.Modele;
using System.Data.SqlClient;

namespace ApplicationENI.DAL
{
    public class TitresDAL
    {

        public static List<Titre> GetListeTitres()
        {
            List<Titre> listTitres = new List<Titre>();

            SqlConnection connexion = ConnexionSQL.CreationConnexion();

            if (connexion != null)
            {
                string reqTitre = "select CodeTitre, LibelleCourt, LibelleLong, niveau, "+
                "codeRome, codeNSF, DateCreation, DateModif, TitreENI, Archiver from Titre";

                SqlCommand commande = connexion.CreateCommand();
                commande.CommandText = reqTitre;
                SqlDataReader reader = commande.ExecuteReader();

                while (reader.Read())
                {
                    string codT = reader[0] != null ? reader.GetString(0) : string.Empty;
                    string libC = reader[1] != null ? reader.GetString(1) : string.Empty;
                    string libL = reader[2] != null ? reader.GetString(2) : string.Empty;
                    string niv = reader[3] != null ? reader.GetString(3) : string.Empty;
                    string codR = reader[4] != null ? reader.GetString(4) : string.Empty;
                    string codN = reader[5] != null ? reader.GetString(5) : string.Empty;
                    DateTime dateC = reader[6] != null ? reader.GetDateTime(6) : new DateTime();
                    DateTime dateM = new DateTime(); //dans BDD, donnée de type Byte... inutilisable
                    bool titreENI = reader[8] != null ? reader.GetBoolean(8) : false;
                    bool archiver = reader[9] != null ? reader.GetBoolean(9) : false;

                    listTitres.Add(new Titre(codT, libC, libL, niv, codR, codN, dateC, dateM, titreENI, archiver, GetEpreuvesTitre(codT)));
                }
            }

            //Récupération jeu de données pour test
            //listTitres = JeuDonnees.GetTitres();

            return listTitres;
        }

        private static List<EpreuveTitre> GetEpreuvesTitre(string codeTitre)
        {
            string req = "select CodeSalle, dateEpreuve from EPREUVETITRE where CodeTitre=@code";

            SqlConnection conn = ConnexionSQL.CreationConnexion();
            SqlCommand commande = conn.CreateCommand();
            commande.CommandText = req;
            commande.Parameters.AddWithValue("@code", codeTitre);

            List<EpreuveTitre> let = new List<EpreuveTitre>();

            SqlDataReader reader = commande.ExecuteReader();
            while (reader.Read())
            {
                let.Add(new EpreuveTitre((DateTime)reader[1],(string)reader[0],GetJury(reader.GetDateTime(1))));
            }

            return let;
        }

        private static List<Jury> GetJury(DateTime datePassage)
        {
            string req = "select idJury, civilite, nom, prenom from JURY where idJury in " +
                "(select idJury from EPTITREJURY where dateEpreuve=@date)";

            SqlConnection conn = ConnexionSQL.CreationConnexion();
            SqlCommand commande = conn.CreateCommand();
            commande.CommandText = req;
            commande.Parameters.AddWithValue("@date", datePassage);

            List<Jury> lj = new List<Jury>();

            SqlDataReader reader = commande.ExecuteReader();
            while (reader.Read())
            {
                lj.Add(new Jury(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3)));
            }

            return lj;
        }

        //THIS IS IT boubou!
        public static int AjouterTitre(Titre titre)
        {
            try 
            {
                string req = "insert into Titre (CodeTitre,LibelleCourt,LibelleLong,DateCreation,TitreENI,Archiver,niveau,codeRome,codeNSF) " +
    "select @codeT, @libC, @libL, @dateC, @titENI, @archiv, @nivo, @codeR, @codeN " +
             "where not exists (select 0 from Titre where CodeTitre=@codeT)";

                SqlConnection conn = ConnexionSQL.CreationConnexion();
                SqlCommand commande = conn.CreateCommand();
                commande.CommandText = req;
                commande.Parameters.AddWithValue("@codeT", titre.CodeTitre);
                commande.Parameters.AddWithValue("@libC", titre.LibelleCourt);
                commande.Parameters.AddWithValue("@libL", titre.LibelleLong ?? string.Empty);
                commande.Parameters.AddWithValue("@dateC", titre.DateCreation);
                commande.Parameters.AddWithValue("@titENI", titre.TitreENI);
                commande.Parameters.AddWithValue("@archiv", titre.Archiver);
                commande.Parameters.AddWithValue("@nivo", titre.Niveau ?? string.Empty);
                commande.Parameters.AddWithValue("@codeR", titre.CodeRome ?? string.Empty);
                commande.Parameters.AddWithValue("@codeN", titre.CodeNSF ?? string.Empty);

                return commande.ExecuteNonQuery();
            } 
            catch(Exception e) 
            {
                System.Windows.MessageBox.Show("L'ajout de ce titre est impossible : " + e.Message, 
                    "Suppression Titre", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Stop);
                return -1;
            }

        }

        public static void ModifierTitre(Titre titre)
        {
        }

        //THIS IS IT boubou!
        public static int SupprimerTitre(string codeTitre)
        {
            // Supprimer le titre en catchant si la suppression est impossible
            try 
            {              
                SqlConnection conn = ConnexionSQL.CreationConnexion();
                SqlCommand commande = conn.CreateCommand();
                string req = "delete from Titre where CodeTitre=@codeTitre";
                commande.CommandText = req;
                commande.Parameters.AddWithValue("@codeTitre", codeTitre);
                return commande.ExecuteNonQuery();
            } 
            catch(Exception) 
            {
                System.Windows.MessageBox.Show("La suppression de ce titre est impossible : d'autres informations dépendent de celui-ci.", "Suppression Titre", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Stop);
                return -1;
            }
        }
    }
}
