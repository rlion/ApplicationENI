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
        //TODO regions 
        private List<SessionECF> _listeSessionECFs = null;

        public List<SessionECF> ListeSessionECFs
        {
            get { return _listeSessionECFs; }
            set { _listeSessionECFs = value; }
        }
        //private ECF _ecfCourant = null;
        private List<DateTime> _planif = null;

        public List<DateTime> Planif
        {
            get { return _planif; }
            set { _planif = value; }
        }
        private SessionECF _sessionECFcourant = null;

        public SessionECF SessionECFcourant
        {
            get { return _sessionECFcourant; }
            set { _sessionECFcourant = value; }
        }
        private Evaluation _evaluationEnCours = null;

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

        public List<Stagiaire> getListParticipants(SessionECF pSessionECF)
        {
            return SessionECFDAL.getListParticipants(pSessionECF);
        }


    }
}
