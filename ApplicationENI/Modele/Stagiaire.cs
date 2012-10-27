﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplicationENI.Modele
{
    public class Stagiaire
    {
        // utilisation des propriétés simplifiées 
        public int _id { get; set; }
        public String _civilité { get; set; }
        public String _nom { get; set; }
        public String _prenom { get; set; }
        public String _adresse1 { get; set; }
        public String _adresse2 { get; set; }
        public String _adresse3 { get; set; }
        public String _cp { get; set; }
        public String _ville { get; set; }
        public String _telephonePortable { get; set; }
        public String _telephoneFixe { get; set; }
        public String _email { get; set; }
        public DateTime _dateNaissance { get; set; }
        public String _codeRegion { get; set; }
        public String _codeNationalité { get; set; }
        public String _codeOrigineMedia { get; set; }
        public DateTime _datePremierEnvoiDoc { get; set; }
        public DateTime _dateCreation { get; set; }
        public String _repertoire { get; set; }
        public bool _permis { get; set; }
        public String _photo { get; set; }
        public bool _envoiDocEnCours { get; set; }
        public String _historique { get; set; }
        public Contact _tuteur { get; set; }
        //public List<Evaluation> _resultats;
        public List<Observation> listeObservations { get; set; }
		public List<Absence> listeAbsences { get; set; }
        public List<ItemAlerte> listeAlertes { get; set; }

        
        public Stagiaire(int pId, String pCivilité, String pNom, String pPrenom, String pAdresse1, String pAdresse2, String pAdresse3,
            String pCp, String pVille, String pTelephonePortable, String pTelephoneFixe, String pEmail, DateTime pDateNaissance,
            String pCodeRegion, String pCodeNationalité, String pCodeOrigineMedia, DateTime pDatePremierEnvoiDoc, DateTime pDateCreation,
            String pRepertoire, bool pPermis, String pPhoto, bool pEnvoiDocEnCours, String pHistorique)
        {
            this._id = pId;
            this._adresse1 = pAdresse1;
            this._adresse2 = pAdresse2;
            this._adresse3 = pAdresse3;
            this._civilité = pCivilité;
            this._codeNationalité = pCodeNationalité;
            this._codeOrigineMedia = pCodeOrigineMedia;
            this._codeRegion = pCodeRegion;
            this._cp = pCp;
            this._dateCreation = pDateCreation;
            this._dateNaissance = pDateNaissance;
            this._datePremierEnvoiDoc = pDatePremierEnvoiDoc;
            this._email = pEmail;
            this._envoiDocEnCours = pEnvoiDocEnCours;
            this._historique = pHistorique;
             this._nom = pNom;
            this._permis = pPermis;
            this._photo = pPhoto;
            this._prenom = pPrenom;
            this._repertoire = pRepertoire;
            this._telephoneFixe = pTelephoneFixe;
            this._telephonePortable = pTelephonePortable;
            this._ville = pVille;
            this._tuteur = DAL.ContactDAL.rechercherContact(pId);
            this.listeObservations = new List<Observation>();
            this.listeObservations = DAL.ObservationsDAL.getListObservations(this);
            this.listeAbsences = new List<Absence>();
            this.listeAbsences = DAL.AbsencesDAL.getListeAbsences(this);
            this.listeAlertes = new List<ItemAlerte>();
        }

        public Stagiaire ()
	    {
     
	    }

        public Stagiaire (String pNom, String pPrenom)
	    {
            this._id = 1;   
            this._nom = pNom;
            this._prenom = pPrenom;
	    }

        public void modifierStagiaire()
        {
            DAL.StagiairesDAL.modifierStagiaire(this);
        }

        public void supprimerStagiaire()
        {
            DAL.StagiairesDAL.supprimerStagiaire(this);
        }

        public int nombreAbsences() {
            return DAL.AlerteDAL.nombreAbsences(this);
        }

        public int nombreRetards()
        {
            return DAL.AlerteDAL.nombreRetards(this);
        }

        public List<ECF> listeECFNonCorriges() {
            //TODO: Attente Mathias
            //return DAL.ECFDAL.getListeEcfNonCorriges(Stagiaire stg);
            //    ou
            // return DAL.StagiairesDAL.getListeEcfNonCorriges(Stagiaire stg);
            return null;
        }
    }   

    
}
