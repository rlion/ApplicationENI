using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplicationENI.Modele
{
    public class Evaluation
    {
        #region Attributs (id, ecf, competence, stagiaire, version, note, date)
        private Guid _id;
        private ECF _ecf;
        private Competence _competence;
        private Stagiaire _stagiaire;
        private int _version;
        private int _note; //si numerique entre 0 et 20 - sinom 0 non acquis, 1 en cours, 2 acquis
        private DateTime _date;
        #endregion

        #region Proprietes
        public Guid Id
        {
            get { return _id; }
            set { _id = value; }
        }
        public ECF Ecf
        {
            get { return _ecf; }
            set { _ecf = value; }
        }        
        public Competence Competence
        {
            get { return _competence; }
            set { _competence = value; }
        }        
        public Stagiaire Stagiaire
        {
            get { return _stagiaire; }
            set { _stagiaire = value; }
        }
        public int Version
        {
            get { return _version; }
            set { _version = value; }
        }
        public int Note
        {
            get { return _note; }
            set { _note = value; }
        }
        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }
        #endregion

        #region Constructeurs
        public Evaluation()
        {
            _id = new Guid();
            _ecf = new ECF();
            _competence = new Competence();
            _stagiaire = new Stagiaire();
            _version = 0;
            _note = 0;
            _date = new DateTime();
        }
        public Evaluation(ECF pEcf, Competence pComp, Stagiaire pStag, int pVersion, int pNote, DateTime pDate)
        {
            _id = new Guid();
            _ecf = new ECF();
            _competence = new Competence();
            _stagiaire = new Stagiaire();
            _version = 0;
            _note = 0;
            _date = new DateTime();
        }
        #endregion

    }
}
