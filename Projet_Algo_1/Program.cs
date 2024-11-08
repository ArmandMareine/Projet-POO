namespace Projet_Algo_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Entrez la taille du plateau (par exemple, 3 pour un plateau 3x3) : ");
            int taille = int.Parse(Console.ReadLine());
            Console.Write("Saisir le temps désiré pour la partie en minutes (par exemple, 2 pour 2 minutes de partie): ");
            int tempstotal=int.Parse(Console.ReadLine())*60;
            Console.Write("Saisir le temps de jeu par joueur en minutes (par exemple, 2 pour 2 minutes de jeu):");
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
