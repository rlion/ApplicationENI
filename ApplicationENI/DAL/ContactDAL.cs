using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApplicationENI.Modele;
using System.Data.SqlClient;

namespace ApplicationENI.DAL
{
    class ContactDAL
    {
        // Gestion des contacts assez souple
        static String SELECT_CONTACT_PAR_STAGIAIRE = "SELECT CONTACT.CODECONTACT, Contact.NOM, Contact.PRENOM, Contact.TELFIXE, Contact.TelMobile, Contact.Email, Contact.CodeFonction, Entreprise.RaisonSociale FROM CONTACT, Entreprise WHERE Contact.CodeEntreprise=Entreprise.CodeEntreprise AND Entreprise.CodeEntreprise IN(select CodeEntreprise FROM StagiaireParEntreprise where CodeStagiaire=@num_stagiaire)";
        static String DELETE_CONTACT = "DELETE FROM CONTACT WHERE CodeContact=@codeContact";
        static String UPDATE_CONTACT = "UPDATE CONTACT SET Nom=@nom, Prenom=@prenom, TelFixe=@telFixe, TelMobile=@telPortable, Email=@email WHERE CODECONTACT=@codeContact";
        static String INSERT_CONTACT = "insert into Contact(Nom, Prenom, CodeFonction, CodeImportance, Archive, TelMobile, TelFixe, Email, CodeEntreprise) Values(@nom, @prenom, @codeFonction, 1, 0, @portable, @fixe, @mail, @codeEntreprise)";
        static String INSERT_STAGIAIREPARENTREPRISE = "insert into StagiaireParEntreprise(CodeStagiaire, CodeEntreprise, DateLien, CodeTypeLien, CodeFonction)" +
            "select @codeStagiaire, @codeEntreprise, @dateLien, 'PP', 'AL'" +
            "where not exists (select 0 from StagiaireParEntreprise where CodeEntreprise=@codeEntreprise and CodeStagiaire=@codeStagiaire)";

        static String GET_NUM_CONTACT = "SELECT @@IDENTITY AS IDENT";

        public static List<Contact> rechercherContacts(int pNumStagiaire){

            SqlConnection connexion = ConnexionSQL.CreationConnexion();
            SqlCommand cmd = new SqlCommand(SELECT_CONTACT_PAR_STAGIAIRE, connexion);
            cmd.Parameters.AddWithValue("@num_stagiaire", pNumStagiaire);
            List<Contact> listeContacts = new List<Contact>();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read()) {
                Contact contact = new Contact();
                Entreprise ent = new Entreprise();
                contact._codeContact = reader.GetInt32(reader.GetOrdinal("CODECONTACT"));
                contact._nom = reader.GetSqlString(1).IsNull ? String.Empty : reader.GetString(1);
                contact._prenom = reader.GetSqlString(2).IsNull ? String.Empty : reader.GetString(2);
                contact._telFixe = reader.GetSqlString(3).IsNull ? String.Empty : reader.GetString(3);
                contact._telMobile = reader.GetSqlString(4).IsNull ? String.Empty : reader.GetString(4);
                contact._email = reader.GetSqlString(5).IsNull ? String.Empty : reader.GetString(5);
                contact._codeFonction = reader.GetSqlString(6).IsNull ? String.Empty : reader.GetString(6);
                ent._raisonSociale = reader.GetSqlString(7).IsNull ? String.Empty : reader.GetString(7);
                contact._Entreprise = ent;
                listeContacts.Add(contact);
            }
            connexion.Close();
            return listeContacts;
        }

        public static bool supprimerContact(int pCodeContact) {
            try
            {
                SqlConnection connexion = ConnexionSQL.CreationConnexion();
                SqlCommand cmd = new SqlCommand(DELETE_CONTACT, connexion);
                cmd.Parameters.AddWithValue("@codeContact", pCodeContact);
                cmd.ExecuteReader();
                connexion.Close();
                return true;

            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show("Impossible d'éxécuter la requête, veuillez au préalable supprimer les évènements lier à ce contact. Détail de l'erreur : " + e.Message, "Echec de la requête",
                      System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                return false;
            }
            
        }

        public static void modifierContact(Contact pC)
        {
            
            try
            {
                SqlConnection connexion = ConnexionSQL.CreationConnexion();
            SqlCommand cmd = new SqlCommand(UPDATE_CONTACT, connexion);
            cmd.Parameters.AddWithValue("@nom", pC._nom);
            cmd.Parameters.AddWithValue("@prenom", pC._prenom);
            cmd.Parameters.AddWithValue("@telFixe", pC._telFixe);
            cmd.Parameters.AddWithValue("@telPortable", pC._telMobile);
            cmd.Parameters.AddWithValue("@email", pC._email);
            cmd.Parameters.AddWithValue("@codeContact", pC._codeContact);
            cmd.ExecuteReader();
            connexion.Close();
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show("Impossible d'éxécuter la requête : " + e.Message, "Echec de la requête",
                      System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
            
            
        }

        public static void ajouterContact(Contact pC)
        {
           try
            {
                // ajout du contact en lui même
                SqlConnection connexion = ConnexionSQL.CreationConnexion();
                SqlCommand cmd = new SqlCommand(INSERT_CONTACT, connexion);
                cmd.Parameters.AddWithValue("@nom", pC._nom);
                cmd.Parameters.AddWithValue("@prenom", pC._prenom);
                cmd.Parameters.AddWithValue("@codeFonction", pC._codeFonction);
                cmd.Parameters.AddWithValue("@portable", pC._telFixe);
                cmd.Parameters.AddWithValue("@fixe", pC._telMobile);
                cmd.Parameters.AddWithValue("@mail", pC._email);
                cmd.Parameters.AddWithValue("@codeEntreprise", pC._Entreprise._codeEntreprise);
                cmd.ExecuteNonQuery();

                // maintenant il faut mettre à jour l'objet Contact en lui assignant son numéro
                SqlCommand cmd2 = new SqlCommand(GET_NUM_CONTACT, connexion);
                int idDernierContact = Convert.ToInt32(cmd2.ExecuteScalar());
                pC._codeContact = idDernierContact;

                // maintenant, on fait la liason entre la nouvelle entreprise et le stagiaire courant, si ce n'est pas déjà fait.
                SqlCommand cmd3 = new SqlCommand(INSERT_STAGIAIREPARENTREPRISE, connexion);
                cmd3.Parameters.AddWithValue("@codeStagiaire", Parametres.Instance.stagiaire._id);
                cmd3.Parameters.AddWithValue("@dateLien", DateTime.Now);
                cmd3.Parameters.AddWithValue("@codeEntreprise", pC._Entreprise._codeEntreprise);
                cmd3.ExecuteNonQuery();

                connexion.Close();
           }
            catch (Exception)
            {
                System.Windows.MessageBox.Show("Ce contact ne peut être ajouté.",
                    "Ajout Contact impossible", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Stop);
            }

        }


    }
}
