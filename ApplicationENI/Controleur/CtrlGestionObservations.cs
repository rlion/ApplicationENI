using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApplicationENI.Modele;

namespace ApplicationENI.Controleur {
    class CtrlGestionObservations {
        public List<Observation> listeObservation(Stagiaire stg){
            return stg.listeObservations;   
        }

        public void ajouterObservation(String pTypeObs, String pTitre, String pTexte, Stagiaire pStg) {
            Observation obs = new Observation(Parametres.Instance.login, pTypeObs, pTitre, pTexte, pStg);
            obs.AjouterObservation();
        }

        public void supprimerObservation(int pIndex) {
            Parametres.Instance.stagiaire.listeObservations.RemoveAt(pIndex);
        }

        public void modifierOperation(Observation pObs, String pTypeObs, String pTitre, String pTexte)
        {
            pObs._type = pTypeObs;
            pObs._texte = pTexte;
            pObs._titre = pTitre;
        }

    }
}
