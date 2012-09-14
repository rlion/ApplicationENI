using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplicationENI.Modele
{
    public class Cours
    {
        private int _idCours;
        private DateTime _debut;
        private DateTime _fin;
        private int _dureeReelleEnHeures;
        private double _prixPublicAffecte;
        private DateTime _dateCreation;
        private DateTime _dateModif;
        private string _libelleCours;
        private int _dureePrevueEnHeures;

        #region Propriétés

        public int DureePrevueEnHeures
        {
            get { return _dureePrevueEnHeures; }
            set { _dureePrevueEnHeures = value; }
        }

        public string LibelleCours
        {
            get { return _libelleCours; }
            set { _libelleCours = value; }
        }

        public DateTime DateModif
        {
            get { return _dateModif; }
            set { _dateModif = value; }
        }

        public DateTime DateCreation
        {
            get { return _dateCreation; }
            set { _dateCreation = value; }
        }

        public double PrixPublicAffecte
        {
            get { return _prixPublicAffecte; }
            set { _prixPublicAffecte = value; }
        }

        public int DureeReelleEnHeures
        {
            get { return _dureeReelleEnHeures; }
            set { _dureeReelleEnHeures = value; }
        }

        public DateTime Fin
        {
            get { return _fin; }
            set { _fin = value; }
        }

        public DateTime Debut
        {
            get { return _debut; }
            set { _debut = value; }
        }

        public int IdCours
        {
            get { return _idCours; }
            set { _idCours = value; }
        }

        #endregion

        public Cours() { }

        public Cours(int idCours, DateTime debut, DateTime fin, int dureePHeure, int dureeRHeure, double prix,
            DateTime dateC, DateTime dateM, string libCours)
        {
            this._idCours = idCours;
            this._debut = debut;
            this._fin = fin;
            this._dureePrevueEnHeures = dureePHeure;
            this._dureeReelleEnHeures = dureeRHeure;
            this._prixPublicAffecte = prix;
            this._dateCreation = dateC;
            this._dateModif = dateM;
            this._libelleCours = libCours;
        }

    }
}
