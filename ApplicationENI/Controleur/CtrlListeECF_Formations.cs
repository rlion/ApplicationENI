using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApplicationENI.DAL;
using ApplicationENI.Modele;

namespace ApplicationENI.Controleur
{
    public class CtrlListeECF_Formations
    {
        #region Attributs, proprietes, constructeur
        private List<SelectionFormation> _listeFormations;
        public List<SelectionFormation> ListeFormations
        {
            get { return _listeFormations; }
            set { _listeFormations = value; }
        }
        public CtrlListeECF_Formations()
        {
            _listeFormations = null;
        }
        #endregion

        #region classe spéciale SelectionFormation
        //classe listant l'ensemble des formations (avec coche si elle est liée à l'ECF courant)
        public class SelectionFormation
        {
            private Formation _formation;
            private bool _isChecked;

            public Formation Formation
            {
                get { return _formation; }
                set { _formation = value; }
            }
            public bool IsChecked
            {
                get { return _isChecked; }
                set { _isChecked = value; }
            }

            public override string ToString()
            {
                return _formation.Libelle;// +" - " + _competence.Libelle;
            }
        }
        #endregion

        #region Formation
        public List<Formation> getListFormations()
        {
            return FormationDAL.listeFormations();
        }
        #endregion

        #region lien ECF-formation
        public void ajouterLienFormation(ECF pECF, Formation pForm)
        {
            ECFDAL.ajouterLienFormation(pECF, pForm);
        }
        public void supprimerLienFormation(ECF pECF, Formation pForm)
        {
            ECFDAL.supprimerLienFormation(pECF, pForm);
        }
        public void supprimerLiensFormations(ECF pECF)
        {
            ECFDAL.supprimerLiensFormations(pECF);
        }
        #endregion
    }
}
