using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApplicationENI.Modele;

namespace ApplicationENI.Modele
{
    class SessionECF
    {
        private ECF _ecf;

        public ECF Ecf
        {
            get { return _ecf; }
            set { _ecf = value; }
        }
        private DateTime _date;

        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }
    }
}
