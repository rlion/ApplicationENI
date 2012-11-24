using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApplicationENI.Modele;

namespace ApplicationENI.DAL
{
    static class JeuDonnees
    {
        public static List<Titre> GetTitres()
        {
            List<Titre> listTitres = new List<Titre>();

            Titre t1 = new Titre("AL", "Archi. Log.", "Architecte Logiciel", "1", "ALROM", "ALNSF",
                new DateTime(2011, 03, 01), new DateTime(2011, 03, 01), true, true);

            listTitres.Add(new Titre("DL", "Dev. Log.", "Développeur Logiciel", "3", "DLROM", "DLNSF",
                new DateTime(2001, 01, 01), new DateTime(2007, 03, 15), true, false));
            listTitres.Add(new Titre("CDI", "Concept. Dev. Log.", "Concepteur Développeur Informatique", "2", "CDIROM", "CDINSF",
                new DateTime(2001, 12, 01), new DateTime(2006, 03, 31), true, false));
            listTitres.Add(new Titre("ASR", "Archi. Spé. Réseau", "Architecte Spécialiste Réseau", "2", "ASRROM", "ASRNSF",
                new DateTime(2002, 06, 11), new DateTime(2004, 03, 01), true, false));
            listTitres.Add(new Titre("EISI", "EISI", "EISI", "1", "EISIROM", "EISINSF",
                new DateTime(2011, 05, 01), new DateTime(2011, 06, 15), true, false));

            listTitres.Add(t1);

            t1.ListeEpreuves = GetEpreuveTitres();

            return listTitres;
        }

        public static List<Salle> GetSalles()
        {
            Salle s1 = new Salle("S101", "salle 101");
            Salle s2 = new Salle("S102", "salle 102");
            Salle s3 = new Salle("S103", "salle 103");
            Salle s4 = new Salle("S104", "salle 104");

            List<Salle> ls = new List<Salle>();
            ls.Add(s1);
            ls.Add(s2);
            ls.Add(s3);
            ls.Add(s4);

            return ls;
        }

        public static List<Jury> GetJurys()
        {
            Jury j1 = new Jury(1, "Mr", "Bond", "James");
            Jury j2 = new Jury(2, "Mme", "Spears", "Britney");
            Jury j3 = new Jury(3, "Mlle", "Gomez", "Selena");
            Jury j4 = new Jury(4, "Mr", "Dujardin", "Jean");

            List<Jury> lj = new List<Jury>();
            lj.Add(j1);
            lj.Add(j2);
            lj.Add(j3);
            lj.Add(j4);

            return lj;
        }

        public static List<EpreuveTitre> GetEpreuveTitres()
        {
            List<Jury> lj1 = new List<Jury>();
            List<Jury> lj2 = new List<Jury>();

            lj1.Add(GetJurys().ElementAt(0));
            lj1.Add(GetJurys().ElementAt(1));
            lj2.Add(GetJurys().ElementAt(2));
            lj2.Add(GetJurys().ElementAt(3));

            // GetSalles().ElementAt(0)
            EpreuveTitre et1 = new EpreuveTitre(new DateTime(2012, 12, 12),"S101", "AL", lj1);
            //GetSalles().ElementAt(1)
            EpreuveTitre et2 = new EpreuveTitre(new DateTime(2012, 12, 19),"S102", "AL", lj2); 

            List<EpreuveTitre> let1 = new List<EpreuveTitre>();
            let1.Add(et1);
            let1.Add(et2);

            return let1;
        }

        public static List<Absence> GetListeAbsence()
        {
            Absence a = new Absence("réveil qu'a pas sonné", "Le stagiaire semble sincère", DateTime.Now, DateTime.Now.AddMinutes(25), DateTime.Now.AddMinutes(25)-DateTime.Now, false, Parametres.Instance.stagiaire, false);
            Absence a2 = new Absence("panne d'oreiller", "il est 15h00", DateTime.Now, DateTime.Now.AddDays(1), DateTime.Now.AddDays(1) - DateTime.Now, false, Parametres.Instance.stagiaire, true);
            List<Absence> listeAbs = new List<Absence>();
            listeAbs.Add(a);
            listeAbs.Add(a2);
            return listeAbs;
        }

