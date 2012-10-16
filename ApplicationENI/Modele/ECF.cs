using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplicationENI.Modele
{   
    public class ECF
    {
        #region Attributs (id, code, libelle, coefficient, notationNumerique, nbreVersion, commentaire, competences)
        private String _id;//Guid _id;
        private String _code;        
        private String _libelle;       
        private double _coefficient;        
        private Boolean _notationNumerique; //Vrai : numerique (0 a 20) - Faux : acquisition (oui, en cours, non)
        private int _nbreVersion;        
        private String _commentaire;
        private List<Competence> _competences;
        #endregion

        #region Proprietes
        public String Id//Guid Id       
        {
            get { return _id; }
            set { _id = value; }
        }
        public String Code
        {
            get { return _code; }
            set { _code = value; }
        }
        public String Libelle
        {
            get { return _libelle; }
            set { _libelle = value; }
        }
        public double Coefficient
        {
            get { return _coefficient; }
            set { _coefficient = value; }
        }
        public Boolean NotationNumerique
        {
            get { return _notationNumerique; }
            set { _notationNumerique = value; }
        }        
        public int NbreVersion
        {
            get { return _nbreVersion; }
            set { _nbreVersion = value; }
        }
        public String Commentaire
        {
            get { return _commentaire; }
            set { _commentaire = value; }
        }
        public List<Competence> Competences
        {
            get { return _competences; }
            set { _competences = value; }
        }
        #endregion

        #region Constructeurs
        public ECF()
        {
            _id = "";
            _code = "";
            _libelle = "";
            _coefficient = 1;
            _notationNumerique = true;
            _nbreVersion = 1;
            _commentaire = "";
            _competences = new List<Competence>();
        }

        public ECF(String pCode, String pLibelle)
        {
            _id = "";
            _code = pCode.Trim();
            _libelle = pLibelle.Trim();
            _coefficient = 1;
            _notationNumerique = true;
            _nbreVersion = 1;
            _commentaire = "";
            _competences = new List<Competence>();
        }

        //public ECF(String pCode, String pLibelle, Boolean pNotationNumerique)
        //{
        //    _id = "";
        //    _code = pCode;
        //    _libelle = pLibelle;
        //    if (pNotationNumerique)
        //    {
        //        _coefficient = 1;
        //    }
        //    else
        //    {
        //        _coefficient = 0;
        //    }
        //    _notationNumerique = pNotationNumerique;
        //    _nbreVersion = 1;
        //    _commentaire = "";
        //}
        
        //public ECF(String pCode, String pLibelle, int pCoeff, Boolean pNotationNumerique, int pNbreVersion, String pCommentaire)
        //{
        //    _id = "";
        //    _code = pCode;
        //    _libelle = pLibelle;
        //    if (pNotationNumerique)
        //    {
        //        _coefficient = pCoeff;
        //    }
        //    else
        //    {
        //        _coefficient = 0;
        //    }
        //    _notationNumerique = pNotationNumerique;
        //    _nbreVersion = pNbreVersion;
        //    _commentaire = pCommentaire;
        //}

        public ECF(String pCode, String pLibelle, int pCoeff, Boolean pNotationNumerique, int pNbreVersion, String pCommentaire, List<Competence> pCompetences)
        {
            _id = "";
            _code = pCode.Trim();
            _libelle = pLibelle.Trim();
            if (pNotationNumerique)
            {
                _coefficient = pCoeff;
            }
            else
            {
                _coefficient = 0;
            }
            _notationNumerique = pNotationNumerique;
            _nbreVersion = pNbreVersion;
            _commentaire = pCommentaire;
            _competences = pCompetences;
        }
        #endregion

        public void ajouterCompetence(ECF pECF, Competence pCompetence)
        {
            pECF._competences.Add(pCompetence);
        }

        //public void ajouterCompetence(ECF pECF, String pLibelleCompetence)
        //{
        //    Competence Cp = new Competence(pLibelleCompetence);
        //    pECF._competences.Add(Cp);
        //}

        public void changerNbreVersion(ECF pECF, int pNbreVersion)
        {
            pECF._nbreVersion=pNbreVersion;
        }

        public override string ToString()
        {
            return _code + " - " + _libelle;
        }

        public override bool Equals(object obj)
        {
            bool b = false;
            int i = 0;

            if (this == null || obj == null)
            {
                if (this == null && obj == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            if (this.Id == ((ECF)obj).Id &&
                this.Code == ((ECF)obj).Code &&
                this.Libelle == ((ECF)obj).Libelle &&
                this.Coefficient == ((ECF)obj).Coefficient &&
                this.NbreVersion == ((ECF)obj).NbreVersion &&
                this.NotationNumerique == ((ECF)obj).NotationNumerique &&
                this.Commentaire == ((ECF)obj).Commentaire &&
                this.Competences.Count() == ((ECF)obj).Competences.Count) b = true;

            if (b) {
                foreach (Competence comp in ((ECF)obj).Competences)
                {
                    if (!comp.Equals(this.Competences[i])) b=false;
                    i++;
                }
            }

            return b;
        }
    }
}
