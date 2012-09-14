using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplicationENI.Modele
{
    public class Observation
    {
        private Contact con;
        private string p;
        private string p_2;
        private string p_3;
        private Stagiaire pStg;

        public Guid _id { get; set; }
        public DateTime _date { get; set; }
        public String _nomAuteur { get; set; }
        public String _type { get; set; }
        public String _titre { get; set; }
        public String _texte { get; set; }
        public Stagiaire _stagiaire { get; set; }

        public Observation(String pAuteur, String pType, String pTitre, String pTexte, Stagiaire pStagiaire)
        {
            this._nomAuteur = pAuteur;
            this._date = DateTime.Now;
            this._id = Guid.NewGuid();
            this._texte = pTexte;
            this._titre = pTitre;
            this._type = pType;
            this._stagiaire = pStagiaire;
        }

        public Observation()
        {
            this._date = DateTime.Now;
        }

       

        public void AjouterObservation() {
            DAL.ObservationsDAL.ajouterObservation(this);
        }

        public void ModifierObservation()
        {
            DAL.ObservationsDAL.modifierObservation(this);
        }

        public void SupprimerObservation()
        {
            DAL.ObservationsDAL.supprimerObservation(this);
        }
    }
}
