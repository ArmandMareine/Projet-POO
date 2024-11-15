using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Projet_Algo_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*
            Dé teste = new Dé();
            Console.WriteLine(teste.toString());
<<<<<<< HEAD
            string cheminFichier = @"C:/Users/LENOVO/Documents/ESILV/A2/S1/MODULES/ALGORITHME & POO/PROJET POO/Projet-POO/Projet_POO_1/bin/Debug/net8.0/MotsPossiblesFR.txt";
            Console.Write(cheminFichier);
=======
            /*
            string Langue = "oui oui";
            Dictionnaire test = new Dictionnaire(cheminFichier,Langue);
            List<string> list = test.ChargerMots(cheminFichier);
            
            foreach (string mot in list)
            {
                Console.WriteLine(mot);
            }
            */
>>>>>>> 412e16c18be1f298e1bc99d50f5767d3449e1988
            Console.WriteLine("Bienvenue au jeu du Boggle ! ");
            string cheminFichier = "";
            Console.Write("Saisir la langue désirée (Anglais ou Français):");///On définit la langue utilisée
            string langue = Convert.ToString(Console.ReadLine());
            string EN = "Anglais";
            string FR = "Français";
            bool fr = FR.Equals(langue);
            bool en = EN.Equals(langue);
            do
            {
                Console.Write("Le format est incorrect ! Format attendu : Anglais ou Français  ");///En cas d'erreur de saisie, on recommence
                langue = Convert.ToString(Console.ReadLine());
            } while(!fr && !en);
            if (fr)
            {
                cheminFichier = "C:\\Users\\linji\\Desktop\\ESILV\\Cours A2\\POO\\Projet-POO\\MotsPossiblesFR.txt";

            }
            else if(en)
            {
                cheminFichier = "C:\\Users\\linji\\Desktop\\ESILV\\Cours A2\\POO\\Projet-POO\\MotsPossiblesEN.txt";
            }
            
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
                Console.Write("Entrez la taille du plateau (par exemple, 4 pour un plateau 4x4) : ");
                int taille = int.Parse(Console.ReadLine());
                Console.Write("Saisir le temps désiré pour la partie en minutes (par exemple, 2 pour 2 minutes de partie): ");
                int tempstotal = int.Parse(Console.ReadLine()) * 60;
                Console.Write("Saisir le temps de jeu par joueur en minutes (par exemple, 1 pour 1 minute de jeu) :");
                int tempsjoueur = int.Parse(Console.ReadLine()) * 60;

                Temps temps = new Temps(tempstotal, tempsjoueur);///Appel au constructeur naturel pour initialisation
                Plateau plateau = new Plateau(taille);
                ///Lancement de la partie 
                Console.WriteLine("La partie commence ! ");

                while (tempstotal > 0)///Tant que le temps n'est pas écoulé, la partie continue
                {

                    for (int i = 0; i < joueurs.Length; i++)
                    {
                        Console.WriteLine($"C'est au tour du joueur {i + 1} : {joueurs[i]} de jouer !");///On donne le temps de jeu au joueur
                        Console.WriteLine($"Ton temps de jeu est de {tempsjoueur / 60} minutes");///On donne le temps de jeu au joueur 
                        Console.WriteLine("Voici ton plateau :");
                        plateau.LancerTousLesDés();///Le plateau du joueur s'affiche à l'écran
                        while (tempsjoueur > 0)
                        {
                            ///Le jeu se déroule ici 

                            Console.WriteLine("Hello ! ");
                            tempsjoueur--;
                            Thread.Sleep(1000);///Temps d'une seconde entre chaque itération
                                               ///Utilisation de la méthode 1 pout le tri et la recherche du mot
                            
                            Tri_Fichier_2 Tri = new Tri_Fichier_2(cheminFichier, langue);
                            List<string> mots = Tri.ChargerMots(cheminFichier);
                            List<string> motstriés = Tri_Fichier_2.TriparFusion(mots);
                            string motchercher = "Arbre";
                            try
                            {
                                if (Tri_Fichier_2.RechercheDichotomique(mots, motchercher)==true)
                                {
                                    Console.WriteLine($"Le mot {motchercher} est bien dans le dictionnaire !");
                                }
                                else
                                {
                                    Console.WriteLine("Il n'y est pas");
                                }
                            }
                            catch(FileNotFoundException ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                        }



                    }
                    tempstotal--;
                    Thread.Sleep(tempsjoueur);///On laisse le temps au joueur de jouer avant d'afficher un nouveau plateau
                }
            }
            
        }
    }
}
