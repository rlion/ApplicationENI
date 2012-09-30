using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplicationENI.Modele
{
    public class ItemAlerte
    {
        private string icone;
        private string description;
        private string type;

        public ItemAlerte()
        {
            icone = GetIcone(3);
            description = "Boubou est bleu";
            type = "";
        }

        public ItemAlerte(int codeAlerte, string libelle, int codeType)
        {
            icone = GetIcone(codeAlerte);
            type = GetType(codeType);
            description = libelle;
        }

        public string ICONE
        {
            get { return icone; }
        }

        public string DESCRIPTION
        {
            get { return description; }
        }

        public String TYPE
        {
            get { return type; }
        }

        //0:information, 1:avertissement, 2:erreur, 3:interdiction
        private string GetIcone(int code)
        {
            switch (code)
            {
                case 0:
                    return @"..\Images\info.png";
                case 1:
                    return @"..\Images\warning.png";
                case 2:
                    return @"..\Images\erreur.png";
                case 3:
                    return @"..\Images\error.png";
                default:
                    return @"..\Images\ledgreen.png";
            }
        }

        private String GetType(int code)
        { 
            switch (code)
            {
                case 0:
                    return "Absences";
                case 1:
                    return "ECF";
                case 2:
                    return "Observations";
                default:
                    return "Divers";
            }


        }

        public void EnregistrerAlerte() {
            DAL.AlerteDAL.AjouterAlerte(this);
        }

        public void ModifierAlerte() {
            DAL.AlerteDAL.ModifierAlerte(this);
        }

        public void SupprimerAlerte() {
            DAL.AlerteDAL.SupprimerAlerte(this);
        }

    }
}
