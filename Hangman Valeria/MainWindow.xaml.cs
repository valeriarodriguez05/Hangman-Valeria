using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hangman_Valeria
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        // Variables globales
        string mot ="";
        int vies= 6;
        string letter="";
        string[] List_mot = { "ordinateur","souris",   "clavier",  "ecran",  "telephone",   "tablette",  "internet",    "reseau",    "logiciel",   "hardware",    "processeur",    "memoire",   "disque",    "serveur",    "imprimante",    "usb",    "wifi",    "bluetooth",    "navigateur","fichier",   "dossier",   "programme",   "application",   "systeme",   "donnees",    "base",   "cloud",    "code",    "developpeur",    "bug",    "script",    "interface",    "algorithme",    "intelligence",    "robot",    "virus",    "parefeu",    "cybersecurite",    "reseaux",    "serveur",    "logiciel", "pixel"};
        string newmot = "";
        string MotIntern;
        Random rand = new Random();
        char[] affichage;

        //{int N =Rand.next(List-mot.length);
        //    mot = List-mot[N];
        //}

        public MainWindow()
        {
            InitializeComponent();

            startGame();
        }


        public void startGame()
        {
            mot="";
            MotIntern = "";
            int N = rand.Next(List_mot.Length);
            mot = List_mot[N];
            vies = 6;

            ImagePendu.Source = new BitmapImage(new Uri("assets\\pendu\\pendu1.png", UriKind.Relative));
            // Initialiser le tableau affichage avec des *
            affichage = new char[mot.Length];
            for (int i = 0; i < mot.Length; i++)
            {
                affichage[i] = '*';
            }

            TB_Display.Text = new string(affichage);
            TB_vie.Text = "Vies : " + vies;
            // Réactivation des boutons
            ActivateBtn();
        }

        public void ActivateBtn()
        {
            foreach (var elm in Grd_Keypad.Children)
            {
                if (elm is Button btn)
                    btn.IsEnabled = true;
            }
        }

        private void Letter_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (sender) as Button;
            // Récupération du texte de la lettre sur le bouton
            string letter = btn.Content.ToString();
            // Désactivation du bouton après clic
            btn.IsEnabled = false;
            bool correct = false;

            // Initialiser MotIntern si nécessaire
            if (string.IsNullOrEmpty(MotIntern))
                MotIntern = new string(affichage);

            // Parcours chaque lettre du mot à deviner
            for (int i = 0; i < mot.Length; i++)
            {
                if (mot[i].ToString() == letter.ToLower())
                {
                    // Remplace * par la lettre trouvée
                    MotIntern = MotIntern.Remove(i, 1).Insert(i, letter.ToLower());
                    correct = true;
                }
            }

            // Affiche le nouveau mot masqué mis à jour
            TB_Display.Text = MotIntern;

            if (MotIntern == mot)
            {
                MessageBox.Show(" Vous avez gagné !");
                startGame();
            }
            else
            {
                if (!correct) // Si la lettre est absente → perdre une vie
                {
                    vies--;
                    TB_vie.Text = "Vies : " + vies;
                    if (vies == 0)
                    {
                        TB_vie.Text = "Vies : " + vies;
                        ImagePendu.Source = new BitmapImage(new Uri("assets\\pendu\\corps6.png", UriKind.Relative));
                        MessageBox.Show(" Game Over ! Le mot était : " + mot);
                        startGame();
                    }
                    if (vies == 1)
                    {
                        ImagePendu.Source = new BitmapImage(new Uri("assets\\pendu\\corps5.png", UriKind.Relative));
                    }
                    if (vies == 2)
                    {
                        ImagePendu.Source = new BitmapImage(new Uri("assets\\pendu\\corps4.png", UriKind.Relative));
                    }
                    if (vies == 3)
                    {
                        ImagePendu.Source = new BitmapImage(new Uri("assets\\pendu\\corps3.png", UriKind.Relative));
                    }
                    if (vies == 4)
                    {
                        ImagePendu.Source = new BitmapImage(new Uri("assets\\pendu\\corps2.png", UriKind.Relative));
                    }
                    if (vies == 5)
                    {
                        ImagePendu.Source = new BitmapImage(new Uri("assets\\pendu\\corps1.png", UriKind.Relative));
                    }

                }
            }

        }

        //  Vérification d’une lettre dans le mot
        
    }
}
