using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplicationENI.Modele
{
    public class Formation
    {
        private String _code;
        public String Code
        {
            get { return _code; }
            set { _code = value; }
        }

        private String _libelle;

        public String Libelle
        {
            get { return _libelle; }
            set { _libelle = value; }
        }
        private List<ECF> _epreuves;

        public Formation()
        {
            _code = "";
            _libelle = "";
            _epreuves = null;
        }

        public Formation(String pCode, String pLibelle)
        {
            _code = pCode;
            _libelle = pLibelle;
            _epreuves = null;
        }

        public Formation(String pCode, String pLibelle, List<ECF> pEpreuves)
        {
            _code = pCode;
            _libelle = pLibelle;
            _epreuves = pEpreuves;
        }

        public void ajouterECF(Formation pFormation,ECF pECF)
        {
            pFormation._epreuves.Add(pECF);
        }

        public override String ToString() {
            return this._libelle;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType()) return false; 
            
            bool b = false;
            if (this.Code == ((Formation)obj).Code && this.Libelle == ((Formation)obj).Libelle)
            {
                b = true;
            }
            return b;
        }
        //Pour éviter le warning (lorsque l'on surcharge Equals() il faut surcharger GetHashCode)
        //utile pour les HashTable que nous n'utilisons pas
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
   }
}
