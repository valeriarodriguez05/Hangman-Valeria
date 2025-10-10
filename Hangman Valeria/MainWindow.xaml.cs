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

            // Afficher des * pour chaque lettre du mot

            for (int i = 0; i < mot.Length; i++)
            {
                TB_Display.Text += "*";
            }
             TB_vie.Text = "Vies : " + vies;
        }

        private void Letter_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (sender) as Button;
            string letter = btn.Content.ToString();
            btn.IsEnabled = false;

            GuessLetter(letter.ToLower());

        }


        public void GuessLetter( string Letter)
        {

            if (mot.Contains(letter))
            {
                // 🔍 Vérification lettre dans le mot
                for (int i = 0; i < mot.Length; i++)
                {
                    newmot = mot.Remove(i, 1).Insert(i, mot[i].ToString());
                    TB_Display.Text = newmot;
                }
            }
            else
            {
                               vies--;
                TB_vie.Text = "Vies : " + vies;
            }

         
        }
    }
}
