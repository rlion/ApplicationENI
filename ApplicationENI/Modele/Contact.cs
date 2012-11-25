using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplicationENI.Modele
{
    public class Contact
    {
        public int _codeContact { get; set; }
        public String _nom { get; set; }
        public String _prenom { get; set; }
        public String _telFixe { get; set; }
        public String _telMobile { get; set; }
        public String _fax { get; set; }
        public String _email { get; set; }
        public String _observation { get; set; }
        public String _archive { get; set; }
        public String _civilite { get; set; }
        public Entreprise _Entreprise { get; set; }
        public String _codeFonction { get; set; }

        public Contact(int pCodeContact, String pNom, String pPrenom, String pTelFixe, String pTelMobile, String pFax, String pEmail, String pObs, String pArchive, String pCivilite, Entreprise pEntreprise, String pCodeFonction)
        {
            this._archive = pArchive;
            this._civilite = pCivilite;
            this._codeContact = pCodeContact;
            this._email = pEmail;
            this._fax = pFax;
            this._nom = pNom;
            this._observation = pObs;
            this._prenom = pPrenom;
            this._telFixe = pTelFixe;
            this._telMobile = pTelMobile;
            this._Entreprise = pEntreprise;
            this._codeFonction = pCodeFonction;
        }

        public Contact()
        {
        }

    }
}
