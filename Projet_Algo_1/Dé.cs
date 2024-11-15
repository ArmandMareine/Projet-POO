﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Algo_1
{
    public class Dé
    {
        private Random aléatoire;
        private char[] lettres;
        private char[] faces_du_dé;
        private string cheminTextes;


        public Dé(string cheminTextes)
        {
            cheminTextes = "";///Ajout du chemin du fichier 
            ///Initialisation de l'instance Random
            aléatoire = new Random();
            string ligne;
            List<string> Lettres = new List<string>();

            try
            {
                StreamReader sr = new StreamReader(cheminTextes);
                ligne = sr.ReadLine();
                while (ligne != null)
                {
                    string[] mots = ligne.Split(';', StringSplitOptions.RemoveEmptyEntries);
                    foreach (string mot in mots)
                    {
                        Lettres.Add(mot.Trim());
                    }
                    ligne = sr.ReadLine();
                }
                sr.Close();
            }
            catch (FileNotFoundException)///Exécute ce bloc si l'exception FileNotFoundException est relevée, soit que le fichier n'a pas été trouvé
            {
                Console.WriteLine($"Erreur : Le fichier '{cheminTextes}' est introuvable.");
            }
            catch (UnauthorizedAccessException)///Exécute ce bloc si l'exception UnauthorizedAccessException est relevée, soit que l'accès n'est pas autorisé
            {
                Console.WriteLine($"Erreur : Accès non autorisé au fichier '{cheminTextes}'.");
            }
            catch (Exception ex)///Attrape toutes les autres exceptions
            {
                Console.WriteLine($"Erreur lors de la lecture du fichier : {ex.Message}");
            }
            

            ///Définir les lettres que les faces du dé peuvent prendre
            lettres = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'};

            ///Initialisation d'un dé carré à 6 faces
            faces_du_dé = new char[6];

            ///Initialisation de chaque faces du dé avec une lettre aléatoire de l'alphabet
            for(int i = 0; i < faces_du_dé.Length; i++)
            {
                int index = aléatoire.Next(0, lettres.Length);
                faces_du_dé[i] = this.lettres[index];
            }
        }

        /// <summary>
        /// La fonction lancé permet de "lancer" le dé et de retourner une seule lettre qui sera considérer comme la lettre de la face supérieure qui apparaîtra sur le plateau
        /// </summary>
        /// <returns>Lettre de la face supérieur</returns>
        public char Lancé()
        {
            int index = aléatoire.Next(0, faces_du_dé.Length);
            return faces_du_dé[index];
        }

        /// <summary>
        /// Retourne une chaîne de caractère décrivant les faces du dé en séparant chacune des lettres des faces du dé par " , " sans mettre ce séparateur à la fin.
        /// </summary>
        /// <returns>Exemple de retour : Les faces du dé sont composées par les lettres : A , B , C , D , E , F</returns>
        public string toString()
        {
            string resultat = "Les faces du dé sont composées par les lettres : ";
            for (int i = 0; i < faces_du_dé.Length - 1; i++)
            {
                resultat += faces_du_dé[i] + " , ";
            }
            resultat += faces_du_dé[faces_du_dé.Length - 1];
            return resultat;
        }
    }
}
