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
using System.Windows.Media;
using System.Threading;


namespace Hangman_Valeria
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        // Variables globales
        string mot =""; //  Mot à deviner
        int vies= 6;  // Nombre de vies
        string letter="";  // Lettre choisie
        string[] List_mot = { "ordinateur","souris",   "clavier",  "ecran",  "telephone",   "tablette",  "internet",    "reseau",    "logiciel",   "hardware",    "processeur",    "memoire",   "disque",    "serveur",    "imprimante",    "usb",    "wifi",    "bluetooth",    "navigateur","fichier",   "dossier",   "programme",   "application",   "systeme",   "donnees",    "base",   "cloud",    "code",    "developpeur",    "bug",    "script",    "interface",    "algorithme",    "intelligence",    "robot",    "virus",    "parefeu",    "cybersecurite",    "reseaux",    "serveur",    "logiciel", "pixel"};
        string newmot; // Mot intermédiaire avec lettres trouvées
        Random rand = new Random();  // Générateur aléatoire
        char[] affichage;

        //{int N =Rand.next(List-mot.length);
        //    mot = List-mot[N];
        //}

        public MainWindow()
        {
            InitializeComponent();

            startGame(); // Démarre une nouvelle partie 
        }


        public void startGame() // 🚀 Fonction pour initialiser une nouvelle partie
        {
            mot="";
            newmot = "";
            int N = rand.Next(List_mot.Length); // 🎲 Choisit un mot au hasard dans la liste
            mot = List_mot[N];
            vies = 6;

            ImagePendu.Source = new BitmapImage(new Uri("assets\\pendu\\pendu1.png", UriKind.Relative)); //  Affiche la première image du pendu
            ImageCoeurs.Source = new BitmapImage(new Uri("assets\\vies\\coeurs.jpg", UriKind.Relative)); // Affiche l’image initiale des vies
            // Initialiser le tableau affichage avec des *
            affichage = new char[mot.Length];
            for (int i = 0; i < mot.Length; i++)
            {
                affichage[i] = '*';
            }
            // Affiche le mot masqué et les vies à l’écran
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
                {
                    // Réactive le bouton et restaure sa couleur par défaut
                    btn.IsEnabled = true;
                    btn.IsHitTestVisible = true;
                    btn.Background = new SolidColorBrush(Colors.White);
                }
            }
        }

       

        private void Letter_Click(object sender, RoutedEventArgs e) //  Gestion du clic sur une lettre
        {
            Button btn = (sender) as Button;
            // Récupération du texte de la lettre sur le bouton
            string letter = btn.Content.ToString();
            bool correct = false;

            MediaPlayer clickMedia = new MediaPlayer(); // Renommé pour éviter le conflit
            var uri = new Uri(@"assets/sounds/click.mp3", UriKind.Relative);
            clickMedia.Open(uri);
            clickMedia.Play();

            // Initialiser MotIntern si nécessaire
            if (string.IsNullOrEmpty(newmot))
                newmot = new string(affichage);

            // Parcours chaque lettre du mot à deviner
            for (int i = 0; i < mot.Length; i++)
            {
                if (mot[i].ToString() == letter.ToLower())
                {
                    // Remplace * par la lettre trouvée
                    newmot = newmot.Remove(i, 1).Insert(i, letter.ToLower());
                    correct = true;
                    btn.Background = new SolidColorBrush(Colors.LightGreen);
                    btn.IsHitTestVisible = false;   // Empêcher le bouton d’être recliqué sans le griser
                }
            }

            // Affiche le nouveau mot masqué mis à jour
            TB_Display.Text = newmot;

            if (newmot == mot) // Vérifie si le joueur a trouvé tout le mot
            {
                MediaPlayer winMedia = new MediaPlayer(); // Renommé pour éviter le conflit
                var winUri = new Uri(@"assets/sounds/son_gagne.mp3", UriKind.Relative);
                winMedia.Open(winUri);
                winMedia.Play();
                MessageBox.Show(" Vous avez gagné !");
                startGame();
            }
            else
            {
                if (!correct) // Si la lettre est absente → perdre une vie
                {
                    btn.Background = new SolidColorBrush(Colors.LightCoral);
                    btn.IsHitTestVisible = false;
                    vies--;
                    TB_vie.Text = "Vies : " + vies;

                    //  Met à jour l’image du pendu selon le nombre de vies restantes
                    if (vies == 0)
                    {
                        MediaPlayer gameoverMedia = new MediaPlayer(); // Renommé pour éviter le conflit
                        var goUri = new Uri(@"assets/sounds/son_perdu.mp3", UriKind.Relative);
                        gameoverMedia.Open(goUri);
                        gameoverMedia.Play();
                        TB_vie.Text = "Vies: 0";
                        ImagePendu.Source = new BitmapImage(new Uri("assets\\pendu\\corps6.png", UriKind.Relative));
                        MessageBox.Show(" Game Over ! Le mot était : " + mot);
                        startGame();
                    }
                    if (vies == 1)
                    {
                        ImagePendu.Source = new BitmapImage(new Uri("assets\\pendu\\corps5.png", UriKind.Relative));
                        ImageCoeurs.Source = new BitmapImage(new Uri("assets\\vies\\coeurs5.jpg", UriKind.Relative));
                    }
                    if (vies == 2)
                    {
                        ImagePendu.Source = new BitmapImage(new Uri("assets\\pendu\\corps4.png", UriKind.Relative));
                        ImageCoeurs.Source = new BitmapImage(new Uri("assets\\vies\\coeurs4.jpg", UriKind.Relative));
                    }
                    if (vies == 3)
                    {
                        ImagePendu.Source = new BitmapImage(new Uri("assets\\pendu\\corps3.png", UriKind.Relative));
                        ImageCoeurs.Source = new BitmapImage(new Uri("assets\\vies\\coeurs3.jpg", UriKind.Relative));
                    }
                    if (vies == 4)
                    {
                        ImagePendu.Source = new BitmapImage(new Uri("assets\\pendu\\corps2.png", UriKind.Relative));
                        ImageCoeurs.Source = new BitmapImage(new Uri("assets\\vies\\coeurs2.jpg", UriKind.Relative));
                    }
                    if (vies == 5)
                    {
                        ImagePendu.Source = new BitmapImage(new Uri("assets\\pendu\\corps1.png", UriKind.Relative));
                        ImageCoeurs.Source = new BitmapImage(new Uri("assets\\vies\\coeurs1.jpg", UriKind.Relative));
                    }
                }
            }
        }
    }
}
