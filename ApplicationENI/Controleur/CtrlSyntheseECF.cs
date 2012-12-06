using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApplicationENI.DAL;
using ApplicationENI.Modele;

namespace ApplicationENI.Controleur
{
    public class CtrlSyntheseECF
    {
        #region Attributs, proprietes et constructeur
        private Stagiaire _stagiaireEncours = null;
        private List<SessionECF> _lesSessionsECFsStag = null;
        private SessionECF _sessionSelectionnee = null;
        private Evaluation _evaluationSelectionnee = null;

        public Stagiaire StagiaireEncours
        {
            get { return _stagiaireEncours; }
            set { _stagiaireEncours = value; }
        }
        public List<SessionECF> LesSessionsECFsStag
        {
            get { return _lesSessionsECFsStag; }
            set { _lesSessionsECFsStag = value; }
        }
        public SessionECF SessionSelectionnee
        {
            get { return _sessionSelectionnee; }
            set { _sessionSelectionnee = value; }
        }
        public Evaluation EvaluationSelectionnee
        {
            get { return _evaluationSelectionnee; }
            set { _evaluationSelectionnee = value; }
        }

        public CtrlSyntheseECF()
        {
            _evaluationSelectionnee = null;
            _lesSessionsECFsStag = null;
            _sessionSelectionnee = null;
            _stagiaireEncours = null;
        }
        #endregion

        #region SessionECF
        public List<SessionECF> getListSessionsECFStagiaire(Stagiaire pStag)
        {
            return SessionECFDAL.getListSessionsECFStagiaire(pStag);
        }
        #endregion

        #region Evaluation
        public Evaluation donneNote(SessionECF pSession, Stagiaire pStag, Competence pComp)
        {
            return EvaluationsDAL.donneNote(pSession, pStag, pComp);
        }
        #endregion

        #region Lien ECF-Formation
        public void supprimerLienFormation(ECF pECF, Formation pForm)
        {
            ECFDAL.supprimerLienFormation(pECF, pForm);
        }
        #endregion
    }
}
