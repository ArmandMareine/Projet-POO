namespace Projet_Algo_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Dé de = new Dé();
            char resultat = de.Lancé();
            Console.WriteLine(resultat);
        }
    }
}