        public static List<Stagiaire> GetListeStagiaire() {
            Contact tuteur = new Contact(1, "Jones", "Indiana", "0202020202", "0602020202", "0202020202", "indianajones@gmail.com", "il est sympa", "", "Melle", 9066, "");
            Stagiaire stg1 = new Stagiaire(1, "Mr.", "Denis", "Choniphroa", "36 rue des papillons", "", "", "35000", "Pancé", "0606060606", "0206060606", "toto@toto.fr", DateTime.Now, "", "", "", DateTime.Now, DateTime.Now, "/test/rep", true, "c:/testPhotos/1.jpg", true, "");
            Stagiaire stg2 = new Stagiaire(2, "Mme.", "Denise", "Hipec", "35 rue des papillons", "", "", "35000", "Pancé", "0606060606", "0206060606", "toto@toto.fr", DateTime.Now, "", "", "", DateTime.Now, DateTime.Now, "/test/rep", true, "c:/testPhotos/2.jpg", true, "");
            Stagiaire stg3 = new Stagiaire(3, "Mme.", "Sylvie", "Tanmieux", "34 rue des papillons", "", "", "35000", "Pancé", "0606060606", "0206060606", "toto@toto.fr", DateTime.Now, "", "", "", DateTime.Now, DateTime.Now, "/test/rep", true, "c:/testPhotos/3.jpg", true, "");
            Stagiaire stg4 = new Stagiaire(4, "Mme.", "Denise", "Toto", "33 rue des papillons", "", "", "35000", "Pancé", "0606060606", "0206060606", "toto@toto.fr", DateTime.Now, "", "", "", DateTime.Now, DateTime.Now, "/test/rep", true, "c:/testPhotos/4.jpg", true, "");
            Stagiaire stg5 = new Stagiaire(5, "Mr.", "Jean", "Haymard", "32 rue des papillons", "", "", "35000", "Pancé", "0606060606", "0206060606", "toto@toto.fr", DateTime.Now, "", "", "", DateTime.Now, DateTime.Now, "/test/rep", true, "c:/testPhotos/5.jpg", true, "");
            Stagiaire stg6 = new Stagiaire(6, "Mr.", "Titi", "Test", "31 rue des papillons", "", "", "35000", "Pancé", "0606060606", "0206060606", "toto@toto.fr", DateTime.Now, "", "", "", DateTime.Now, DateTime.Now, "/test/rep", true, "c:/testPhotos/6.jpg", true, "");
            Stagiaire stg7 = new Stagiaire(7, "Mr.", "Denis", "Denat", "30 rue des papillons", "", "", "35000", "Pancé", "0606060606", "0206060606", "toto@toto.fr", DateTime.Now, "", "", "", DateTime.Now, DateTime.Now, "/test/rep", true, "c:/testPhotos/7.jpg", true, "");
            Stagiaire stg8 = new Stagiaire(8, "Mr.", "Jo", "Pasquier", "30 rue des papillons", "", "", "35000", "Pancé", "0606060606", "0206060606", "toto@toto.fr", DateTime.Now, "", "", "", DateTime.Now, DateTime.Now, "/test/rep", true, "c:/testPhotos/7.jpg", true, "");
            Stagiaire stg9 = new Stagiaire(9, "Mr.", "Fred", "Henri", "36 rue des papillons", "", "", "35000", "Pancé", "0606060606", "0206060606", "toto@toto.fr", DateTime.Now, "", "", "", DateTime.Now, DateTime.Now, "/test/rep", true, "c:/testPhotos/1.jpg", true, "");
            Stagiaire stg10 = new Stagiaire(10, "Mme.", "Denise", "Aufroi", "35 rue des papillons", "", "", "35000", "Pancé", "0606060606", "0206060606", "toto@toto.fr", DateTime.Now, "", "", "", DateTime.Now, DateTime.Now, "/test/rep", true, "c:/testPhotos/2.jpg", true, "");
            Stagiaire stg11 = new Stagiaire(11, "Mme.", "Jules", "Jabert", "34 rue des papillons", "", "", "35000", "Pancé", "0606060606", "0206060606", "toto@toto.fr", DateTime.Now, "", "", "", DateTime.Now, DateTime.Now, "/test/rep", true, "c:/testPhotos/3.jpg", true, "");
            Stagiaire stg12 = new Stagiaire(12, "Mme.", "Saïd", "Croupier", "33 rue des papillons", "", "", "35000", "Pancé", "0606060606", "0206060606", "toto@toto.fr", DateTime.Now, "", "", "", DateTime.Now, DateTime.Now, "/test/rep", true, "c:/testPhotos/4.jpg", true, "");
            Stagiaire stg13 = new Stagiaire(13, "Mr.", "Omar", "Fred", "32 rue des papillons", "", "", "35000", "Pancé", "0606060606", "0206060606", "toto@toto.fr", DateTime.Now, "", "", "", DateTime.Now, DateTime.Now, "/test/rep", true, "c:/testPhotos/5.jpg", true, "");
            Stagiaire stg14 = new Stagiaire(14, "Mr.", "Jean", "Rousseau", "31 rue des papillons", "", "", "35000", "Pancé", "0606060606", "0206060606", "toto@toto.fr", DateTime.Now, "", "", "", DateTime.Now, DateTime.Now, "/test/rep", true, "c:/testPhotos/6.jpg", true, "");
            Stagiaire stg15 = new Stagiaire(15, "Mr.", "Jeff", "Purid", "30 rue des papillons", "", "", "35000", "Pancé", "0606060606", "0206060606", "toto@toto.fr", DateTime.Now, "", "", "", DateTime.Now, DateTime.Now, "/test/rep", true, "c:/testPhotos/7.jpg", true, "");

            List<Stagiaire> listeStagiaires = new List<Stagiaire>();
            listeStagiaires.Add(stg1);
            listeStagiaires.Add(stg2);
            listeStagiaires.Add(stg3);
            listeStagiaires.Add(stg4);
            listeStagiaires.Add(stg5);
            listeStagiaires.Add(stg6);
            listeStagiaires.Add(stg7);
            listeStagiaires.Add(stg8);
            listeStagiaires.Add(stg9);
            listeStagiaires.Add(stg10);
            listeStagiaires.Add(stg11);
            listeStagiaires.Add(stg12);
            listeStagiaires.Add(stg13);
            listeStagiaires.Add(stg14);
            return listeStagiaires;
        }

