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

        #region lien ECF-formation
        public void supprimerLienFormation(ECF pECF, Formation pForm)
        {
            ECFDAL.supprimerLienFormation(pECF, pForm);
        }
        #endregion

    }
}
