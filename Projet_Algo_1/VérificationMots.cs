using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Algo_1
{
    public class VérificationMots
    {
        
        private Dé[,] plateau;
        private List<string> motstrouvés;
        private string langue;
        private int scoreJoueur;
        private string cheminfichier1 ;
        private string cheminfichier2 ;

        public VérificationMots(Dé[,] plateau, List<string> motstrouvés, string langue, int scoreJoueur, string cheminfichier1, string cheminfichier2)///Constrcteur naturel
        {

            this.plateau = plateau;
            this.motstrouvés = motstrouvés;
            this.langue = langue;
            this.scoreJoueur = scoreJoueur;
            cheminfichier1 = "../../../../MotsPossiblesFR.txt";
            cheminfichier2 = "../../../../MotsPossiblesEN.txt";
        }
        
        public static void DefLangue(string langue, string cheminfichier1, string cheminfichier2)///Méthode pour définir la langue de vérification des mots 
        {
            
            if (langue != null)
            {
                if (langue == "Anglais")
                {
                    Dictionnaire dictionnaire = new Dictionnaire(cheminfichier2, langue);
                    List<string> mots = dictionnaire.ChargerMots(cheminfichier2);
                }
                else if(langue=="Français")
                {
                    Dictionnaire dictionnaire = new Dictionnaire(cheminfichier1, langue);
                    List<string> mots = dictionnaire.ChargerMots(cheminfichier1);
                }
                else
                {
                    Console.WriteLine("Erreur sur la langue saisie");
                }
            }
        }

        
    }
}
