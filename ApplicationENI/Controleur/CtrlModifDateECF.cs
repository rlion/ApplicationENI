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
        //TODO regions
        SessionECF _sessionECF = null;

        public SessionECF SessionECF
        {
            get { return _sessionECF; }
            set { _sessionECF = value; }
        }
        Stagiaire _stagaire = null;

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

        public void modifierDateSessionECF_Stagiaire(Stagiaire pStagiaire, SessionECF pSessionECF, DateTime pDate)
        {
            SessionECFDAL.modifierDateSessionECF_Stagiaire(pStagiaire, pSessionECF, pDate);
        }

    }
}
