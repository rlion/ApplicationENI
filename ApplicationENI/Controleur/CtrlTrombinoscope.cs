using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApplicationENI.Modele;

namespace ApplicationENI.Controleur {
    class CtrlTrombinoscope 
    {
        public List<Stagiaire> listeStagiaires() 
        {
            Stagiaire stg = new Stagiaire();
            return stg.ListeStagiaire();
        }


            
    }
}
