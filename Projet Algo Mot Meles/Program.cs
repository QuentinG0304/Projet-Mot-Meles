using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Projet_Algo_Mot_Meles
{
    class Program
    {
        static void timer()
        {
            string[] fichier = File.ReadAllLines("MotsPossiblesFR.txt");
            Dictionnaire b = new Dictionnaire("Français", fichier);



            //Console.WriteLine(b.ToString());
            //Console.WriteLine(a.RechDichoRecursif("aeu"));
            //Console.WriteLine(fichier[7][25950]);

            //StreamReader CSV = new StreamReader(""CasComplexe.csv");
            //string a= CSV.ReadLine();
            //string b =CSV.ReadLine();
            //string c = CSV.ReadToEnd();
            Plateau a = new Plateau(5,b);
            //a.ToRead(CSV)
            Console.WriteLine(a.ToString());
            
        }
        static void Main(string[] args)
        {
            //Task taskA = new Task(timer);
            //taskA.Start();

            #region 1) Détermination du dictionnaire

            Console.Write("MOTS MELEES\n\nDébut du Jeu: Voulez-vous jouez en Français(tapez fr) ou en Anglais(tapez en) ? ");
            string langue = Console.ReadLine().ToLower();
            Dictionnaire dictio = null;
            if(langue!="en" && langue!="fr")
            {
                do
                {
                    Console.Write("\nEntrée incorrect, voulez-vous jouez en Français(tapez fr) ou en Anglais(tapez en) ? ");
                    langue = Console.ReadLine().ToLower();
                } while (langue != "en" && langue != "fr");
            }
            if (langue == "fr")
            {
                Console.WriteLine("\nVous avez choisi de jouer en Français");
                string[] fichier = File.ReadAllLines("MotsPossiblesFR.txt");
                dictio = new Dictionnaire("Français", fichier);
            }
            else
            {
                Console.WriteLine("\nVous avez choisi de jouer en Anglais");
                string[] fichier = File.ReadAllLines("MotsPossiblesEN.txt");
                dictio = new Dictionnaire("Anglais", fichier);
            }

            #endregion

            #region 2) Initialisation des Joueurs

            string ee;
            Console.Write("\n\nInitialisation des joueurs\n\nJoueur 1, quel est votre nom ? ");
            Joueur J1 = new Joueur(Console.ReadLine());
            Console.Write("\n\nJoueur 2, quel est votre nom ? ");
            Joueur J2 = new Joueur(Console.ReadLine());
            #endregion

            #region 3) Début du Jeu
            
            Joueur[] joueurs = new Joueur[]{ J1, J2 };
            int numjoueur;
            Jeu jeu = new Jeu(dictio, joueurs, 2);
            Plateau plat = new Plateau();
            for(int i=0; i<10; i++)
            {
                if(i%2==0)
                {
                    plat = new Plateau((i / 2) + 1, dictio);
                    numjoueur = 0;
                }
                else
                {
                    plat = new Plateau((i / 2) + 1, dictio);
                    numjoueur = 1;
                }
                
                Console.WriteLine("Manche numéro "+ ((i/2)+1) +" pour " +joueurs[numjoueur].Nom+" :");

                Console.Write("\nLa manche va commencer, dès que vous aller a");
                Console.WriteLine(plat.PourJouer());
            }

            #endregion



            //string a= CSV.ReadLine();
            //string b =CSV.ReadLine();
            //string c = CSV.ReadToEnd();
            //Plateau a = new Plateau(5,dictio);


            //a.ToRead(CSV)

            //Console.WriteLine(a.ToString());

            Console.ReadKey();
        }
    }
}
