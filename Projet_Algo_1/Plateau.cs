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
        private int taille;
        private Dé[,] plateau; /// On ajoute directement la matrice de dés

        
        
        public Plateau(int taille)
        {
            this.taille = taille;
            this.plateau = new Dé[taille, taille];
        }
        /// <summary>
        /// Méthode pour initialiser les dés 
        /// </summary>
        /// <param name="facespondérées"></param>
        public void InitialiserDés(List<Lettre> facespondérées)
        {
            Random random = new Random();
            for (int i = 0; i < taille; i++)
            {
                for (int j = 0; j < taille; j++)
                {
                    List<Lettre> faces = new List<Lettre>();  /// Créer une liste de six lettres
                    for (int k = 0; k < 6; k++)
                    {
                        Lettre face = facespondérées[random.Next(facespondérées.Count)];  /// On choisit une face aléatoire parmi les faces pondérées
                        faces.Add(face);
                    }
                    plateau[i, j] = new Dé(faces);  /// Assigner un dé au plateau
                }
            }
        }
        /// <summary>
        /// Méthode qui va lancer tous les dés et donc construire le plateau de jeu
        /// </summary>
        public void LancerTousLesDés()
        {
            Random random = new Random();

            /// Remplissage du plateau avec les dés
            for (int i = 0; i < taille; i++)
            {
                for (int j = 0; j < taille; j++)
                {
                    if (plateau[i, j] != null)
                    {
                        plateau[i, j].Lance(random);  /// Lancer le dé pour choisir une face visible
                    }
                    else
                    {
                        plateau[i, j] = null;  /// Cas où il n'y a pas assez de dés
                    }
                }
            }

            /// Affichage du plateau
            for (int i = 0; i < taille; i++)
            {
                Console.Write("| ");
                for (int j = 0; j < taille; j++)
                {
                    if (plateau[i, j] != null)
                    {
                        Console.Write(plateau[i, j].ToString() + " | ");  /// Affiche la face visible du dé
                    }
                    else
                    {
                        Console.Write("[ ]\t");  /// Case vide
                    }
                }
                Console.WriteLine();
            }
        }
        /// <summary>
        /// Méthode qui vérifie que le mot a plus de deux carractères
        /// </summary>
        /// <param name="mot"></param>
        /// <returns></returns>
        public static bool VérifLongueur(string mot) => mot.Length >= 2;///On vérifie que la longueur est d'au moins 2 caractères pour le mot
        /// <summary>
        /// On vérifie que le mot saisi est bien formable avec les lettres présentes sur le plateau 
        /// </summary>
        /// <param name="mot"></param>
        /// <returns></returns>

        public bool FormableAvecPlateau(string mot)
        {
            int lignes = plateau.GetLength(0);
            int colonnes = plateau.GetLength(1);

            /// On assure une conversion en plateau de char
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
                        Console.WriteLine($"Erreur : Le dé à la position ({i},{j}) ou face visible nulle.");
                        return false;  /// Retourner false si une case est vide ou mal initialisée
                    }
                }
            }
            ///On initialise les directions possibles autour d'une case sous la forme d'un tableau. 
            int[] dirx = { -1, -1, -1, 0, 1, 1, 1, 0 };
            int[] diry = { -1, 0, 1, -1, -1, 0, 1, 1 };
            ///On crée une fonction récursive pour la recherche du mot
            bool TrouverMot(int i, int j, int index, bool[,]Caseparcourue)///On ajoute un tableau de Booléen qui va marquer de true le passage de chaque case afin d'empêcher les retours en arrière
            {
                ///Distinguons le cas où toutes les lettres du mot sont trouvées
                if (index == mot.Length)
                {
                    return true;
                }
                Caseparcourue[i, j] = true;
                ///Nous vérifions ensuite les cases voisines du mot recherché
                for (int k = 0; k < 8; k++)///8 car 8 cas possible
                {
                    int novi = i + dirx[k];
                    int novj = j + diry[k];///Les nouvelles cases

                    if (novi >= 0 && novi < lignes && novj >= 0 && novj < colonnes && !Caseparcourue[novi,novj] &&plateauChars[novi, novj] == mot[index])
                    {
                        if (TrouverMot(novi, novj, index + 1, Caseparcourue))
                        {
                            return true;
                        }  /// Recherche récursive
                            
                    }

                }
                ///On marque la case comme non parcourue afin de permettre d'autres chemins
                Caseparcourue[i, j] = false;
                return false;
            }
            ///On recherche la première lettre du mot à trouver et on appelle la fonction récursive
            for (int i = 0; i < lignes; i++)
            {
                for (int j = 0; j < colonnes; j++)
                {
                    bool[,] Caseparcourue = new bool[lignes, colonnes];///On initialise le tableau de Booléen permettant de marquer le passage sur chaque case 
                    if (plateauChars[i, j] == mot[0] && TrouverMot(i, j, 1, Caseparcourue))
                    {
                        return true;
                    }
                }
            }
            return false;/// Mot non trouvé
        }
        /// <summary>
        /// Méthode qui vérifie que le mot saisi par l'utlisateur est bien contenu dans le dictionnaire de la langue sélectionnée 
        /// </summary>
        /// <param name="mot"></param>
        /// <param name="langue"></param>
        /// <param name="cheminfichier1"></param>
        /// <param name="cheminfichier2"></param>
        /// <returns></returns>
        public static bool AppartientDictionnaire(string mot, string langue, string cheminfichier1, string cheminfichier2)///Méthode qui va faire appel à la méthode de recherche dichotomique dans la liste triée afin de vérifier que le mot recherché s'y trouve bien
        {
            string cheminfichier = langue == "Français" ? cheminfichier1 : cheminfichier2;

            if (string.IsNullOrEmpty(cheminfichier) || !File.Exists(cheminfichier))///Vérification de l'existance du chemin menant au fichier texte
            {
                Console.WriteLine($"Le fichier du dictionnaire n'existe pas : {cheminfichier}");
                return false;
            }

            Dictionnaire dictionnaire = new Dictionnaire(cheminfichier, langue);
            List<string> mots = dictionnaire.ChargerMots(cheminfichier);
            List<string> tri = Tri_Fichier_2.TriparFusion(mots);///Appel du tri fusion préalable à la recherche dichotomique 

            /// Assurez-vous que le mot recherché est en majuscules, ou bien le mot du dictionnaire est normalisé
            mot = mot.ToUpper();

            return RechercheDichotomique(tri, mot);///Appel de la recherche dichotomique
        }
        /// <summary>
        /// Méthode de recherche dichotomique dans la liste du mot 
        /// </summary>
        /// <param name="list"></param>
        /// <param name="motRechercher"></param>
        /// <returns></returns>

        public static bool RechercheDichotomique(List<string> list, string motRechercher)///Algorithme sélectionné pour la recherche dichotomique 
        {
            /// Trier la liste avant de faire la recherche
            list.Sort(StringComparer.OrdinalIgnoreCase);

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

