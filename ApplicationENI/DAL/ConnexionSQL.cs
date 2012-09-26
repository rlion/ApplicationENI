using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace ApplicationENI.DAL {
    public static class ConnexionSQL {
        
        private static String chaineCnx = "Data Source=ASDEPIX\\SQLEXPRESS;Initial Catalog=APPLICATION_ENI;User Id=sa;Password=opti;";   //Data Source=RESEAU-93C88A71;Initial Catalog=eni;User ID=sa;
        private static SqlConnection cnx;

        public static SqlConnection CreationConnexion()
        {
            SqlConnection cnx = new SqlConnection(chaineCnx);
            cnx.Open();
            return cnx;
        }
        



    }
}
