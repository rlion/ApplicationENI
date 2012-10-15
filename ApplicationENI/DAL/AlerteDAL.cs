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
         * Tu peux t'en servir Roman, j'ai laissé mes vieilles requêtes au cas où et j'ai ajouté les nouvelles.
         */

        static String ABSENCES_COUNT = "select COUNT(*) nb from ABSENCE where id_stagiaire=@num_stagiaire and isAbsence=1";
        static String RETARDS_COUNT = "select COUNT(*) nb from ABSENCE where id_stagiaire=@num_stagiaire and isAbsence=0";


        static String SELECT_ALERTES_PAR_STAGIAIRE = "SELECT * FROM EVENEMENT";
        static String GET_NUM_ALERTE = "SELECT MAX(CodeEvenement) NbAlertes FROM EVENEMENT";
        static String INSERT_ALERTE = "INSERT INTO EVENEMENT VALUES(@texte, @date, @date, @date, @numContactEni, 'AL', null, null, null, 0, null);";


        public static List<ItemAlerte> listeAlertesParStagiaire(Stagiaire pStg) 
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
            return pStg.listeAlertes;
        }

        public static void AjouterAlerte(ItemAlerte pAlerte) {
            // test d'ajout dans la base de données bidon
            SqlConnection connexion = ConnexionSQL.CreationConnexion();
            SqlCommand cmd = new SqlCommand(INSERT_ALERTE, connexion);

            cmd.Parameters.AddWithValue("@texte", pAlerte.DESCRIPTION);
            cmd.Parameters.AddWithValue("@date", DateTime.Now);

            //TODO: trouver un moyen pour contourner ça (si jamais il n'y a pas de contact 1 dans la base, ça va planter)
            cmd.Parameters.AddWithValue("@numContactEni", 1);
            cmd.ExecuteNonQuery();

            // maintenant il faut mettre à jour l'objet Absence en lui assignant son numéro
            SqlCommand cmd2 = new SqlCommand(GET_NUM_ALERTE, connexion);
            int idDernierAlerte = Convert.ToInt32(cmd2.ExecuteScalar());
            Parametres.Instance.stagiaire.listeAlertes.Add(pAlerte);
            connexion.Close();
        }

        public static int nombreAbsences(Stagiaire pStg)
        {

            SqlConnection connexion = ConnexionSQL.CreationConnexion();
            SqlCommand cmd = new SqlCommand(ABSENCES_COUNT, connexion);
            cmd.Parameters.AddWithValue("@num_stagiaire", pStg._id);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            return reader.GetInt32(reader.GetOrdinal("nb"));
            }
        public static int nombreRetards(Stagiaire pStg)
        {
            SqlConnection connexion = ConnexionSQL.CreationConnexion();
            SqlCommand cmd = new SqlCommand(RETARDS_COUNT, connexion);
            cmd.Parameters.AddWithValue("@num_stagiaire", pStg._id);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            return reader.GetInt32(reader.GetOrdinal("nb"));
        }
    }

}
