using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Algo_1
{
    internal class Plateau 
    {
        private List<Dé> Dés;
        private int taille { get; }
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
            
            this.taille = taille;
            Dés = new List<Dé>(taille);
        }
        public void LancerTousLesDés(List<Lettre> lettres, int taille) ///Construction et affichage du plateau 
        {
            Console.WriteLine("Le plateau : ");
            int index = 0;
            string res = "";
            for (int i = 0; i < taille; i++)
            {
                for (int j = 0; j < taille; j++)
                {
                    /// Vérifie si nous avons encore des dés à afficher
                    if (index < Dés.Count)
                    {
                        res += Dés[index].ToString() + "\t"; /// Ajoute le dé et une tabulation pour espacement
                        Console.Write(res);
                        index++; /// Incrémente l'index pour passer au dé suivant
                    }
                    else
                    {
                        res += "[ ]\t"; /// Si aucun dé n'est disponible, afficher une case vide
                    }

                }
                Console.WriteLine("\n\n\n");///On intègre un espacement suffisant
                //result += "\n"; // Saut de ligne à la fin de chaque rangée
            }

            


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
            
            foreach (Lettre lettre in lettres)
            {
                Dé dé = new Dé(lettres);
                for(int  i = 0; i < lettres.Count; i++)
                {
                    dé.AjouteFace(lettre);
                }
                Dés.Add(dé);
            }
            */
        }
        public override string ToString()
        {
            string result = "Plateau :\n";
            
            foreach (Dé de in Dés)
            {
                result += de + "\n";
                Console.Write(result + "\t");
            }
            Console.WriteLine("\n\n\n");
            return result;
        
        }

    }
}
