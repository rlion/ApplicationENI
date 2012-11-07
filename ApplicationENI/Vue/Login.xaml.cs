using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.DirectoryServices;

namespace ApplicationENI.Vue
{
    /// <summary>
    /// Logique d'interaction pour login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();

            this.textBox1.Text = "jgabillaud";
            this.passwordBox1.Password = "password";
        }

        //Bouton "Valider"
        private void button2_Click(object sender, RoutedEventArgs e)
        {
            Parametres.Instance.login = "jgabillaud";
            if (this.textBox1.Text != "jgabillaud")
            {
                if (authentificationOk(this.textBox1.Text, this.passwordBox1.Password))
                {
                    //initialisation des paramètres
                    Parametres.Instance.login = this.textBox1.Text;

                }
            }
            this.Close();
        }

        //Bouton "Annuler"
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Close();
            this.Close();
        }

        public bool authentificationOk(String login, String password)
        {
            Guid guid;
            try
            {
                //TODO : pour les tests, on utilise le domaine STAGIAIRES, mais les utilisteurs de l'appli utiliseront un autre domaine.
                DirectoryEntry Ldap = new DirectoryEntry("LDAP://STAGIAIRES.local", login, password, AuthenticationTypes.Secure);
                guid = Ldap.Guid;
            }
            catch (Exception)
            {
                
                throw;
            }
            
            if (guid == null) return false;
            return true;
        }
    }
}
