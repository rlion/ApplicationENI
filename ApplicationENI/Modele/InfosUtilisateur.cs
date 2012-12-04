using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplicationENI.Modele
{
    public class InfosUtilisateur
    {
        public string prenom { get; set; }
        public string nom { get; set; }
        public DateTime derniereConnexion { get; set; }
        public string role { get; set; }
    }
}
