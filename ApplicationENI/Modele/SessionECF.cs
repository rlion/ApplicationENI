using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApplicationENI.Modele;

namespace ApplicationENI.Modele
{
    public class SessionECF
    {
        private int _id;
        private ECF _ecf;
        private DateTime _date;
        private List<Stagiaire> _participants;
        private int _version;

        public int Id
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

        //Propriétés pour le rapport
        public string Ecf_r
        {
            get { return _ecf.Libelle; }
        }
        public int EcfId_r
        {
            get { return _ecf.Id; }
        }

        public SessionECF()
        {
            _id = 0;
            _ecf = null;
            _date = DateTime.MinValue;
            _participants = new List<Stagiaire>();
            _version = 0;
        }
        public SessionECF(int id, ECF ecf, DateTime date)
        {
            _id = id;
            _ecf = ecf;
            _date = date;
            _version = 0;
            _participants= new List<Stagiaire>();
        }
        public SessionECF(ECF ecf, DateTime date, int version)
        {
            _id = 0;
            _ecf = ecf;
            _date = date;
            _version = version;
            _participants = new List<Stagiaire>();
        }
        public SessionECF(ECF ecf, DateTime date, int version,Stagiaire stagiaire)
        {
            _id = 0;
            _ecf = ecf;
            _date = date;
            _version = version;
            _participants = new List<Stagiaire>();
            _participants.Add(stagiaire);
        }
        public SessionECF(ECF ecf, DateTime date, int version,List<Stagiaire> lesParticipants)
        {
            _id = 0;
            _ecf = ecf;
            _date = date;
            _version = version;
            _participants = lesParticipants;
        }

        public override string ToString()
        {
            return "épreuve " + _ecf.ToString() + " du " + _date.ToShortDateString() + " (version " + _version + ")";
        }
    }
}
