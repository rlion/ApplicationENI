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

        #region lien ECF-competence
        public static void ajouterLien(ECF pECF, Competence pComp)
        {
            ECFDAL.ajouterLien(pECF, pComp);
        }
        public static void supprimerLien(ECF pECF, Competence pComp)
        {
            ECFDAL.supprimerLien(pECF, pComp);
        }
        public static void supprimerLiens(ECF pECF)
        {
            ECFDAL.supprimerLiens(pECF);
        }
        #endregion

        #region sessionECF
        public static List<SessionECF> getListSessionsECFs()
        {
            return SessionECFDAL.getListSessionsECFs();
        }
        public static void ajouterSessionECF(SessionECF pSessionECF)
        {
            SessionECFDAL.ajouterSessionECF(pSessionECF);
        }
        #endregion
    }
}
