using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApplicationENI.Modele;

namespace ApplicationENI.Controleur {
    class CtrlGestionAbsences {
        public void AjouterAbsence(String pDateDebut, String pDateFin, int pHeureDeb, int pMinuteDeb, int pHeureFin, int pMinuteFin, String pRaison, String pCommentaire, bool pValide, bool pAbsence, bool pRetard)
        {
            DateTime dateDebut = conversionStringEnDate(pDateDebut, pHeureDeb, pMinuteDeb);
            DateTime dateFin = conversionStringEnDate(pDateFin, pHeureFin, pMinuteFin);

            TimeSpan duree = dateDebut - dateFin;

            Absence a = new Absence(pRaison, pCommentaire, Parametres.Instance.login, dateDebut, dateFin, duree, pValide, Parametres.Instance.stagiaire);
            a.ajouterAbsence();
        }


        public DateTime conversionStringEnDate(String pDate, int pHeure, int pMinute)
        {
            int jour, mois, an;
            jour = int.Parse(pDate.Substring(0, 2));
            mois = int.Parse(pDate.Substring(3, 2));
            an = int.Parse(pDate.Substring(6, 4));
            DateTime date = new DateTime(an, mois, jour, pHeure, pMinute, 0);
            return date;
        }

        public List<Absence> getListAbsences() {
            // TODO: ici ou dans la classe Absence mettre en forme les propriétés
            return Parametres.Instance.stagiaire.listeAbsences;
        }

    }
}
