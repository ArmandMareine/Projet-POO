using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Projet_Algo_1
{
    public class Lettre
    {
        private char caractere;
        private int valeur;
        private int nombre; 
        private double poids;

        
        public Lettre(char caractere, int valeur, int nombre, double poids)///Constrcuteur naturel de la classe Lettre
        {
            this.caractere = caractere;
            this.valeur = valeur;
            this.nombre = nombre;
            this.poids = poids;
            
        }
        ///Propriétés en Get pour les attributs
        public int Nombre
        {
            get { return nombre; }
        }
        public int Valeur
        {
            get { return valeur; }
        }
        public double Poids
        {
            get { return poids; }
        }
        public char Caractere
        {
            get { return caractere; }   
        }
        public string ToString()
        {
            return $"{caractere} (Valeur : {valeur}, Nombre : {nombre}, Poids : {poids})";
        }

        public static List<Lettre> LectureFichier(string cheminFichier2)
        {
            if (!File.Exists(cheminFichier2))
            {
                Console.WriteLine("Le fichier n'existe pas : " + cheminFichier2);
                
            }
            List<Lettre> lettres = new List<Lettre>();///On initialise la liste de départ
            string ligne;
            try
            {
                StreamReader sr = new StreamReader(cheminFichier2);
                ligne = sr.ReadLine();
                while (ligne != null)
                {
                    string[] parties = ligne.Split(';', StringSplitOptions.RemoveEmptyEntries);
                    if(parties.Length == 4)///Un regroupement de quatre informations sur la lettre, on les sépare ici. On peut modifier par 3 si besoin
                    {
                        char caractere = parties[0][0];
                        int valeur = int.Parse(parties[1]);
                        int nombre = int.Parse(parties[2]);
                        double poids = double.Parse(parties[3]);///On divise les différentes parties

                        lettres.Add(new Lettre(caractere, valeur, nombre, poids));///On ajoute un nouvel élément Lettre à la liste 
                    }
                    foreach (var lettre in lettres)
                    {
                        Console.WriteLine(lettre);  
                    }
                    ligne = sr.ReadLine();
                }
                sr.Close();
            }
            catch (FileNotFoundException)///Exécute ce bloc si l'exception FileNotFoundException est relevée, soit que le fichier n'a pas été trouvé
            {
                Console.WriteLine($"Erreur : Le fichier '{cheminFichier2}' est introuvable.");
            }
            catch (UnauthorizedAccessException)///Exécute ce bloc si l'exception UnauthorizedAccessException est relevée, soit que l'accès n'est pas autorisé
            {
                Console.WriteLine($"Erreur : Accès non autorisé au fichier '{cheminFichier2}'.");
            }
            catch (Exception ex)///Attrape toutes les autres exceptions
            {
                Console.WriteLine($"Erreur lors de la lecture du fichier : {ex.Message}");
            }
            return lettres;
        }
    }
}
