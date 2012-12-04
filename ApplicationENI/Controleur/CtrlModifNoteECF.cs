using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApplicationENI.Modele;
using ApplicationENI.DAL;

namespace ApplicationENI.Controleur
{
    class CtrlModifNoteECF
    {
        //TODO regions

        Evaluation _evaluation = null;

        public Evaluation Evaluation
        {
            get { return _evaluation; }
            set { _evaluation = value; }
        }
        Stagiaire _stagaire = null;

        public Stagiaire Stagaire
        {
            get { return _stagaire; }
            set { _stagaire = value; }
        }

        public CtrlModifNoteECF()
        {
            _evaluation = null;
            _stagaire = null;
        }

        public void modifierNoteEvaluation(Evaluation pEvaluation, float pNote)
        {
            EvaluationsDAL.modifierNoteEvaluation(pEvaluation, pNote);
        }
    }
}
