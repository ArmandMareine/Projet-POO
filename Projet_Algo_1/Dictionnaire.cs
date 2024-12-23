﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Algo_1
{
    internal class Dictionnaire
    {
        /// <summary>
        /// Attributs de la classe Dictionnaire
        /// </summary>
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
        /// Charge les mots d'un fichier dans la liste des mots du dictionnaire.
        /// </summary>
        /// <param name="cheminFichier">Chemin du fichier contenant les mots.</param>
        /// <returns>La liste des mots chargés.</returns>
        /// <example>
        /// Dictionnaire dictionnaire = new Dictionnaire("cheminFichier.txt", "Français");
        /// List<string> mots = dictionnaire.ChargerMots("cheminFichier.txt");
        /// </example>
        public List<string> ChargerMots(string cheminFichier)
        {
            try
            {
                using (StreamReader sr = new StreamReader(cheminFichier))
                {
                    string ligne = sr.ReadLine();
                    while (ligne != null)
                    {
                        string[] mots = ligne.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                        foreach (string mot in mots)
                        {
                            if (mot.All(char.IsLetter)) // Vérifie que le mot ne contient que des lettres
                            {
                                Mots.Add(mot.Trim().ToUpper());
                            }
                        }
                        ligne = sr.ReadLine();
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
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la lecture du fichier : {ex.Message}");
            }

            return Mots;
        }
        /// <summary>
        /// Retourne un dictionnaire où les clés sont les lettres de l'alphabet et la valeur de chaque clé est le nombre de mots commençant par cette lettre
        /// </summary>
        /// <returns>Un dictionnaire "compteur"</returns>
        public Dictionary<char ,  int> TriParLettres()
        {
            Dictionary <char , int> compteur = new Dictionary <char , int> ();///Création d'un nouveau dictionnaire compteur avec comme clé de type char la lettre et comme valeur de type int le nombre de mot commencant par cette lettre

            foreach(string mot in Mots)///Parcours chaque mot de la liste Mots où se trouvent tous les mots du fichier 
            {
                char premiereLettre = char.ToLower(mot[0]);///Initialise une variable appelée "premiereLettre" de type char en tant que première lettre du mot

                if (char.IsLetter(premiereLettre))///Vérifie si "premiereLettre" est bien une lettre de l'alphabet retourne false si c'est un chiffre ou autre
                {
                    if (compteur.ContainsKey(premiereLettre))///Vérifie si "premiereLettre" est déjà une clé du dictionnaire "compteur"
                    {
                        compteur[premiereLettre]++;///Si oui, j'incrémente sa valeur de 1
                    }
                    else
                    {
                        compteur[premiereLettre] = 1;///Si non, j'initialise la clé et set sa valeur à 1
                    }
                }
            }
            return compteur;///Retourne le dictionnaire
        }
        /// <summary>
        ///Retourne un dictionnaire où les clés sont les longueurs des mots et la valeur de chaque clé est le nombre de mot étant de cette longueur 
        /// </summary>
        /// <returns>Un dictionnaire "compteur"</returns>
        public Dictionary<int , int> TriParLongueur()
        {
            Dictionary<int , int> compteur = new Dictionary<int, int> ();///Création d'un nouveau dictionnaire compteur avec comme clé la longueur et comme valeur le nombre de mot étant de cette longueur 

            foreach (string mot in Mots)///Parcours chaque mot de la liste Mots où se trouvent tous les mots du fichier
            {
                int longueur = mot.Length;///Initialise une variable appelée "longueur" de type int étant la longueur de chaque mot

                if (compteur.ContainsKey(longueur))///Vérifie si "longueur" est déjà une clé du dictionnaire "compteur"
                {
                    compteur[longueur]++;///Si oui, j'incrémente sa valeur de 1
                }
                else
                {
                    compteur[longueur] = 1;///Si non, j'initialise la clé et set sa valeur à 1
                }
            }
            return compteur;///Retourne le dictionnaire
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

        public List<string> ObtenirTousLesMots()///Retourne une copie de la liste pour éviter des changements non-voulus dans la liste originale
        {
            return new List<string>(Mots);
        }
        /// <summary>
        /// Trie le dictionnaire "compteur" par ordre alphabétique
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        static Dictionary<char, int> TrieDictionnaireLettre(Dictionary<char, int> dict)
        {
            return dict.OrderBy(entry => entry.Key).ToDictionary(entry => entry.Key, entry => entry.Value);
        }

        /// <summary>
        /// Trie le dictionnaire par ordre de longueur de mot 
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        static Dictionary<int, int> TrieDictionnaireLongueur(Dictionary<int, int> dict)
        {
            return dict.OrderBy(entry => entry.Key).ToDictionary(entry => entry.Key, entry => entry.Value);
        }
        /// <summary>
        /// Méthode toString pour le tri
        /// </summary>
        /// <param name="compteur"></param>
        /// <param name="compteur1"></param>
        /// <returns></returns>
        public string toString(Dictionary<char, int> compteur,Dictionary<int,int> compteur1)
        {
            var res = new System.Text.StringBuilder();
            var compteurTrié = TrieDictionnaireLettre(compteur);
            var compteur1Trié = TrieDictionnaireLongueur(compteur1);
            res.AppendLine($"Le dictionnaire est {Langue}");
            foreach(var entry in compteurTrié)
            {
                 res.AppendLine($"Lettre {entry.Key} : {entry.Value} mots");
            }
            foreach(var entry in compteur1Trié)
            {
                res.AppendLine($"Mots de longueur {entry.Key} : {entry.Value} mots");
            }
            return res.ToString();
        }

    }
}
