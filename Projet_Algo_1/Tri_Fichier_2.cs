using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Algo_1
{
    public class Tri_Fichier_2
    {
        ///On présente ici la troisième méthode utilisée pour le tri et la recherche des éléments dans le fichier 
        ///On va utiliser une méthode de tri par dichotomie
        ///On utilise la liste crée dans la classe dictionnaire pour appliquer la méthode de tri par dichotomie sur cette liste. 
        public List<string> Mots { get; set; }
        public string Langue { get; set; }

        /// <summary>
        /// Constructreur naturel d'un dictionnaire
        /// </summary>
        /// <param name="cheminFichier">Ici sera mit le nom du fichier s'il est dans le répertoire du code sinon le chemin d'accès devra être mis précédé d'un @</param>
        /// <param name="langue">Indique la langue du dictionnaire</param>
        public Tri_Fichier_2(string cheminFichier, string langue)
        {
            Langue = langue;

            Mots = new List<string>();

        }

      
        public static List<string> LectureFichierMots(string cheminFichier)
        {
            if (!File.Exists(cheminFichier))
            {
                Console.WriteLine("Le fichier n'existe pas : " + cheminFichier);

            }
            List<string> mots = new List<string>();///On initialise la liste de départ
            string ligne;
            try
            {
                StreamReader sr = new StreamReader(cheminFichier);
                ligne = sr.ReadLine();
                while (ligne != null)
                {
                    if (!string.IsNullOrEmpty(ligne))///Si la ligne est vide ou remplie d'espace, retourne False sinon retourne True
                    {
                        string[] motss = ligne.Split(' ', StringSplitOptions.RemoveEmptyEntries);///Sépare la ligne en mot en les prélévant un par un en détectant le séparateur (espace)

                        foreach (string mot in mots)///Parcour chaque mot dans le tableau de mots
                        {
                            mots.Add(mot.Trim());///Ajoute chaque mot du tableau mots dans la liste Mots en retirant les espaces superflues avant et après le mot
                        }
                    }
                  
                    ligne = sr.ReadLine();
                }
                sr.Close();
            }
            catch (FileNotFoundException)///Exécute ce bloc si l'exception FileNotFoundException est relevée, soit que le fichier n'a pas été trouvé
            {
                Console.WriteLine($"Erreur : Le fichier '{cheminFichier}' est introuvable.");
            }
            catch (UnauthorizedAccessException)///Exécute ce bloc si l'exception UnauthorizedAccessException est relevée, soit que l'accès n'est pas autorisé
            {
                Console.WriteLine($"Erreur : Accès non autorisé au fichier '{cheminFichier}'.");
            }
            catch (Exception ex)///Attrape toutes les autres exceptions
            {
                Console.WriteLine($"Erreur lors de la lecture du fichier : {ex.Message}");
            }
            return mots;
        }
        
        

        public static List<string> TriparFusion(List<string> Mots)///Tri par fusion sur le fichier de mots 
        {
            if (Mots.Count <= 1) /// Si la liste contient 0 ou 1 élément, elle est déjà triée donc on retourne la liste 
            {
                return Mots;
            }

            int milieu = Mots.Count / 2; /// On trouve le milieu de la liste
            List<string> Gauche = Mots.GetRange(0, milieu); /// Partie gauche de la liste
            List<string> Droite = Mots.GetRange(milieu, Mots.Count - milieu); /// Partie droite de la liste

            /// Tri récursif des deux moitiés en appellant la fonction TriparFusion
            Gauche = TriparFusion(Gauche);
            Droite = TriparFusion(Droite);

            /// Fusion des deux listes triées en appellant la méthode 
            return Fusionner(Gauche, Droite);
        }

        public static List<string> Fusionner(List<string> Gauche, List<string> Droite)
        {
            List<string> res = new List<string>(Gauche.Count + Droite.Count);
            int i = 0, j = 0;

            /// Comparer et fusionner les deux listes
            while (i < Gauche.Count && j < Droite.Count)
            {
                if (String.Compare(Gauche[i], Droite[j], StringComparison.OrdinalIgnoreCase) <= 0)
                {
                    res.Add(Gauche[i]);
                    i++;
                }
                else
                {
                    res.Add(Droite[j]);
                    j++;
                }
            }

            /// Ajouter le reste des éléments de la liste Gauche
            while (i < Gauche.Count)
            {
                res.Add(Gauche[i]);
                i++;
            }

            /// Ajouter le reste des éléments de la liste Droite
            while (j < Droite.Count)
            {
                res.Add(Droite[j]);
                j++;
            }

            return res; /// Retourne la liste triée pour utilisation
        }


        ///Recherche dichotomique dans le fichier 
        public static bool RechercheDichotomique(List<string> list, string motRechercher)
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
        ///Méthode de tri 2 : Tri Quicksort
        public static int Partionner(List<string> list, int debut, int fin)
        {
            
           
           string pivot = list[fin]; /// Choisir le dernier élément comme pivot (arbitraire)

            int i = debut - 1; /// Index de l'élément plus petit que le pivot

            for(int j=debut; j<fin;j++)
            {
                if (list[j].CompareTo(pivot) <= 0) /// On teste si l'élément est plus petit ou égal au pivot
                {
                    i++; /// Incrémenter l'index de l'élément plus petit
                         
                    string temp = list[i];
                    list[i] = list[j];
                    list[j] = temp;///On réalise ici l'échange des éléments 

                }
               
                
            }
            string temp2 = list[i + 1];
            list[i + 1] = list[fin];
            list[fin] = temp2;

            return i + 1; /// Retourner l'index du pivot
        }
        public static void Quick_Sort(List<string> list, int debut, int fin)
        {
            if (debut < fin)
            {
                /// Obtenir l'index du pivot
                int pivotIndex = Partionner(list, debut, fin);

                /// Appliquer récursivement sur la partie gauche et droite de la liste
                Quick_Sort(list, debut, pivotIndex - 1); /// Avant le pivot
                Quick_Sort(list, pivotIndex + 1, fin);   /// Après le pivot
            }
        }

    }
}
