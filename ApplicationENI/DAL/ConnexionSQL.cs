using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace ApplicationENI.DAL {
    public static class ConnexionSQL {
        //private static String chaineCnx = "Data Source=localhost;Initial Catalog=APPLICATION_ENI;User ID=sa";
        private static String chaineCnx = "Data Source=ASDEPIX\\SQLEXPRESS;Initial Catalog=APPLICATION_ENI;User Id=sa;Password=opti;";
        //"Data Source=HERMES-PC\\SQLEXPRESS;Initial Catalog=APPLICATION_ENI;Persist Security Info=True;User ID=sa;Password=Pa$$w0rd";
        //private static String chaineCnx = "Data Source=localhost;Initial Catalog=APPLICATION_ENI;User ID=sa;Password=password";
        //"Data Source=HERMES-PC\\SQLEXPRESS;Initial Catalog=APPLICATION_ENI;Persist Security Info=True;User ID=sa;Password=Pa$$w0rd";

        //"Data Source=ASDEPIX\\SQLEXPRESS;Initial Catalog=APPLICATION_ENI;User Id=sa;Password=opti;"
        //pas ma source : "Data Source=RESEAU-93C88A71;Initial Catalog=eni;User ID=sa"
        //(MAT) ma source : "Data Source=ASDEPIX\\SQLEXPRESS;Initial Catalog=APPLICATION_ENI;User Id=sa;Password=opti;"

        public static SqlConnection CreationConnexion()
        {
            try
            {
                SqlConnection cnx = new SqlConnection(chaineCnx);
                cnx.Open();
                return cnx;
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show("Impossible de se connecter à la base de données : " + e.Message, "Echec de connexion", 
                    System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                
                // si la connexion n'est pas opérationnelle, autant fermer l'appli tout de suite.
                System.Environment.Exit(0);
                return null;
            }
        }
    }
}
