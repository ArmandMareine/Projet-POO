using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Algo_1
{
    internal class Plateau 
    {
        public List<Dé> Dés {  get; set; }  
        private int taille;
        /*
        public Plateau(int taille)
        {
            this.taille = taille; /// Initialisation de la taille entrée par l'utilisateur en tant que taille de la classe plateau
            plateau = new Dé[taille, taille];/// Initialise le plateau en tant que matrice carré de Dé de taille : taille * taille

            for (int i = 0; i < taille; i++)
            {
                for (int j = 0; j < taille; j++)
                {
                    plateau[i, j] = new Dé(); /// Initialisation de chaque case de la matrice en tant qu'objet de la classe Dé
                }
            }

        }
        */
        public Plateau(int taille)
        {
            Dés = new List<Dé>(taille);
        }
        public void LancerTousLesDés(List<Lettre> lettres) 
        {
            /*
            for (int i = 0; i < taille; i++)
            {
                for (int j = 0; j < taille; j++)
                {
                    char résultat = plateau[i, j].Lancé(); /// Parcour chaque case du plateau et lance le dé puis initialise la variable résultat avec la lettre obtenue avec le lancé de Dé
                    Console.Write(résultat + "\t"); /// Affichage du plateau
                }
                Console.WriteLine("\n\n\n");/// Espacement pour la clarité du plateau
            }
            */
            foreach (Lettre lettre in lettres)
            {
                Dé dé = new Dé();
                for(int  i = 0; i < lettres.Count; i++)
                {
                    dé.AjouteFace(lettre);
                }
                Dés.Add(dé);
            }
        }
        public string ToString()
        {
            return string.Join("\n", Dés);
        }

    }
}
