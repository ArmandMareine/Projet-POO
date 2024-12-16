using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Projet_Algo_1
{
    internal class Jeu
    {
        static void Main(string[] args)
        {
            #region Paramètres
            string langue;
            string cheminFichierMots; 
            string cheminFichierLettres = "../../../../lettres.txt";
            #endregion

            #region Choix de la langue du dictionnaire
            do
            {
                Console.WriteLine("Choisissez la langue du dictionnaire (Français ou Anglais) : ");
                langue = Console.ReadLine()?.Trim();
            } while (langue != "Français" && langue != "Anglais");

            ///Définir chemin du fichier de dictionnaire en fonction de la langue choisie
            cheminFichierMots = langue == "Français" ? "../../../../MotsPossiblesFR.txt" : "../../../../MotsPossiblesEN.txt";

            ///Chargement du dicionnaire
            Dictionnaire dictionnaire = new Dictionnaire(cheminFichierMots, langue);
            List<string> mots = dictionnaire.ChargerMots(cheminFichierMots);
            List<string> triée = Tri_Fichier_2.TriparFusion(mots);
            #endregion

            #region Joueurs
            ///Nombre de joueurs
            int nbJoueurs;
            do
            {
                Console.WriteLine("Saisir le nombre de joueurs (minimum 2, maximum 10) : ");
            }while(!int.TryParse(Console.ReadLine(), out nbJoueurs) || nbJoueurs < 2 || nbJoueurs > 10);

            ///Créer les joueurs
            Joueur[] joueurs = new Joueur[nbJoueurs];
            for(int i = 0;i < joueurs.Length; i++)
            {
                Console.WriteLine($"Saisir le pseudo du joueur n°{i + 1} : ");
                string pseudo = Console.ReadLine()?.Trim();
                joueurs[i] = new Joueur(i+1,pseudo,0,new List<Lettre>());
            }
            #endregion

            #region Plateau
            ///Initialisation du plateau
            Console.WriteLine("Saisir la taille du plateau voulue (minimum 4, maximum 8) : ");
            int taillePlateau = int.Parse(Console.ReadLine());
            List<Lettre> lettres = Lettre.LectureFichier(cheminFichierLettres);
            Plateau plateau = new Plateau(taillePlateau);
            plateau.InitialiserDés(lettres, taillePlateau);
            plateau.LancerTousLesDés(taillePlateau);
            #endregion

            #region Déroulé du jeu
            ///Durée de la partie
            int dureePartieMinutes;
            do
            {
                Console.Write("Saisir la durée de la partie en minutes : ");
            }while(!int.TryParse(Console.ReadLine(), out dureePartieMinutes) || dureePartieMinutes <= 0);

            TimeSpan dureePartie = TimeSpan.FromMinutes(dureePartieMinutes);

            ///Durée des tours
            int dureeTourSecondes;
            do
            {
                Console.Write("Saisir la durée des tours en secondes : ");
            } while (!int.TryParse(Console.ReadLine(), out dureeTourSecondes) || dureeTourSecondes <= 0);

            TimeSpan dureeTours = TimeSpan.FromSeconds(dureeTourSecondes);

            ///Début du jeu

            DateTime debutPartie = DateTime.Now;

            while(DateTime.Now - debutPartie < dureePartie)
            {
                foreach(var joueur in joueurs)
                {
                    Console.WriteLine($"C'est au tour du joueur {joueur.pseudo}:");
                    Console.WriteLine($"Temps restant pour ce tour : {dureeTours.TotalSeconds} secondes");

                    DateTime debutTour = DateTime.Now;

                    ///Tour du Joueur
                    while(DateTime.Now - debutTour <= dureeTours)
                    {
                        Console.WriteLine("Entrez un mot à former avec les lettres du plateau:");
                        string mot = Console.ReadLine().ToUpper();

                        if (!string.IsNullOrEmpty(mot))
                        {
                            ///Vérification conditions du mot
                            if(VérificationMots.VérifLongueur(mot) && plateau.FormableAvecPlateau(mot))
                            {
                                if(VérificationMots.AppartientDictionnaire(mot, langue, cheminFichierMots))
                                {
                                    if (!joueur.ContientMot(mot))
                                    {
                                        joueur.Add_Mot(mot);
                                        joueur.AjouterAuScore(mot);
                                    }
                                    else
                                    {
                                        Console.WriteLine("Ce mot a déjà été trouvé !");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Le mot n'est pas dans le dictionnaire.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Le mot n'est pas formable ou trop court.");
                            }
                        }
                    }
                }
                if(DateTime.Now - debutPartie > dureePartie)
                {
                    Console.WriteLine("Le temps de la partie est écoulé !");
                    break;
                }
            }

            ///Annonce du gagnant
            Joueur gagnant = joueurs.OrderByDescending(j => j.GetScore()).First();
            Console.WriteLine($"Le gagnant est {gagnant.pseudo} avec un score de {gagnant.GetScore()} points.");
            Console.WriteLine("Fin de la partie ! Merci d'avoir joué !");
            #endregion




            /*
            string cheminFichier = "../../../../MotsPossiblesFR.txt";
            Console.Write(cheminFichier+ "\n");
            string Langue = "Français";
            Dictionnaire test = new Dictionnaire(cheminFichier,Langue);
            List<string> list = test.ChargerMots(cheminFichier);
            Dictionary<char, int> compteur = test.TriParLettres();
            Dictionary<int , int> compteur1 = test.TriParLongueur();
            string res = test.toString(compteur,compteur1);
            Console.WriteLine(res);
            ///Test lecturefichier mots
            List<string> tri = Tri_Fichier_2.TriparFusion(list);
            Console.WriteLine(Tri_Fichier_2.RechercheDichotomique(tri,"Zoo"));
            int taille1 = 4;
            string cheminFichier2 = "../../../../lettres.txt";
            List<Lettre> lettres = Lettre.LectureFichier(cheminFichier2);
            Plateau plateau = new Plateau(taille1);
            plateau.InitialiserDés(lettres,taille1);
            plateau.LancerTousLesDés(4);
            /// Affiche les lettres importées
            if (lettres.Count > 0)
            {
                Console.WriteLine("Lettres importées avec succès !");
            }
            else
            {
                Console.WriteLine("Aucune lettre n'a été importée.");
            }
           
            /*
           foreach (string str in tri)
           {
               Console.WriteLine(string.Join(",", tri));
           }
           Console.WriteLine("Ok");



           foreach (string mot in list)
           {
               Console.WriteLine(mot);
           }
           */







            ////////////////////////////////////////////////////////////////////////
            //Fin de la zone de test : Début du jeu 
            ///Console.WriteLine("Bienvenue au jeu du Boggle ! ");
            ///Définition des chemins fichiers à rajouter (le lettre)
            ///Défintion des timers
            /*
            Console.Write("Saisir la langue désirée (Anglais ou Français):");///On définit la langue utilisée
            string langue = Convert.ToString(Console.ReadLine());
            string EN = "Anglais";
            string FR = "Français";
            bool fr = FR.Equals(langue);
            bool en = EN.Equals(langue);
            if (fr)
            {
                cheminFichier = "../../../../MotsPossiblesFR.txt";///On initialise le fichier français

            }
            else if (en)
            {
                cheminFichier = "../../../../MotsPossiblesEN.txt";///On initialise le fichier anglais
            }
            else
            {
                do
                {
                    Console.Write("Le format est incorrect ! Format attendu : Anglais ou Français  ");///En cas d'erreur de saisie, on recommence
                    langue = Convert.ToString(Console.ReadLine());
                } while (!fr && !en);
            }///Test de la bonne saisie des langues 
            
            if (langue == "Anglais" || langue == "Français")
            {
                Console.Write("Saisir le nombre de joueurs voulus (Attention, le minimum est 2 et le maximum 10) : ");///On saisit le nombre de joueurs
                int nbjoueurs = int.Parse(Console.ReadLine());
                Joueur[] joueurs = new Joueur[nbjoueurs];///Définition d'un tableau de joueur pour contenir les informations chaque participant
                int score = 0;
                for (int i = 0; i < nbjoueurs; i++)
                {
                    Console.Write($"Saisir le pseudo du joueur {i + 1} : ");
                    string pseudo = Convert.ToString(Console.ReadLine());
                    joueurs[i] = new Joueur(i + 1, pseudo, score);///On remplit le tableau de joueurs avec les pseudos de le numéro de chaque joueur
                }
                Console.WriteLine("Les joueurs sont donc : ");
                foreach (var joueur in joueurs)///Affichage de tous les joueurs
                {
                    Console.WriteLine(joueur.ToString());
                }
                Console.Write("Entrez la taille du plateau (par exemple, 4 pour un plateau 4x4) : ");///Initialisation de la taille du plateau
                int taille = int.Parse(Console.ReadLine());
                Console.Write("Saisir le temps désiré pour la partie en minutes (par exemple, 2 pour 2 minutes de partie): ");
                int tempstotal = int.Parse(Console.ReadLine()) * 60;
                TimeSpan dureetotale = TimeSpan.FromMinutes(tempstotal);///Initialisation du premier timer, le timer pour le jeu total
                Console.Write("Saisir le temps de jeu par joueur en minutes (par exemple, 1 pour 1 minute de jeu) :");
                int tempsjoueur = int.Parse(Console.ReadLine()) * 60;
                TimeSpan dureeparjoueur = TimeSpan.FromMinutes(tempsjoueur); ///Initialisation du temps de jeu par joueur à une valeur par défaut de 1 minute
                Console.WriteLine($"Le temps total de la partie est donc de : {dureetotale} minutes");
                Console.WriteLine($"Le temps de jeu pour chaque joueur est de : {dureeparjoueur}");
                ///Début du jeu 
                Console.WriteLine("La partie commence ! ");
                DateTime debutPartie = DateTime.Now;///Initialisation du début de la partie au moment de son lancement

                while (DateTime.Now - debutPartie < dureetotale)
                {
                    for (int i = 0; i < joueurs.Length; i++)
                    {
                        Console.WriteLine($"C'est au tour du joueur {i + 1} : {joueurs[i]} de jouer !");///On donne le temps de jeu au joueur
                        Console.WriteLine($"Ton temps de jeu est de {tempsjoueur / 60} minutes");///On donne le temps de jeu au joueur 
                        Console.WriteLine("Voici ton plateau :");
                        ///Affichage du plateau
                        ///SUITE DU JEU






                    }


                    DateTime debutTour = DateTime.Now;
                    while (true)///Test en permanence
                    {

                        if (DateTime.Now - debutTour > dureeparjoueur)///On teste si le temps de jeu du joueur est écoulé
                        {
                            Console.WriteLine("Le temps pour le joueur X est écoulé");
                            break;
                        }
                        if (Console.KeyAvailable)///Si le joueur appuie sur une touche, la boucle s'arrête 
                        {
                            Console.ReadKey();
                            Console.WriteLine("Action du joueur enregistrée");
                            break;
                        }
                    }
                    if (DateTime.Now - debutPartie > dureetotale)///Fin du jeu : test
                    {
                        Console.WriteLine("Temps total du jeu du Boggle écoulé ! ");
                        break;
                    }
                    

                }
           
                Console.WriteLine("Fin de la partie ! Merci ! ");///Affichage fin de partie 

            
            }  
            */

        }
            
    }
   
}

