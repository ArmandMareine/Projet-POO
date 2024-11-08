using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Algo_1
{
    public class Temps
    {
        private int tempsinitial;
        private int tempsparjoueur;
        //joueur à importer en paramètres

        public Temps(int tempsinitial, int tempsparjoueur)///Constructeur naturel de la classe Temps
        { 
            this.tempsinitial = tempsinitial; /// Le temps initial pour la partie est saisi en paramètres
            this.tempsparjoueur = tempsparjoueur; ///Le temps par joueur est initialement de 60 secondes, mais configurable par l'utilisateur
        }   

        /*
        public int TempsRestantTotal()///Cette fonction permet de calculer à tout instant le temps restant de la partie
        {
            int tempsrestant=tempsinitial;
            Console.WriteLine(AfficheDebutTempsPartie());///On affiche que la partie débute
            for(int i =0; i < tempsinitial; i++)
            {
                if (i == tempsinitial-1)
                {
                    Console.WriteLine(AfficheFinTemps());///On affiche que le temps est écoulé
                }
                tempsrestant = tempsrestant - 1;
                Thread.Sleep(1000);///On force une pause d'une seconde entre chaque itération
            }
            return tempsrestant;
        }
        */
        /*
        public int TempsRestantJoueur()///Cette focntion calcule le temps restant par joueur 
        {
            int tempsrestantjoueur = tempsparjoueur;
            Console.WriteLine(AfficheDebutTempsJoueur());///On affiche le temps initial pour le joueur
            for(int i=0;  i < tempsrestantjoueur; i++)
            {
                if (i == tempsrestantjoueur-1)
                {
                    Console.WriteLine(AfficheFinTemps());///On affiche que le temps est écoulé
                }
                tempsrestantjoueur =tempsrestantjoueur - 1;
                Thread.Sleep(1000);///On force une pause d'une seconde entre chaque itération
            }
            return tempsrestantjoueur;
        }
        */
        public string AfficheFinTemps()///On affiche que le temps est écoulé
        {
            string res1 = $"Le temps est écoulé ! ";
            return res1 ;
        }
        public string AfficheDebutTempsJoueur()///On affiche le temps dont le joueur dispose pour jouer
        {
            string res2 = $"Tu as {tempsparjoueur} secondes pour jouer ! ";
            return res2 ;
        }
        public string AfficheDebutTempsPartie()///On affiche le temps total de la partie 
        {
            string res3 = $"Bienvenue dans le jeu ! La durée totale du jeu est de {tempsinitial/60} minutes";
            return res3 ;
        }
        
    }
}
