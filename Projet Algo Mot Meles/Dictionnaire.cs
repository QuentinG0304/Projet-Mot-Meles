using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO; //Pour lire et écrire dans des fichiers

namespace Projet_Algo_Mot_Meles
{
    class Dictionnaire
    {
        string langue;
        public string[] fichier;

        public Dictionnaire(string langue, string[] fichier)
        {
            this.langue = langue;
            this.fichier= fichier;
        }

        public override string ToString()
        {
            string a = "Langue: " + langue+"\nNombre de mots par nombre de lettres: \n";
            int cpt1 = 2; //Compteur pour savoir le nombre de lettre, commence à 2 car les mots commencent à 2
            for(int i=0; i<fichier.Length;i++)
            {
                if(fichier[i]==Convert.ToString(cpt1))
                {
                    a += Convert.ToString(cpt1) + " lettres: " + Convert.ToString(NbMot(fichier[i+1]) + " mots\n");
                    cpt1++;
                }
            }
            return a;
        }

        static public int NbMot (string fichier)
        {
            int cpt2=1; //Compteur pour savoir le nombre de mots ayant cpt1 lettres
            
            for (int j = 0; j < fichier.Length; j++)
            {
                if (fichier[j] == ' ')
                {
                    cpt2++;
                }
            }
            return cpt2;
        }

        public bool RechDichoRecursif(string mot) //Test si un mot est dans le dictionaire
        {
            mot = mot.ToUpper(); //En majuscule car en majuscule dans le fichier.txt
            bool a = false;
            int cpt; //Compteur pour savoir si le mot est correct
            for(int i=0; i<fichier.Length && !a;i++)
            {
                if (fichier[i] == Convert.ToString(mot.Length))
                {
                    for (int j = 0; j < fichier[i+1].Length && !a; j++)
                    {
                        if (fichier[i + 1][j] == ' ')
                        {
                            if (fichier[i + 1][j+1] == mot[0])
                            {
                                cpt = 0;
                                for (int k = 0; k < mot.Length; k++)
                                {
                                    if (mot[k] == fichier[i + 1][j + k + 1])
                                    {
                                        cpt++;
                                    }
                                }
                                if ((fichier[i+1].Length - mot.Length) != j+1) //On vérifie que ce n'est pas le dernier mot du tableau
                                {
                                    if (cpt == mot.Length && (fichier[i + 1][j + 1 + mot.Length] == ' '))
                                    {
                                        a = true;
                                    }
                                }
                                else
                                {
                                    if(cpt==mot.Length)
                                    {
                                        a = true;
                                    }
                                }
                            }
                        }
                        if (j == 0) //On verifie la première valeur car pas d'espace avant cette dernière donc
                                    //elle ne rentrera pas dans l'autre boucle
                        {
                            if (fichier[i + 1][j] == mot[0])
                            {
                                cpt = 0;
                                for (int k = 0; k < mot.Length; k++)
                                {
                                    if (mot[k] == fichier[i + 1][j + k])
                                    {
                                        cpt++;
                                    }
                                }
                                if (cpt == mot.Length && (fichier[i + 1][j + mot.Length] == ' '))
                                {
                                    a = true;
                                }
                            }
                        }
                    }
                }
            }
            return a;
        }
    }
}
