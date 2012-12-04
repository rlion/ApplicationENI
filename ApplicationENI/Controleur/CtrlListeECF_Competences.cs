using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApplicationENI.Modele;
using ApplicationENI.DAL;

namespace ApplicationENI.Controleur
{
    public class CtrlListeECF_Competences
    {
        #region Attributs, proprietes et constructeur
        private List<SelectionCompetence> _listeCompetences;
        private bool _isInitAutoCompBox;
        public List<SelectionCompetence> ListeCompetences
        {
            get { return _listeCompetences; }
            set { _listeCompetences = value; }
        }        
        public bool IsInitAutoCompBox
        {
            get { return _isInitAutoCompBox; }
            set { _isInitAutoCompBox = value; }
        }

        public CtrlListeECF_Competences()
        {
            _listeCompetences = null;
            _isInitAutoCompBox = false;
        }
        #endregion

        #region classe spéciale SelectionCompetence
        //classe listant l'ensemble des compétences (avec coche si elle est liée à l'ECF courant)
        //exemple : http://merill.net/2009/10/wpf-checked-listbox/
        public class SelectionCompetence
        {
            private Competence _competence;
            private bool _isChecked;

            public Competence Competence
            {
                get { return _competence; }
                set { _competence = value; }
            }
            public bool IsChecked
            {
                get { return _isChecked; }
                set { _isChecked = value; }
            }

            public override string ToString()
            {
                return _competence.Code + " - " + _competence.Libelle;
            }
        }
        #endregion

        #region Competence
        public List<Competence> getListCompetences()
        {
            return CompetencesDAL.getListCompetences();
        }
        //public String ajouterCompetence(Competence pComp)
        //{
        //    return CompetencesDAL.ajouterCompetence(pComp);
        //}
        public String supprimerCompetence(Competence pComp)
        {
            return CompetencesDAL.supprimerCompetence(pComp);
        }
        #endregion

        #region lien ECF-competence
        public String ajouterLienCompetence(ECF pECF, Competence pComp)
        {
            return ECFDAL.ajouterLienCompetence(pECF, pComp);
        }
        //public String supprimerLienCompetence(ECF pECF, Competence pComp)
        //{
        //    return ECFDAL.supprimerLienCompetence(pECF, pComp);
        //}
        public String supprimerLiensCompetences(ECF pECF)
        {
            return ECFDAL.supprimerLiensCompetences(pECF);
        }
        #endregion

        
    }
}
