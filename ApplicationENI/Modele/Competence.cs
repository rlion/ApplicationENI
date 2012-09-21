using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplicationENI.Modele
{
    public class Competence
    {
        #region Attributs( id, libelle)
        private Guid _id;
        private String _libelle;        
        #endregion

        #region Proprietes
        public Guid Id
        {
            get { return _id; }
            set { _id = value; }
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
            _id = new Guid();
            _libelle = "";
        }
        public Competence(String pLibelle)
        {
            _id = new Guid();
            _libelle = pLibelle;
        }
        #endregion
    }
}
