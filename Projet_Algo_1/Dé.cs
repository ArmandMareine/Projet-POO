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
        private char[] face_sup;
        public Dé()
        {
            ///Initialisation de l'instance Random
            aléatoire = new Random();

            ///Définir les faces du dé
            face_sup = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'};
        }

        public char Lancé()
        {
            ///Retourner une lettre aléatoire
            int index = aléatoire.Next(0, face_sup.Length);///Indice aléatoire entre 0 et 26 avec 26 exclus pour retourner une lettre du tableau faces
            return face_sup[index];
        }

    }
}
