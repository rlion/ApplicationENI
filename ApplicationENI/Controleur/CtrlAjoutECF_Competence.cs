using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApplicationENI.Modele;
using ApplicationENI.DAL;

namespace ApplicationENI.Controleur
{
    class CtrlAjoutECF_Competence
    {
        #region Attributs, proprietes et constructeur
        private bool _ecfAdd; //si true on est en train d'ajouter un ECF sinon une Competence
        private ECF _ECF = null;
        private Competence _competence = null;
        public bool ECFAdd
        {
            get { return _ecfAdd; }
            set { _ecfAdd = value; }
        }
        public ECF ECF
        {
            get { return _ECF; }
            set { _ECF = value; }
        }
        public Competence Competence
        {
            get { return _competence; }
            set { _competence = value; }
        }
        public CtrlAjoutECF_Competence()
        {
            _ecfAdd = false;
            _ECF = null;
            _competence = null;
        }
        #endregion

        public String ajouterECF(ECF pECF)
        {
            return ECFDAL.ajouterECF(pECF);
        }
        public String ajouterCompetence(Competence pComp)
        {
            return CompetencesDAL.ajouterCompetence(pComp);
        }

    }
}
