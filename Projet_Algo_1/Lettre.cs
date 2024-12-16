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
        //private double poids; NEUTRALISE car pas encore fonctionnel 

        
        public Lettre(char caractere, int valeur, int nombre)///Constrcuteur naturel de la classe Lettre
        {
            this.caractere = caractere;
            this.valeur = valeur;
            this.nombre = nombre;
            //this.poids = poids;
            
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
        /*
        public double Poids
        {
            get { return poids; }
        }
        */
        public char Caractere
        {
            get { return caractere; }   
        }
        public string ToString()
        {
            return $"{caractere} (Valeur : {valeur}, Occurences : {nombre})";
        }
        
        public static List<Lettre> LectureFichier(string cheminFichier2)
        {
            if (!File.Exists(cheminFichier2))
            {
                Console.WriteLine("Le fichier n'existe pas : " + cheminFichier2);
                return new List<Lettre>(); /// Retourne une liste vide si le fichier est absent
            }

            List<Lettre> lettres = new List<Lettre>(); /// Initialise la liste de lettres
            try
            {
                using (StreamReader sr = new StreamReader(cheminFichier2))
                {
                    string ligne;
                    while ((ligne = sr.ReadLine()) != null)
                    {
                        string[] parties = ligne.Split(';', StringSplitOptions.RemoveEmptyEntries);
                        if (parties.Length == 3) /// Vérifie qu'il y a bien 3 éléments par ligne
                        {
                            try
                            {
                                char caractere = parties[0][0]; // Premier caractère de la chaîne
                                int valeur = int.Parse(parties[1]);
                                int nombre = int.Parse(parties[2]);
                                
                                /// Ajoute la lettre si tout est valide
                                lettres.Add(new Lettre(caractere, valeur, nombre));
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine($"Format incorrect dans la ligne : {ligne}");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Ligne mal formée : {ligne}");
                        }
                    }
                }

                /// Affiche toutes les lettres pour vérification
                /*
                Console.WriteLine("Lettres importées :");
                foreach (var lettre in lettres)
                {
                    Console.WriteLine(lettre.ToString());
                }
                */
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la lecture du fichier : {ex.Message}");
            }

            return lettres;
        }
    }
}
