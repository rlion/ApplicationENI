using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApplicationENI.Modele;

namespace ApplicationENI.DAL
{
    class ContactDAL
    {
        public static Contact rechercherContact(String pNum){
            //TODO: c'est foireux vu qu'il y a pas de bdd derrière.
            return DAL.JeuDonnees.GetContact();
        }
    }
}
