using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplicationENI.Modele
{
    public class PassageTitre
    {
        private string codeTitre;
        private int codeStagiaire;
        private DateTime datePassage;
        private bool estObtenu;
        private bool estValide;

        #region Propriétés

        public bool EstValide
        {
            get { return estValide; }
            set { estValide = value; }
        }

        public bool EstObtenu
        {
            get { return estObtenu; }
            set { estObtenu = value; }
        }

        public DateTime DatePassage
        {
            get { return datePassage; }
            set { datePassage = value; }
        }

        public int CodeStagiaire
        {
            get { return codeStagiaire; }
            set { codeStagiaire = value; }
        }

        public string CodeTitre
        {
            get { return codeTitre; }
            set { codeTitre = value; }
        }

        #endregion

        public PassageTitre() { }

        public PassageTitre(string codeTitre, int codeStagiaire)
        {
            this.codeStagiaire = codeStagiaire;
            this.codeTitre = codeTitre;
            this.datePassage = DateTime.Now;
            this.estObtenu = false;
            this.estValide = false;
        }

        public PassageTitre(string codeTitre, int codeStagiaire, DateTime datePassage, bool estObtenu, bool estValide)
        {
            this.codeStagiaire = codeStagiaire;
            this.codeTitre = codeTitre;
            this.datePassage = datePassage;
            this.estObtenu = estObtenu;
            this.estValide = estValide;
        }
    }
}
