using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;


namespace Projet_Algo_1
{
    public class Tri_Fichier
    {
        ///On propose ici 3 méthodes pour le tri du fichier et la recherche du mot à l'intérieur du fichier 
        /// Méthode 1 : Méthode basique, on prend le fichier tel quel et on recherche le mot à l'intérieur 
        
        private string cheminFichier { get; set; }///On définit l'attribut

        public Tri_Fichier(string cheminFichier)///Constructeur naturel
        {
            if (File.Exists(cheminFichier))///On teste l'existence du chemin du fichier 
            {
                this.cheminFichier = cheminFichier;///On fait l'association du constructeur naturel
            }
            else
            {
                Console.WriteLine("Le fichier n'existe pas.");///On affiche ce message si le fichier n'existe pas 
            }
        }

        public static bool RechercheMot(string mot, string cheminFichier)///Méthode permettant de rechercher un mot dans le fichier
        {
            
            try///Contient le code que l'on veut exécuter et qui pourrait provoquer une erreur
            {
                string contenu = File.ReadAllText(cheminFichier);///On définit le contenu du fichier comme étant le texte tout entier en lisant la totalité du document
                return contenu.Contains(mot);///On retourne vrai si le fichier contient le mot
            }
            catch(Exception ex) ///Permet de traiter les exceptions restantes 
            {
                Console.WriteLine($"Erreur lors de la lecture du fichier : {ex.Message}");///On affiche le message d'erreur apparu 
                return false;
            }
        }
                

    }
}
