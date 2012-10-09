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

        public EpreuveTitre(DateTime dateEpreuve, string salle, List<Jury> listeJury)
        {
            this._dateEpreuve = dateEpreuve;
            this._salle = salle;
            this._listeJury = listeJury;
        }
    }
}
