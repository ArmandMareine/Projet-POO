using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Algo_1
{
    public class Joueur
    {
        private int Numero { get; set; }
        private string pseudo { get; set; }
        private int score { get; set; } 

        public Joueur(int Numero, string pseudo)
        {
            this.Numero = Numero;
            this.pseudo = pseudo;
        }
        public string ToString()
        {
            return $"Joueur {Numero} : {pseudo}";
        }
        
    }
}
