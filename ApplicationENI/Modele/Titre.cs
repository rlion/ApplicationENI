using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplicationENI.Modele
{
    public class Titre
    {
        private string codeTitre;
        private string libelleCourt;
        private string libelleLong;
        private string niveau;
        private string codeRome;
        private string codeNSF;
        private DateTime dateCreation;
        private DateTime dateModif;
        private bool titreENI;
        private bool archiver;

        private List<EpreuveTitre> listeEpreuves;

        #region Propriétés

        public string CodeTitre
        {
            get { return codeTitre; }
            set { codeTitre = value; }
        }
        public string LibelleCourt
        {
            get { return libelleCourt; }
            set { libelleCourt = value; }
        }
        public string LibelleLong
        {
            get { return libelleLong; }
            set { libelleLong = value; }
        }
        public string Niveau
        {
            get { return niveau; }
            set { niveau = value; }
        }
        public string CodeRome
        {
            get { return codeRome; }
            set { codeRome = value; }
        }
        public string CodeNSF
        {
            get { return codeNSF; }
            set { codeNSF = value; }
        }
        public DateTime DateCreation
        {
            get { return dateCreation; }
            set { dateCreation = value; }
        }
        public DateTime DateModif
        {
            get { return dateModif; }
            set { dateModif = value; }
        }
        public bool TitreENI
        {
            get { return titreENI; }
            set { titreENI = value; }
        }
        public bool Archiver
        {
            get { return archiver; }
            set { archiver = value; }
        }
        public List<EpreuveTitre> ListeEpreuves
        {
            get { return listeEpreuves; }
            set { listeEpreuves = value; }
        }

        #endregion

        public Titre()
        {
        }

        public Titre(string codeT, string libC, string libL, string nivo, string codeR, string codNSF,
            DateTime dateC, DateTime dateM, bool titre, bool archiv)
        {
            codeTitre = codeT;
            libelleCourt = libC;
            libelleLong = libL;
            niveau = nivo;
            codeRome = codeR;
            codeNSF = codNSF;
            dateCreation = dateC;
            dateModif = dateM;
            titreENI = titre;
            archiver = archiv;
        }

        public Titre(string codeT, string libC, string libL, string nivo, string codeR, string codNSF,
            DateTime dateC, DateTime dateM, bool titre, bool archiv, List<EpreuveTitre> let)
            : this(codeT,libC,libL,nivo,codeR,codNSF,dateC,dateM,titre,archiv)
        {
            listeEpreuves = let;
        }



    }
}
