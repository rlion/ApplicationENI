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

        public List<string> GetListeStagiaires()
        {
            List<string> ls = new List<string>();
            ls = DAL.AccueilDAL.GetListeStagiaires().Select(x => x._prenom + " " + x._nom).ToList();

            return ls;
        }
    }
}
