using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApplicationENI.Modele;

namespace ApplicationENI.Modele
{
    public class SessionECF
    {
        private String _id;              
        private ECF _ecf;
        private DateTime _date;
        private List<Stagiaire> _participants;
        private int _version;

        public String Id
        {
            get { return _id; }
            set { _id = value; }
        }  
        public ECF Ecf
        {
            get { return _ecf; }
            set { _ecf = value; }
        }       
        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }
        public List<Stagiaire> Participants
        {
            get { return _participants; }
            set { _participants = value; }
        }
        public int Version
        {
            get { return _version; }
            set { _version = value; }
        }


        public SessionECF()
        {
            _id = "";
            _ecf = null;
            _date = DateTime.MinValue;
            _participants = new List<Stagiaire>();
            _version = 0;
        }
        public SessionECF(String id, ECF ecf, DateTime date)
        {
            _id = id;
            _ecf = ecf;
            _date = date;
            _version = 0;
            _participants= new List<Stagiaire>();
        }
        public SessionECF(ECF ecf, DateTime date)
        {
            _id = "";
            _ecf = ecf;
            _date = date;
            _version = 0;
            _participants = new List<Stagiaire>();
        }

        public override string ToString()
        {
            return "épreuve " + _ecf.ToString() + " du " + _date.ToShortDateString() + " (version " + _version + ")";
        }
    }
}
