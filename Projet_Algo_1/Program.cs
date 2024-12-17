using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Drawing;
using WordCloudSharp;
using System.Drawing.Imaging;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;
using System.Diagnostics;


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

            #region Annonce jeu
            Console.WriteLine("Bienvenue dans le jeu du Boggle ! Ce jeu a été conçu par Aubin Lin et Armand Mareine. Bonne partie !");
            #endregion

            #region Choix de la langue du dictionnaire

            /// Demander le choix de la langue jusqu'à ce que l'utilisateur entre une langue valide
            langue = GetLangueDictionnaire();

            /// Définir le chemin du fichier de dictionnaire en fonction de la langue choisie
            cheminFichierMots = langue == "Français" ? "../../../../MotsPossiblesFR.txt" : "../../../../MotsPossiblesEN.txt";

            /// Chargement du dictionnaire
            Dictionnaire dictionnaire = new Dictionnaire(cheminFichierMots, langue);
            List<string> mots = dictionnaire.ChargerMots(cheminFichierMots);
            #endregion

            #region Joueurs
            /// Demander le nombre de joueurs
            int nbJoueurs = GetNombreJoueurs();

            ///Lecture du fichier lettres
            List<Lettre> lettres = Lettre.LectureFichier(cheminFichierLettres);

            /// Créer les joueurs
            Joueur[] joueurs = CreateJoueurs(nbJoueurs, lettres);
            #endregion

            #region Plateau
            // Initialiser le plateau
            int taillePlateau = GetTaillePlateau();

            Plateau plateau = new Plateau(taillePlateau);
            plateau.InitialiserDés(lettres);
            #endregion

            #region Déroulé du jeu
            /// Demander la durée de la partie
            TimeSpan dureePartie = GetTempsDePartie();

            /// Demander la durée des tours
            TimeSpan dureeTours = GetTempsParTour();

            ///Affichage du plateau
            /// plateau.LancerTousLesDés();

            DateTime debutPartie = DateTime.Now;
            while (DateTime.Now - debutPartie < dureePartie)
            {
                foreach (var joueur in joueurs)
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

                if (DateTime.Now - debutPartie > dureePartie)
                {
                    Console.WriteLine("Le temps de la partie est écoulé !");
                    break;
                }
            }

            /// Annonce du gagnant
            Joueur gagnant = joueurs.OrderByDescending(j => j.GetScore()).First();
            Console.WriteLine($"Le gagnant est {gagnant.pseudo} avec un score de {gagnant.GetScore()} points.");
            foreach (var joueur in joueurs)
            {
                GenererNuageDeMots(joueur);
            }
            Console.WriteLine("Fin de la partie ! Merci d'avoir joué !");
            #endregion

            #region Méthodes Utilitaires
            /// Méthode pour obtenir la langue du dictionnaire
            static string GetLangueDictionnaire()
            {
                string langue;
                do
                {
                    Console.WriteLine("Choisissez la langue du dictionnaire (Français ou Anglais) : ");
                    langue = Console.ReadLine()?.Trim();
                } while (langue != "Français" && langue != "Anglais");

                return langue;
            }

            /// Méthode pour obtenir le nombre de joueurs
            static int GetNombreJoueurs()
            {
                int nbJoueurs;
                do
                {
                    Console.WriteLine("Saisir le nombre de joueurs (minimum 2, maximum 10) : ");
                } while (!int.TryParse(Console.ReadLine(), out nbJoueurs) || nbJoueurs < 2 || nbJoueurs > 10);

                return nbJoueurs;
            }

            /// Méthode pour créer les joueurs
            static Joueur[] CreateJoueurs(int nbJoueurs, List<Lettre> lettres)
            {
                Joueur[] joueurs = new Joueur[nbJoueurs];
                for (int i = 0; i < joueurs.Length; i++)
                {
                    Console.WriteLine($"Saisir le pseudo du joueur n°{i + 1} : ");
                    string pseudo = Console.ReadLine()?.Trim();
                    joueurs[i] = new Joueur(i + 1, pseudo, 0, lettres);
                }
                return joueurs;
            }

            /// Méthode pour obtenir la taille du plateau
            static int GetTaillePlateau()
            {
                int taillePlateau;
                do
                {
                    Console.WriteLine("Saisir la taille du plateau voulue (minimum 4, maximum 8) : ");
                } while (!int.TryParse(Console.ReadLine(), out taillePlateau) || taillePlateau < 4 || taillePlateau > 8);

                return taillePlateau;
            }

            /// Méthode pour obtenir le temps de la partie
            static TimeSpan GetTempsDePartie()
            {
                int dureePartieMinutes;
                do
                {
                    Console.Write("Saisir la durée de la partie en minutes : ");
                } while (!int.TryParse(Console.ReadLine(), out dureePartieMinutes) || dureePartieMinutes <= 0);

                return TimeSpan.FromMinutes(dureePartieMinutes);
            }

            /// Méthode pour obtenir la durée des tours
            static TimeSpan GetTempsParTour()
            {
                int dureeTourSecondes;
                do
                {
                    Console.Write("Saisir la durée des tours en secondes : ");
                } while (!int.TryParse(Console.ReadLine(), out dureeTourSecondes) || dureeTourSecondes <= 0);

                return TimeSpan.FromSeconds(dureeTourSecondes);
            }

            /// Méthode pour demander un mot au joueur
            static string DemanderMotPlateau()
            {
                Console.WriteLine("Entrez un mot à former avec les lettres du plateau:");
                return Console.ReadLine()?.ToUpper();
            }
            #endregion
            #region nuage de mot
            static void GenererNuageDeMots(Joueur joueur)
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
                                                                                          /// Vérifier si le répertoire existe, sinon le créer
                    string nomFichier = $"nuage_{joueur.pseudo}.png";
                    string cheminImage = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, nomFichier);
                    string cheminimage = $"nuage_{joueur.pseudo}.png";
                    string cheminRepertoire = Path.GetDirectoryName(cheminImage);
                    if (!Directory.Exists(cheminRepertoire))
                    {
                        Directory.CreateDirectory(cheminRepertoire);  /// Crée le répertoire si nécessaire
                    }
                    
                    /// Sauvegarder le nuage de mots dans un fichier
                    Console.WriteLine($"Chemin complet du fichier : {cheminImage}");
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

                    Console.WriteLine($"Nuage de mots sauvegardé pour {joueur.pseudo} : {cheminImage}");
                    
                }
                catch (Exception ex)
                {
                    /// Capture des exceptions pour gérer les erreurs éventuelles
                    Console.WriteLine($"Erreur lors de la génération du nuage de mots pour {joueur.pseudo}: {ex.Message}");
                }
            }
            
            #endregion
        }
    }
}





    

   
   


