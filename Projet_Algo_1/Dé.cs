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

        public Dé(List<Lettre> faces)
        {
            if (faces.Count != 6)
            {
                throw new ArgumentException("Erreur. Un dé doit avoir exactement 6 faces.");
            }
               

            this.faces = faces;
            Lance(new Random()); /// Tire une face au hasard à l'initialisation
        }

        public void Lance(Random random)
        {
            FaceVisible = faces[random.Next(faces.Count)];
        }
        
        public Lettre FaceVisible
        {
            get { return faceVisible; }
            set { faceVisible = value; }
        }

        public override string ToString()
        {
            return faceVisible.Caractere.ToString(); /// Affiche uniquement le caractère de la face visible
        }

    }
}