using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplicationENI.Modele
{
    public class Competence
    {
        #region Attributs(id, libelle)
        private String _id;
        private String _code;        
        private String _libelle;        
        #endregion

        #region Proprietes
        public String Id
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
            _id = "";
            _code = "";
            _libelle = "";
        }
        public Competence(String pCode, String pLibelle)
        {
            _id = "";
            _code = pCode;
            _libelle = pLibelle;
        }
        #endregion

        public override string ToString()
        {
            return _code + " - " + _libelle;
        }
    }
}
