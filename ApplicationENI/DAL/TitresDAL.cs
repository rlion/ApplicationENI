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

        public static void AjouterTitre(Titre titre)
        {
        }

        public static void ModifierTitre(Titre titre)
        {
        }

        public static void SupprimerTitre(string codeTitre)
        {
            
        }
    }
}
