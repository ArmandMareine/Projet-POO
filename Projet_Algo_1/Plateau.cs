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
        private Dé[,] plateau;
        private List<Dé> Dés;
        private int taille { get; }
        
        public Plateau(int taille)
        {
            
            this.taille = taille;
            Dés = new List<Dé>(taille);
            plateau = new Dé[taille,taille];
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
                if(lettre.Nombre <= 0)
                {
                    Console.WriteLine($"Nombre invalide pour la lettre {lettre.Caractere}. Il doit être supérieur à 0.");
                    return;
                }
                for(int i = 0; i < lettre.Nombre; i++)
                {
                    facesPonderees.Add(lettre);
                }
            }

            Dés = new List<Dé>();
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
            Random random = new Random();
            plateau = new Dé[taille, taille];
            int index = 0;

            /// Remplissage du plateau
            for (int i = 0; i < taille; i++)
            {
                for (int j = 0; j < taille; j++)
                {
                    if (index < Dés.Count)
                    {
                        plateau[i, j] = Dés[index];
                        plateau[i, j].Lance(new Random()); /// Lancer du dé avec le Random
                        index++;
                    }
                    else
                    {
                        plateau[i, j] = null; /// Case vide
                    }
                }
            }

            /// Vérification des cases vides
            for (int i = 0; i < taille; i++)
            {
                for (int j = 0; j < taille; j++)
                {
                    if (plateau[i, j] == null)
                    {
                        Console.WriteLine($"Erreur : Le dé à la position ({i},{j}) n'est pas initialisé.");
                        return;  // Retourner si une case est vide
                    }
                }
            }

            /// Affichage du plateau de jeu 
            for (int i = 0; i < taille; i++)
            {
                Console.Write("|");

                for (int j = 0; j < taille; j++)
                {
                    if (plateau[i, j] != null)
                    {
                        Console.Write(plateau[i, j].FaceVisible.Caractere + "|");///On affiche chaque élément du plateau
                    }
                    else
                    {
                        Console.Write("[ ]".PadRight(4) + "| ");
                    }
                }
                Console.WriteLine();
                Console.Write(new string('-', taille * 2));
                Console.WriteLine();
            }
            
        }


        public bool FormableAvecPlateau(string mot)
        {
            int lignes = plateau.GetLength(0);
            int colonnes = plateau.GetLength(1);

            ///On assure une conversion en plateau de char
            char[,] plateauChars = new char[lignes, colonnes];

            for (int i = 0; i < lignes; i++)
            {
                for (int j = 0; j < colonnes; j++)
                {
                    if (plateau[i,j] == null)
                    {
                        Console.WriteLine($"Erreur : Le dé à la position ({i},{j}) n'est pas initialisé.");
                        return false;  ///Retourner false si un dé n'a pas de face visible
                    }

                    if(plateau[i,j].FaceVisible == null)
                    {
                        Console.WriteLine($"Erreur : La face visible du dé à la position ({i},{j}) est nulle.");
                        return false;  ///Retourner false si la face visible est nulle
                    }
                    plateauChars[i, j] = plateau[i, j].FaceVisible.Caractere; ///Accède à la lettre visible
                }
            }

            ///On initialise les directions possibles autour d'une case sous la forme d'un tableau. 
            int[] dirx = { -1, -1, -1, 0, 1, 1, 1, 0 };
            int[] diry = { -1, 0, 1, -1, -1, 0, 1, 1 };

            ///Tableau pour marqué les cases visitées
            bool[,] visited = new bool[lignes, colonnes];


            ///On crée une fonction récursive pour la recherche du mot
            bool TrouverMot(int i, int j, int index)
            {
                /// Si l'index atteint la longueur du mot, cela signifie que toutes les lettres ont été trouvées
                if (index == mot.Length)
                {
                    return true;
                }

                /// Marquer la case comme visitée
                visited[i, j] = true;

                ///Vérifier les cases voisines du mot recherché
                for (int k = 0; k < 8; k++)///8 directions possibles
                {
                    int novi = i + dirx[k];
                    int novj = j + diry[k];///Les nouvelles coordonnées

                    if (novi >= 0 && novi < lignes && novj >= 0 && novj < colonnes && !visited[novi, novj] && plateauChars[novi, novj] == mot[index])
                    {
                        if (TrouverMot(novi, novj, index + 1))///On va appeler récursivement la focntion pour le caractère suivant
                        {
                            return true;
                        }
                        
                    }

                }

                /// Si aucune direction n'a fonctionné, revenir sur la case et continuer la recherche
                visited[i,j] = false;
                return false;
            }

            ///Recherche de la première lettre du mot et appel de la fonction récursive
            for (int i = 0; i < lignes; i++)
            {
                for (int j = 0; j < colonnes; j++)
                {
                    if (plateauChars[i, j] == mot[0])
                    {
                        if(TrouverMot(i, j, 1))
                        {
                            return true;
                        }
                    }
                }
            }
            return false; /// Si aucune lettre n'a pu être trouvée
        }

    }
}
