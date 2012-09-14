using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplicationENI.Modele
{
    public class Competence
    {
        private Guid _id;
        private String _libelle;

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
    }
}
