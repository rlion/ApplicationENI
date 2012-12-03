using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplicationENI.Modele
{
    public class Observation
    {
        public int _id { get; set; }
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
            this._id = 1;
            this._texte = pTexte;
            this._titre = pTitre;
            this._type = pType;
            this._stagiaire = pStagiaire;
        }

        public Observation()
        {
            this._date = DateTime.Now;
        }

    }
}
