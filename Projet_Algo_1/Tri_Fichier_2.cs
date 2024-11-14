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

        /// <summary>
        /// Ajoute chaque mot du fichier à la liste Mots
        /// </summary>
        /// <param name="cheminFichier"></param>
        public List<string> ChargerMots(string cheminFichier)
        {
            try
            {
                string[] lignes = File.ReadAllLines(cheminFichier); /// Lit toutes les lignes du fichier et les stockes dans le tableau string

                foreach (string ligne in lignes)///Parcour chaque ligne dans le tableau de lignes
                {
                    if (!string.IsNullOrEmpty(ligne))///Si la ligne est vide ou remplie d'espace, retourne False sinon retourne True
                    {
                        string[] mots = ligne.Split(' ', StringSplitOptions.RemoveEmptyEntries);///Sépare la ligne en mot en les prélévant un par un en détectant le séparateur (espace)

                        foreach (string mot in mots)///Parcour chaque mot dans le tableau de mots
                        {
                            Mots.Add(mot.Trim());///Ajoute chaque mot du tableau mots dans la liste Mots en retirant les espaces superflues avant et après le mot
                        }
                    }
                }
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
            return Mots;
        }
        ///Tri Fusion sur la liste de mots 
        public static  List<string> TriparFusion(List<string> Mots)
        {
            if(Mots.Count <=1)///Si la liste contient 0 ou un élément ou si la liste est déjà triée 
            {
                return Mots;
            }
            int milieu = Mots.Count / 2;///On définit le milieu de la liste
            List<string> Droite = Mots.GetRange(0, milieu);///On définit une liste contenant la première moitiée de la liste principale
            List<string> Gauche = Mots.GetRange(milieu, Mots.Count - milieu);///On définit une liste contenant la seconde moitiée de la liste principale

            Droite = TriparFusion(Droite);
            Gauche = TriparFusion(Gauche);///On effectue le tri par fusion sur les deux listes

            return Fusionner(Gauche, Droite);
        }
        public static List<string> Fusionner(List<string> Droite, List<string> Gauche)
        {
            if(Droite.Count <= 1)
            {
                return Gauche;
            }
            if(Gauche.Count <= 1)
            {
                return Droite;
            }
            List<string> res = new List<string>(Gauche.Count+Droite.Count);
            int i=0, j=0;
            while(i<Gauche.Count && j < Droite.Count)
            {
                if (String.Compare(Gauche[i], Droite[j], StringComparison.OrdinalIgnoreCase) <= 0)///On compare les deux mots pour les trier par ordre alphabétique 
                {
                    res.Add(Gauche[i]);///On ajoute l'élément de la sous liste gauche à la liste finale
                    i++;
                }
                else
                {
                    res.Add(Droite[j]);///On ajoute l'élément de la sous liste de droite à la liste finale
                    j++;
                } 
               
            }
            while (i < Gauche.Count)///On ajoute le reste éléments de la liste de gauche
            {
                res.Add(Gauche[i]);
            }
            while(j< Droite.Count)///On ajoute le reste éléments de la liste de droite
            {
                res.Add(Droite[j]);
            }
            return res;///On retourne la liste triée totalement 
        }
        ///Nous devons désormais effectuer la recherche dichotomique 
        
        public static bool RechercheDichotomique(List<string> list, string motrechercher)
        {

            int début = 0;
            int fin = list.Count-1;
            bool b = false;
            while (début <= fin)/// On recherche dans la liste 
            {
                int milieu = (début + fin) / 2;
                if (list[milieu]== motrechercher)
                {
                    b = true;
                }
                else if (String.Compare(list[milieu], motrechercher, StringComparison.OrdinalIgnoreCase) <= 0)
                {
                    début=milieu+1;///On va désormais rechercher dans la moitié supérieure de la liste
                }
                else
                {
                    fin = milieu - 1;///On recherche dans la moitié inférieure de la liste 
                }
            }
            return b;


        }




    }
}
