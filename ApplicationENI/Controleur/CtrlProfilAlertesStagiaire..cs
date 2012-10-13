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

            if (Parametres.Instance.stagiaire.listeAlertes.Count > 0) {
                //foreach(ItemAlerte alerte in Parametres.Instance.stagiaire.listeAlertes
                Parametres.Instance.stagiaire.listeAlertes.RemoveRange(0, Parametres.Instance.stagiaire.listeAlertes.Count);
            }

            GererItemAlerteAbsenceRetard("Absence", Parametres.Instance.stagiaire.nombreAbsences());
            GererItemAlerteAbsenceRetard("Retard", Parametres.Instance.stagiaire.nombreRetards());
            //GererItemAlerte("ECF", DAL.AlerteDAL.nombreRetards(Parametres.Instance.stagiaire));
            //TODO: attente du code de Mathias vérifiant les ECFS non corrigés pour un stagiaire
            /*if (Parametres.Instance.stagiaire.listeECFNonCorriges()!=null) {
                Parametres.Instance.stagiaire.listeAlertes.Add(new ItemAlerte(0, "ECF blablabla (mettre le numéro et l'intitulé)", 0));
                    
            }*/

            return Parametres.Instance.stagiaire.listeAlertes;
        }

        public void GererItemAlerteAbsenceRetard(String type, int nb)
        {
            // manipulation permettant de contourner le fait qu'un Switch ne prend pas d'interval
            int indice = (int)Math.Floor((decimal)nb / 10);
            if (indice>3) {indice=3;}  // pour éviter de mettre en place un grand nombre de cas dans le switch (à partir de 30 abs/ret, il n'y a plus d'augmentation du niveau de criticité).

            
            switch (indice)
            {
                case 1:
                    Parametres.Instance.stagiaire.listeAlertes.Add(new ItemAlerte(0, nb + " " + type + " pour ce stagiaire", 0));
                    break;
                case 2:
                    Parametres.Instance.stagiaire.listeAlertes.Add(new ItemAlerte(1, nb + " " + type + " pour ce stagiaire", 0));
                    break;
                case 3:
                    Parametres.Instance.stagiaire.listeAlertes.Add(new ItemAlerte(2, nb + " " + type + " pour ce stagiaire", 0));
                    break;
                default:
                    break;

            }
        }

    }
}
