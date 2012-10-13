using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplicationENI.Modele
{
    class Formations
    {
        public List<Formation> listeFormations { get; set; }

        public Formations()
        {
            listeFormations = DAL.FormationDAL.listeFormations();
        }

    }
}
