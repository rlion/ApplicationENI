using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApplicationENI.Modele;

namespace ApplicationENI.Controleur
{
    public class CtrlGestionPassageTitre
    {
        private List<Titre> listTitres;
        private Dictionary<string, string> dictTitre;
        private Dictionary<int,string> dictNiveau;
        private Titre histoTitre;

        public Titre HistoTitre
        {
            get 
            {
                //On remplace le titre bindé par histoTitre dans la liste, puis on 
                //retourne l'élément de cette liste
                int i = listTitres.IndexOf(listTitres.Where(x => x.CodeTitre.
                    Equals(histoTitre.CodeTitre)).First());
                listTitres[i] = new Titre(histoTitre.CodeTitre,histoTitre.LibelleCourt,
                    histoTitre.LibelleLong,histoTitre.Niveau,histoTitre.CodeRome,
                    histoTitre.CodeNSF,histoTitre.DateCreation,histoTitre.DateModif,
                    histoTitre.TitreENI,histoTitre.Archiver);

                return listTitres[i];
            }
        }

        public CtrlGestionPassageTitre()
        {
            listTitres = DAL.TitresDAL.GetListeTitres();

            InitDictionnaires();
        }

        private void InitDictionnaires()
        {
            dictTitre = new Dictionary<string, string>();
            foreach(Titre t in listTitres.OrderBy(x => x.CodeTitre))
                dictTitre.Add(t.CodeTitre, t.CodeTitre);

            dictNiveau = new Dictionary<int, string>();
            int i = 1;
            foreach(string s in listTitres.OrderBy(x => x.Niveau).Select(x => x.Niveau).Distinct())
            {
                dictNiveau.Add(i, s);
                i++;
            }
        }

        public Dictionary<string, string> GetListeCodeTitre()
        {
            return dictTitre;
        }

        public Dictionary<int, string> GetListeNiveaux()
        {
            return dictNiveau;
        }

        public Titre GetTitre(string codeTitre)
        {
            Titre t = listTitres.Where(x => x.CodeTitre.Equals(codeTitre)).First();

            //Il faut créer un nouvel objet titre pour que l'historique fonctionne bien.
            //Sinon, avec le binding, la liste du contrôleur est automatiquement modifiée.
            histoTitre = new Titre(t.CodeTitre,t.LibelleCourt,t.LibelleLong,t.Niveau,
                t.CodeRome,t.CodeNSF,t.DateCreation,t.DateModif,t.TitreENI,t.Archiver);
            return t;
        }

        public List<Salle> GetSalles() 
        {
            return DAL.TitresDAL.GetListeSalles();
        }

        //THIS IS IT boubou!
        public void AjouterTitre(Titre titre)
        {
            if(!string.IsNullOrEmpty(titre.CodeTitre) && !string.IsNullOrEmpty(titre.LibelleCourt)) 
            {
                titre.DateCreation = DateTime.Now;
                titre.DateModif = titre.DateCreation;
                if(DAL.TitresDAL.AjouterTitre(titre) > 0) 
                {
                    listTitres.Add(titre);
                    InitDictionnaires();
                }
            } 
            else 
            {
                System.Windows.MessageBox.Show("Le code et le libellé court du titre doivent être obligatoirement renseignés!",
                    "Ajout titre", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Stop);
            }
        }

        public void ModifierTitre(Titre titre)
        {
            if(!string.IsNullOrEmpty(titre.LibelleCourt)) 
            {
                titre.DateModif = DateTime.Now;
                if(DAL.TitresDAL.ModifierTitre(titre) > 0) 
                {
                    histoTitre = new Titre(titre.CodeTitre, titre.LibelleCourt, titre.LibelleLong,
                        titre.Niveau, titre.CodeRome, titre.CodeNSF, titre.DateCreation,
                        titre.DateModif, titre.TitreENI, titre.Archiver, titre.ListeEpreuves);

                    System.Windows.MessageBox.Show("Le titre a bien été modifié!", "Modification titre", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                }
            } 
            else 
            {
                System.Windows.MessageBox.Show("Le libellé court du titre doit être obligatoirement renseigné!",
                    "Modification titre", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Stop);
            }
        }

        //THIS IS IT boubou!
        public void SupprimerTitre(string codeTitre)
        {
            if(DAL.TitresDAL.SupprimerTitre(codeTitre) != -1) 
            {
                Titre titreToDelete = listTitres.Where(x => x.CodeTitre.Equals(codeTitre)).First();
                listTitres.Remove(titreToDelete);
                dictTitre.Remove(titreToDelete.CodeTitre);
            }
        }
    }
}
