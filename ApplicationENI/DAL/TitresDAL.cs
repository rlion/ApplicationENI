using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApplicationENI.Modele;

namespace ApplicationENI.DAL
{
    public class TitresDAL
    {
        private string test;

        public static List<Titre> GetListeTitres()
        {
            List<Titre> listTitres = new List<Titre>();

            //Récupération jeu de données pour test
            listTitres = JeuDonnees.GetTitres();

            return listTitres;
        }

        public static void AjouterTitre(Titre titre)
        {
        }

        public static void ModifierTitre(Titre titre)
        {
        }

        public static void SupprimerTitre(string codeTitre)
        {
            
        }
    }
}
