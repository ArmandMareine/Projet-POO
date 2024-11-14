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

                foreach(string ligne in lignes)
                {
                    if (!string.IsNullOrEmpty(ligne))
                    {
                        string[] mots = ligne.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                        foreach(string mot in mots)
                        {
                            Mots.Add(mot.Trim());
                        }
                    }
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"Erreur : Le fichier '{cheminFichier}' est introuvable.");
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine($"Erreur : Accès non autorisé au fichier '{cheminFichier}'.");
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Erreur lors de la lecture du fichier : {ex.Message}");
            }
        }

        public bool ContientLeMot(string mot)
        {
            return Mots.Contains(mot, StringComparer.OrdinalIgnoreCase);
        }

        public List<string> ObtenirTousLesMots()
        {
            return new List<string>(Mots);
        }

        public string toString()
        {
            return $"Dictionnaire {Langue} avec un nombre de mots de : {Mots.Count}";
        }

    }
}
