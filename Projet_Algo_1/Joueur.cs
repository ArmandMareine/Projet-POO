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
        private string pseudo { get; set; }
        private int score { get; set; } 
        private HashSet<string> Motstrouvés { get; set; }
        private Dictionary<char, int> valeurLettres;

        public Joueur(int Numero, string pseudo, int score)
        {
            this.Numero = Numero;
            this.pseudo = pseudo;
            this.score = score;
            Motstrouvés = new HashSet<string>();
            valeurLettres = lettres.ToDictionary(l => l.Caractere , l => l.Valeur);
        }

        public string ToString()
        {
            return $"Joueur {Numero} : {pseudo}";
        }

        public bool ContientMot(string mot)///On teste si le mot passé en paramètre appartient déjà aux mots trouvés par le joueur pendant la partie
        {
            bool b = false;
            foreach (string Mot in Motstrouvés)///On teste si le mot trouvé par l'utilisateur est déjà dans la liste contenant tous les mots trouvés
            {
                if (Mot == mot)
                {
                    b = true;
                }
            }
            return b;
        }

        public void Add_Mot(string mot)///Ajoute le mot dans la liste des mots déjà trouvés par l'utilisateur 
        {
            Motstrouvés.Add(mot);   ///On ajoute le mot trouvé par le joueur à la liste de mots
            Console.WriteLine($"Les mots trouvés sont : {Motstrouvés}");
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
                    pointstotal += valeurLettres[lettre];
                }
            }
            return pointstotal;
        }
    }
}
