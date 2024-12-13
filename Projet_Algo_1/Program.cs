using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Projet_Algo_1
{
    internal class Jeu
    {
        static void Main(string[] args)
        {
            string cheminFichier = "../../../../MotsPossiblesFR.txt";
            Console.Write(cheminFichier+ "\n");
            string Langue = "Français";
            Dictionnaire test = new Dictionnaire(cheminFichier,Langue);
            List<string> list = test.ChargerMots(cheminFichier);
            Dictionary<char, int> compteur = test.TriParLettres();
            Dictionary<int , int> compteur1 = test.TriParLongueur();
            string res = test.toString(compteur,compteur1);
            Console.WriteLine(res);
            ///Test lecturefichier mots
            List<string> tri = Tri_Fichier_2.TriparFusion(list);
            Console.WriteLine(Tri_Fichier_2.RechercheDichotomique(tri,"Zoo"));
            int taille1 = 4;
            string cheminFichier2 = "../../../../lettres.txt";
            List<Lettre> lettres = Lettre.LectureFichier(cheminFichier2);
            Plateau plateau = new Plateau(taille1);
            plateau.InitialiserDés(lettres,taille1);
            plateau.LancerTousLesDés(4);
            /// Affiche les lettres importées
            if (lettres.Count > 0)
            {
                Console.WriteLine("Lettres importées avec succès !");
            }
            else
            {
                Console.WriteLine("Aucune lettre n'a été importée.");
            }
           
            /*
           foreach (string str in tri)
           {
               Console.WriteLine(string.Join(",", tri));
           }
           Console.WriteLine("Ok");



           foreach (string mot in list)
           {
               Console.WriteLine(mot);
           }
           */






            
            ////////////////////////////////////////////////////////////////////////
            //Fin de la zone de test : Début du jeu 
            Console.WriteLine("Bienvenue au jeu du Boggle ! ");
            ///Définition des chemins fichiers à rajouter (le lettre)
            ///Défintion des timers
            /*
            Console.Write("Saisir la langue désirée (Anglais ou Français):");///On définit la langue utilisée
            string langue = Convert.ToString(Console.ReadLine());
            string EN = "Anglais";
            string FR = "Français";
            bool fr = FR.Equals(langue);
            bool en = EN.Equals(langue);
            if (fr)
            {
                cheminFichier = "../../../../MotsPossiblesFR.txt";///On initialise le fichier français

            }
            else if (en)
            {
                cheminFichier = "../../../../MotsPossiblesEN.txt";///On initialise le fichier anglais
            }
            else
            {
                do
                {
                    Console.Write("Le format est incorrect ! Format attendu : Anglais ou Français  ");///En cas d'erreur de saisie, on recommence
                    langue = Convert.ToString(Console.ReadLine());
                } while (!fr && !en);
            }///Test de la bonne saisie des langues 
            
            if (langue == "Anglais" || langue == "Français")
            {
                Console.Write("Saisir le nombre de joueurs voulus (Attention, le minimum est 2 et le maximum 10) : ");///On saisit le nombre de joueurs
                int nbjoueurs = int.Parse(Console.ReadLine());
                Joueur[] joueurs = new Joueur[nbjoueurs];///Définition d'un tableau de joueur pour contenir les informations chaque participant
                int score = 0;
                for (int i = 0; i < nbjoueurs; i++)
                {
                    Console.Write($"Saisir le pseudo du joueur {i + 1} : ");
                    string pseudo = Convert.ToString(Console.ReadLine());
                    joueurs[i] = new Joueur(i + 1, pseudo, score);///On remplit le tableau de joueurs avec les pseudos de le numéro de chaque joueur
                }
                Console.WriteLine("Les joueurs sont donc : ");
                foreach (var joueur in joueurs)///Affichage de tous les joueurs
                {
                    Console.WriteLine(joueur.ToString());
                }
                Console.Write("Entrez la taille du plateau (par exemple, 4 pour un plateau 4x4) : ");///Initialisation de la taille du plateau
                int taille = int.Parse(Console.ReadLine());
                Console.Write("Saisir le temps désiré pour la partie en minutes (par exemple, 2 pour 2 minutes de partie): ");
                int tempstotal = int.Parse(Console.ReadLine()) * 60;
                TimeSpan dureetotale = TimeSpan.FromMinutes(tempstotal);///Initialisation du premier timer, le timer pour le jeu total
                Console.Write("Saisir le temps de jeu par joueur en minutes (par exemple, 1 pour 1 minute de jeu) :");
                int tempsjoueur = int.Parse(Console.ReadLine()) * 60;
                TimeSpan dureeparjoueur = TimeSpan.FromMinutes(tempsjoueur); ///Initialisation du temps de jeu par joueur à une valeur par défaut de 1 minute
                Console.WriteLine($"Le temps total de la partie est donc de : {dureetotale} minutes");
                Console.WriteLine($"Le temps de jeu pour chaque joueur est de : {dureeparjoueur}");
                ///Début du jeu 
                Console.WriteLine("La partie commence ! ");
                DateTime debutPartie = DateTime.Now;///Initialisation du début de la partie au moment de son lancement

                while (DateTime.Now - debutPartie < dureetotale)
                {
                    for (int i = 0; i < joueurs.Length; i++)
                    {
                        Console.WriteLine($"C'est au tour du joueur {i + 1} : {joueurs[i]} de jouer !");///On donne le temps de jeu au joueur
                        Console.WriteLine($"Ton temps de jeu est de {tempsjoueur / 60} minutes");///On donne le temps de jeu au joueur 
                        Console.WriteLine("Voici ton plateau :");
                        ///Affichage du plateau
                        ///SUITE DU JEU






                    }


                    DateTime debutTour = DateTime.Now;
                    while (true)///Test en permanence
                    {

                        if (DateTime.Now - debutTour > dureeparjoueur)///On teste si le temps de jeu du joueur est écoulé
                        {
                            Console.WriteLine("Le temps pour le joueur X est écoulé");
                            break;
                        }
                        if (Console.KeyAvailable)///Si le joueur appuie sur une touche, la boucle s'arrête 
                        {
                            Console.ReadKey();
                            Console.WriteLine("Action du joueur enregistrée");
                            break;
                        }
                    }
                    if (DateTime.Now - debutPartie > dureetotale)///Fin du jeu : test
                    {
                        Console.WriteLine("Temps total du jeu du Boggle écoulé ! ");
                        break;
                    }
                    

                }
           
                Console.WriteLine("Fin de la partie ! Merci ! ");///Affichage fin de partie 

            
            }  
            */
           
        }
            
    }
   
}

