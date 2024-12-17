using Microsoft.VisualStudio.TestTools.UnitTesting;
using Projet_Algo_1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Algo_1.Tests
{
    [TestClass()]
    public class JoueurTests
    {
        private Joueur InitialisationJoueurTest()
        {
            var lettres = new List<Lettre>
            {
                new Lettre('A', 2, 3),
                new Lettre('B', 3, 3),
                new Lettre('C', 3, 2),
                new Lettre('D',2,4), 
            };
            return new Joueur(1,"Aubin",0, lettres);///On définit un nouvel élément qui sera utile pour le test
        }
        [TestMethod()]
        public void JoueurTest()
        {
            var joueur = InitialisationJoueurTest();///On appelle les éléments initialisés 
            Assert.IsNotNull(joueur);
            Assert.AreEqual("Aubin", joueur.pseudo);///on teste ici si le pseudo du joueur est bien égal à celui initialisé
            Assert.AreEqual(0, joueur.GetScore());  ///On vérifie si le score est bien nul ici. 
            Assert.AreEqual("Joueur 1 : Aubin",joueur.toString());///On test si l'affichage de la méthode est correcte 
            
        }

        [TestMethod()]
        public void GetScoreTest()///test de la méthode GetScoreTest()
        {
            var joueur = InitialisationJoueurTest();
            int score = joueur.GetScore(); ///On définit le score 
            Assert.AreEqual(0, score);///On teste si le score initial est bien égal à 0.
           
        }

        [TestMethod()]
        public void toStringTest()
        {
            var joueur = InitialisationJoueurTest();
            string res = joueur.toString();///On définit le string
            Assert.AreEqual("Joueur 1 : Aubin", res);///Test de l'égalité dans l'affichage par la fonction
            
        }

        [TestMethod()]
        public void ContientMotTest()///On teste la méthode qui vérifie si un mot a déjà été trouvé par le joueur
        {
            var joueur = InitialisationJoueurTest();
            joueur.Add_Mot("Test");///On ajoute un mot dans la liste de string
            bool motcontenu=joueur.ContientMot("Test");///On initialise le résultat true
            bool motnoncontenu = joueur.ContientMot("PASTEST");///On initialise le false

            Assert.IsTrue(motcontenu);
            Assert.IsFalse(motnoncontenu);  ///On vérifie que l'on retourne bien true/false sur les deux cas

        }

        [TestMethod()]
        public void Add_MotTest()///On teste la méthode d'ajout du mot
        {
            var joueur = InitialisationJoueurTest();
            joueur.Add_Mot("TEST1");
            Assert.IsTrue(joueur.ContientMot("TEST1"));///On définit une sortie true pour le cas d'un mot valide
            joueur.Add_Mot("");///Mot au format invalide
            joueur.Add_Mot("TEST1");///Mot déjà trouvé par le joueur, ne peut donc pas être ajouté 
            
        }

        [TestMethod()]
        public void AjouterAuScoreTest()///On teste ici la méthode d'ajout du score 
        {
            var joueur = InitialisationJoueurTest();
            joueur.AjouterAuScore("AB");///Rappel : A=2 et B=3
            Assert.AreEqual(5, joueur.GetScore());  ///Vérification de l'égalité 
            
        }

        [TestMethod()]
        public void CalculerScoreTest()///TTest de la méthode du calcul du score
        {
            var joueur = InitialisationJoueurTest();
            int score = joueur.CalculerScore("AB");///A=2 et B=3
            int scoreFAUX = joueur.CalculerScore("U");///U n'est pas défini au départ, donc il retournera faux
            Assert.AreEqual(5, score);  ///Egalité TRUE
            Assert.AreEqual(0, scoreFAUX);///Egalité 0=0 car socre invalide 
            
        }
    }
}