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
        /// <summary>
        /// Constrcuteur naturel de la classe Lettre
        /// </summary>
        /// <param name="caractere"></param>
        /// <param name="valeur"></param>
        /// <param name="nombre"></param>
        public Lettre(char caractere, int valeur, int nombre)
        {
            this.caractere = caractere;
            this.valeur = valeur;
            this.nombre = nombre;
            
        }
        /// <summary>
        /// Propriétés d'accès en lecture de Nombre
        /// </summary>
        public int Nombre
        {
            get { return nombre; }
        }
        /// <summary>
        ///  Propriétés d'accès en lecture de Valeur
        /// </summary>
        public int Valeur
        {
            get { return valeur; }
        }
        /// <summary>
        ///  Propriétés d'accès en lecture de Caractère
        /// </summary>
        public char Caractere
        {
            get { return caractere; }   
        }
        /// <summary>
        /// Méthode ToString pour l'affichage de la lettre et de ses attributs 
        /// </summary>
        /// <returns></returns>
        public string ToString()
        {
            return $"{caractere} (Valeur : {valeur}, Occurences : {nombre})";
        }
        /// <summary>
        /// Méthode de pondération des lettres
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static List<Lettre> Pondération(List<Lettre> list)
        {
            List<Lettre> result = new List<Lettre>();
            foreach (Lettre lettre in list)
            {
                for(int i=0; i<lettre.nombre; i++)
                {
                    result.Add(lettre);
                }
                
            }
            return result;
        }
        /// <summary>
        /// Méthode de lecture du fichier lettres avec usage du StreamReader
        /// </summary>
        /// <param name="cheminFichier2"></param>
        /// <returns></returns>
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
                                char caractere = parties[0][0]; /// Premier caractère de la chaîne
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
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la lecture du fichier : {ex.Message}");
            }
            return lettres;
        }
    }
}
