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
        private HashSet<string> Motstrouvés;

        private Dictionary<char, int> valeurLettres;

        /// <summary>
        /// Constructeur naturel de la classe Joueur 
        /// </summary>
        /// <param name="Numero"></param>
        /// <param name="pseudo"></param>
        /// <param name="score"></param>
        /// <param name="lettres"></param>
        public Joueur(int Numero, string pseudo, int score,List<Lettre> lettres)
        {
            this.Numero = Numero;
            this.pseudo = pseudo;
            this.score = score;
            Motstrouvés = new HashSet<string>();
            valeurLettres = lettres.ToDictionary(l => l.Caractere , l => l.Valeur);
        }
        /// <summary>
        /// Propriété accès en lecture de MotsTrouvés
        /// </summary>
        
        public HashSet<string> MotsTrouvés
        {
           get { return Motstrouvés; }
        }
        /// <summary>
        /// Propriétés pour obtenir le score du joueur
        /// </summary>
        /// <returns></returns>
        public int GetScore()
        {
            return score;
        }
        /// <summary>
        /// Méthode toString affichage du joueur et de son pseudo
        /// </summary>
        /// <returns></returns>
        public string toString()
        {
            return $"Joueur {Numero} : {pseudo}";
        }
        /// <summary>
        /// Méthode qui teste si le mot saisi par l'utilisateur a déjà été trouvé par le joueur 
        /// </summary>
        /// <param name="mot"></param>
        /// <returns></returns>

        public bool ContientMot(string mot)///On teste si le mot passé en paramètre appartient déjà aux mots trouvés par le joueur pendant la partie
        {
            return Motstrouvés.Contains(mot);
        }
        /// <summary>
        /// Méthode qui ajoute le mot saisi dans la liste des mots déjà trouvés par l'utilisateur 
        /// </summary>
        /// <param name="mot"></param>
        public void Add_Mot(string mot)
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

            Motstrouvés.Add(mot); 
            Console.WriteLine($"{pseudo} a trouvé le mot : {mot} !");
        }
        /// <summary>
        /// Méthode pour ajouter le mot trouvé au score du joueur en utilisant le nombre de points que rapporte chaque mot 
        /// </summary>
        /// <param name="mot"></param>
        public void AjouterAuScore(string mot)
        {
            int points = CalculerScore(mot);
            score += points;
            Console.WriteLine($"{pseudo} a gagné {points} points ! Nouveau score : {score}");
        }
        /// <summary>
        /// Méthode pour calculer le score du joueur 
        /// </summary>
        /// <param name="mot"></param>
        /// <returns></returns>
        public int CalculerScore(string mot)
        {
            int pointstotal = 0;

            foreach (char lettre in mot)
            {
                if (valeurLettres.ContainsKey(lettre))
                {
                    int pointsLettre = valeurLettres[lettre];
                    pointstotal += pointsLettre;

                   ///Affichage des points 
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
