using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplicationENI.Modele
{
    public class Formation
    {
        /*Commenté par Mathias, je ne vois pas l'interet d un Guid si il y a déjà un champ codeFormation dans la BDD
         * private Guid _id;

        public Guid Id
        {
            get { return _id; }
            set { _id = value; }
        }*/
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
            //_id=new Guid();
            _code = "";
            _libelle = "";
            _epreuves = null;
        }

        public Formation(String pCode, String pLibelle)
        {
            //_id = new Guid();
            _code = pCode;
            _libelle = pLibelle;
            _epreuves = null;
        }

        public Formation(String pCode, String pLibelle, List<ECF> pEpreuves)
        {
            //_id = new Guid();
            _code = pCode;
            _libelle = pLibelle;
            _epreuves = pEpreuves;
        }

        public void ajouterECF(Formation pFormation,ECF pECF)
        {
            pFormation._epreuves.Add(pECF);
        }

        public List<Cours> listeCours()
        {
           return DAL.JeuDonnees.GetListeCours();
        }


        public override String ToString() {
            return this._libelle;
        }

        public override bool Equals(object obj)
        {
            bool b = false;
            if (this.Code == ((Formation)obj).Code && this.Libelle == ((Formation)obj).Libelle)
            {
                b = true;
            }
            return b;
        }
   }
}
