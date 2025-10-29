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
        int vies= 5;
        string letter="";
        string[] List_mot = { "ordinateur", "souris", "clavier", "ecran", "telephone", "tablette", "internet", "reseau", "logiciel", "hardware" };
        string newmot = "";
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
            int N = rand.Next(List_mot.Length);
            mot = List_mot[N];
            vies = 5;

            // Initialiser le tableau affichage avec des *
            affichage = new char[mot.Length];
            for (int i = 0; i < mot.Length; i++)
            {
                affichage[i] = '*';
            }

            TB_Display.Text = new string(affichage);
            TB_vie.Text = "Vies : " + vies;
        }

        private void Letter_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (sender) as Button;
            // Récupération du texte de la lettre sur le bouton
            string letter = btn.Content.ToString();
            // Désactivation du bouton après clic
            btn.IsEnabled = false;
            // Vérifie si la lettre est dans le mot
            GuessLetter(letter.ToLower());

        }

        //  Vérification d’une lettre dans le mot
        public void GuessLetter( string Letter)
        {
            bool correct = false;

            // Parcours chaque lettre du mot à deviner
            for (int i = 0; i < mot.Length; i++)
            {
                if (mot[i] == letter[0])
                {
                    // Remplace * par la lettre trouvée
                    affichage[i] = letter[0];
                    correct = true;
                }
            }

            // Affiche le nouveau mot masqué mis à jour
            TB_Display.Text = new string(affichage);

            // Si la lettre est absente → perdre une vie
            if (!correct)
            {
                vies--;
                TB_vie.Text = "Vies : " + vies;
            }

        }
    }
}
