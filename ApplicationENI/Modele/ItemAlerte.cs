using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplicationENI.Modele
{
    public class ItemAlerte
    {
        public string ICONE { get; set; }
        public string DESCRIPTION { get; set; }
        public string TYPE { get; set; }

        public ItemAlerte()
        {
            
        }

        public ItemAlerte(int codeAlerte, string libelle, int codeType)
        {
            ICONE = GetIcone(codeAlerte);
            TYPE = GetType(codeType);
            DESCRIPTION = libelle;
        }

        //0:information, 1:avertissement, 2:erreur, 3:interdiction
        public string GetIcone(int code)
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
                    return "Retards";
                case 2:
                    return "ECF";
                case 3:
                    return "Observations";
                default:
                    return "Divers";
            }
        }
    }
}
