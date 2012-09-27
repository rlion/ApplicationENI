using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace ApplicationENI
{
    static class instanceFenetre
    {
        private static Object _instanceFenetreEnCours = null;
        public static Object InstanceFenetreEnCours
        {
            get { return instanceFenetre._instanceFenetreEnCours; }
            set { instanceFenetre._instanceFenetreEnCours = value; }
        }

    }
}
