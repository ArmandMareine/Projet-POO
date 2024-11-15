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
        private char character;
        private int valeur;
        private int nombre; 
        private double poids;

        
        public Lettre(char character, int valeur, int nombre, double poids)
        {
            this.character = character;
            this.valeur = valeur;
            this.nombre = nombre;
            this.poids = poids;
            
        }
        public int Nombre
        {
            get { return nombre; }
        }


        public string ToString()
        {
            return $"{character} (Valeur : {valeur}, Nombre : {nombre}, Poids : {poids})";
        }

        public List<Lettre> LectureFichier()
        {
            string cheminFichier = @"C:\Users\LENOVO\Documents\ESILV\A2\S1\MODULES\ALGORITHME & POO\PROJET POO\Lettres (2).txt";
            List<Lettre> lettres = new List<Lettre>();///On initialise la liste de départ
            string ligne;
            try
            {
                StreamReader sr = new StreamReader(cheminFichier);
                ligne = sr.ReadLine();
                while (ligne != null)
                {
                    string[] parties = ligne.Split(';', StringSplitOptions.RemoveEmptyEntries);
                    if(parties.Length == 4)///Un regroupement de quatre informations sur la lettre, on les sépare ici. On peut modifier par 3 si besoin
                    {
                        character = parties[0][0];
                        valeur = int.Parse(parties[1]);
                        nombre = int.Parse(parties[2]);
                        poids = double.Parse(parties[3]);///On divise les différentes parties

                        lettres.Add(new Lettre(character, valeur, nombre, poids));
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
                Console.WriteLine($"Erreur : Le fichier '{cheminFichier}' est introuvable.");
            }
            catch (UnauthorizedAccessException)///Exécute ce bloc si l'exception UnauthorizedAccessException est relevée, soit que l'accès n'est pas autorisé
            {
                Console.WriteLine($"Erreur : Accès non autorisé au fichier '{cheminFichier}'.");
            }
            catch (Exception ex)///Attrape toutes les autres exceptions
            {
                Console.WriteLine($"Erreur lors de la lecture du fichier : {ex.Message}");
            }
            return lettres;
        }
    }
}
