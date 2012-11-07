using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApplicationENI.Modele;

namespace ApplicationENI.Controleur
{
    public class CtrlAccueilGeneral
    {

        public CtrlAccueilGeneral() { }

        public List<Stagiaire> GetListeStagiaires(string filtre=null)
        {
            List<Stagiaire> ls = new List<Stagiaire>();
            ls = DAL.AccueilDAL.GetListeStagiaires(filtre);

            return ls;
        }

        public Dictionary<string, string> GetListeFormations()
        {
            return DAL.AccueilDAL.GetListeFormations();
        }

        public Dictionary<string, string> GetListePromotions()
        {
            return DAL.AccueilDAL.GetListePromotions();
        }
    }
}
