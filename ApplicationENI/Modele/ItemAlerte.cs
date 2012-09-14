using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplicationENI.Modele
{
    public class ItemAlerte
    {
        private string icone;
        private string theme;
        private string description;

        public ItemAlerte()
        {
            icone = GetIcone(3);
            theme = "Passage titre";
            description = "Boubou est bleu";
        }

        public ItemAlerte(int codeAlerte, string titre, string libelle)
        {
            icone = GetIcone(codeAlerte);
            theme = titre;
            description = libelle;
        }

        public string ICONE
        {
            get { return icone; }
        }

        public string THEME
        {
            get { return theme; }
        }

        public string DESCRIPTION
        {
            get { return description; }
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
    }
}
