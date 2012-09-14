using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplicationENI.Modele
{
    public class Jury
    {
        private int _idPersonneJury;
        private string _civilite;
        private string _nom;
        private string _prenom;

        public int IdPersonneJury
        {
            get { return _idPersonneJury; }
            set { _idPersonneJury = value; }
        }

        public string Civilite
        {
            get { return _civilite; }
            set { _civilite = value; }
        }

        public string Nom
        {
            get { return _nom; }
            set { _nom = value; }
        }

        public string Prenom
        {
            get { return _prenom; }
            set { _prenom = value; }
        }

        public Jury() { }

        public Jury(int idPersonneJury, string civilite, string nom, string prenom)
        {
            this._idPersonneJury = idPersonneJury;
            this._civilite = civilite;
            this._nom = nom;
            this._prenom = prenom;
        }

    }
}
