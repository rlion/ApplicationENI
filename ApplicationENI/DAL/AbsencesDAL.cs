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
		static String SELECT_INFOS_ABSENCES = "SELECT * FROM ABSENCE WHERE NUM_STAGIAIRE=@num_stagiaire";
        static String INSERT_ABSENCES = "INSERT INTO ABSENCE VALUES (@id, @dateDebut, @dateFin, @nom_auteur, @commentaire, @duree, @raison, @valide, @num_stagiaire)";
        static String DELETE_ABSENCES = "DELETE FROM ABSENCE WHERE NUM=@num_absence";
        static String UPDATE_ABSENCES = "UPDATE ABSENCE SET DATEDEBUT=@dateDebut, DATEFIN=@dateFin, NOMAUTEUR=@nomAuteur, COMMENTAIRE=@commentaire, DUREE=@duree, RAISON=@raison, VALIDE=@valide WHERE NUM=@num_absence";
	
        public static List<Absence> getListeAbsences(Stagiaire pS)
        {
            
			
			/*SqlConnection connexion = ConnexionSQL.CreationConnexion();
            SqlCommand cmd = new SqlCommand(SELECT_INFOS_ABSENCES, connexion);
            cmd.Parameters.AddWithValue("@num_stagiaire", pStg._id);

            SqlDataReader reader = cmd.ExecuteReader();

            
            
            while(reader.Read()) 
            {
				Absence absTemp = new Absence();
                absTemp._id = reader.GetGuid(reader.GetOrdinal("id")); //et ainsi de suite (attendre que la base soit faire pour avoir les bons noms de paramètres)...
				absTemp._dateDebut = reader.GetDate(reader.GetOrdinal("dateDebut"));
				absTemp._dateFin = reader.GetDate(reader.GetOrdinal("dateFin"));
				absTemp._nomAuteur = reader.GetString(reader.GetOrdinal("nomAuteur"));
				absTemp._raison = reader.GetString(reader.GetOrdinal("raison"));
				absTemp._commentaire = reader.GetString(reader.GetOrdinal("commentaire"));
				absTemp._duree = reader.GetString(reader.GetOrdinal("duree"));
				absTemp._valide = reader.GetString(reader.GetOrdinal("valide"));
				absTemp._stagiaire = reader.GetStagiaire(reader.GetOrdinal("stagiaire")); //voir comment faire ici...
				listeAbs.Add(absTemp);
			}*/
			
            return DAL.JeuDonnees.GetListeAbsence();
        }

        public static void supprimerAbsence(Absence pA) 
        { 
			// test de suppression dans la base de données bidon
            SqlConnection connexion = ConnexionSQL.CreationConnexion();
            SqlCommand cmd = new SqlCommand(DELETE_ABSENCES, connexion);
            cmd.Parameters.AddWithValue("@num_absence", pA._id);  // il faut modifier tout ça

            cmd.ExecuteReader();
            connexion.Close();
		   
        }
        public static void modifierAbsence(Absence pA)
        {
            // test de modification dans la base de données bidon
            SqlConnection connexion = ConnexionSQL.CreationConnexion();
            SqlCommand cmd = new SqlCommand(UPDATE_ABSENCES, connexion);
            cmd.Parameters.AddWithValue("@dateDebut", DateTime.Now);
            cmd.Parameters.AddWithValue("@dateFin", DateTime.Now);
            cmd.Parameters.AddWithValue("@nom_auteur", pA._auteur);
            cmd.Parameters.AddWithValue("@commentaire", pA._commentaire);
            cmd.Parameters.AddWithValue("@duree", pA._duree);
            cmd.Parameters.AddWithValue("@raison", pA._raison);
            cmd.Parameters.AddWithValue("@valide", pA._valide);
            cmd.Parameters.AddWithValue("@num_absence", pA._id);

            cmd.ExecuteReader();
            connexion.Close();
        }
        public static void ajouterAbsence(Absence pA)
        {
            Parametres.Instance.stagiaire.listeAbsences.Add(pA);
            // test d'ajout dans la base de données bidon
            /*SqlConnection connexion = ConnexionSQL.CreationConnexion();
            SqlCommand cmd = new SqlCommand(INSERT_ABSENCES, connexion);
            cmd.Parameters.AddWithValue("@id", Guid.NewGuid());  
            cmd.Parameters.AddWithValue("@dateDebut", DateTime.Now);
            cmd.Parameters.AddWithValue("@dateFin", DateTime.Now);
            cmd.Parameters.AddWithValue("@nom_auteur", pA._auteur);
            cmd.Parameters.AddWithValue("@commentaire", pA._commentaire);
            cmd.Parameters.AddWithValue("@duree", pA._duree);
            cmd.Parameters.AddWithValue("@raison", pA._raison);
            cmd.Parameters.AddWithValue("@valide", pA._valide);
            cmd.Parameters.AddWithValue("@num_stagiaire", pA._stagiaire._id);
   
            cmd.ExecuteReader();
            connexion.Close();*/
        }
    }
}
