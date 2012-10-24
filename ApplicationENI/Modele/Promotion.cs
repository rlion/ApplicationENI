using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplicationENI.Modele
{
    class Promotion
    {
        public String _code { get; set; }
        public String _libelle { get; set; }

        public Promotion(String pCode, String pLibelle) {
            this._code = pCode;
            this._libelle = pLibelle;
        }

        public override string ToString()
        {
            return _code;
        }
    }

}
