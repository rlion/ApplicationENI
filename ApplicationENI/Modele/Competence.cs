using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplicationENI.Modele
{
    public class Competence
    {
        #region Attributs(id, libelle)
        private int _id;
        private String _code;        
        private String _libelle;        
        #endregion

        #region Proprietes
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        public String Code
        {
            get { return _code; }
            set { _code = value; }
        }
        public String Libelle
        {
            get { return _libelle; }
            set { _libelle = value; }
        }
        #endregion

        #region Constructeurs
        public Competence()
        {
            _id = 0;
            _code = "";
            _libelle = "";
        }
        public Competence(String pCode, String pLibelle)
        {
            _id = 0;
            _code = pCode;
            _libelle = pLibelle;
        }
        #endregion

        //Surcharge
        public override string ToString()
        {
            return _code + " - " + _libelle;
        }
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType()) return false;
            
            bool b = false;
            if (this.Id == ((Competence)obj).Id && this.Code == ((Competence)obj).Code && this.Libelle == ((Competence)obj).Libelle)
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
