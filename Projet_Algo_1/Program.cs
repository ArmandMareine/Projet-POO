using System.ComponentModel.DataAnnotations;

namespace Projet_Algo_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Bienvenue au jeu du Boggle ! ");
            Console.Write("Saisir la langue désirée (Anglais ou Français) : ");///On définit la langue utilisée
            string langue = Convert.ToString(Console.ReadLine()); 
            if(langue != "Anglais" || langue!= "Français")
            {
                Console.Write("Le format est incorrect ! Format attendu : Anglais ou Français  ");///En cas d'erreur de saisie, on recommence
                langue = Convert.ToString(Console.ReadLine());
            }
            Console.Write("Saisir le nombre de joueurs voulus (Attention, le minimum est 2 et le maximum 10) : ");///On saisit le nombre de joueurs
            int nbjoueurs=int.Parse(Console.ReadLine());
            Joueur[]joueurs = new Joueur[nbjoueurs];///Définition d'un tableau de joueur pour contenir les informations chaque participant
            for(int i = 0;i< nbjoueurs; i++)
            {
                Console.Write($"Saisir le pseudo du joueur {i+1} : ");
                string pseudo = Convert.ToString(Console.ReadLine());
                joueurs[i] = new Joueur(i + 1, pseudo);///On remplit le tableau de joueurs avec les pseudos de le numéro de chaque joueur
            }
            Console.WriteLine("Les joueurs sont donc : ");
            foreach(var  joueur in joueurs)///Affichage de tous les joueurs
            {
                Console.WriteLine(joueur.ToString());
            }
            Console.Write("Entrez la taille du plateau (par exemple, 3 pour un plateau 3x3) : ");
            int taille = int.Parse(Console.ReadLine());
            Console.Write("Saisir le temps désiré pour la partie en minutes (par exemple, 2 pour 2 minutes de partie): ");
            int tempstotal=int.Parse(Console.ReadLine())*60;
            Console.Write("Saisir le temps de jeu par joueur en minutes (par exemple, 1 pour 1 minute de jeu) :");
            int tempsjoueur=int.Parse(Console.ReadLine())*60;   

            Temps temps = new Temps(tempstotal, tempsjoueur);///Appel au constructeur naturel pour initialisation
            Plateau plateau = new Plateau(taille);
            ///Lancement de la partie 
            Console.WriteLine("La partie commence ! ");
            
            while (tempstotal> 0)///Tant que le temps n'est pas écoulé, la partie continue
            {
                
                for(int i =0; i<joueurs.Length; i++)
                {
                    Console.WriteLine($"C'est au tour du joueur {i+1} : {joueurs[i]} de jouer !");///On donne le temps de jeu au joueur
                    Console.WriteLine($"Ton temps de jeu est de {tempsjoueur/60} minutes");///On donne le temps de jeu au joueur 
                    Console.WriteLine("Voici ton plateau :");
                    plateau.LancerTousLesDés();
                    while (tempsjoueur > 0)
                    {
                        ///Le jeu se déroule ici 

                        Console.WriteLine("Hello ! ");
                        tempsjoueur--;
                        
                    }
                    Thread.Sleep(1000);///Temps d'une seconde entre chaque itération


                }
                tempstotal--;
                Thread.Sleep(tempsjoueur);///On laisse le temps au joueur de jouer avant d'afficher un nouveau plateau


            }




        }
    }
}
