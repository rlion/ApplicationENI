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
        #region Attributs, proprietes et constructeur
        Evaluation _evaluation = null;
        Stagiaire _stagaire = null;
        public Evaluation Evaluation
        {
            get { return _evaluation; }
            set { _evaluation = value; }
        }
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
        #endregion

        #region Evaluation
        public void modifierNoteEvaluation(Evaluation pEvaluation, float pNote)
        {
            EvaluationsDAL.modifierNoteEvaluation(pEvaluation, pNote);
        }
        #endregion
    }
}
