using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApplicationENI.Modele;
using System.Data.SqlClient;

namespace ApplicationENI.DAL
{
    class AlerteDAL
    {
        /*
         * Classe conservée en vue d'évolutions futures.
         */

        static String ABSENCES_COUNT = "select COUNT(*) nb from ABSENCE where id_stagiaire=@num_stagiaire and isAbsence=1";
        static String RETARDS_COUNT = "select COUNT(*) nb from ABSENCE where id_stagiaire=@num_stagiaire and isAbsence=0";
        static String ABSENCES_TEMPORAIRES_COUNT = "select COUNT(*) nb from ABSENCE where id_stagiaire=@num_stagiaire and raison is null";

        static String SELECT_ALERTES_PAR_STAGIAIRE = "SELECT * FROM EVENEMENT";
        //static String GET_NUM_ALERTE = "SELECT MAX(CodeEvenement) NbAlertes FROM EVENEMENT";
        //static String INSERT_ALERTE = "INSERT INTO EVENEMENT VALUES(@texte, @date, @date, @date, @numContactEni, 'AL', null, null, null, 0, null);";


        public static List<ItemAlerte> listeAlertesParStagiaire(Stagiaire pStg) 
        {
            try
            {
                SqlConnection connexion = ConnexionSQL.CreationConnexion();
                SqlCommand cmd = new SqlCommand(SELECT_ALERTES_PAR_STAGIAIRE, connexion);
                List<ItemAlerte> listeAlertes = new List<ItemAlerte>();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read()) {
                    ItemAlerte alerteTemp = new ItemAlerte();
                    alerteTemp.DESCRIPTION = reader.GetString(reader.GetOrdinal("Commentaire"));
                    alerteTemp.TYPE = reader.GetString(reader.GetOrdinal("CodeTypeEvenement"));
                    // une absence correspond à une icone de niveau Warning.
                    alerteTemp.ICONE = alerteTemp.GetIcone(0);
                    pStg.listeAlertes.Add(alerteTemp);
                }
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show("Impossible d'éxécuter la requête : " + e.Message, "Echec de la requête",
                      System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                return null;
            }
            
            return pStg.listeAlertes;
        }

        public static void AjouterAlerte(ItemAlerte pAlerte) {
            /* les alertes nes sont plus historisées en BDD
            SqlConnection connexion = ConnexionSQL.CreationConnexion();
            SqlCommand cmd = new SqlCommand(INSERT_ALERTE, connexion);

            cmd.Parameters.AddWithValue("@texte", pAlerte.DESCRIPTION);
            cmd.Parameters.AddWithValue("@date", DateTime.Now);
            
            try
            {
                cmd.Parameters.AddWithValue("@numContactEni", 1);
                cmd.ExecuteNonQuery();
                // maintenant il faut mettre à jour l'objet Absence en lui assignant son numéro
                SqlCommand cmd2 = new SqlCommand(GET_NUM_ALERTE, connexion);
                int idDernierAlerte = Convert.ToInt32(cmd2.ExecuteScalar());
                Parametres.Instance.stagiaire.listeAlertes.Add(pAlerte);
                connexion.Close();
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show(e.ToString());
            }
            
            */
            

            
        }

        public static int nombreAbsences(Stagiaire pStg)
        {

            SqlConnection connexion = ConnexionSQL.CreationConnexion();
            SqlCommand cmd = new SqlCommand(ABSENCES_COUNT, connexion);
            cmd.Parameters.AddWithValue("@num_stagiaire", pStg._id);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            int test = reader.GetInt32(reader.GetOrdinal("nb"));
            return test;    
        }
        public static int nombreRetards(Stagiaire pStg)
        {
            SqlConnection connexion = ConnexionSQL.CreationConnexion();
            SqlCommand cmd = new SqlCommand(RETARDS_COUNT, connexion);
            cmd.Parameters.AddWithValue("@num_stagiaire", pStg._id);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            int test = reader.GetInt32(reader.GetOrdinal("nb"));
             return test;
        }

        public static int nombreAbsencesTemporaires(Stagiaire pStg)
        {
            SqlConnection connexion = ConnexionSQL.CreationConnexion();
            SqlCommand cmd = new SqlCommand(ABSENCES_TEMPORAIRES_COUNT, connexion);
            cmd.Parameters.AddWithValue("@num_stagiaire", pStg._id);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            int test = reader.GetInt32(reader.GetOrdinal("nb"));
            return test;
        }
    }

}
