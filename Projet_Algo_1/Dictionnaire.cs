using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Algo_1
{
    internal class Dictionnaire
    {
        public List<string> Mots {  get; set; }
        public string Langue { get; set; }

        /// <summary>
        /// Constructreur naturel d'un dictionnaire
        /// </summary>
        /// <param name="cheminFichier">Ici sera mit le nom du fichier s'il est dans le répertoire du code sinon le chemin d'accès devra être mis précédé d'un @</param>
        /// <param name="langue">Indique la langue du dictionnaire</param>
        public Dictionnaire(string cheminFichier, string langue) 
        {
            Langue = langue;

            Mots = new List<string>();

            ChargerMots(cheminFichier);
        }

        /// <summary>
        /// Ajoute chaque mot du fichier à la liste Mots
        /// </summary>
        /// <param name="cheminFichier"></param>
        public void ChargerMots(string cheminFichier)
        {
            try
            {
                string[]lignes = File.ReadAllLines(cheminFichier); /// Lit toutes les lignes du fichier et les stock dans le tableau string

                foreach(string ligne in lignes)///Parcour chaque ligne dans le tableau de lignes
                {
                    if (!string.IsNullOrEmpty(ligne))///Si la ligne est vide ou remplie d'espace, retourne False sinon retourne True
                    {
                        string[] mots = ligne.Split(' ', StringSplitOptions.RemoveEmptyEntries);///Sépare la ligne en mot en les prélévant un par un en détectant le séparateur (espace)

                        foreach(string mot in mots)///Parcour chaque mot dans le tableau de mots
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
            catch(Exception ex)///Attrape toutes les autres exceptions
            {
                Console.WriteLine($"Erreur lors de la lecture du fichier : {ex.Message}");
            }
        }

        /// <summary>
        /// Retourne true ou false en fonction de si le mot qu'on cherche est dans la liste ou non
        /// </summary>
        /// <param name="mot">Mot qu'on veut chercher dans la liste</param>
        /// <returns></returns>
        public bool ContientLeMot(string mot)
        {
            return Mots.Contains(mot, StringComparer.OrdinalIgnoreCase);///Retourne true si le mot 'mot' passé en paramètre est contenu dans la liste de mots 'Mots' tout en ignorant si les lettres du mot est sont majuscules ou pas, False sinon
        }

        public List<string> ObtenirTousLesMots()///Retourne une copie de la liste pour éviter des changements invoulus dans la liste originale
        {
            return new List<string>(Mots);
        }

        public string toString()///Fonction à finir
        {
            return $"Dictionnaire {Langue} avec un nombre de mots de : {Mots.Count}";
        }

    }
}
