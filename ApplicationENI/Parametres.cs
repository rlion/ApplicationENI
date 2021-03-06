﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApplicationENI.Modele;

namespace ApplicationENI {
    public sealed class Parametres {
        private static Parametres instance = null;
        private static readonly object padlock = new object();

        public String login { get; set; }
        public String password { get; set; }
        public Stagiaire stagiaire { get; set; }
        public InfosUtilisateur utilisateur { get; set; }
        //public static Object _instanceFenetreEnCours { get; set; }
        
        //public Parametres() {
        //}

        public static Parametres Instance {
            get {
                lock(padlock) {
                    if(instance == null) {
                        instance = new Parametres();
                    }
                    return instance;
                }
            }
        }

        //public static Object InstanceFenetreEnCours
        //{
        //    get { return instanceFenetre._instanceFenetreEnCours; }
        //    set { instanceFenetre._instanceFenetreEnCours = value; }
        //}
    }
}

