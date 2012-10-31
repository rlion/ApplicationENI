using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplicationENI.Modele
{
    public class Formation
    {
        private Guid _id;

        public Guid Id
        {
            get { return _id; }
            set { _id = value; }
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
            _id=new Guid();
            _libelle = "";
            _epreuves = null;
        }

        public Formation(String pLibelle)
        {
            _id = new Guid();
            _libelle = pLibelle;
            _epreuves = null;
        }

        public Formation(String pLibelle,List<ECF> pEpreuves)
        {
            _id = new Guid();
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
   }
}
