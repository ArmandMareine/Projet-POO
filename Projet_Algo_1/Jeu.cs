using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using WordCloudSharp;

namespace Projet_Algo_1
{
    public  class Jeu
    {
        /// <summary>
        /// Déclaration des arguments de la classe Jeu
        /// </summary>
        private string langue;
        private string cheminFichierMots;
        private string cheminFichierLettres = "../../../../lettres.txt";
        private Joueur[] joueurs;
        private Plateau plateau;
        private TimeSpan dureePartie;
        private TimeSpan dureeTours;
        /// <summary>
        /// Constructeur naturel de la classe Jeu
        /// </summary>
        public Jeu()
        {
            InitialiserParamètresJeu();
        }
        /// <summary>
        /// Méthode pour initialiser les différents paramètres nécessaires au lancement du jeu
        /// </summary>
        public void InitialiserParamètresJeu()
        {
            Console.WriteLine("Bienvenue dans le jeu du Boggle ! Ce jeu a été conçu par Aubin Lin et Armand Mareine. Bonne partie !");
            langue = GetLangueDictionnaire();
            cheminFichierMots = langue == "Français" ? "../../../../MotsPossiblesFR.txt" : "../../../../MotsPossiblesEN.txt";
            List<Lettre> lettres = Lettre.LectureFichier(cheminFichierLettres);
            List<Lettre> facespondérées = Lettre.Pondération(lettres);
            Dictionnaire dictionnaire = new Dictionnaire(cheminFichierMots, langue);
            List<string> mots = dictionnaire.ChargerMots(cheminFichierMots);
            int nbJoueurs = GetNbJoueurs(); /// Demander le nombre de joueurs
            joueurs = CreateJoueurs(nbJoueurs, lettres);

            ///Initialisation du plateau
            int taillePlateau = GetTaillePlateau();
            this.plateau = new Plateau(taillePlateau);
            plateau.InitialiserDés(facespondérées);

            ///Initialisation du temps de la partie
            dureePartie = GetTempsDePartie();
            dureeTours = GetTempsParTour();

            DérouléJeu();///Lancement du jeu
            return;
        }/// <summary>
        /// Méthode pour le déroulé du jeu 
        /// </summary>
        public void DérouléJeu()
        {
            Console.WriteLine($"La partie commence ! Vous avez {dureePartie.TotalMinutes} minutes.");
            DateTime debutPartie = DateTime.Now;
            while (DateTime.Now - debutPartie < dureePartie)
            {
                foreach (var joueur in joueurs)///Partie pour chaque joueur
                {
                    Console.WriteLine($"C'est au tour du joueur {joueur.pseudo}:");
                    Console.WriteLine($"Temps restant pour ce tour : {dureeTours.TotalSeconds} secondes");
                    plateau.LancerTousLesDés();
                    DateTime debutTour = DateTime.Now;

                    /// Tour du joueur
                    while (DateTime.Now - debutTour <= dureeTours)
                    {
                        string mot = DemanderMotPlateau();

                        if (!string.IsNullOrEmpty(mot))
                        {
                            /// Vérification des conditions du mot
                            if (Plateau.VérifLongueur(mot) && plateau.FormableAvecPlateau(mot))
                            {
                                if (Plateau.AppartientDictionnaire(mot, langue, cheminFichierMots, cheminFichierMots))
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

                if (DateTime.Now - debutPartie > dureePartie)///Test temporel
                {
                    Console.WriteLine("Le temps de la partie est écoulé !");
                    break;
                }
            }
            FindePartie();///On appelle la fin de partie
        }
        /// <summary>
        /// Fonction pour retourner la langue utilisée pour le dictionnaire
        /// </summary>
        /// <returns></returns>
        public string GetLangueDictionnaire()
        {
            string langue;
            do
            {
                Console.WriteLine("Choisissez la langue du dictionnaire (Français ou Anglais) : ");
                langue = Console.ReadLine()?.Trim();
            } while (langue != "Français" && langue != "Anglais");

            return langue;
        }
        /// <summary>
        /// Fonction pour retourner le temps de partie voulu
        /// </summary>
        /// <returns></returns>
        public TimeSpan GetTempsDePartie()
        {
            int dureePartieMinutes;
            do
            {
                Console.Write("Saisir la durée de la partie en minutes : ");
            } while (!int.TryParse(Console.ReadLine(), out dureePartieMinutes) || dureePartieMinutes <= 0);

            return TimeSpan.FromMinutes(dureePartieMinutes);
        }
        /// <summary>
        /// Fonction pour obtenir le nombre de joueurs 
        /// </summary>
        /// <returns></returns>
        public int GetNbJoueurs()
        {
            int nbJoueurs;
            do
            {
                Console.WriteLine("Saisir le nombre de joueurs (minimum 2, maximum 10) : ");
            } while (!int.TryParse(Console.ReadLine(), out nbJoueurs) || nbJoueurs < 2 || nbJoueurs > 10);

            return nbJoueurs;
        }
        /// <summary>
        /// Fonction qui gère les différents joueurs en renvoyant un tableau constitué de tous les joueurs 
        /// </summary>
        /// <param name="nbJoueurs"></param>
        /// <param name="lettres"></param>
        /// <returns></returns>
        public Joueur[] CreateJoueurs(int nbJoueurs, List<Lettre> lettres)///Méthode pour créer chaque joueur 
        {
            Joueur[] joueurs = new Joueur[nbJoueurs];
            for (int i = 0; i < joueurs.Length; i++)
            {
                Console.WriteLine($"Saisir le pseudo du joueur n°{i + 1} : ");
                string pseudo = Console.ReadLine()?.Trim();///Trim : supprime les espaces blancs
                joueurs[i] = new Joueur(i + 1, pseudo, 0, lettres);
            }
            AfficherJoueurs(joueurs);///Affichage des joueurs 
            return joueurs;
            
        }
        /// <summary>
        /// Méthode pour afficher le tableau de joueurs au début de la partie
        /// </summary>
        /// <param name="joueurs"></param>
        public void AfficherJoueurs(Joueur[] joueurs)
        {
            Console.WriteLine("Les joueurs sont donc : ");
            foreach(var  joueur in joueurs)
            {
                Console.WriteLine(joueur.toString());
            }
            Console.WriteLine("Que la partie commence !");
        }
        /// <summary>
        /// Fonction pour définir la taille du plateau à partir de la saisie utilisateur
        /// </summary>
        /// <returns></returns>
        public int GetTaillePlateau()
        {
            int taillePlateau;
            bool b = false;
            do
            {
                Console.WriteLine("Saisir la taille du plateau voulue (minimum 4, maximum 8) : ");
                string sortie = Console.ReadLine();
                if(!int.TryParse(sortie,out taillePlateau))
                {
                    Console.WriteLine("Saisie incorrecte. Merci de rééssayer");
                    continue;
                }
                if(taillePlateau < 4 || taillePlateau > 8)
                {
                    Console.WriteLine("Erreur. La taille du plateau est de minimum 4 et maximum 8");
                }
                b = true;

            } while (!b);

            return taillePlateau;
        }
        /// <summary>
        /// Fonction qui retourne le temps de jeu par joueur
        /// </summary>
        /// <returns></returns>
        public TimeSpan GetTempsParTour()
        {
            int dureeTourSecondes;
            do
            {
                Console.Write("Saisir la durée des tours en secondes : ");
            } while (!int.TryParse(Console.ReadLine(), out dureeTourSecondes) || dureeTourSecondes <= 0);

            return TimeSpan.FromSeconds(dureeTourSecondes);
        }
        /// <summary>
        /// Fonction qui pemert de gérer la saisie utilisateur du mot vu sur le plateau 
        /// </summary>
        /// <returns></returns>
        public string DemanderMotPlateau()
        {
            Console.WriteLine("Entrez un mot à former avec les lettres du plateau:");
            return Console.ReadLine()?.ToUpper();
        }
        /// <summary>
        /// Méthode du nuage de mots 
        /// </summary>
        /// <param name="joueur"></param>
        public void GenererNuageDeMots(Joueur joueur)
        {
            try
            {
                /// Récupérer les mots trouvés et calculer leur fréquence
                var motsFrequencies = new Dictionary<string, int>();
                foreach (string mot in joueur.MotsTrouvés)
                {
                    if (motsFrequencies.ContainsKey(mot))
                    {
                        motsFrequencies[mot]++;
                    }
                    else
                    {
                        motsFrequencies[mot] = 1;
                    }
                }
                /// Initialisation du WordCloud
                /// Conversion nécessaire
                IList<string> motsTrouvés = joueur.MotsTrouvés.ToList();
                IList<int> frequences = motsFrequencies.Values.ToList();
                var wordCloud = new WordCloud(800, 600); /// Taille de l'image
                System.Drawing.Image image = wordCloud.Draw(motsTrouvés, frequences); /// Explicitement System.Drawing.Image

                string nomFichier = $"nuage_{joueur.pseudo}.png";
                string cheminImage = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, nomFichier);
                string cheminimage = $"nuage_{joueur.pseudo}.png";
                string cheminRepertoire = Path.GetDirectoryName(cheminImage); /// Vérifier si le répertoire existe, sinon le créer
                if (!Directory.Exists(cheminRepertoire))
                {
                    Directory.CreateDirectory(cheminRepertoire);  /// Crée le répertoire si nécessaire
                }
                /// Sauvegarder le nuage de mots dans un fichier
                
                image.Save(cheminimage, ImageFormat.Png);  /// Sauvegarde l'image au chemin spécifié

                if (File.Exists(cheminImage))
                {
                    System.Diagnostics.Process.Start("mspaint.exe", cheminimage);///Ouverture du fichier dans paint
                }
                else
                {
                    Console.WriteLine($"Le fichier {cheminImage} n'a pas été trouvé.");
                }
                

            }
            catch (Exception ex)/// Capture des exceptions pour gérer les erreurs éventuelles
            {
                Console.WriteLine($"Erreur lors de la génération du nuage de mots pour {joueur.pseudo}: {ex.Message}");
            }
        }
        /// <summary>
        /// Méthode qui gère la fin de la partie ; Affiche le gagnant et les instructions de fin
        /// </summary>
        public void FindePartie()
        {
            Joueur gagnant = joueurs.OrderByDescending(j => j.GetScore()).First();
            Console.WriteLine($"Le gagnant est {gagnant.pseudo} avec un score de {gagnant.GetScore()} points.");
            foreach (var joueur in joueurs)
            {
                GenererNuageDeMots(joueur);
            }
            Console.WriteLine("Fin de la partie ! Merci d'avoir joué !");
            Console.WriteLine("Clique sur une touche du clavier pour quitter le jeu et la console ! ");
            return;
        }
       


    }
}
