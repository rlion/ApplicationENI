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

        #region GestionPassageTitre

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
                    string codT = !reader.IsDBNull(0) ? reader.GetString(0) : string.Empty;
                    string libC = !reader.IsDBNull(1) ? reader.GetString(1) : string.Empty;
                    string libL = !reader.IsDBNull(2) ? reader.GetString(2) : string.Empty;
                    string niv = !reader.IsDBNull(3) ? reader.GetString(3) : string.Empty;
                    string codR = !reader.IsDBNull(4) ? reader.GetString(4) : string.Empty;
                    string codN = !reader.IsDBNull(5) ? reader.GetString(5) : string.Empty;
                    DateTime dateC = !reader.IsDBNull(6) ? reader.GetDateTime(6) : new DateTime();
                    DateTime dateM = new DateTime(); //dans BDD, donnée de type Byte... inutilisable
                    bool titreENI = !reader.IsDBNull(8) ? reader.GetBoolean(8) : false;
                    bool archiver = !reader.IsDBNull(9) ? reader.GetBoolean(9) : false;

                    listTitres.Add(new Titre(codT, libC, libL, niv, codR, codN, dateC, dateM, titreENI, archiver, GetEpreuvesTitre(codT)));
                }

                connexion.Close();
            }

            //Récupération jeu de données pour test
            //listTitres = JeuDonnees.GetTitres();

            return listTitres;
        }

        public static List<Salle> GetListeSalles() 
        {
            List<Salle> listeSalles = new List<Salle>();

            SqlConnection connexion = ConnexionSQL.CreationConnexion();

            if(connexion != null) 
            {
                string reqSalle = "select CodeSalle, libelle from Salle";

                SqlCommand commande = connexion.CreateCommand();
                commande.CommandText = reqSalle;
                SqlDataReader reader = commande.ExecuteReader();

                while(reader.Read()) {
                    string codS = reader[0] != null ? reader.GetString(0) : string.Empty;
                    string lib = reader[1] != null ? reader.GetString(1) : string.Empty;

                    listeSalles.Add(new Salle(codS, lib));
                }

                connexion.Close();
            }
            return listeSalles;
        }

        private static List<EpreuveTitre> GetEpreuvesTitre(string codeTitre)
        {
            string req = "select CodeSalle, dateEpreuve, CodeTitre from EPREUVETITRE where CodeTitre=@code";

            SqlConnection conn = ConnexionSQL.CreationConnexion();
            if(conn != null)
            {

                SqlCommand commande = conn.CreateCommand();
                commande.CommandText = req;
                commande.Parameters.AddWithValue("@code", codeTitre);

                List<EpreuveTitre> let = new List<EpreuveTitre>();

                SqlDataReader reader = commande.ExecuteReader();
                while(reader.Read())
                {
                    EpreuveTitre et = new EpreuveTitre((DateTime)reader[1], (string)reader[0], (string)reader[2]);
                    et.ListeJury = GetListeJuryParEpreuve(et.DateEpreuve, et.Salle, et.Titre);
                    let.Add(et);

                }
                conn.Close();

                return let;
            }
            else return null;
        }

        private static List<Jury> GetListeJuryParEpreuve(DateTime datePassage, string CodeSalle, string CodeTitre)
        {
            string req = "select idJury, civilite, nom, prenom from JURY where idJury in " +
                "(select idJury from EPTITREJURY where dateEpreuve=@date and CodeSalle=@salle and CodeTitre=@titre)";

            SqlConnection conn = ConnexionSQL.CreationConnexion();
            if(conn != null)
            {

                SqlCommand commande = conn.CreateCommand();
                commande.CommandText = req;
                commande.Parameters.AddWithValue("@date", datePassage);
                commande.Parameters.AddWithValue("@salle", CodeSalle);
                commande.Parameters.AddWithValue("@titre", CodeTitre);

                List<Jury> lj = new List<Jury>();

                SqlDataReader reader = commande.ExecuteReader();
                while(reader.Read())
                {
                    lj.Add(new Jury(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3)));
                }
                conn.Close();

                return lj;
            }
            else return null;
        }

        public static List<Jury> GetListeJury()
        {
            string req = "select idJury, civilite, nom, prenom from JURY";

            SqlConnection conn = ConnexionSQL.CreationConnexion();
            if(conn != null)
            {
                SqlCommand commande = conn.CreateCommand();
                commande.CommandText = req;

                List<Jury> lj = new List<Jury>();

                SqlDataReader reader = commande.ExecuteReader();
                while(reader.Read())
                {
                    lj.Add(new Jury(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3)));
                }
                conn.Close();

                return lj;
            }
            else return null;
        }

        private static int GetJuryId(string civilite, string nom, string prenom)
        {
            string req = "select idJury from JURY where civilite=@civ and nom=@nom and prenom=@prenom";

            SqlConnection conn = ConnexionSQL.CreationConnexion();
            SqlCommand commande = conn.CreateCommand();
            commande.CommandText = req;

            commande.Parameters.AddWithValue("@civ", civilite);
            commande.Parameters.AddWithValue("@nom", nom);
            commande.Parameters.AddWithValue("@prenom", prenom);

            int? i = (int)commande.ExecuteScalar();
            conn.Close();

            return i ?? -1;
        }

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

                int retour = commande.ExecuteNonQuery();
                conn.Close();

                return retour;
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

                int retour = commande.ExecuteNonQuery();
                conn.Close();
                return retour;
            } 
            catch(Exception e) 
            {
                System.Windows.MessageBox.Show("La modification de ce titre est impossible : " + e.Message,
                                    "Modification Titre", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Stop);
                return -1;
            }
        }

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

                int retour = commande.ExecuteNonQuery();
                conn.Close();
                return retour;
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

                int retour = commande.ExecuteNonQuery();
                conn.Close();
                return retour;
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
                 "select @idJury, @codeS, @codeT, @dateE where not exists " +
                 "(select 0 from EpTitreJury where CodeSalle=@codeS and CodeTitre=@codeT and dateEpreuve=@dateE and idJury=@idJury)";

                        SqlConnection conn = ConnexionSQL.CreationConnexion();
                        SqlCommand commande = conn.CreateCommand();
                        commande.CommandText = req;
                        commande.Parameters.AddWithValue("@codeS", epTitre.Salle);
                        commande.Parameters.AddWithValue("@codeT", epTitre.Titre);
                        commande.Parameters.AddWithValue("@dateE", epTitre.DateEpreuve);
                        commande.Parameters.AddWithValue("@idJury", j.IdPersonneJury);

                        i = commande.ExecuteNonQuery();
                        conn.Close();
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

                int retour = commande.ExecuteNonQuery();
                conn.Close();
                return retour;
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show("L'ajout de ce jury est impossible : " + e.Message,
                    "Ajout Jury", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Stop);
                return -1;
            }
        }

        public static int SupprimerEpreuveTitre(EpreuveTitre epTitre)
        {
            try
            {
                SqlConnection conn = ConnexionSQL.CreationConnexion();
                SqlCommand commande = conn.CreateCommand();
                string req = "delete from EpreuveTitre where CodeTitre=@codeT and CodeSalle=@codeS and dateEpreuve=@date";
                commande.CommandText = req;
                commande.Parameters.AddWithValue("@codeT", epTitre.Titre);
                commande.Parameters.AddWithValue("@codeS", epTitre.Salle);
                commande.Parameters.AddWithValue("@date", epTitre.DateEpreuve);
                
                int retour = commande.ExecuteNonQuery();
                conn.Close();
                return retour;
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show("La suppression de cette épreuve au titre est impossible :" + e.Message, "Suppression Epreuve Titre",
                    System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Stop);
                return -1;
            }
        }

        public static int SupprimerJuryEpreuveTitre(EpreuveTitre epTitre)
        {
            try
            {
                SqlConnection conn = ConnexionSQL.CreationConnexion();
                SqlCommand commande = conn.CreateCommand();
                string req = "delete from EpTitreJury where CodeTitre=@codeT and CodeSalle=@codeS and dateEpreuve=@date";
                commande.CommandText = req;
                commande.Parameters.AddWithValue("@codeT", epTitre.Titre);
                commande.Parameters.AddWithValue("@codeS", epTitre.Salle);
                commande.Parameters.AddWithValue("@date", epTitre.DateEpreuve);
                
                int retour = commande.ExecuteNonQuery();
                conn.Close();
                return retour;
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show("La suppression de ce jury à cette épreuve est impossible :"+e.Message, "Suppression Jury Epreuve Titre",
                    System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Stop);
                return -1;
            }
        }

        private static void SupprimerJuryEpreuveTitre(int idJury)
        {
            try
            {
                SqlConnection conn = ConnexionSQL.CreationConnexion();
                SqlCommand commande = conn.CreateCommand();
                string req = "delete from EpTitreJury where idJury=@id";
                commande.CommandText = req;
                commande.Parameters.AddWithValue("@id", idJury);
                
                int retour = commande.ExecuteNonQuery();
                conn.Close();
            }
            catch(Exception ex)
            {
                System.Windows.MessageBox.Show("La suppression des épreuves pour ce membre du jury est impossible :" + ex.Message, "Suppression Jury",
                    System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Stop);
                throw;
            }
        }

        public static int SupprimerJury(int idJury)
        {
            try
            {
                SupprimerJuryEpreuveTitre(idJury);

                SqlConnection conn = ConnexionSQL.CreationConnexion();
                SqlCommand commande = conn.CreateCommand();
                string req = "delete from Jury where idJury=@jury";
                commande.CommandText = req;
                commande.Parameters.AddWithValue("@jury", idJury);
                
                int retour = commande.ExecuteNonQuery();
                conn.Close();
                return retour;
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show("La suppression de ce membre du jury est impossible :" + e.Message, "Suppression Jury",
                    System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Stop);
                return -1;
            }
        }

        #endregion

        #region InscriptionTitre

        public static PassageTitre GetPassageTitre(int codeStagiaire, string codeTitre)
        {
            try
            {
                SqlConnection connexion = ConnexionSQL.CreationConnexion();
                
                if (connexion != null)
                {
                    string reqPassTitre = "select datePassage, estObtenu, estValide " +
                    "from PASSAGETITRE where CodeStagiaire = @codeStagiaire and CodeTitre = @codeTitre";

                    SqlCommand commande = connexion.CreateCommand();
                    commande.CommandText = reqPassTitre;
                    commande.Parameters.AddWithValue("@codeStagiaire", codeStagiaire);
                    commande.Parameters.AddWithValue("@codeTitre", codeTitre);
                    SqlDataReader reader = commande.ExecuteReader();

                    PassageTitre pt = new PassageTitre();

                    while (reader.Read())
                    {
                        DateTime date = reader.GetDateTime(0);
                        bool obtenu = !reader.IsDBNull(1) ? reader.GetBoolean(1) : false;
                        bool valide = !reader.IsDBNull(2) ? reader.GetBoolean(2) : false;

                        pt = new PassageTitre(codeTitre, codeStagiaire, date, obtenu, valide);
                    }
                    connexion.Close();

                    return pt;
                }

                return null;
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show("Impossible de récupérer les informations sur son passage au titre : " + e.Message,
                    "Inscription Titre", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Stop);
                return null;
            }
        }

        /// <summary>
        /// Cette méthode récupère le code et le libellé du Titre.
        /// </summary>
        /// <param name="code">Attend le codeStagiaire (int) ou le codeTitre (string)</param>
        /// <returns>le champ CodeTitre (key), et le champ libelleCourt (value) de la table Titre</returns>
        public static KeyValuePair<string, string>? GetInfosTitre(object code)
        {
            try
            {
                SqlConnection connexion = ConnexionSQL.CreationConnexion();

                if (connexion != null && (code.GetType() == typeof(string) || code.GetType() == typeof(int)))
                {
                    SqlCommand commande = connexion.CreateCommand();

                    //type string -> on a le codeStagiaire
                    if (code.GetType() == typeof(int))
                    {
                        string reqInfoT = "select t.CodeTitre, t.LibelleCourt from Titre t, Formation f, PlanningIndividuelFormation p " +
                            "where p.CodeStagiaire = @codeStagiaire and p.CodeFormation = f.CodeFormation " +
                            "and f.CodeTitre = t.CodeTitre";
                        
                        commande.CommandText = reqInfoT;
                        commande.Parameters.AddWithValue("@codeStagiaire", (int)code);

                        SqlDataReader reader = commande.ExecuteReader();

                        while (reader.Read())
                            return new KeyValuePair<string, string>(reader.GetString(0), reader.GetString(1));
                    }
                    //type int -> on a le codeTitre
                    else
                    {
                        string reqInfoT = "select LibelleCourt from Titre where CodeTitre=@codeTitre";

                        commande.CommandText = reqInfoT;
                        commande.Parameters.AddWithValue("@codeTitre", (string)code);

                        string value = (string)commande.ExecuteScalar();
                        return new KeyValuePair<string, string>((string)code, value);
                    }
                }

                return null;
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show("Impossible de récupérer le titre correspondant à cette formation : " + e.Message,
                    "Inscription Titre", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Stop);
                return null;
            }
        }

        public static int AjouterPassageTitre(PassageTitre passageT)
        {
            try
            {
                int checkEtat = ControlerSiInscrit(passageT.CodeStagiaire, passageT.CodeTitre);
                int retour = -1;

                if(checkEtat == 0)
                {
                    string req = "insert into PassageTitre (CodeTitre,CodeStagiaire,datePassage,estObtenu,estValide) " +
                                 "select @codeT, @codeS, @dateP, @isO, @isV where not exists " +
                                 "(select 0 from PassageTitre where CodeStagiaire=@codeS and CodeTitre=@codeT)";

                    SqlConnection conn = ConnexionSQL.CreationConnexion();
                    SqlCommand commande = conn.CreateCommand();
                    commande.CommandText = req;
                    commande.Parameters.AddWithValue("@codeT", passageT.CodeTitre);
                    commande.Parameters.AddWithValue("@codeS", passageT.CodeStagiaire);
                    commande.Parameters.AddWithValue("@dateP", passageT.DatePassage);
                    commande.Parameters.AddWithValue("@isO", passageT.EstObtenu);
                    commande.Parameters.AddWithValue("@isV", passageT.EstValide);

                    retour = commande.ExecuteNonQuery();
                    conn.Close();
                }
                else if(checkEtat == 1)
                {
                    string req = "update PassageTitre set estValide=@isV where CodeStagiaire=@codeS and CodeTitre=@codeT";
                    SqlConnection conn = ConnexionSQL.CreationConnexion();
                    SqlCommand commande = conn.CreateCommand();
                    commande.CommandText = req;
                    commande.Parameters.AddWithValue("@codeT", passageT.CodeTitre);
                    commande.Parameters.AddWithValue("@codeS", passageT.CodeStagiaire);
                    commande.Parameters.AddWithValue("@isV", passageT.EstValide);

                    retour = commande.ExecuteNonQuery();
                    conn.Close();
                }
                return retour;
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show("Impossible d'inscrire le stagiaire au titre : " + e.Message,
                    "Inscription Titre", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Stop);
                return -2;
            }
        }

        public static int UpdatePassageTitre(PassageTitre passageT)
        {
            try
            {
                string req = "update PassageTitre set datePassage=@dateP, estObtenu=@isO, estValide=@isV"+
                    " where codeTitre=@codeT and codeStagiaire=@codeS";

                SqlConnection conn = ConnexionSQL.CreationConnexion();
                SqlCommand commande = conn.CreateCommand();
                commande.CommandText = req;
                commande.Parameters.AddWithValue("@codeT", passageT.CodeTitre);
                commande.Parameters.AddWithValue("@codeS", passageT.CodeStagiaire);
                commande.Parameters.AddWithValue("@dateP", passageT.DatePassage);
                commande.Parameters.AddWithValue("@isO", passageT.EstObtenu);
                commande.Parameters.AddWithValue("@isV", passageT.EstValide);

                int retour = commande.ExecuteNonQuery();
                conn.Close();
                return retour;
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show("Impossible de mettre à jour l'inscription du stagiaire au titre : " + e.Message,
                    "Inscription Titre", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Stop);
                return -2;
            }
        }

        //Méthode qui permet de savoir si le stagiaire est inscrit à un titre ou non
        //return : 0 -> non inscrit, 1 -> inscrit mais pas valide, 2 -> inscrit et valide mais pas obtenu, 
        // 3 -> inscrit, valide et obtenu
        public static int ControlerSiInscrit(int codeStagiaire, string codeTitre)
        {
            try
            {
                SqlConnection connexion = ConnexionSQL.CreationConnexion();
                int retour = -1;

                if (connexion != null)
                {
                    string reqInscrit = "select estObtenu from PASSAGETITRE where CodeStagiaire=@codeS and CodeTitre=@codeT";

                    SqlCommand commande = connexion.CreateCommand();
                    commande.CommandText = reqInscrit;
                    commande.Parameters.AddWithValue("@codeS", codeStagiaire);
                    commande.Parameters.AddWithValue("@codeT", codeTitre);

                    bool? b = (bool?)commande.ExecuteScalar();

                    if (b.HasValue)
                    {
                        if(b.Value) retour = 3;
                        else
                        {
                            b = false;
                            string reqValide = "select estValide from PASSAGETITRE where CodeStagiaire=@codeS and CodeTitre=@codeT";
                            commande.CommandText = reqValide;
                            b = (bool?)commande.ExecuteScalar();
                            if(b.HasValue && b.Value) retour = 2;
                            else retour = 1;
                        } 
                    }
                    else retour = 0;

                    connexion.Close();
                }

                return retour;
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show("Impossible de vérifier si le stagiaire est inscrit au titre : " + e.Message,
                    "Inscription Titre", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Stop);
                return -2;
            }
        }

        public static string GetFormationStagiaire(int codeStagiaire)
        {
            try
            {
                SqlConnection connexion = ConnexionSQL.CreationConnexion();

                if (connexion != null)
                {
                    SqlCommand commande = connexion.CreateCommand();

                    string reqInfoF = "select f.LibelleLong from Formation f, PlanningIndividuelFormation p " +
                        "where p.CodeStagiaire = @codeStagiaire and p.CodeFormation = f.CodeFormation ";

                    commande.CommandText = reqInfoF;
                    commande.Parameters.AddWithValue("@codeStagiaire", codeStagiaire);

                    string s = (string)commande.ExecuteScalar();
                    connexion.Close();
                    return s;
                }

                return string.Empty;
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show("Impossible de récupérer la formation de ce stagiaire : " + e.Message,
                    "Inscription Titre", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Stop);
                return string.Empty;
            }
        }

        public static List<DateTime> GetListeDateEpreuveTiTre(string codeTitre)
        {
            try
            {
                List<DateTime> ld = new List<DateTime>();
                SqlConnection connexion = ConnexionSQL.CreationConnexion();

                if (connexion != null)
                {
                    SqlCommand commande = connexion.CreateCommand();
                    string req = "select distinct(dateEpreuve) from EPREUVETITRE where CodeTitre=@code and dateEpreuve>=GETDATE()";
                    commande.CommandText = req;
                    commande.Parameters.AddWithValue("@code", codeTitre);
                    SqlDataReader reader = commande.ExecuteReader();
                    while (reader.Read()) ld.Add(reader.GetDateTime(0));

                    connexion.Close();
                }
                return ld;
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show("Impossible de récupérer les dates planifiées : " + e.Message,
                    "Inscription Titre", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Stop);
                return null;
            }
        }

        #endregion
    }
}
