using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Projet_Algo_1
{
    public class Dé
    {
        private List<Lettre> faces; /// Liste de lettres associées aux faces du dé
        private Lettre faceVisible; /// La lettre actuellement visible
        /// <summary>
        /// Constructeur naturel de la classe Dé
        /// </summary>
        /// <param name="faces"></param>
        /// <exception cref="ArgumentException"></exception>
        public Dé(List<Lettre> faces)
        {
            if (faces.Count != 6)
            {
                throw new ArgumentException("Erreur. Un dé doit avoir exactement 6 faces.");
            }
               

            this.faces = faces;
            Lance(new Random()); /// Tire une face au hasard à l'initialisation
        }
        /// <summary>
        /// Méthode du lancé de dé
        /// </summary>
        /// <param name="random"></param>

        public void Lance(Random random)
        {
            FaceVisible = faces[random.Next(faces.Count)];
        }
        /// <summary>
        /// Propriété de la face visible
        /// </summary>
        public Lettre FaceVisible
        {
            get { return faceVisible; }
            set { faceVisible = value; }
        }
        /// <summary>
        /// Méthode ToString pour afficher uniquement le caractère de la face visible
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return faceVisible.Caractere.ToString(); 
        }

    }
}