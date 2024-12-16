using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Algo_1
{
    public class Plateau 
    {
        private int taille { get; }
        private Dé[,] plateau; ///On ajoute directement la matrice de dés


        public Plateau(int taille, List<Lettre> facespondérées)
        {
            this.taille = taille;
            this.plateau = new Dé[taille, taille];
            InitialiserDés(facespondérées);
            ///Initialisation des faces du dé ainsi que du plateau qui est une matrice de dés
        }
        public void InitialiserDés(List<Lettre> facespondérées)///On initilise les dés 
        {
            Random random = new Random();
            for (int i = 0; i < taille; i++)
            {
                for (int j = 0; j < taille; j++)
                {
                    List<Lettre> faces = new List<Lettre>();///On crée une liste de six lettres 
                    for (int k = 0; k < 6; k++)
                    {
                        Lettre face = facespondérées[random.Next(facespondérées.Count)];///On va ajouter avec le random une face visible au dé
                        faces.Add(face);
                    }
                    plateau[i, j] = new Dé(faces);///On ajoute à la matrice de dés les faces
                }
            }

        }

        public void AffichagePlateau()
        {
            for (int i = 0; i < taille; i++)
            {
                Console.Write("| ");
                for (int j = 0; j < taille; j++)
                {
                    if (plateau[i, j] != null)
                    {
                        Console.Write(plateau[i, j].ToString() + "\t");  // Afficher la face visible du dé
                    }
                    else
                    {
                        Console.Write("[ ]\t");  // Case vide
                    }
                }
                Console.WriteLine("|");
                Console.WriteLine(new string('-', taille * 6));  // Ligne de séparation
            }
        }



        public static bool VérifLongueur(string mot) => mot.Length > 2;///On vérifie que la longueur est d'au moins 2 caractères pour le mot

        public bool FormableAvecPlateau(string mot, Dé[,] plateau)
        {
            int lignes = plateau.GetLength(0);
            int colonnes = plateau.GetLength(1);

            ///On assure une conversion en plateau de char
            char[,] plateauChars = new char[lignes, colonnes];

            for (int i = 0; i < lignes; i++)
            {
                for (int j = 0; j < colonnes; j++)
                {
                    if (plateau[i, j] != null && plateau[i, j].FaceVisible != null)
                    {
                        plateauChars[i, j] = plateau[i, j].FaceVisible.Caractere; /// Accède à la lettre visible
                    }
                    else
                    {
                        Console.WriteLine($"Erreur : Le dé à la position ({i},{j}) ou sa face visible est nulle.");
                        return false;  /// Retourner false si une case est vide ou mal initialisée
                    }
                }
            }
            ///On initialise les directions possibles autour d'une case sous la forme d'un tableau. 
            int[] dirx = { -1, -1, -1, 0, 1, 1, 1, 0 };
            int[] diry = { -1, 0, 1, -1, -1, 0, 1, 1 };
            ///On crée une fonction récursive pour la recherche du mot
            bool TrouverMot(int i, int j, int index)
            {
                ///Distinguons le cas où toutes les lettres du mot sont trouvées
                if (index == mot.Length)
                {
                    return true;
                }
                ///Nous vérifions ensuite les cases voisines du mot recherché
                for (int k = 0; k < 8; k++)///8 car 8 cas possible
                {
                    int novi = i + dirx[k];
                    int novj = j + diry[k];///Les nouvelles cases

                    if (novi >= 0 && novi < lignes && novj >= 0 && novj < colonnes && plateauChars[novi, novj] == mot[index])
                    {
                        if (TrouverMot(novi, novj, index + 1))///On va appeler récursivement la focntion pour le caractère suivant
                            return true;
                    }

                }
                return false;
            }
            ///On recherche la première lettre du mot à trouver et on appelle la fonction récursive
            for (int i = 0; i < lignes; i++)
            {
                for (int j = 0; j < colonnes; j++)
                {
                    if (plateauChars[i, j] == mot[0] && TrouverMot(i, j, 1))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public static bool AppartientDictionnaire(string mot, string langue, string cheminfichier1, string cheminfichier2)///Vérification de l'appartenance au mot dans le dictionnaire français ou anglais
        {
            string cheminfichier = langue == "Français" ? cheminfichier1 : cheminfichier2;

            if (string.IsNullOrEmpty(cheminfichier1) || !File.Exists(cheminfichier1))
            {
                Console.WriteLine($"Le fichier du dictionnaire n'existe pas : {cheminfichier}");
                return false;
            }

            Dictionnaire dictionnaire = new Dictionnaire(cheminfichier, langue);
            List<string> mots = dictionnaire.ChargerMots(cheminfichier);
            return RechercheDichotomique(mots, mot);
        }

        public static bool RechercheDichotomique(List<string> list, string motRechercher)///Algorithme sélectionné pour la recherche dichotomique 
        {
            int debut = 0;
            int fin = list.Count - 1;

            while (debut <= fin) /// Recherche dans la liste triée
            {
                int milieu = (debut + fin) / 2;

                /// Comparaison insensible à la casse
                int comparaison = String.Compare(list[milieu], motRechercher, StringComparison.OrdinalIgnoreCase);

                if (comparaison == 0) /// Élément trouvé
                {
                    return true;
                }
                else if (comparaison < 0) /// Rechercher dans la moitié supérieure
                {
                    debut = milieu + 1;
                }
                else /// Rechercher dans la moitié inférieure
                {
                    fin = milieu - 1;
                }
            }

            return false; /// Élément non trouvé
        }
    }
}

