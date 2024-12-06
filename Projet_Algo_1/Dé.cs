using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Algo_1
{
    public class Dé
    {
        private Random aléatoire;
        private List<string> lettres; /// Les 6 lettres du dé
        private string faceVisible;   /// La lettre visible après un lancer
        private List<string> faces_du_dé;
        public Dé(List<string> lettres)///Constructeur naturel
        {
            this.lettres = lettres;
            faceVisible = lettres[0];

            ///Initialisation de chaque faces du dé avec une lettre aléatoire de l'alphabet
            for(int i = 0; i < faces_du_dé.Count; i++)
            {
                int index = aléatoire.Next(0, lettres.Count);
                faces_du_dé[i] = lettres[index];
            }
            
        }

        /// <summary>
        /// La fonction lancé permet de "lancer" le dé et de retourner une seule lettre qui sera considérer comme la lettre de la face supérieure qui apparaîtra sur le plateau
        /// </summary>
        /// <returns>Lettre de la face supérieur</returns>
        public void Lancé(Random r)
        {
            int index = r.Next(lettres.Count); /// Tire un index au hasard
            faceVisible = lettres[index];///Face visible du plateau 
        }


        /// <summary>
        /// Retourne une chaîne de caractère décrivant les faces du dé en séparant chacune des lettres des faces du dé par " , " sans mettre ce séparateur à la fin.
        /// </summary>
        /// <returns>Exemple de retour : Les faces du dé sont composées par les lettres : A , B , C , D , E , F</returns>

        
        public string toString()
        {
            string resultat = "Les faces du dé sont composées par les lettres : ";
            for (int i = 0; i < faces_du_dé.Count - 1; i++)
            {
                resultat += faces_du_dé[i] + " , ";
            }
            resultat += faces_du_dé[faces_du_dé.Count - 1];
            return resultat;
        }
        
        public string ToString()
        {
            return $"Dé : {string.Join(", ", lettres)} | Face visible : {faceVisible}";///On affiche le dé et la face visible du dé 
        }
    }
}
