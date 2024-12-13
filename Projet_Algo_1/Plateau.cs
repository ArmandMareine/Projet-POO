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
        
        public Plateau(int taille)
        {
            
            this.taille = taille;
            Dés = new List<Dé>(taille);
        }
        public void InitialiserDés(List<Lettre> lettres,int taille)///On initilise les dés 
        {
            if (lettres.Count < 6)///On vérifie que l'on a le bon nombre de lettres pour créer le dé
            {
                Console.WriteLine("Il n'y a pas assez de lettres pour créer le dé");
                return;
            }

            List<Lettre> facesPonderees = new List<Lettre>();
            foreach (var lettre in lettres)
            {
                for(int i = 0; i < lettre.Nombre; i++)
                {
                    facesPonderees.Add(lettre);
                }
            }

            Dés = new List<Dé>();
            int indexlettres = 0;
            int nbdedés = taille * taille;
            Random random = new Random();
            
            while(Dés.Count < nbdedés)
            {
                List<Lettre> faces = new List<Lettre>();

                for(int i=0; i < 6; i++)
                {
                    Lettre face = facesPonderees[random.Next(facesPonderees.Count)];
                    faces.Add(face);
                }
                Dés.Add(new Dé(faces)); ///On crée un dé à six faces avec six lettres
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
            Console.WriteLine("Plateau du Jeu :");
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
                Console.Write("| ");
                for (int j = 0; j < taille; j++)
                {
                    if (plateau[i, j] != null)
                    {
                        Console.Write(plateau[i, j].ToString() + "\t");///On affiche chaque élément du plateau
                    }
                    else
                    {
                        Console.Write("[ ]".PadRight(4) + "| ");
                    }
                }
                Console.WriteLine();
            }
            Console.Write(new string('-', taille * 6));
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
