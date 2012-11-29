﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApplicationENI.Modele;

namespace ApplicationENI.Controleur
{
    public class CtrlInscriptionTitre
    {

        public CtrlInscriptionTitre() { }

        public PassageTitre GetPassageTitre(int codeStagiaire, string codeTitre)
        {
            return DAL.TitresDAL.GetPassageTitre(codeStagiaire, codeTitre);
        }

        public bool CheckIfInscrit(int codeStagiaire)
        {
            return DAL.TitresDAL.ControlerSiInscrit(codeStagiaire);
        }

        public KeyValuePair<string, string> GetInfosTitre(int codeStagiaire)
        {
            return DAL.TitresDAL.GetInfosTitre(codeStagiaire).GetValueOrDefault();
        }

        public int InscrireStagiaire(PassageTitre passageT)
        {
            return DAL.TitresDAL.AjouterPassageTitre(passageT);
        }

        public int UpdateInscription(PassageTitre passageT)
        {
            return DAL.TitresDAL.UpdatePassageTitre(passageT);
        }

        public string GetFormationStagiaire(int codeStagiaire)
        {
            return DAL.TitresDAL.GetFormationStagiaire(codeStagiaire);
        }
    }
}
