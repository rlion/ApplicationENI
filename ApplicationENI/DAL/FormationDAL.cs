using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApplicationENI.Modele;

namespace ApplicationENI.DAL
{
    class FormationDAL
    {
        public static List<Formation> listeFormations() {
            return DAL.JeuDonnees.GetListeFormation();
        }

    }
}
