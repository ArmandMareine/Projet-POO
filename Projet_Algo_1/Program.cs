namespace Projet_Algo_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Entrez la taille du plateau (par exemple, 3 pour un plateau 3x3) : ");
            int taille = int.Parse(Console.ReadLine());

            Plateau plateau = new Plateau(taille);
            Console.WriteLine("Voici votre plateau :");
            plateau.LancerTousLesDés();
        }
    }
}
