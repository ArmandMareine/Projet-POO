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
        private List<Lettre> Faces; /// Liste de lettres associées aux faces du dé
        private Lettre faceVisible; /// La lettre actuellement visible

        public Dé(List<Lettre> lettres)
        {
            if (lettres.Count != 6)
                throw new ArgumentException("Un dé doit avoir exactement 6 faces.");

            Faces = lettres;
            faceVisible = Faces[0]; /// Initialise avec la première lettre
        }
        public Lettre FaceVisible
        {
            get { return faceVisible; }
            set { faceVisible = value; }
        }

        public void Lance(Random r)
        {
            if(r == null) throw new ArgumentNullException(nameof(r));
            if(Faces == null || Faces.Count == 0) throw new InvalidOperationException("Les faces du dé ne sont pas initialisées.");

            List<Lettre> facesPonderees = new List<Lettre>();
            foreach (Lettre lettre in Faces)
            {
                for(int i = 0; i < lettre.Nombre; i++)
                {
                    facesPonderees.Add(lettre);
                }
            }

            // Sélectionner une lettre au hasard dans la liste pondérée
            faceVisible = facesPonderees[r.Next(facesPonderees.Count)];
        }

        public string AfficherFaces()
        {
            return string.Join(", ", Faces.Select(f => f.Caractere));
        }
        public override string ToString()
        {
            return faceVisible.Caractere.ToString(); /// Affiche uniquement le caractère de la face visible
        }

    }
}