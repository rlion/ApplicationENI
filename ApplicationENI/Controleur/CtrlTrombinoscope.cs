using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApplicationENI.Modele;

namespace ApplicationENI.Controleur {
    class CtrlTrombinoscope     
    {
        public List<Stagiaire> listeStagiaires(String filtre = null)
        {
            List<Stagiaire> liste = new List<Stagiaire>();
            liste = DAL.AccueilDAL.GetListeStagiaires(filtre);

            return liste;
        }

        public Dictionary<String, String> GetListeFormations()
        {
            return DAL.AccueilDAL.GetListeFormations();
        }

        public Dictionary<String, String> GetListePromotions()
        {
            return DAL.AccueilDAL.GetListePromotions();
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
