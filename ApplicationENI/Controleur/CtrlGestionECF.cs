using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApplicationENI.Modele;
using ApplicationENI.DAL;

namespace ApplicationENI.Controleur
{
    public static class CtrlGestionECF
    {
        #region ECF
        public static List<ECF> getListECFs()
        {
            return ECFDAL.getListECFs();
        }
        public static String ajouterECF(ECF pECF)
        {
            return ECFDAL.ajouterECF(pECF);
        }
        public static void modifierECF(ECF pECF)
        {
            ECFDAL.modifierECF(pECF);
        }
        public static void supprimerECF(ECF pECF)
        {
            ECFDAL.supprimerECF(pECF);
        }
        #endregion

        #region competence
        public static List<Competence> getListCompetences()
        {
            return CompetencesDAL.getListCompetences();
        }
        public static String ajouterCompetence(Competence pComp)
        {
            return CompetencesDAL.ajouterCompetence(pComp);
        }
        public static String supprimerCompetence(Competence pComp)
        {
            return CompetencesDAL.supprimerCompetence(pComp);
        }
        #endregion

        #region formation
        public static List<Formation> getListFormations()
        {
            return FormationDAL.listeFormations();
        }
        #endregion

        #region lien ECF-competence
        public static void ajouterLienCompetence(ECF pECF, Competence pComp)
        {
            ECFDAL.ajouterLienCompetence(pECF, pComp);
        }
        public static void supprimerLienCompetence(ECF pECF, Competence pComp)
        {
            ECFDAL.supprimerLienCompetence(pECF, pComp);
        }
        public static void supprimerLiensCompetences(ECF pECF)
        {
            ECFDAL.supprimerLiensCompetences(pECF);
        }
        #endregion

        #region lien ECF-formation
        public static void ajouterLienFormation(ECF pECF, Formation pForm)
        {
            ECFDAL.ajouterLienFormation(pECF, pForm);
        }
        public static void supprimerLienFormation(ECF pECF, Formation pForm)
        {
            ECFDAL.supprimerLienFormation(pECF, pForm);
        }
        public static void supprimerLiensFormations(ECF pECF)
        {
            ECFDAL.supprimerLiensFormations(pECF);
        }
        //public static List<Formation> getListFormationsECF(ECF pECF)
        //{
        //    ECFDAL.getListFormationsECF(pECF);
        //}
        #endregion

        #region sessionECF
        public static List<SessionECF> getListSessionsECFs()
        {
            return SessionECFDAL.getListSessionsECFs();
        }
        public static List<SessionECF> getListSessionsECF(ECF pECF, int pVersion)
        {
            return SessionECFDAL.getListSessionsECF(pECF, pVersion);
        }
        public static void ajouterSessionECF(SessionECF pSessionECF)
        {
            SessionECFDAL.ajouterSessionECF(pSessionECF);
        }
        public static String donneIdSessionECF(ECF pECF, DateTime pDate, int pVersion)
        {
            return SessionECFDAL.donneIdSessionECF(pECF, pDate, pVersion);
        }
        #endregion


        //participants
        public static List<Stagiaire> getListeStagiaires() 
        {
            return StagiairesDAL.getListeStagiaires();
        }

        public static List<Stagiaire> getListeStagiaires(Formation pFormation, int pTypeFormation, String pFiltreNomPrenom)
        {
            return StagiairesDAL.getListeStagiaires(pFormation, pTypeFormation, pFiltreNomPrenom);
        }

        public static List<SessionECF> donneSessionsECFJour(ECF pECF, DateTime pDate)
        {
            return SessionECFDAL.donneSessionsECFJour(pECF, pDate);
        }

        public static List<Stagiaire> getListParticipants(SessionECF pSessionECF)
        {
            return SessionECFDAL.getListParticipants(pSessionECF);
        }

        public static void ajouterParticipants(SessionECF pSessionECF)
        {
            SessionECFDAL.ajouterParticipants(pSessionECF);
        }

        //evaluations
        public static void ajouterEvaluation(Evaluation pEval)
        {
            EvaluationsDAL.ajouterEvaluation(pEval);
        }

        public static Evaluation donneEvaluation(Evaluation pEval){
            return EvaluationsDAL.donneEvaluation(pEval);
        }
    }
}
