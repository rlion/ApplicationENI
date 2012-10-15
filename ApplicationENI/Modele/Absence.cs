using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplicationENI.Modele
{
    public class Absence
    {
        public int _id { get; set; }
        public String _raison { get; set; }
        public String _commentaire { get; set; }
        public DateTime _dateDebut { get; set; }
        public DateTime _dateFin { get; set; }
        public TimeSpan _duree { get; set; }
        public bool _valide { get; set; }
		public Stagiaire _stagiaire { get; set; }
        public bool _isAbsence { get; set; }

        public Absence(String pRaison, String pCommentaire, DateTime pDateDebut, DateTime pDateFin, TimeSpan pDuree, bool pValide, Stagiaire pStagiaire, bool pIsAbsence)
        {
            this._commentaire = pCommentaire;
            this._dateDebut = pDateDebut;
            this._dateFin = pDateFin;
            this._duree = pDuree;
            this._raison = pRaison;
            this._valide = pValide;
            this._stagiaire = pStagiaire;
            this._isAbsence = pIsAbsence;
        }

        public Absence() 
        { 
            
        }

        public void ajouterAbsence()
        {
            DAL.AbsencesDAL.ajouterAbsence(this);
            
        }

        public void supprimerAbsence()
        {
            DAL.AbsencesDAL.supprimerAbsence(this);
        }

        public void modifierAbsence()
        {
            DAL.AbsencesDAL.modifierAbsence(this);
        }
    }
}
