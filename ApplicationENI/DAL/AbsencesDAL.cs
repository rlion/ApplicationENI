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
        static String INSERT_ABSENCES = "INSERT INTO ABSENCE (RAISON, COMMENTAIRE, DATEDEBUT, DATEFIN, JUSTIFIEE, ISABSENCE, ID_STAGIAIRE) " +
            "SELECT @raison, @commentaire, @dateDebut, @dateFin, @justifiee, @isAbsence, @num_stagiaire " +
            "WHERE NOT EXISTS (SELECT 0 FROM ABSENCE WHERE RAISON=@raison AND COMMENTAIRE=@commentaire AND DATEDEBUT=@dateDebut AND DATEFIN=@dateFin "+
            "AND JUSTIFIEE=@justifiee AND ISABSENCE=@isAbsence AND ID_STAGIAIRE=@num_stagiaire)";
        static String INSERT_ABSENCE_TEMPORAIRE = "INSERT INTO ABSENCE (DATEDEBUT, ID_STAGIAIRE) VALUES(@dateDebut, @num_stagiaire)";
        static String DELETE_ABSENCES = "DELETE FROM ABSENCE WHERE ID_ABSENCE=@id_absence";
        static String UPDATE_ABSENCES = "UPDATE ABSENCE SET DATEDEBUT=@dateDebut, DATEFIN=@dateFin, COMMENTAIRE=@commentaire, RAISON=@raison, JUSTIFIEE=@justifiee WHERE ID_ABSENCE=@id_absence";
        static String GET_NUM_ABSENCE = "SELECT @@IDENTITY AS IDENT";

        public static List<Absence> getListeAbsences(Stagiaire pS)
        {

			SqlConnection connexion = ConnexionSQL.CreationConnexion();
            SqlCommand cmd = new SqlCommand(SELECT_INFOS_ABSENCES, connexion);
            cmd.Parameters.AddWithValue("@num_stagiaire", pS._id);
            try
            {
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Absence absTemp = new Absence();
                    absTemp._id = reader.GetInt32(reader.GetOrdinal("id_absence"));

                    if (!reader.GetSqlDateTime(3).IsNull) { absTemp._dateDebut = reader.GetDateTime(3); }
                    if (!reader.GetSqlDateTime(4).IsNull) { absTemp._dateFin = reader.GetDateTime(4); } else { absTemp._dateFin = DateTime.Now; }
                    absTemp._raison = reader.GetSqlString(1).IsNull ? String.Empty : reader.GetString(1);
                    absTemp._commentaire = reader.GetSqlString(2).IsNull ? String.Empty : reader.GetString(2);
                    if (!reader.GetSqlBoolean(5).IsNull) { absTemp._valide = reader.GetBoolean(5); }
                    if (!reader.GetSqlBoolean(6).IsNull) { absTemp._isAbsence = reader.GetBoolean(6); }
                    try{absTemp._duree = absTemp._dateFin - absTemp._dateDebut;}catch (Exception){absTemp._duree = new TimeSpan(0);}
                    absTemp._stagiaire = pS;
                    if (pS.listeAbsences == null)
                    {
                        pS.listeAbsences = new List<Absence>();                    
                    }
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
        }

        public static void supprimerAbsence(Absence pA) 
        { 
            SqlConnection connexion = ConnexionSQL.CreationConnexion();
            SqlCommand cmd = new SqlCommand(DELETE_ABSENCES, connexion);
            cmd.Parameters.AddWithValue("@id_absence", pA._id); 

            cmd.ExecuteReader();
            connexion.Close();
            Parametres.Instance.stagiaire.listeAbsences.Remove(pA);
		   
        }
        public static void modifierAbsence(Absence pA)
        {
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

            try
            {
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
                cmd.ExecuteNonQuery();

                // maintenant il faut mettre à jour l'objet Absence en lui assignant son numéro
                SqlCommand cmd2 = new SqlCommand(GET_NUM_ABSENCE, connexion);
                int idDernierAbsence = Convert.ToInt32(cmd2.ExecuteScalar());
                pA._id = Convert.ToInt32(idDernierAbsence);
                Parametres.Instance.stagiaire.listeAbsences.Add(pA);
                connexion.Close();
            }
            catch (Exception)
            {
                System.Windows.MessageBox.Show("Cette absence a déjà été ajoutée. Pour la modifier, veuillez consulter l'historique des absences.",
                    "Ajout Absence impossible", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Stop);
            }
            
        }

        public static void ajouterAbsenceTemporaire(Absence pA, Stagiaire pStagiaire)
        {

           // try
            //{
                SqlConnection connexion = ConnexionSQL.CreationConnexion();
                SqlCommand cmd = new SqlCommand(INSERT_ABSENCE_TEMPORAIRE, connexion);
                cmd.Parameters.AddWithValue("@dateDebut", pA._dateDebut);
                cmd.Parameters.AddWithValue("@num_stagiaire", pStagiaire._id);
                cmd.ExecuteNonQuery();

                // maintenant il faut mettre à jour l'objet Absence en lui assignant son numéro
                SqlCommand cmd2 = new SqlCommand(GET_NUM_ABSENCE, connexion);
                int idDernierAbsence = Convert.ToInt32(cmd2.ExecuteScalar());
                pA._id = Convert.ToInt32(idDernierAbsence);
                if (pStagiaire.listeAbsences != null) {
                    pStagiaire.listeAbsences.Add(pA); 
                }
                connexion.Close();
            /*}
            catch (Exception)
            {
                System.Windows.MessageBox.Show("Cette absence ne peut être ajoutée.",
                    "Ajout Absence impossible", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Stop);
            }*/

        }
    }
}
