using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplicationENI.Modele
{
    class Entreprise
    {
        public int _codeEntreprise { get; set; }
        public String _raisonSociale { get; set; }
        public String _codePostal { get; set; }
        public String _ville { get; set; }
        public String _tel { get; set; }
        public String _mail { get; set; }

        public override string ToString()
        {
            return _raisonSociale.ToUpper();
        }

        public Entreprise()
        {

        }

        public Entreprise(String pNom, String pCp, String pVille, String pTel, String pMail)
        {
            this._codePostal = pCp;
            this._mail = pMail;
            this._raisonSociale = pNom;
            this._tel = pTel;
            this._ville = pVille;
        }

    }
}
