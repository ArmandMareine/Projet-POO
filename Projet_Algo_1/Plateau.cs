using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Algo_1
{
    internal class Plateau
    {
        private Dé[,] plateau;
        private int taille;

        public Plateau(int taille)
        {
            this.taille = taille;
            plateau = new Dé[taille,taille];

            for (int i = 0; i < taille; i++)
            {
                for(int j = 0; j < taille; j++)
                {
                    plateau[i, j] = new Dé();
                }
            }

        }

        public void LancerTousLesDés()
        {
            for(int i = 0;i < taille; i++)
            {
                for(int j = 0;j < taille; j++)
                {
                    char résultat = plateau[i,j].Lancé();
                    Console.Write(résultat + "\t");
                }
                Console.WriteLine("\n\n\n");

            }
        }
    }
}
