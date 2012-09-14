using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplicationENI.Modele
{
    public class Evaluation
    {
        private Guid _id;
        private ECF _ecf;
        private Competence _competence;
        private Stagiaire _stagiaire;
        private int _version;
        private int _note; //si numerique entre 0 et 20 - sinom 0 non acquis, 1 en cours, 2 acquis
        private DateTime _date;

    }
}
