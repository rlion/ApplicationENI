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
