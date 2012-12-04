using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApplicationENI.Modele;
using ApplicationENI.DAL;

namespace ApplicationENI.Controleur
{
    public class CtrlGestionECF
    {

        #region Attributs, proprietes et constructeur
        private List<ECF> _listeECF = null; //liste de tous les ECFs
        private ECF _ecfCourant = null; //ecf selectionne
        private bool _ecfAdd; //si true on est en train d'ajouter un ECF sinon une Competence
        public List<ECF> ListeECF
        {
            get { return _listeECF; }
            set { _listeECF = value; }
        }
        public ECF EcfCourant
        {
            get { return _ecfCourant; }
            set { _ecfCourant = value; }
        }
        public bool EcfAdd
        {
            get { return _ecfAdd; }
            set { _ecfAdd = value; }
        }
        public CtrlGestionECF()
        {
            _listeECF = null;
            _ecfCourant = null;
            _ecfAdd = false;
        }
        #endregion

        #region ECF
        public List<ECF> getListECFs()
        {
            return ECFDAL.getListECFs();
        }
        
        public String modifierECF(ECF pECF)
        {
            return ECFDAL.modifierECF(pECF);
        }
        public void supprimerECF(ECF pECF)
        {
            ECFDAL.supprimerECF(pECF);
        }
        #endregion

        #region Competence
        public String supprimerCompetence(Competence pComp)
        {
            return CompetencesDAL.supprimerCompetence(pComp);
        }
        #endregion

        #region ECF-Competence
        public String supprimerLienCompetence(ECF pECF, Competence pComp)
        {
            return ECFDAL.supprimerLienCompetence(pECF, pComp);
        }
        #endregion


        #region formation
        //public List<Formation> getListFormations()
        //{
        //    return FormationDAL.listeFormations();
        //}
        #endregion

        

        #region lien ECF-formation
        //public void ajouterLienFormation(ECF pECF, Formation pForm)
        //{
        //    ECFDAL.ajouterLienFormation(pECF, pForm);
        //}
        public void supprimerLienFormation(ECF pECF, Formation pForm)
        {
            ECFDAL.supprimerLienFormation(pECF, pForm);
        }
        //public void supprimerLiensFormations(ECF pECF)
        //{
        //    ECFDAL.supprimerLiensFormations(pECF);
        //}
        //public static List<Formation> getListFormationsECF(ECF pECF)
        //{
        //    ECFDAL.getListFormationsECF(pECF);
        //}
        #endregion

        #region sessionECF
        //public List<SessionECF> getListSessionsECFs()
        //{
        //    return SessionECFDAL.getListSessionsECFs();
        //}
        
        //public List<SessionECF> getListSessionsECFStagiaire(Stagiaire pStag)
        //{
        //    return SessionECFDAL.getListSessionsECFStagiaire(pStag);
        //}
        //public List<SessionECF> getListSessionsECF(ECF pECF)
        //{
        //    return SessionECFDAL.getListSessionsECF(pECF);
        //}
        
        //public static void modifierDateSessionECF(SessionECF pSessionECF, DateTime pDate)
        //{
        //    SessionECFDAL.modifierDateSessionECF(pSessionECF,pDate);
        //}
        //public void modifierDateSessionECF_Stagiaire(Stagiaire pStagiaire, SessionECF pSessionECF, DateTime pDate)
        //{
        //    SessionECFDAL.modifierDateSessionECF_Stagiaire(pStagiaire, pSessionECF, pDate);
        //}
        
        #endregion


        //participants
        

        

        //public List<SessionECF> donneSessionsECFJour(ECF pECF, DateTime pDate)
        //{
        //    return SessionECFDAL.donneSessionsECFJour(pECF, pDate);
        //}

        

        

        //evaluations
        //public void ajouterEvaluation(Evaluation pEval)
        //{
        //    EvaluationsDAL.ajouterEvaluation(pEval);
        //}
        //public void modifierNoteEvaluation(Evaluation pEvaluation, float pNote)
        //{
        //    EvaluationsDAL.modifierNoteEvaluation(pEvaluation, pNote);
        //}
        //public static Evaluation donneEvaluation(Evaluation pEval){
        //    return EvaluationsDAL.donneEvaluation(pEval);
        //}
        //public Evaluation donneNote(SessionECF pSession, Stagiaire pStag, Competence pComp)
        //{
        //    return EvaluationsDAL.donneNote(pSession, pStag, pComp);
        //}
    }
}
