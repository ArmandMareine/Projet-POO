using System.ComponentModel.DataAnnotations;

namespace Projet_Algo_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Bienvenue au jeu du Boggle ! ");
            Console.WriteLine("Saisir la langue désirée : (Anglais ou Français)");///On définit la langue utilisée
            string langue = Convert.ToString(Console.ReadLine()); 
            if(langue != "Anglais" || langue!= "Français")
            {
                Console.WriteLine("Le format est incorrect ! Format attendu : Anglais ou Français");///En cas d'erreur de saisie, on recommence
                langue = Convert.ToString(Console.ReadLine());
            }
            Console.WriteLine("Saisir le nombre de joueurs voulus (Attention, le minimum est 2 et le maximum 10) : ");///On saisit le nombre de joueurs
            int nbjoueurs=int.Parse(Console.ReadLine());
            int affichenbjoueurs = 1;
            while (nbjoueurs > 0)
            {
                Console.WriteLine($"Saisir le pseudo du {affichenbjoueurs} joueur : ");
                nbjoueurs--;
            }
            Console.Write("Entrez la taille du plateau (par exemple, 3 pour un plateau 3x3) : ");
            int taille = int.Parse(Console.ReadLine());
            Console.Write("Saisir le temps désiré pour la partie en minutes (par exemple, 2 pour 2 minutes de partie): ");
            int tempstotal=int.Parse(Console.ReadLine())*60;
            Console.Write("Saisir le temps de jeu par joueur en minutes (par exemple, 1 pour 1 minute de jeu");
            int tempsjoueur=int.Parse(Console.ReadLine())*60;   

            Temps temps = new Temps(tempstotal, tempsjoueur);///Appel au constructeur naturel pour initialisation
            Plateau plateau = new Plateau(taille);
            ///Lancement de la partie 
            Console.WriteLine("La partie commence ! ");
            
            while (tempstotal> 0)///Tant que le temps n'est pas écoulé, la partie continue
            {
                tempstotal--;
                Console.WriteLine("Voici votre plateau :");
                plateau.LancerTousLesDés();
                Thread.Sleep(tempsjoueur);///On laisse le temps au joueur de jouer avant d'afficher un nouveau plateau

            }
           
            
           

        }
    }
}
