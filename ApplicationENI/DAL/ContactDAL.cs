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
        static String SELECT_CONTACT_PAR_STAGIAIRE = "SELECT CONTACT.CODECONTACT, Contact.NOM, Contact.PRENOM, Contact.TELFIXE, Contact.TelMobile, Contact.Email, Entreprise.RaisonSociale FROM CONTACT, Fonction, StagiaireParEntreprise, Entreprise WHERE Contact.CodeFonction = Fonction.CodeFonction AND StagiaireParEntreprise.CodeEntreprise=Entreprise.CodeEntreprise AND StagiaireParEntreprise.CodeStagiaire=@num_stagiaire";
        static String DELETE_CONTACT = "DELETE FROM CONTACT WHERE CodeContact=@codeContact";
        static String UPDATE_CONTACT = "UPDATE CONTACT SET Nom=@nom, Prenom=@prenom, TelFixe=@telFixe, TelMobile=@telPortable, Email=@email WHERE CODECONTACT=@codeContact";

        public static List<Contact> rechercherContacts(int pNumStagiaire){

            SqlConnection connexion = ConnexionSQL.CreationConnexion();
            SqlCommand cmd = new SqlCommand(SELECT_CONTACT_PAR_STAGIAIRE, connexion);
            cmd.Parameters.AddWithValue("@num_stagiaire", pNumStagiaire);
            List<Contact> listeContacts = new List<Contact>();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read()) {
                Contact contact = new Contact();
                contact._nomEntreprise = reader.GetString(reader.GetOrdinal("RaisonSociale"));
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
    }
}
