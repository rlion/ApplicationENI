using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplicationENI.Modele
{
    public class ECF
    {
        #region Attributs (id, libelle, coefficient, notationNumerique, nbreVersion, commentaire, competences)
        private Guid _id;       
        private String _libelle;       
        private int _coefficient;        
        private Boolean _notationNumerique; //Vrai : numerique (0 a 20) - Faux : acquisition (oui, en cours, non)
        private int _nbreVersion;        
        private String _commentaire;
        private List<Competence> _competences;
        #endregion

        #region Proprietes
        public Guid Id       
        {
            get { return _id; }
            set { _id = value; }
        }
        public String Libelle
        {
            get { return _libelle; }
            set { _libelle = value; }
        }
        public int Coefficient
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
            _id = new Guid();
            _libelle = "";
            _coefficient = 1;
            _notationNumerique = true;
            _nbreVersion = 1;
            _commentaire = "";
        }

        public ECF(String pLibelle)
        {
            _id = new Guid();
            _libelle = pLibelle;
            _coefficient = 1;
            _notationNumerique = true;
            _nbreVersion = 1;
            _commentaire = "";
        }

        public ECF(String pLibelle, Boolean pNotationNumerique)
        {
            _id = new Guid();
            _libelle = pLibelle;
            if (pNotationNumerique)
            {
                _coefficient = 1;
            }
            else
            {
                _coefficient = 0;
            }
            _notationNumerique = pNotationNumerique;
            _nbreVersion = 1;
            _commentaire = "";
        }
        #endregion

        public void ajouterCompetence(ECF pECF, Competence pCompetence)
        {
            pECF._competences.Add(pCompetence);
        }

        public void ajouterCompetence(ECF pECF, String pLibelleCompetence)
        {
            Competence Cp = new Competence(pLibelleCompetence);
            pECF._competences.Add(Cp);
        }

        public void changerNbreVersion(ECF pECF, int pNbreVersion)
        {
            pECF._nbreVersion=pNbreVersion;
        }

        public override string ToString()
        {
            return _libelle;
        }

    }
}
