using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplicationENI.Modele
{
    public class EpreuveTitre
    {
        private DateTime _dateEpreuve;
        private string _salle;
        private List<Jury> _listeJury;
        private string _titre;

        public string Titre {
            get { return _titre; }
            set { _titre = value; }
        }

        public List<Jury> ListeJury
        {
            get { return _listeJury; }
            set { _listeJury = value; }
        }

        public string Salle
        {
            get { return _salle; }
            set { _salle = value; }
        }

        public DateTime DateEpreuve
        {
            get { return _dateEpreuve; }
            set { _dateEpreuve = value; }
        }

        public EpreuveTitre(){}

        public EpreuveTitre(DateTime dateEpreuve, string salle, string titre) {
            this._dateEpreuve = dateEpreuve;
            this._salle = salle;
            this._titre = titre;
        }

        public EpreuveTitre(DateTime dateEpreuve, string salle, string titre, List<Jury> listeJury)
        {
            this._dateEpreuve = dateEpreuve;
            this._salle = salle;
            this._listeJury = listeJury;
        }
    }
}
