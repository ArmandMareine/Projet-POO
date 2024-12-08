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

        public Dé(List<Lettre> lettres)
        {
            if (lettres.Count != 6)
                throw new ArgumentException("Un dé doit avoir exactement 6 faces.");

            faces = lettres;
            faceVisible = faces[0]; /// Initialise avec la première lettre
        }
        public Lettre FaceVisble
        {
            get { return faces[1]; }
            set { faces[1] = value; }
        }

        public void Lance(Random r)
        {
            faceVisible = faces[r.Next(faces.Count)]; /// Tire une face au hasard
        }

        public override string ToString()
        {
            return faceVisible.Caractere.ToString(); /// Affiche uniquement le caractère de la face visible
        }

    }
}