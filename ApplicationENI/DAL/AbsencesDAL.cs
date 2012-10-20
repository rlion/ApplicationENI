using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApplicationENI.Modele;
using System.Data.SqlClient;

namespace ApplicationENI.DAL
{
    class AbsencesDAL
    {
		static String SELECT_INFOS_ABSENCES = "SELECT * FROM ABSENCE WHERE ID_STAGIAIRE=@num_stagiaire";
        static String INSERT_ABSENCES = "INSERT INTO ABSENCE VALUES (@raison, @commentaire, @dateDebut, @dateFin, @justifiee, @isAbsence, @num_stagiaire)";
        static String DELETE_ABSENCES = "DELETE FROM ABSENCE WHERE ID_ABSENCE=@id_absence";
        static String UPDATE_ABSENCES = "UPDATE ABSENCE SET DATEDEBUT=@dateDebut, DATEFIN=@dateFin, COMMENTAIRE=@commentaire, RAISON=@raison, JUSTIFIEE=@justifiee WHERE ID_ABSENCE=@id_absence";
        static String GET_NUM_ABSENCE = "SELECT @@IDENTITY AS IDENT";

        public static List<Absence> getListeAbsences(Stagiaire pS)
        {
            //Stagiaire stgCourant = new Stagiaire();

			SqlConnection connexion = ConnexionSQL.CreationConnexion();
            SqlCommand cmd = new SqlCommand(SELECT_INFOS_ABSENCES, connexion);
            cmd.Parameters.AddWithValue("@num_stagiaire", pS._id);
            try
            {
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Absence absTemp = new Absence();
                    absTemp._id = reader.GetInt32(reader.GetOrdinal("id_absence")); //et ainsi de suite (attendre que la base soit faire pour avoir les bons noms de paramètres)...
                    absTemp._dateDebut = reader.GetDateTime(reader.GetOrdinal("dateDebut"));
                    absTemp._dateFin = reader.GetDateTime(reader.GetOrdinal("dateFin"));
                    absTemp._raison = reader.GetString(reader.GetOrdinal("raison"));
                    absTemp._commentaire = reader.GetString(reader.GetOrdinal("commentaire"));
                    absTemp._duree = absTemp._dateFin - absTemp._dateDebut;
                    absTemp._valide = reader.GetBoolean(reader.GetOrdinal("justifiee"));
                    absTemp._isAbsence = reader.GetBoolean(reader.GetOrdinal("isAbsence"));
                    absTemp._stagiaire = pS;
                    pS.listeAbsences.Add(absTemp);
                }
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show("Impossible d'éxécuter la requête : " + e.Message, "Echec de la requête",
                      System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                return null;
            }
            
            return pS.listeAbsences;
            //return DAL.JeuDonnees.GetListeAbsence();
        }

        public static void supprimerAbsence(Absence pA) 
        { 
			// test de suppression dans la base de données bidon
            SqlConnection connexion = ConnexionSQL.CreationConnexion();
            SqlCommand cmd = new SqlCommand(DELETE_ABSENCES, connexion);
            cmd.Parameters.AddWithValue("@id_absence", pA._id);  // il faut modifier tout ça

            cmd.ExecuteReader();
            connexion.Close();
            Parametres.Instance.stagiaire.listeAbsences.Remove(pA);
		   
        }
        public static void modifierAbsence(Absence pA)
        {
            // test de modification dans la base de données bidon
            SqlConnection connexion = ConnexionSQL.CreationConnexion();
            SqlCommand cmd = new SqlCommand(UPDATE_ABSENCES, connexion);
            cmd.Parameters.AddWithValue("@dateDebut", pA._dateDebut);
            cmd.Parameters.AddWithValue("@dateFin", pA._dateFin);
            cmd.Parameters.AddWithValue("@commentaire", pA._commentaire);
            cmd.Parameters.AddWithValue("@raison", pA._raison);
            cmd.Parameters.AddWithValue("@justifiee", pA._valide);
            cmd.Parameters.AddWithValue("@id_absence", pA._id);

            cmd.ExecuteReader();
            connexion.Close();
        }
        public static void ajouterAbsence(Absence pA)
        {
            //Parametres.Instance.stagiaire.listeAbsences.Add(pA);
            
            
            // test d'ajout dans la base de données bidon
            SqlConnection connexion = ConnexionSQL.CreationConnexion();
            SqlCommand cmd = new SqlCommand(INSERT_ABSENCES, connexion);
            cmd.Parameters.AddWithValue("@id", 1);  
            cmd.Parameters.AddWithValue("@dateDebut", pA._dateDebut);
            cmd.Parameters.AddWithValue("@dateFin", pA._dateFin);
            cmd.Parameters.AddWithValue("@commentaire", pA._commentaire);
            cmd.Parameters.AddWithValue("@raison", pA._raison);
            cmd.Parameters.AddWithValue("@justifiee", pA._valide);
            cmd.Parameters.AddWithValue("@num_stagiaire", pA._stagiaire._id);
            cmd.Parameters.AddWithValue("@isAbsence", pA._isAbsence);
            //cmd.Parameters.AddWithValue("@num_stagiaire", pA._stagiaire._id);
            cmd.ExecuteNonQuery();

            // maintenant il faut mettre à jour l'objet Absence en lui assignant son numéro
            SqlCommand cmd2 = new SqlCommand(GET_NUM_ABSENCE, connexion);
            int idDernierAbsence = Convert.ToInt32(cmd2.ExecuteScalar());
            pA._id = Convert.ToInt32(idDernierAbsence);
            Parametres.Instance.stagiaire.listeAbsences.Add(pA);
            connexion.Close();
        }
    }
}
