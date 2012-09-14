using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApplicationENI.Modele;

namespace ApplicationENI.Controleur
{
    class CtrlProfilAlertesStagiaire
    {
        public List<Stagiaire> listeStagiaires() 
        {
            Stagiaire stg = new Stagiaire();
            return stg.ListeStagiaire();
        }

        public Stagiaire rechercherStagiaire()
        {
            Stagiaire stg = new Stagiaire();
            List<Stagiaire> listeStg = stg.ListeStagiaire();
            foreach (Stagiaire stag in listeStg)
            {
                stg = stag;
            }
            return stg;
        }
        
    }
}
