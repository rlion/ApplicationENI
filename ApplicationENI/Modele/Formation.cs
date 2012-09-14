using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplicationENI.Modele
{
    public class Formation
    {
        private Guid _id;
        private String _libelle;
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
    }
}
