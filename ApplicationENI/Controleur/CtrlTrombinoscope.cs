using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApplicationENI.Modele;

namespace ApplicationENI.Controleur {
    class CtrlTrombinoscope     
    {
        public List<Stagiaire> listeStagiaires(String pNomPromo)
        {
            return DAL.PromotionDAL.listeStagiaires(pNomPromo);
        }

        public List<Formation> listeFormation()
        {
            return new Formations().listeFormations;
        }

        public List<Cours> listeCours(Formation pF)
        {
            return pF.listeCours();
        }

        public List<Promotion> listePromotion() {
            return DAL.PromotionDAL.listePromotions();
        }
    }
}
