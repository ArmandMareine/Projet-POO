using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Algo_1
{
    public class VérificationMots
    {

        private Dé[,] plateau { get; set; }
        private HashSet <string> motstrouvés;
        
        private string langue;  
        private int scoreJoueur;
        private string cheminfichier1;
        private string cheminfichier2;

        public VérificationMots(Dé[,] plateau, HashSet<string> motstrouvés, string langue, string cheminfichier1, string cheminfichier2)///Constrcteur naturel
        {

            this.plateau = plateau;
            this.motstrouvés = motstrouvés;
            this.langue = langue;
            ///this.scoreJoueur = scoreJoueur;
            cheminfichier1 = "../../../../MotsPossiblesFR.txt";
            cheminfichier2 = "../../../../MotsPossiblesEN.txt";
            

        }

        /*
        public static void DefLangue(string langue, string cheminfichier1, string cheminfichier2)///Méthode pour définir la langue de vérification des mots 
        {

            if (langue != null)
            {
                if (langue == "Anglais")
                {
                    Dictionnaire dictionnaire = new Dictionnaire(cheminfichier2, langue);
                    List<string> mots = dictionnaire.ChargerMots(cheminfichier2);
                }
                else if (langue == "Français")
                {
                    Dictionnaire dictionnaire = new Dictionnaire(cheminfichier1, langue);
                    List<string> mots = dictionnaire.ChargerMots(cheminfichier1);
                }
                else
                {
                    Console.WriteLine("Erreur sur la langue saisie");
                }
            }
        }
        */
        public static bool VérifLongueur(string mot) => mot.Length >= 2;///On vérifie que la longueur est d'au moins 2 caractères pour le mot



        public static bool AppartientDictionnaire(string mot, string langue, string cheminfichier)///Vérification de l'appartenance au mot dans le dictionnaire français ou anglais
        {

            if(string.IsNullOrEmpty(cheminfichier) || !File.Exists(cheminfichier))
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