using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordCloudSharp;

namespace Projet_Algo_1
{
    public  class Jeu
    {
        ///Déclaration des arguments
        private string langue;
        private string cheminFichierMots;
        private string cheminFichierLettres = "../../../../lettres.txt";
        private Joueur[] joueurs;
        private Plateau plateau;
        private TimeSpan dureePartie;
        private TimeSpan dureeTours;

        public Jeu()
        {
            InitialiserParamètresJeu();
        }
        
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

        }
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
        public TimeSpan GetTempsDePartie()
        {
            int dureePartieMinutes;
            do
            {
                Console.Write("Saisir la durée de la partie en minutes : ");
            } while (!int.TryParse(Console.ReadLine(), out dureePartieMinutes) || dureePartieMinutes <= 0);

            return TimeSpan.FromMinutes(dureePartieMinutes);
        }
        public int GetNbJoueurs()
        {
            int nbJoueurs;
            do
            {
                Console.WriteLine("Saisir le nombre de joueurs (minimum 2, maximum 10) : ");
            } while (!int.TryParse(Console.ReadLine(), out nbJoueurs) || nbJoueurs < 2 || nbJoueurs > 10);

            return nbJoueurs;
        }
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
        public void AfficherJoueurs(Joueur[] joueurs)
        {
            Console.WriteLine("Les joueurs sont donc : ");
            foreach(var  joueur in joueurs)
            {
                Console.WriteLine(joueur.toString());
            }
            Console.WriteLine("Que la partie commence !");
        }
        public int GetTaillePlateau()/// Méthode pour obtenir la taille du plateau
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
        public TimeSpan GetTempsParTour()///Méthode pour le temps 
        {
            int dureeTourSecondes;
            do
            {
                Console.Write("Saisir la durée des tours en secondes : ");
            } while (!int.TryParse(Console.ReadLine(), out dureeTourSecondes) || dureeTourSecondes <= 0);

            return TimeSpan.FromSeconds(dureeTourSecondes);
        }
        public string DemanderMotPlateau()///On demande à l'utilisateur le mot à saisir
        {
            Console.WriteLine("Entrez un mot à former avec les lettres du plateau:");
            return Console.ReadLine()?.ToUpper();
        }
        public void GenererNuageDeMots(Joueur joueur)///Nuage de mots
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
                ///Console.WriteLine($"Chemin complet du fichier : {cheminImage}");
                image.Save(cheminimage, ImageFormat.Png);  /// Sauvegarde l'image au chemin spécifié

                if (File.Exists(cheminImage))
                {
                    System.Diagnostics.Process.Start("mspaint.exe", cheminimage);///Ouverture du fichier dans paint
                }
                else
                {
                    Console.WriteLine($"Le fichier {cheminImage} n'a pas été trouvé.");
                }
                /// Utilisation d'ImageFormat.Png pour sauver l'image au format PNG
                ///Console.WriteLine($"Nuage de mots sauvegardé pour {joueur.pseudo} : {cheminImage}");

            }
            catch (Exception ex)/// Capture des exceptions pour gérer les erreurs éventuelles
            {
                Console.WriteLine($"Erreur lors de la génération du nuage de mots pour {joueur.pseudo}: {ex.Message}");
            }
        }
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
        }


    }
}
