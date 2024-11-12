using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Algo_1
{
    public class Dé
    {
        private Random aléatoire;
        private char[] lettres;
        private char[] faces_du_dé;
        public Dé()
        {
            ///Initialisation de l'instance Random
            aléatoire = new Random();

            ///Définir les lettres que les faces du dé peuvent prendre
            lettres = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'};

            ///Initialisation d'un dé carré à 6 faces
            faces_du_dé = new char[6];
            for(int i = 0; i < 6; i++)
            {
                int index = aléatoire.Next(0, lettres.Length);
                faces_du_dé[i] = this.lettres[index];
            }
        }
        public char Lancé()
        {
            int index = aléatoire.Next(0, faces_du_dé.Length);
            return faces_du_dé[index];
        }

        public string toString()
        {
            string resultat = "Les faces du dé sont composées par les lettres : ";
            for (int i = 0; i < faces_du_dé.Length - 1; i++)
            {
                resultat += faces_du_dé[i] + " , ";
            }
            resultat += faces_du_dé[faces_du_dé.Length - 1];
            return resultat;
        }
    }
}
