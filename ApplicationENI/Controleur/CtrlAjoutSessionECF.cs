using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApplicationENI.Modele;
using ApplicationENI.DAL;

namespace ApplicationENI.Controleur
{
    class CtrlAjoutSessionECF
    {
        #region Attributs, proprietes et constructeur
        private SessionECF _sessionECF = null;
        private List<SessionECF> _listeECFPlanif = null;
        private List<DateTime> _planif = null;
        public SessionECF SessionECF
        {
            get { return _sessionECF; }
            set { _sessionECF = value; }
        }        
        public List<SessionECF> ListeECFPlanif
        {
            get { return _listeECFPlanif; }
            set { _listeECFPlanif = value; }
        }
        public List<DateTime> Planif
        {
            get { return _planif; }
            set { _planif = value; }
        }

        public CtrlAjoutSessionECF()
        {
            _sessionECF = null;
            _listeECFPlanif = null;
            _planif = null;
        }
        #endregion

        #region ECF
        public List<ECF> getListECFs()
        {
            return ECFDAL.getListECFs();
        }
        #endregion

        #region SessionECF
        public List<SessionECF> getListSessionsECFVersion(ECF pECF, int pVersion)
        {
            return SessionECFDAL.getListSessionsECFVersion(pECF, pVersion);
        }
        public List<Stagiaire> ajouterSessionECF(SessionECF pSessionECF)
        {
            return SessionECFDAL.ajouterSessionECF(pSessionECF);
        }
        public int donneIdSessionECF(ECF pECF, DateTime pDate, int pVersion)
        {
            return SessionECFDAL.donneIdSessionECF(pECF, pDate, pVersion);
        }
        #endregion

        #region Stagiaire
        public List<Stagiaire> getListeStagiaires()
        {
            return StagiairesDAL.getListeStagiaires();
        }
        public List<Stagiaire> getListeStagiaires(Formation pFormation, int pTypeFormation, String pFiltreNomPrenom)
        {
            return StagiairesDAL.getListeStagiaires(pFormation, pTypeFormation, pFiltreNomPrenom);
        }
        public List<Stagiaire> getListParticipants(SessionECF pSessionECF)
        {
            return SessionECFDAL.getListParticipants(pSessionECF);
        }
        public List<Stagiaire> ajouterParticipants(SessionECF pSessionECF)
        {
            return SessionECFDAL.ajouterParticipants(pSessionECF);
        }
        #endregion
    }
}
