using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApplicationENI.Modele;
using ApplicationENI.DAL;

namespace ApplicationENI.Controleur
{
    class CtrlSaisieResultats
    {
        #region Attributs, proprietes et constructeur
        private List<SessionECF> _listeSessionECFs = null;
        private List<DateTime> _planif = null;
        private SessionECF _sessionECFcourant = null;
        private Evaluation _evaluationEnCours = null
        public List<SessionECF> ListeSessionECFs
        {
            get { return _listeSessionECFs; }
            set { _listeSessionECFs = value; }
        }
        public List<DateTime> Planif
        {
            get { return _planif; }
            set { _planif = value; }
        }
        public SessionECF SessionECFcourant
        {
            get { return _sessionECFcourant; }
            set { _sessionECFcourant = value; }
        }
        public Evaluation EvaluationEnCours
        {
            get { return _evaluationEnCours; }
            set { _evaluationEnCours = value; }
        }

        public CtrlSaisieResultats()
        {
            _evaluationEnCours = null;
            _listeSessionECFs = null;
            _planif = null;
            _sessionECFcourant = null;
        }
        #endregion 

        #region sessionECF
        public List<SessionECF> getListSessionsECFs()
        {
            return SessionECFDAL.getListSessionsECFs();
        }
        public List<SessionECF> getListSessionsECFStagiaire(Stagiaire pStag)
        {
            return SessionECFDAL.getListSessionsECFStagiaire(pStag);
        }
        public List<SessionECF> getListSessionsECF(ECF pECF)
        {
            return SessionECFDAL.getListSessionsECF(pECF);
        }
        public int donneIdSessionECF(ECF pECF, DateTime pDate, int pVersion)
        {
            return SessionECFDAL.donneIdSessionECF(pECF, pDate, pVersion);
        }
        public List<SessionECF> donneSessionsECFJour(ECF pECF, DateTime pDate)
        {
            return SessionECFDAL.donneSessionsECFJour(pECF, pDate);
        }
        #endregion

        #region Evaluation
        public Evaluation donneNote(SessionECF pSession, Stagiaire pStag, Competence pComp)
        {
            return EvaluationsDAL.donneNote(pSession, pStag, pComp);
        }
        public void ajouterEvaluation(Evaluation pEval)
        {
            EvaluationsDAL.ajouterEvaluation(pEval);
        }
        public void modifierNoteEvaluation(Evaluation pEvaluation, float pNote)
        {
            EvaluationsDAL.modifierNoteEvaluation(pEvaluation, pNote);
        }
        #endregion

        #region Stagiaire
        public List<Stagiaire> getListParticipants(SessionECF pSessionECF)
        {
            return SessionECFDAL.getListParticipants(pSessionECF);
        }
        #endregion

    }
}
