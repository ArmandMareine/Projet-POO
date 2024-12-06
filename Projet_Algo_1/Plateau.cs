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
        public void InitialiserDés(List<Lettre> lettres,int taille)///On initilise les dés 
        {
            Dés = new List<Dé>();
            int indexlettres = 0;
            int nbdedés = taille * taille;

            if (lettres.Count < 6)///On vérifie que l'on a le bon nombre de lettres pour créer le dé
            {
                Console.WriteLine("Il n'y a pas assez de lettres pour créer le dé");
            }
            while(Dés.Count < nbdedés)
            {
                List<Lettre> faces = new List<Lettre>();
                for(int i=0; i < 6; i++)
                {
                    if (indexlettres >= lettres.Count)
                    {
                        indexlettres = 0;
                    }
                    faces.Add(lettres[indexlettres]);
                    indexlettres++;
                }
                Dés.Add(new Dé(faces)); ///On crée un dé à six faces avec donc six lettres
            }
            if (Dés.Count == 0)
            {
                Console.WriteLine("Erreur. Le dé n'a pas pu être créé");
            }
            else
            {
                Console.WriteLine("Les dés sont initialisés avec succès ! ");
            }
            
        }
       
        public void LancerTousLesDés(int taille)
        {
            Console.WriteLine("Le plateau :");
            Random random = new Random();
            Dé[,] plateau = new Dé[taille, taille];
            int index = 0;

            /// Remplissage du plateau
            for (int i = 0; i < taille; i++)
            {
                for (int j = 0; j < taille; j++)
                {
                    if (index < Dés.Count)
                    {
                        plateau[i, j] = Dés[index];
                        plateau[i, j].Lance(random); /// Lancer du dé avec le Random
                        index++;
                    }
                    else
                    {
                        plateau[i, j] = null; /// Case vide
                    }
                }
            }

            /// Affichage du plateau de jeu 
            for (int i = 0; i < taille; i++)
            {
                for (int j = 0; j < taille; j++)
                {
                    if (plateau[i, j] != null)
                    {
                        Console.Write(plateau[i, j].ToString() + "\t");///On affiche chaque élément du plateau
                    }
                    else
                    {
                        Console.Write("[ ]\t");
                    }
                }
                Console.WriteLine();
            }
        }

        
        public override string ToString()///Méthode To string pour l'affichage du plateau de jeu
        {
            string result = "Plateau :\n";
            
            foreach (Dé de in Dés)
            {
                result += de ;
                Console.Write(result);
                
            }
            Console.WriteLine("\n");
            return result;
        
        }

    }
}