        public static List<Observation> GetListeObservation() {
            Observation obs1 = new Observation("jgabillaud", "Pédagogique", "Ne bosse pas bien", "Bosse vraiment mal", Parametres.Instance.stagiaire);
            Observation obs2 = new Observation("jgabillaud", "Entreprise", "N'a pas le niveau", "Mérite l'exclusion.", Parametres.Instance.stagiaire);
            List<Observation> listeObservation = new List<Observation>();
            listeObservation.Add(obs1);
            listeObservation.Add(obs2);
            return listeObservation;
            }

        public static Contact GetContact() {
            Contact c = new Contact(1, "contact", "Jean", "0202020202", "0602020202", "0202020202", "jean@jean.fr", "semble correct", "", "M.", 9066, "");
            return c;
        }

        public static List<ECF> GetListECF()
        {
            List<Competence> lesC1 = GetListCompetence();            

            List<ECF> lesECFs = new List<ECF>();
            ECF e1 = new ECF("SQL1", "SQL 1er niveau",1, false,3,"toto commentaire test", lesC1);
            ECF e2 = new ECF("C#", "Développement C#", 1, false, 2, "toto commentaire test1", lesC1);
            ECF e3 = new ECF("R1", "Réseau Administration", 1, true, 1, "toto commentaire test2", lesC1);
            ECF e4 = new ECF("R4", "Réseau Active Directory", 1, true, 5, "toto commentaire test3", lesC1);
            ECF e5 = new ECF("VB.NET", "développement VB.NET", 1, false, 2, "toto commentaire test4", lesC1);

            lesECFs.Add(e1);
            lesECFs.Add(e2);
            lesECFs.Add(e3);
            lesECFs.Add(e4);
            lesECFs.Add(e5);
            return lesECFs;
        }

        public static List<Competence> GetListCompetence()
        {
            List<Competence> lesCompetences = new List<Competence>();
            Competence c1 = new Competence("Dev1", "SQL1");
            Competence c2 = new Competence("Dev3", "SQL2");
            Competence c3 = new Competence("Dev9", "C#");
            Competence c4 = new Competence("Dev6", "VB.NET1");
            Competence c5 = new Competence("Dev7", "VB.NET2");

            lesCompetences.Add(c1);
            lesCompetences.Add(c2);
            lesCompetences.Add(c3);
            lesCompetences.Add(c4);
            lesCompetences.Add(c5);
            return lesCompetences;
        }

        public static List<Evaluation> GetListEvaluation()
        {
            List<Evaluation> lesEvaluations = new List<Evaluation>();
            Evaluation e1 = new Evaluation();
            Evaluation e2 = new Evaluation();
            Evaluation e3 = new Evaluation();
            Evaluation e4 = new Evaluation();
            Evaluation e5 = new Evaluation();

            lesEvaluations.Add(e1);
            lesEvaluations.Add(e2);
            lesEvaluations.Add(e3);
            lesEvaluations.Add(e4);
            lesEvaluations.Add(e5);
            return lesEvaluations;
        }

        public static List<Cours> GetListeCours()
        {
            List<Cours> lesCours = new List<Cours>();
            Cours c1 = new Cours(1, DateTime.Now, DateTime.Now, 20, 15, 5000, DateTime.Now, DateTime.Now, "c#");
            Cours c2 = new Cours(2, DateTime.Now, DateTime.Now, 10, 5, 4000, DateTime.Now, DateTime.Now, "SCRUM");
            Cours c3 = new Cours(3, DateTime.Now, DateTime.Now, 60, 16, 520, DateTime.Now, DateTime.Now, "Prince2");
            lesCours.Add(c1);
            lesCours.Add(c2);
            lesCours.Add(c3);
            return lesCours;
        }

        public static List<Formation> GetListeFormation()
        {
            List<Formation> lesFormations = new List<Formation>();
            Formation form1 = new Formation("AL","Architecte Logiciel");
            Formation form2 = new Formation("CDI","Concepteur Développeur Informatique");
            Formation form3 = new Formation("DL","Développeur Logiciel");
            Formation form4 = new Formation("IM","Informaticien Micro");
            lesFormations.Add(form1);
            lesFormations.Add(form2);
            lesFormations.Add(form3);
            lesFormations.Add(form4);
            return lesFormations;
        }
    }
}
