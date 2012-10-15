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

            TimeSpan duree = dateFin - dateDebut;

            // on détermine si c'est une absence ou un retard.
            bool isAbsence;
            if (pAbsence == true) {
                isAbsence = true;
            }
            else{
                isAbsence = false;
            }

            Absence a = new Absence(pRaison, pCommentaire, dateDebut, dateFin, duree, pValide, Parametres.Instance.stagiaire, isAbsence);
            a.ajouterAbsence();
        }


        public DateTime conversionStringEnDate(String pDate, int pHeure, int pMinute)
        {
            int jour, mois, an;
            jour = int.Parse(pDate.Substring(0, 2));
            mois = int.Parse(pDate.Substring(3, 2));
            an = int.Parse(pDate.Substring(6, 4));
            DateTime date = new DateTime(an, mois, jour, pHeure, pMinute, 0, DateTimeKind.Utc);
            
            return date;
        }

        public List<Absence> getListAbsences() {
            return Parametres.Instance.stagiaire.listeAbsences;
        }

        public void supprimerAbsence(Absence a) {
            a.supprimerAbsence();
        }

        public void modifierAbsence(Absence pA, String pDateDebut, String pDateFin, int pHeureDeb, int pMinuteDeb, int pHeureFin, int pMinuteFin, String pRaison, String pCommentaire, bool pValide, bool pAbsence) {
            DateTime dateDebut = conversionStringEnDate(pDateDebut, pHeureDeb, pMinuteDeb);
            DateTime dateFin = conversionStringEnDate(pDateFin, pHeureFin, pMinuteFin);
            
            TimeSpan duree = dateFin - dateDebut;

            // on détermine si c'est une absence ou un retard.
            bool isAbsence;
            if (pAbsence == true) {
                isAbsence = true;
            }
            else{
                isAbsence = false;
            }

            pA._commentaire = pCommentaire;
            pA._dateDebut = dateDebut;
            pA._dateFin = dateFin;
            pA._duree = duree;
            pA._isAbsence = isAbsence;
            pA._raison = pRaison;
            pA._valide = pValide;
            pA.modifierAbsence();
        }
    }
}
