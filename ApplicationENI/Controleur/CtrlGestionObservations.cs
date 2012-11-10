﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApplicationENI.Modele;

namespace ApplicationENI.Controleur {
    class CtrlGestionObservations {
        public List<Observation> listeObservation(Stagiaire stg){
            return stg.getListeObservations(); 
        }

        public void ajouterObservation(String pTypeObs, String pTitre, String pTexte, Stagiaire pStg) {
            Observation obs = new Observation(Parametres.Instance.login, pTypeObs, pTitre, pTexte, pStg);
            obs.AjouterObservation();
        }

        public void supprimerObservation(Observation pObs, int pIndex) {
            pObs.SupprimerObservation();
        }

        public void modifierOperation(Observation pObs, String pTypeObs, String pTitre, String pTexte)
        {
            pObs._type = pTypeObs;
            pObs._texte = pTexte;
            pObs._titre = pTitre;
            pObs.ModifierObservation();
        }

    }
}
