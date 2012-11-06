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

        public List<Stagiaire> GetListeStagiaires()
        {
            List<Stagiaire> ls = new List<Stagiaire>();
            ls = DAL.AccueilDAL.GetListeStagiaires();

            return ls;
        }
    }
}
