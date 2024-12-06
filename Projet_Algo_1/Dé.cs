using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Algo_1
{
    public class Dé
    {
<<<<<<< HEAD
        private Random aléatoire;
        private List<string> lettres; /// Les 6 lettres du dé
        private string faceVisible;   /// La lettre visible après un lancer
        private List<string> faces_du_dé;
        public Dé(List<string> lettres)///Constructeur naturel
=======
        private List<Lettre> faces; /// Liste de lettres associées aux faces du dé
        private Lettre faceVisible; /// La lettre actuellement visible

        public Dé(List<Lettre> lettres)
>>>>>>> 876a54ab12518a60d74cd5c45fdd848c2f697700
        {
            if (lettres.Count != 6)
                throw new ArgumentException("Un dé doit avoir exactement 6 faces.");

<<<<<<< HEAD
            ///Initialisation de chaque faces du dé avec une lettre aléatoire de l'alphabet
            for(int i = 0; i < faces_du_dé.Count; i++)
            {
                int index = aléatoire.Next(0, lettres.Count);
                faces_du_dé[i] = lettres[index];
            }
            
=======
            faces = lettres;
            faceVisible = faces[0]; /// Initialise avec la première lettre
>>>>>>> 876a54ab12518a60d74cd5c45fdd848c2f697700
        }

        public void Lance(Random r)
        {
            faceVisible = faces[r.Next(faces.Count)]; /// Tire une face au hasard
        }
         
        public override string ToString()
        {
            return faceVisible.Caractere.ToString(); /// Affiche uniquement le caractère de la face visible
        }

<<<<<<< HEAD

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
=======
>>>>>>> 876a54ab12518a60d74cd5c45fdd848c2f697700
    }
}
