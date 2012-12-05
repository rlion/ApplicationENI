using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApplicationENI.Modele;
using ApplicationENI.DAL;

namespace ApplicationENI.Controleur
{
    class CtrlModifDateECF
    {
        #region Attributs, proprietes et constructeur
        SessionECF _sessionECF = null;
        Stagiaire _stagaire = null;
        public SessionECF SessionECF
        {
            get { return _sessionECF; }
            set { _sessionECF = value; }
        }
        public Stagiaire Stagaire
        {
            get { return _stagaire; }
            set { _stagaire = value; }
        }

        public CtrlModifDateECF()
        {
            _sessionECF = null;
            _stagaire = null;
        }
        #endregion

        #region SessionECF
        public String modifierDateSessionECF_Stagiaire(Stagiaire pStagiaire, SessionECF pSessionECF, DateTime pDate)
        {
            return SessionECFDAL.modifierDateSessionECF_Stagiaire(pStagiaire, pSessionECF, pDate);
        }
        #endregion
    }
}
