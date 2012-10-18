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
        //THIS IS IT boubou!
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

        //THIS IS IT boubou!
        public static List<Salle> GetListeSalles() 
        {
            List<Salle> listeSalles = new List<Salle>();

            SqlConnection connexion = ConnexionSQL.CreationConnexion();

            if(connexion != null) {
                string reqSalle = "select CodeSalle, libelle from Salle";

                SqlCommand commande = connexion.CreateCommand();
                commande.CommandText = reqSalle;
                SqlDataReader reader = commande.ExecuteReader();

                while(reader.Read()) {
                    string codS = reader[0] != null ? reader.GetString(0) : string.Empty;
                    string lib = reader[1] != null ? reader.GetString(1) : string.Empty;

                    listeSalles.Add(new Salle(codS, lib));
                }
            }
            return listeSalles;
        }

        private static List<EpreuveTitre> GetEpreuvesTitre(string codeTitre)
        {
            string req = "select CodeSalle, dateEpreuve, CodeTitre from EPREUVETITRE where CodeTitre=@code";

            SqlConnection conn = ConnexionSQL.CreationConnexion();
            SqlCommand commande = conn.CreateCommand();
            commande.CommandText = req;
            commande.Parameters.AddWithValue("@code", codeTitre);

            List<EpreuveTitre> let = new List<EpreuveTitre>();

            SqlDataReader reader = commande.ExecuteReader();
            while (reader.Read())
            {
                EpreuveTitre et = new EpreuveTitre((DateTime)reader[1], (string)reader[0], (string)reader[2]);
                et.ListeJury = GetListeJuryParEpreuve(et.DateEpreuve, et.Salle, et.Titre);
                let.Add(et);
                
            }

            return let;
        }

        //THIS IS IT boubou!
        private static List<Jury> GetListeJuryParEpreuve(DateTime datePassage, string CodeSalle, string CodeTitre)
        {
            string req = "select idJury, civilite, nom, prenom from JURY where idJury in " +
                "(select idJury from EPTITREJURY where dateEpreuve=@date and CodeSalle=@salle and CodeTitre=@titre)";

            SqlConnection conn = ConnexionSQL.CreationConnexion();
            SqlCommand commande = conn.CreateCommand();
            commande.CommandText = req;
            commande.Parameters.AddWithValue("@date", datePassage);
            commande.Parameters.AddWithValue("@salle", CodeSalle);
            commande.Parameters.AddWithValue("@titre", CodeTitre);

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
                    "Ajout Titre", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Stop);
                return -1;
            }
        }

        public static int ModifierTitre(Titre titre)
        {
            try 
            {
                string req = "update Titre set LibelleCourt = @libC, LibelleLong = @libL, DateCreation = @dateC, TitreENI = @titENI, Archiver = @archiv, niveau = @nivo, codeRome = @codeR, codeNSF = @codeN where CodeTitre=@codeT";

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
                System.Windows.MessageBox.Show("La modification de ce titre est impossible : " + e.Message,
                                    "Modification Titre", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Stop);
                return -1;
            }
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

        public static int AjouterEpreuveTitre(EpreuveTitre epTitre)
        {
            try
            {
                string req = "insert into EpreuveTitre (CodeSalle,CodeTitre,dateEpreuve) " +
                             "select @codeS, @codeT, @dateE where not exists "+
                             "(select 0 from EpreuveTitre where CodeSalle=@codeS and CodeTitre=@codeT and dateEpreuve=@dateE)";

                SqlConnection conn = ConnexionSQL.CreationConnexion();
                SqlCommand commande = conn.CreateCommand();
                commande.CommandText = req;
                commande.Parameters.AddWithValue("@codeS", epTitre.Salle);
                commande.Parameters.AddWithValue("@codeT", epTitre.Titre);
                commande.Parameters.AddWithValue("@dateE", epTitre.DateEpreuve);

                return commande.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show("L'ajout de cette épreuve est impossible : " + e.Message,
                    "Ajout Epreuve Titre", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Stop);
                return -1;
            }
        }

        public static int AjouterJuryEpreuveTitre(EpreuveTitre epTitre)
        {
            try
            {
                int i =-2;

                if (epTitre.ListeJury != null && epTitre.ListeJury.Count > 0)
                {
                    foreach (Jury j in epTitre.ListeJury)
                    {

                        string req = "insert into EpTitreJury (idJury,CodeSalle,CodeTitre,dateEpreuve) " +
                 "select @codeS, @codeT, @dateE, @idJury where not exists " +
                 "(select 0 from EpTitreJury where CodeSalle=@codeS and CodeTitre=@codeT and dateEpreuve=@dateE and idJury=@idJury)";

                        SqlConnection conn = ConnexionSQL.CreationConnexion();
                        SqlCommand commande = conn.CreateCommand();
                        commande.CommandText = req;
                        commande.Parameters.AddWithValue("@codeS", epTitre.Salle);
                        commande.Parameters.AddWithValue("@codeT", epTitre.Titre);
                        commande.Parameters.AddWithValue("@dateE", epTitre.DateEpreuve);
                        commande.Parameters.AddWithValue("@idJury", j.IdPersonneJury);

                        i = commande.ExecuteNonQuery();
                    }
                }
                return i;
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show("L'ajout du jury à cette épreuve est impossible : " + e.Message,
                    "Ajout Jury Epreuve Titre", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Stop);
                return -1;
            }
        }

        public static int AjouterJury(Jury jury)
        {
            try
            {
                string req = "insert into Jury (civilite, nom, prenom) " +
                             "select @civ, @nom, @prenom where not exists " +
                             "(select 0 from Jury where civilite=@civ and nom=@nom and prenom=@prenom)";

                SqlConnection conn = ConnexionSQL.CreationConnexion();
                SqlCommand commande = conn.CreateCommand();
                commande.CommandText = req;
                commande.Parameters.AddWithValue("@civ", jury.Civilite);
                commande.Parameters.AddWithValue("@nom", jury.Nom);
                commande.Parameters.AddWithValue("@prenom", jury.Prenom);

                return commande.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show("L'ajout de ce jury est impossible : " + e.Message,
                    "Ajout Jury", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Stop);
                return -1;
            }
        }

        //TODO -> requêtes de suppression...
        public static int SupprimerEpreuveTitre(EpreuveTitre epTitre)
        {
            return 0;
        }

        public static int SupprimerJuryEpreuveTitre(EpreuveTitre epTitre, int idJury)
        {
            try
            {
                SqlConnection conn = ConnexionSQL.CreationConnexion();
                SqlCommand commande = conn.CreateCommand();
                string req = "delete from EpTitreJury where CodeTitre=@codeT and CodeSalle=@codeS and dateEpreuve=@date and idJury=@jury";
                commande.CommandText = req;
                commande.Parameters.AddWithValue("@codeT", epTitre.Titre);
                commande.Parameters.AddWithValue("@codeS", epTitre.Salle);
                commande.Parameters.AddWithValue("@date", epTitre.DateEpreuve);
                commande.Parameters.AddWithValue("@jury", idJury);
                int retour = commande.ExecuteNonQuery();
                if (retour == 1) epTitre.ListeJury.RemoveAt(idJury);
                return retour;
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show("La suppression de ce membre du jury à cette épreuve est impossible :"+e.Message, "Suppression Jury Epreuve Titre",
                    System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Stop);
                return -1;
            }
        }

        public static int SupprimerJury(Jury jury)
        {
            return 0;
        }
    }
}
