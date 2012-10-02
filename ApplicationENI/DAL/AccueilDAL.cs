using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApplicationENI.Modele;

namespace ApplicationENI.DAL
{
    public class AccueilDAL
    {
        public static List<Stagiaire> GetListeStagiaires()
        {
            return DAL.JeuDonnees.GetListeStagiaire();
        }
    }
}
