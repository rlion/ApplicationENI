﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplicationENI.Modele
{
    public class Evaluation
    {
        #region Attributs (id, ecf, competence, stagiaire, version, note, date)
        private int _id;
        private ECF _ecf;
        private Competence _competence;
        private Stagiaire _stagiaire;
        private int _version;
        private float _note; //si numerique entre 0 et 20 - sinom 0 non acquis, 1 en cours, 2 acquis
        private DateTime _date;
        #endregion

        #region Proprietes
        
        public int Id
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
        public float Note
        {
            get { return _note; }
            set { _note = value; }
        }
        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }

        //Propriété pour le rapport
        public string Competence_r
        {
            get { return _competence.Libelle; }
        }
        public string Note_r
        {
            get { return GetLibelleNote(); }
        }
        public int EcfId_r
        {
            get { return _ecf.Id; }
        }

        #endregion

        #region Constructeurs
        public Evaluation()
        {
            _id = 0;
            _ecf = new ECF();
            _competence = new Competence();
            _stagiaire = new Stagiaire();
            _version = 0;
            _note = -1;
            _date = new DateTime();
        }
        public Evaluation(int pId, ECF pEcf, Competence pComp, Stagiaire pStag, int pVersion, float pNote, DateTime pDate)
        {
            _id = pId;
            _ecf = pEcf;
            _competence = pComp;
            _stagiaire = pStag;
            _version = pVersion;
            _note = pNote;
            _date = pDate;
        }
        public Evaluation(ECF pEcf, Competence pComp, Stagiaire pStag, int pVersion, float pNote, DateTime pDate)
        {
            _id = 0;
            _ecf =pEcf;
            _competence = pComp;
            _stagiaire = pStag;
            _version = pVersion;
            _note = pNote;
            _date = pDate;
        }
        #endregion

        public override string ToString()
        {
            String competence = "";
            
            if (this.Note != -1)
            {
                if (this.Ecf.NotationNumerique)
                {
                    competence += this.Note + "/20";
                }
                else
                {
                    if (this.Note == Ressources.CONSTANTES.NOTE_ACQUIS)
                    {
                        competence += "ACQUIS";
                    }
                    else if (this.Note == Ressources.CONSTANTES.NOTE_ENCOURS_ACQUISITION)
                    {
                        competence += "EN COURS D'ACQUISITION";
                    }
                    else if (this.Note == Ressources.CONSTANTES.NOTE_NON_ACQUIS)
                    {
                        competence += "NON ACQUIS";
                    }
                }
            }
            else
            {
                competence += "NON NOTE";
            }

            competence += " - " + this.Competence.Libelle + " (" + this.Competence.Code + ")";
            
            return competence;
        }

        private string GetLibelleNote()
        {
            string note = string.Empty;

            if (this.Note != -1)
            {
                if (this.Ecf.NotationNumerique) note = this.Note + "/20";
                else
                {
                    if (this.Note == Ressources.CONSTANTES.NOTE_ACQUIS) note = "ACQUIS";
                    else if (this.Note == Ressources.CONSTANTES.NOTE_ENCOURS_ACQUISITION)
                        note = "EN COURS D'ACQUISITION";
                    else if (this.Note == Ressources.CONSTANTES.NOTE_NON_ACQUIS)
                        note = "NON ACQUIS";
                }
            }
            else note = "NON NOTE";

            return note;
        }

    }
}
