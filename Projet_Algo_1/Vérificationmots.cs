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
        public static bool VérifLongueur(string mot) => mot.Length > 2;///On vérifie que la longueur est d'au moins 2 caractères pour le mot
        
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
                    plateauChars[i, j] = plateau[i, j].FaceVisible.Caractere; /// Accède à la lettre visible
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
                for(int k = 0; k < 8; k++)///8 car 8 cas possible
                {
                    int novi=i+dirx[k];
                    int novj=j+diry[k];///Les nouvelles cases

                    if(novi>=0 && novi<lignes && novj>=0 && novj<colonnes && plateauChars[novi, novj] == mot[index])
                    {
                        if (TrouverMot(novi, novj, index + 1 ))///On va appeler récursivement la focntion pour le caractère suivant
                            return true;
                    }

                }
                return false;
            }
            ///On recherche la première lettre du mot à trouver et on appelle la fonction récursive
            for(int i = 0; i < lignes; i++)
            {
                for(int j = 0; j < colonnes; j++)
                {
                    if (plateauChars[i, j] == mot[0] && TrouverMot(i ,j ,1))
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

            if(string.IsNullOrEmpty(cheminfichier1) || !File.Exists(cheminfichier1))
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