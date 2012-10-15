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
        static String SELECT_CONTACT_PAR_STAGIAIRE = "SELECT CONTACT.CODECONTACT, Contact.NOM, Contact.PRENOM, Contact.TELFIXE, Contact.TelMobile, Contact.Email, Entreprise.RaisonSociale FROM CONTACT, Fonction, StagiaireParEntreprise, Entreprise WHERE Contact.CodeFonction = Fonction.CodeFonction AND Fonction.CodeFonction=StagiaireParEntreprise.CodeFonction AND StagiaireParEntreprise.CodeEntreprise=Entreprise.CodeEntreprise AND Fonction.CodeFonction LIKE 'TUT' AND StagiaireParEntreprise.CodeStagiaire=@num_stagiaire";

        public static Contact rechercherContact(int pNumStagiaire){

            SqlConnection connexion = ConnexionSQL.CreationConnexion();
            SqlCommand cmd = new SqlCommand(SELECT_CONTACT_PAR_STAGIAIRE, connexion);
            cmd.Parameters.AddWithValue("@num_stagiaire", pNumStagiaire);
            Contact tuteur = new Contact();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read()) {
                
                tuteur._nomEntreprise = reader.GetString(reader.GetOrdinal("RaisonSociale"));
                tuteur._codeContact = reader.GetInt32(reader.GetOrdinal("CODECONTACT"));
                tuteur._email = reader.GetString(reader.GetOrdinal("Email"));
                tuteur._nom = reader.GetString(reader.GetOrdinal("NOM"));
                tuteur._prenom = reader.GetString(reader.GetOrdinal("PRENOM"));
                tuteur._telFixe = reader.GetString(reader.GetOrdinal("TELFIXE"));
                tuteur._telMobile = reader.GetString(reader.GetOrdinal("TelMobile"));

            }
            return tuteur;
        }
    }
}
