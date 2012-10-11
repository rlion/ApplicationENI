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

        public List<ItemAlerte> listeAlertes() {
            if(Parametres.Instance.listAlertes.Count>0 ){
                foreach (ItemAlerte ia in Parametres.Instance.listAlertes) {
                    Parametres.Instance.stagiaire.listeAlertes.Remove(ia);
                }
                if(DAL.AlerteDAL.nombreAbsences(Parametres.Instance.stagiaire)>=10){
                    ItemAlerte al = new ItemAlerte(1, "Plus de 10 absences pour ce stagiaire", 1);
                    Parametres.Instance.stagiaire.listeAlertes.Add(al);
                }
                if(DAL.AlerteDAL.nombreRetards(Parametres.Instance.stagiaire)>=10){
                    ItemAlerte al = new ItemAlerte(1, "Plus de 10 retards pour ce stagiaire", 1);
                    Parametres.Instance.stagiaire.listeAlertes.Add(al);
                }
            }
            return Parametres.Instance.stagiaire.listeAlertes;
        }


    }
}
