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
        private List<string> motstrouvés;
        
        private string langue;
        private int scoreJoueur;
        private string cheminfichier1;
        private string cheminfichier2;

        public VérificationMots(Dé[,] plateau, List<string> motstrouvés, string langue, int scoreJoueur, string cheminfichier1, string cheminfichier2)///Constrcteur naturel
        {

            this.plateau = plateau;
            this.motstrouvés = motstrouvés;
            this.langue = langue;
            this.scoreJoueur = scoreJoueur;
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
        public static bool VérifLongueur(string mot)///On vérifie que la longueur est d'au moins 2 caractères pour le mot
        {
            bool b = false;
            if (mot.Length > 2)
            {
                b = true;
            }
            return b;
        }
        public bool FormableAvecPlateau(string mot, Dé[,]plateau)
        {
            bool b = false;
            int lignes = plateau.GetLength(0);
            int colonnes = plateau.GetLength(1);
            ///On assure une conversion en plateau de char
            char[,] plateauChars = new char[lignes, colonnes];
            
            for (int i = 0; i < lignes; i++)
            {
                for (int j = 0; j < colonnes; j++)
                {
                    plateauChars[i, j] = plateau[i, j].FaceVisible.Caractere; /// Accède à la lettre visible
                }
            }
            ///On extrait le premier caractère 
            if (mot != null)
            {
                char caractère1 = mot[0];

            }
            ///On initialise les directions possibles autour d'une case sous la forme d'un tableau. 
                 
            ///Parcourons la matrice à la recherche de la première lettre 
            for(int i=0;  i<plateau.GetLength(0); i++)
            {
                for (int j=0; j<plateau.GetLength(1); j++)
                {
                    if (plateauChars[i,j] == mot[i])
                    {
                        if (i > 0 && i < plateauChars.GetLength(0) && j>0 && j < plateauChars.GetLength(0))
                        {
                            if (plateauChars[i-1,j]== mot[1])
                            {

                                b = true;
                                continue;
                            }
                            else if (plateauChars[i-1,j-1]== mot[1])
                            {

                                b = true;
                                continue;
                            }
                            else if (plateauChars[i - 1, j + 1] == mot[1])
                            {

                                b = true;
                                continue;
                            }
                            else if (plateauChars[i, j - 1] == mot[1])
                            {

                                b = true;
                                continue;
                            }
                            else if (plateauChars[i, j + 1] == mot[1])
                            {

                                b = true;
                                continue;
                            }
                            else if (plateauChars[i + 1, j + 1] == mot[1])
                            {

                                b = true;
                                continue;
                            }
                            else if (plateauChars[i + 1, j] == mot[1])
                            {

                                b = true;
                                continue;
                            }
                            else if (plateauChars[i + 1, j + 1] == mot[1])
                            {

                                b = true;
                                continue;
                            }
                            else
                            {
                                b = false;
                                
                            }
                        }
                        ///On va désormais parcourir chaque case autour de la case actuelle. 
                        
                          
                    }
                }
            }
            return b;
        }
        public static bool AppartientDictionnaire(string mot, string langue, string cheminfichier1, string cheminfichier2)///Vérification de l'appartenance au mot dans le dictionnaire français ou anglais
        {
            bool b=false;
            if (langue == "Français")
            {
                ///Nous allons utiliser une recherhce dichotomique 
                
                Dictionnaire dictionnaire = new Dictionnaire(cheminfichier1, langue);
                List<string> mots = dictionnaire.ChargerMots(cheminfichier1);
                if (RechercheDichotomique(mots, mot) == true)///On teste si l'algorithme récursif renvoie true 
                {
                    b = true;
                }


            }
            else if(langue=="Anglais")
            {
                Dictionnaire dictionnaire = new Dictionnaire(cheminfichier2, langue);
                List<string> mots = dictionnaire.ChargerMots(cheminfichier2);
                if (RechercheDichotomique(mots, mot) == true)///On teste si l'algorithme récursif renvoie true 
                {
                    b = true;
                }
            }
            else
            {
                Console.WriteLine("Erreur");
            }
            return b;
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
                else if (comparaison < 0) // Rechercher dans la moitié supérieure
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