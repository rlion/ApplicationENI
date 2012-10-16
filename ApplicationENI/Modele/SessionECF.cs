using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApplicationENI.Modele;

namespace ApplicationENI.Modele
{
    public class SessionECF
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

        public SessionECF()
        {
            _ecf = null;
            _date = DateTime.MinValue;
        }

        public SessionECF(ECF ecf, DateTime date)
        {
            _ecf = ecf;
            _date = date;
        }
    }
}
