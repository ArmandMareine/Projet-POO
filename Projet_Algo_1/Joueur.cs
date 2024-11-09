using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Algo_1
{
    internal class Joueur
    {
        private string pseudo;
        private int score;

        public Joueur(string pseudo, int score)
        {
            this.pseudo = pseudo;
            this.score = score;
            score = 0;
        }
    }
}
