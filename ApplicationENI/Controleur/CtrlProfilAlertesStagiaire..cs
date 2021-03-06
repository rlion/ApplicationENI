﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApplicationENI.Modele;

namespace ApplicationENI.Controleur
{
    class CtrlProfilAlertesStagiaire
    {
        static List<ItemAlerte> listeDesAlertes;
        public List<ItemAlerte> listeAlertes() {
            listeDesAlertes = new List<ItemAlerte>();
            
            List<Absence> listeAbsences = DAL.AbsencesDAL.getListeAbsences(Parametres.Instance.stagiaire);

            GererItemAlerteAbsenceRetard("Absence", listeAbsences.Count(x => x._isAbsence==true));
            GererItemAlerteAbsenceRetard("Retard", listeAbsences.Count(x => x._isAbsence == false));

            GererItemAlarmesTemporairesNonCompletees(listeAbsences.Count(x => x._raison == ""));

            List<ECF> lesECFsNonCorriges=DAL.EvaluationsDAL.getListeECFsNonCorriges(Parametres.Instance.stagiaire);
            if (lesECFsNonCorriges!=null)
            {
                GererItemAlarmesECFNonCorrigé(lesECFsNonCorriges.Count);
            }            

            return listeDesAlertes;
        }

        public void GererItemAlerteAbsenceRetard(String type, int nb)
        {
            // manipulation permettant de contourner le fait qu'un Switch ne prend pas d'intervales
            int indice = (int)Math.Floor((decimal)nb / 10);
            if (indice>3) {indice=3;}  // pour éviter de mettre en place un grand nombre de cas dans le switch (à partir de 30 absences/retards, il n'y a plus d'augmentation du niveau de criticité).

            
            switch (indice)
            {
                case 1:
                    listeDesAlertes.Add(new ItemAlerte(0, nb + " " + type + " pour ce stagiaire", 0));
                    break;
                case 2:
                    listeDesAlertes.Add(new ItemAlerte(1, nb + " " + type + " pour ce stagiaire", 0));
                    break;
                case 3:
                    listeDesAlertes.Add(new ItemAlerte(2, nb + " " + type + " pour ce stagiaire", 0));
                    break;
                default:
                    break;

            }
        }
        public void GererItemAlarmesTemporairesNonCompletees(int nb)
        {
            if (nb > 0){
            listeDesAlertes.Add(new ItemAlerte(0, nb + " absence(s)/retard(s) à compléter pour ce stagiaire", 0));
            }
        }

        public void GererItemAlarmesECFNonCorrigé(int nb)
        {
            if (nb > 0)
            {
                listeDesAlertes.Add(new ItemAlerte(0, nb + " ECF(s) à corriger pour ce stagiaire", 2));
            }
        }

        public void ajouterContact(Contact pC) {
            DAL.ContactDAL.ajouterContact(pC);
        }

        public bool supprimerContact(Contact pC) {
            return DAL.ContactDAL.supprimerContact(pC);
        }

        public void modifierContact(Contact pC) {
            DAL.ContactDAL.modifierContact(pC);
        }

        public List<Entreprise> listeEntreprises() {
            return DAL.EntrepriseDAL.getListeEntreprises();
        }

        public void ajouterEntreprise(Entreprise pE) {
            DAL.EntrepriseDAL.ajouterEntreprise(pE);
        }

        public List<Fonction> listeFonctions() {
            return DAL.FonctionDAL.listeFonctions();
        }
    }
}
