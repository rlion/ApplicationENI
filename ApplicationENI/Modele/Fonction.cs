using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplicationENI.Modele
{
    class Fonction
    {
        public String _codeFonction { get; set; }
        public String _nom { get; set; }

        public Fonction(String pCode, String pNom)
        {
            this._codeFonction = pCode;
            this._nom = pNom;
        }
        public override string ToString()
        {
            return _codeFonction + " -> " + _nom;
        }
    }
}
