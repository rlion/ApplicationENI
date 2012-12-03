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
using System.Security.Cryptography;

namespace ApplicationENI.Vue
{
    /// <summary>
    /// Logique d'interaction pour login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private const string MDPHACHE = "90ecc336d6200b1389eb49c4b557ee42892345c2f727453ae82c96e6de94098e";

        public Login()
        {
            InitializeComponent();

            //TODO: supprimer ces deux lignes avant d'envoyer les livrables
            this.textBox1.Text = "admin";
            this.passwordBox1.Password = "P@$$w0rd";
        }

        //Bouton "Valider"
        private void button2_Click(object sender, RoutedEventArgs e)
        {
            if (authentificationOk(this.textBox1.Text, this.passwordBox1.Password))
            {
                //initialisation des paramètres
                Parametres.Instance.login = this.textBox1.Text;
                this.Close();
            }
            else
            {
                MessageBox.Show("Identifiant et/ou mot de passe invalide.", "Erreur Login", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
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
                if (login == "admin")
                {
                    return GetHashSha256(password) == MDPHACHE ? true : false;
                }
                else
                {
                    //TODO : pour les tests, on utilise le domaine STAGIAIRES, mais les utilisteurs de l'appli utiliseront un autre domaine.
                    DirectoryEntry Ldap = new DirectoryEntry("LDAP://STAGIAIRES.local", login, password, AuthenticationTypes.Secure);
                    guid = Ldap.Guid;

                    if (guid == null) return false;
                    else return true;
                }
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        //Renvoie une chaine de caractères cryptée
        public static string GetHashSha256(string text)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(text);
            SHA256Managed hashstring = new SHA256Managed();
            byte[] hash = hashstring.ComputeHash(bytes);
            string hashString = string.Empty;
            foreach (byte x in hash)
            {
                hashString += String.Format("{0:x2}", x);
            }
            return hashString;
        }

    }
}
