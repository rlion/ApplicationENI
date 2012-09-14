using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplicationENI.Modele
{
    public class Salle
    {
        private string _codeSalle;
        private string _libelle;

        public string Libelle
        {
            get { return _libelle; }
            set { _libelle = value; }
        }

        public string CodeSalle
        {
            get { return _codeSalle; }
            set { _codeSalle = value; }
        }

        public Salle() { }

        public Salle(string codeSalle, string libelle)
        {
            this._codeSalle = codeSalle;
            this._libelle = libelle;
        }
    }
}
