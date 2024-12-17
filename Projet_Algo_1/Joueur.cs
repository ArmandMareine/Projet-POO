using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Algo_1
{
    public class Joueur
    {
        private int Numero { get; set; }
        public string pseudo { get; set; }
        private int score { get; set; } 
        private HashSet<string> Motstrouvés { get; set; }

        private Dictionary<char, int> valeurLettres;

        public Joueur(int Numero, string pseudo, int score,List<Lettre> lettres)
        {
            this.Numero = Numero;
            this.pseudo = pseudo;
            this.score = score;
            Motstrouvés = new HashSet<string>();
            valeurLettres = lettres.ToDictionary(l => l.Caractere , l => l.Valeur);
        }

        public int GetScore()
        {
            return score;
        }

        public string toString()
        {
            return $"Joueur {Numero} : {pseudo}";
        }

        public bool ContientMot(string mot)///On teste si le mot passé en paramètre appartient déjà aux mots trouvés par le joueur pendant la partie
        {
            return Motstrouvés.Contains(mot);
        }

        public void Add_Mot(string mot)///Ajoute le mot dans la liste des mots déjà trouvés par l'utilisateur 
        {
            if (string.IsNullOrWhiteSpace(mot))
            {
                Console.WriteLine("Mot invalide !");
                return;
            }

            if (Motstrouvés.Contains(mot))
            {
                Console.WriteLine("Ce mot a déjà été trouvé !");
                return;
            }

            Motstrouvés.Add(mot);   ///On ajoute le mot trouvé par le joueur à la liste de mots
            Console.WriteLine($"{pseudo} a trouvé le mot : {mot} !");
        }

        public void AjouterAuScore(string mot)
        {
            int points = CalculerScore(mot);
            score += points;
            Console.WriteLine($"{pseudo} a gagné {points} points ! Nouveau score : {score}");
        }

        public int CalculerScore(string mot)
        {
            int pointstotal = 0;

            foreach (char lettre in mot)
            {
                if (valeurLettres.ContainsKey(lettre))
                {
                    int pointsLettre = valeurLettres[lettre];
                    pointstotal += pointsLettre;

                    /// Affiche des informations pour déboguer
                    Console.WriteLine($"Lettre : {lettre}, points : {pointsLettre}");
                }
                else
                {
                    /// Affiche si la lettre n'est pas trouvée dans le dictionnaire
                    Console.WriteLine($"Lettre '{lettre}' non trouvée dans le dictionnaire.");
                }
            }
            return pointstotal;
        }
    }
}
