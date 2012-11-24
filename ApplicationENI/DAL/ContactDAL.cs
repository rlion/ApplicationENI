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
        static String SELECT_CONTACT_PAR_STAGIAIRE = "SELECT CONTACT.CODECONTACT, Contact.NOM, Contact.PRENOM, Contact.TELFIXE, Contact.TelMobile, Contact.Email, Entreprise.CodeEntreprise FROM CONTACT, Fonction, StagiaireParEntreprise, Entreprise WHERE Contact.CodeFonction = Fonction.CodeFonction AND StagiaireParEntreprise.CodeEntreprise=Entreprise.CodeEntreprise AND StagiaireParEntreprise.CodeStagiaire=@num_stagiaire";
        static String DELETE_CONTACT = "DELETE FROM CONTACT WHERE CodeContact=@codeContact";
        static String UPDATE_CONTACT = "UPDATE CONTACT SET Nom=@nom, Prenom=@prenom, TelFixe=@telFixe, TelMobile=@telPortable, Email=@email WHERE CODECONTACT=@codeContact";
        static String INSERT_CONTACT = "insert into Contact(Nom, Prenom, CodeFonction, CodeImportance, Archive, TelMobile, TelFixe, Fax, Email, CodeEntreprise) Values(@nom, @prenom, @codeFonction, 1, 0, @portable, @fixe, @fax, @mail, @codeEntreprise)";
        static String GET_NUM_CONTACT = "SELECT @@IDENTITY AS IDENT";

        public static List<Contact> rechercherContacts(int pNumStagiaire){

            SqlConnection connexion = ConnexionSQL.CreationConnexion();
            SqlCommand cmd = new SqlCommand(SELECT_CONTACT_PAR_STAGIAIRE, connexion);
            cmd.Parameters.AddWithValue("@num_stagiaire", pNumStagiaire);
            List<Contact> listeContacts = new List<Contact>();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read()) {
                //TODO:vérification null
                Contact contact = new Contact();
                contact._codeEntreprise = reader.GetInt32(reader.GetOrdinal("CodeEntreprise"));
                contact._codeContact = reader.GetInt32(reader.GetOrdinal("CODECONTACT"));
                contact._email = reader.GetString(reader.GetOrdinal("Email"));
                contact._nom = reader.GetString(reader.GetOrdinal("NOM"));
                contact._prenom = reader.GetString(reader.GetOrdinal("PRENOM"));
                contact._telFixe = reader.GetString(reader.GetOrdinal("TELFIXE"));
                contact._telMobile = reader.GetString(reader.GetOrdinal("TelMobile"));
                listeContacts.Add(contact);
            }
            return listeContacts;
        }

        public static void supprimerContact(int pCodeContact) {
            try
            {
                SqlConnection connexion = ConnexionSQL.CreationConnexion();
                SqlCommand cmd = new SqlCommand(DELETE_CONTACT, connexion);
                cmd.Parameters.AddWithValue("@codeContact", pCodeContact);
                cmd.ExecuteReader();

            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show("Impossible d'éxécuter la requête : " + e.Message, "Echec de la requête",
                      System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
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
            //try
            //{

                //  @nom, @prenom, @codeFonction, 1, 0, @portable, @fixe, @fax, @mail, @codeEntreprise
                SqlConnection connexion = ConnexionSQL.CreationConnexion();
                SqlCommand cmd = new SqlCommand(INSERT_CONTACT, connexion);
                cmd.Parameters.AddWithValue("@nom", pC._nom);
                cmd.Parameters.AddWithValue("@prenom", pC._prenom);
                cmd.Parameters.AddWithValue("@codeFonction", pC._codeFonction);
                cmd.Parameters.AddWithValue("@portable", pC._telFixe);
                cmd.Parameters.AddWithValue("@fixe", pC._telMobile);
                cmd.Parameters.AddWithValue("@fax", pC._fax);
                cmd.Parameters.AddWithValue("@mail", pC._email);
                cmd.Parameters.AddWithValue("@codeEntreprise", pC._codeEntreprise);
                cmd.ExecuteNonQuery();

                // maintenant il faut mettre à jour l'objet Absence en lui assignant son numéro
                SqlCommand cmd2 = new SqlCommand(GET_NUM_CONTACT, connexion);
                int idDernierContact = Convert.ToInt32(cmd2.ExecuteScalar());
                pC._codeContact = idDernierContact;
                connexion.Close();
            /*}
            catch (Exception)
            {
                System.Windows.MessageBox.Show("Ce contact ne peut être ajouté.",
                    "Ajout Contact impossible", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Stop);
            }*/

        }


    }
}
