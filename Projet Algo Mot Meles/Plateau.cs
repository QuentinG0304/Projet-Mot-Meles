using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Projet_Algo_Mot_Meles
{
    class Plateau
    {
        public char[,] plat;
        int niveau;
        int nblignes;
        int nbcolonnes;
        public int nbmots;
        public string[] mots;
        Dictionnaire dico;

        public Plateau()
        {
            plat = null;
            niveau = 0;
            nblignes = 0;
            nbcolonnes = 0;
            nbmots = 0;
            mots = null;
            dico = null;
        }
        public void ToRead(StreamReader CSV)
        {
            string[] fichier = File.ReadAllLines("MotsPossiblesFR.txt");
            dico = new Dictionnaire("Français", fichier);
            string a = CSV.ReadLine();
            int taille = -1;
            int debut = 0;
            int iteration=0; //Pour savoir quel est la chaine que je prend
            for (int l = 0; l < a.Length && iteration<4; l++)
            {
                taille++;
                if (a[l] == ';' || l == a.Length - 1 )
                {
                    switch (iteration)
                    {
                        case 0:
                            niveau = Convert.ToInt32(a.Substring(debut, taille));
                            iteration++;
                            break;
                        case 1:
                            nblignes = Convert.ToInt32(a.Substring(debut, taille));
                            iteration++;
                            break;
                        case 2:
                            nbcolonnes = Convert.ToInt32(a.Substring(debut, taille));
                            iteration++;
                            break;
                        case 3:
                            nbmots = Convert.ToInt32(a.Substring(debut, taille));
                            iteration++;
                            break;
                    }
                    debut = l + 1;
                    taille = -1;
                }
            }
            mots = new string[nbmots];
            string b = CSV.ReadLine();
            int cpt = 0; //Savoir ou mettre le mot
            debut = 0; //Début du mot
            taille=-1;
            for(int i=0; i<b.Length;i++)
            {
                taille++;
                if(b[i]==';' || i==b.Length-1)
                {
                    if (i != b.Length - 1)// Pour pallier le pb du dernier mot
                    {
                        mots[cpt] = b.Substring(debut, taille);
                    }
                    else
                    {
                        mots[cpt] = b.Substring(debut, taille+1);
                    }
                    debut = i + 1;
                    cpt++;
                    taille = -1;
                }
            }

            string c = CSV.ReadToEnd();
            plat = new char[nblignes, nbcolonnes];
            int cpt1 = 0;
            for(int i=0; i<plat.GetLength(0);i++)
            {
                for(int j=0; j<plat.GetLength(1);j++)
                {
                    if(c[cpt1]=='A' || c[cpt1]=='B' || c[cpt1] == 'C' || c[cpt1]=='D'|| c[cpt1] == 'E' || c[cpt1] == 'F' || c[cpt1] == 'G' || c[cpt1] == 'H' ||c[cpt1] == 'I' || c[cpt1] == 'J' || c[cpt1] == 'K' || c[cpt1] == 'L' || c[cpt1] == 'M' || c[cpt1] == 'N' || c[cpt1] == 'O' || c[cpt1] == 'P' ||c[cpt1] == 'Q' || c[cpt1] == 'R' || c[cpt1] == 'S' || c[cpt1] == 'T' || c[cpt1] == 'U' || c[cpt1] == 'V' || c[cpt1] == 'W' || c[cpt1] == 'X' || c[cpt1] == 'Y' || c[cpt1] == 'Z')
                    {
                        plat[i, j] = c[cpt1];
                        cpt1++;
                    }
                    else
                    {
                        cpt1++;
                        j--;
                    }
                }
            }
        }
        public string toString()
        {
            string a = "Niveau: "+niveau+"\nNombre de mots: "+nbmots+"\nNombre de lignes: "+nblignes+"\nNombre de colonnes: "+nbcolonnes;
            a += "\n\nLes mots à trouver:\n";
            for(int i=0; i<mots.Length;i++)
            {
                a += mots[i] + "\n";
            }
            a += "\nLe tableau:\n";
            for (int k = 0; k < plat.GetLength(0); k++)
            {
                for (int j = 0; j < plat.GetLength(1); j++)
                {
                    a += plat[k, j] + " ";
                }
                a += "\n";
            }
            return a;
        }
        public string PourJouer()
        {
            string a = "";
            a += "\n\nLes mots à trouver sont :\n";
            for (int i = 0; i < mots.Length; i++)
            {
                a += mots[i] + "  ";
            }
            a += "\n";
            for (int k = 0; k < plat.GetLength(0); k++)
            {
                for (int j = 0; j < plat.GetLength(1); j++)
                {
                    a += plat[k, j] + " ";
                }
                a += "\n";
            }
            return a;
        }
        public string RandomMot(int maxlettre, Dictionnaire dico)
        {
            string a="";
            Random r = new Random();
            int b = r.Next(2, maxlettre+1); //Choix du nb de lettre du mot
            string[] c = Decomposition(dico.fichier[2 * (b - 2) + 1]);
            a += c[r.Next(0,c.Length)];
            return a;
        }
        public string[] Decomposition(string fichier)
        {
            string[] a = new string[Dictionnaire.NbMot(fichier)];
            int cpt = 0; //Savoir ou mettre le mot
            int debut = 0; //Début du mot
            int taille = -1;
            for (int i = 0; i < fichier.Length; i++)
            {
                taille++;
                if (fichier[i] == ' ' || i == fichier.Length - 1)
                {
                    if (i != fichier.Length - 1)// Pour pallier le pb du dernier mot
                    {
                        a[cpt] = fichier.Substring(debut, taille);
                    }
                    else
                    {
                        a[cpt] = fichier.Substring(debut, taille + 1);
                    }
                    debut = i + 1;
                    cpt++;
                    taille = -1;
                }
            }
            return a;
        }
        public int SommeElementMatrice(int[,] a)
        {
            int b=0;
            for (int i = 0; i < a.GetLength(0); i++)
            {
                for (int j = 0; j < a.GetLength(1); j++)
                {
                    b+=a[i, j];
                }
            }
            return b;
        }
        public bool MotDiffDico(string mot, string[] mots, int cpt = 0, bool test= false)
        {
            if (cpt == mots.Length || test)
            {
                return test;
            }
            else
            {
                if (mot == mots[cpt])
                {
                    test = true;
                }
                return MotDiffDico(mot, mots, cpt + 1, test);
            }
        }
        public Plateau(int niveau, Dictionnaire dico)
        {
            char[,] plat = null;
            int nblignes;
            int nbcolonnes;
            int nbmots;
            int maxlettres;
            int maxlettresmobile; 
            int cptmot;//Compteur pour savoir où on en est dans les mots
            switch (niveau)
            {
                #region niveau 1
                case 1:
                    this.niveau = niveau;
                    nblignes = 7;
                    this.nblignes = nblignes;
                    nbcolonnes = 6;
                    this.nbcolonnes = nbcolonnes;
                    nbmots = 8;
                    this.nbmots = nbmots;
                    maxlettres = 6;
                    this.mots = new string[nbmots];
                    cptmot = 0;//Compteur pour savoir où on en est dans les mots

                    maxlettres = Math.Min(nblignes, nbcolonnes);
                    if(maxlettres>15)
                    {
                        maxlettres = 15;
                    }

                    plat = new char[nblignes, nbcolonnes]; //initialisation de plat
                    char[,] plattest = new char[nblignes, nbcolonnes];//Plateau test pour savoir s'il a été changé après être passé par les fonctions de remplissage
                    bool testplateau = true; //Pour tester si le plateau a été changé
                    for (int i = 0; i < nblignes; i++)
                    {
                        for (int j = 0; j < nbcolonnes; j++)
                        {
                           plat[i, j] = ' ';
                           plattest[i, j] = ' ';
                        }
                    }

                    Random r = new Random(); //Pour connaitre le sens du mot;
                    int random;
                    string mot = "";
                    for (int i = 0; i < nbmots; i++)
                    {
                        do
                        {
                            mot = RandomMot(maxlettres, dico);
                        } while (MotDiffDico(mot, this.mots));
                        //Console.WriteLine(mot);
                        random = r.Next(1, 3);
                        switch (random)
                        {
                            case 1:
                                plat = Sud(mot, plat);
                                break;
                            case 2:
                                plat = Est(mot, plat);
                                break;
                        }
                        for (int k = 0; k < plat.GetLength(0) && testplateau; k++) //Savoir si le plateau a été changé
                        {
                            for (int j = 0; j < plat.GetLength(1) && testplateau; j++)
                            {
                                if (plat[k, j] != plattest[k, j])
                                {
                                    testplateau = false;
                                }
                            }
                        }
                        if (testplateau)
                        {
                            i--;
                        }//on n'incrémente pas i si le plateau n'a pas bougé
                        else
                        {
                            for (int l = 0; l < nblignes; l++) //Obligatoire de faire ça sinon, si je fais plattest=plat, dés que plat change, plattest aussi
                            {
                                for (int m = 0; m < nbcolonnes; m++)
                                {
                                    plattest[l, m] = plat[l, m];
                                }
                            }
                            testplateau = true;
                            this.mots[cptmot] = mot;
                            cptmot++;
                        } //plattest=plat
                        //Console.WriteLine(Ecrire(plat));
                    }
                    break;
                #endregion

                #region niveau 2
                case 2:
                    this.niveau = niveau;
                    nblignes = 9;
                    this.nblignes = nblignes;
                    nbcolonnes = 8;
                    this.nbcolonnes = nbcolonnes;
                    nbmots = 10;
                    this.nbmots = nbmots;
                    maxlettres = 8;
                    this.mots = new string[nbmots];
                    cptmot = 0;//Compteur pour savoir où on en est dans les mots

                    plat = new char[nblignes, nbcolonnes]; //initialisation de plat
                    plattest = new char[nblignes, nbcolonnes];//Plateau test pour savoir s'il a été changé après être passé par les fonctions de remplissage
                    testplateau = true; //Pour tester si le plateau a été changé
                    for (int i = 0; i < nblignes; i++)
                    {
                        for (int j = 0; j < nbcolonnes; j++)
                        {
                            plat[i, j] = ' ';
                            plattest[i, j] = ' ';
                        }
                    }

                    r = new Random();
                    for (int i = 0; i < nbmots; i++)
                    {
                        do
                        {
                            mot = RandomMot(maxlettres, dico);
                        } while (MotDiffDico(mot, this.mots));
                        //Console.WriteLine(mot);
                        random = r.Next(1, 5);
                        switch (random)
                        {
                            case 1:
                                plat = Sud(mot, plat);
                                break;
                            case 2:
                                plat = Est(mot, plat);
                                break;
                            case 3:
                                plat = Nord(mot, plat);
                                break;
                            case 4:
                                plat = Ouest(mot, plat);
                                break;
                        }
                        for (int k = 0; k < plat.GetLength(0) && testplateau; k++) //Savoir si le plateau a été changé
                        {
                            for (int j = 0; j < plat.GetLength(1) && testplateau; j++)
                            {
                                if (plat[k, j] != plattest[k, j])
                                {
                                    testplateau = false;
                                }
                            }
                        }
                        if (testplateau)
                        {
                            i--;
                        }//on n'incrémente pas i si le plateau n'a pas bougé
                        else
                        {
                            for (int l = 0; l < nblignes; l++) //Obligatoire de faire ça sinon, si je fais plattest=plat, dés que plat change, plattest aussi
                            {
                                for (int m = 0; m < nbcolonnes; m++)
                                {
                                    plattest[l, m] = plat[l, m];
                                }
                            }
                            testplateau = true;
                            this.mots[cptmot] = mot;
                            cptmot++;
                        } //plattest=plat
                        //Console.WriteLine(Ecrire(plat));
                    }
                    break;
                #endregion

                #region niveau 3
                case 3:
                    this.niveau = niveau;
                    nblignes = 10;
                    this.nblignes = nblignes;
                    nbcolonnes = 10;
                    this.nbcolonnes = nbcolonnes;
                    nbmots = 15;
                    this.nbmots = nbmots;
                    maxlettres = 10;
                    this.mots = new string[nbmots];
                    cptmot = 0;//Compteur pour savoir où on en est dans les mots

                    plat = new char[nblignes, nbcolonnes]; //initialisation de plat
                    plattest = new char[nblignes, nbcolonnes];//Plateau test pour savoir s'il a été changé après être passé par les fonctions de remplissage
                    testplateau = true; //Pour tester si le plateau a été changé
                    for (int i = 0; i < nblignes; i++)
                    {
                        for (int j = 0; j < nbcolonnes; j++)
                        {
                            plat[i, j] = ' ';
                            plattest[i, j] = ' ';
                        }
                    }

                    r = new Random();
                    for (int i = 0; i < nbmots; i++)
                    {
                        do
                        {
                            mot = RandomMot(maxlettres, dico);
                        } while (MotDiffDico(mot, this.mots));
                        random = r.Next(1, 7);
                        switch (random)
                        {
                            case 1:
                                plat = Sud(mot, plat);
                                break;
                            case 2:
                                plat = Est(mot, plat);
                                break;
                            case 3:
                                plat = Nord(mot, plat);
                                break;
                            case 4:
                                plat = Ouest(mot, plat);
                                break;
                            case 5:
                                plat = NE(mot, plat);
                                break;
                            case 6:
                                plat = SO(mot, plat);
                                break;
                        }
                        for (int k = 0; k < plat.GetLength(0) && testplateau; k++) //Savoir si le plateau a été changé
                        {
                            for (int j = 0; j < plat.GetLength(1) && testplateau; j++)
                            {
                                if (plat[k, j] != plattest[k, j])
                                {
                                    testplateau = false;
                                }
                            }
                        }
                        if (testplateau)
                        {
                            i--;
                        }//on n'incrémente pas i si le plateau n'a pas bougé
                        else
                        {
                            for (int l = 0; l < nblignes; l++) //Obligatoire de faire ça sinon, si je fais plattest=plat, dés que plat change, plattest aussi
                            {
                                for (int m = 0; m < nbcolonnes; m++)
                                {
                                    plattest[l, m] = plat[l, m];
                                }
                            }
                            testplateau = true;
                            this.mots[cptmot] = mot;
                            cptmot++;
                        } //plattest=plat
                    }
                    break;
                #endregion

                #region niveau 4
                case 4:
                    this.niveau = niveau;
                    nblignes = 11;
                    this.nblignes = nblignes;
                    nbcolonnes = 11;
                    this.nbcolonnes = nbcolonnes;
                    nbmots = 20;
                    this.nbmots = nbmots;
                    maxlettres = 11;
                    this.mots = new string[nbmots];
                    cptmot = 0;//Compteur pour savoir où on en est dans les mots

                    plat = new char[nblignes, nbcolonnes]; //initialisation de plat
                    plattest = new char[nblignes, nbcolonnes];//Plateau test pour savoir s'il a été changé après être passé par les fonctions de remplissage
                    testplateau = true; //Pour tester si le plateau a été changé
                    for (int i = 0; i < nblignes; i++)
                    {
                        for (int j = 0; j < nbcolonnes; j++)
                        {
                            plat[i, j] = ' ';
                            plattest[i, j] = ' ';
                        }
                    }
                    maxlettresmobile = maxlettres;
                    r = new Random();
                    for (int i = 0; i < nbmots; i++)
                    {
                        do
                        {
                            mot = RandomMot(maxlettresmobile, dico);
                        } while (MotDiffDico(mot, this.mots));
                        //Console.WriteLine(mot);
                        random = r.Next(1, 7);
                        switch (random)
                        {
                            case 1:
                                plat = Sud(mot, plat);
                                break;
                            case 2:
                                plat = Est(mot, plat);
                                break;
                            case 3:
                                plat = Nord(mot, plat);
                                break;
                            case 4:
                                plat = Ouest(mot, plat);
                                break;
                            case 5:
                                plat = NO(mot, plat);
                                break;
                            case 6:
                                plat = SE(mot, plat);
                                break;
                        }
                        for (int k = 0; k < plat.GetLength(0) && testplateau; k++) //Savoir si le plateau a été changé
                        {
                            for (int j = 0; j < plat.GetLength(1) && testplateau; j++)
                            {
                                if (plat[k, j] != plattest[k, j])
                                {
                                    testplateau = false;
                                }
                            }
                        }
                        if (testplateau)
                        {
                            i--;
                            maxlettresmobile = mot.Length;
                        }//on n'incrémente pas i si le plateau n'a pas bougé
                        else
                        {
                            for (int l = 0; l < nblignes; l++) //Obligatoire de faire ça sinon, si je fais plattest=plat, dés que plat change, plattest aussi
                            {
                                for (int m = 0; m < nbcolonnes; m++)
                                {
                                    plattest[l, m] = plat[l, m];
                                }
                            }
                            testplateau = true;
                            maxlettresmobile = maxlettres;
                            this.mots[cptmot] = mot;
                            cptmot++;
                        } //plattest=plat
                        //Console.WriteLine(Ecrire(plat));
                    }
                    break;
                #endregion

                #region niveau 5
                case 5:
                    this.niveau = niveau;
                    nblignes = 13;
                    this.nblignes = nblignes;
                    nbcolonnes = 13;
                    this.nbcolonnes = nbcolonnes;
                    nbmots = 28;
                    this.nbmots = nbmots;
                    maxlettres = 13;
                    this.mots = new string[nbmots];
                    cptmot = 0;//Compteur pour savoir où on en est dans les mots

                    plat = new char[nblignes, nbcolonnes]; //initialisation de plat
                    plattest = new char[nblignes, nbcolonnes];//Plateau test pour savoir s'il a été changé après être passé par les fonctions de remplissage
                    testplateau = true; //Pour tester si le plateau a été changé
                    for (int i = 0; i < nblignes; i++)
                    {
                        for (int j = 0; j < nbcolonnes; j++)
                        {
                            plat[i, j] = ' ';
                            plattest[i, j] = ' ';
                        }
                    }
                    maxlettresmobile = maxlettres;
                    r = new Random();
                    for (int i = 0; i < nbmots; i++)
                    {
                        do
                        {
                            mot = RandomMot(maxlettresmobile, dico);
                        } while (MotDiffDico(mot, this.mots));
                        random = r.Next(1, 9);
                        switch (random)
                        {
                            case 1:
                                plat = Sud(mot, plat);
                                break;
                            case 2:
                                plat = Est(mot, plat);
                                break;
                            case 3:
                                plat = Nord(mot, plat);
                                break;
                            case 4:
                                plat = Ouest(mot, plat);
                                break;
                            case 5:
                                plat = NE(mot, plat);
                                break;
                            case 6:
                                plat = SO(mot, plat);
                                break;
                            case 7:
                                plat = NO(mot, plat);
                                break;
                            case 8:
                                plat = SE(mot, plat);
                                break;
                        }
                        for (int k = 0; k < plat.GetLength(0) && testplateau; k++) //Savoir si le plateau a été changé
                        {
                            for (int j = 0; j < plat.GetLength(1) && testplateau; j++)
                            {
                                if (plat[k, j] != plattest[k, j])
                                {
                                    testplateau = false;
                                }
                            }
                        }
                        if (testplateau)
                        {
                            i--;
                            maxlettresmobile = mot.Length;
                        }//on n'incrémente pas i si le plateau n'a pas bougé
                        else
                        {
                            for (int l = 0; l < nblignes; l++) //Obligatoire de faire ça sinon, si je fais plattest=plat, dés que plat change, plattest aussi
                            {
                                for (int m = 0; m < nbcolonnes; m++)
                                {
                                    plattest[l, m] = plat[l, m];
                                }
                            }
                            testplateau = true;
                            maxlettresmobile = maxlettres;
                            this.mots[cptmot] = mot;
                            cptmot++;
                        } //plattest=plat
                        //Console.WriteLine(Ecrire(plat));
                    }
                    break;
                    #endregion

            }

            this.plat = Completerplateau(plat);
        } //Création des plateaux
        public char[,] Completerplateau(char[,] plat)
        {
            Random r = new Random();
            int random;
            for(int i=0; i<plat.GetLength(0); i++)
            {
                for(int j=0; j< plat.GetLength(1); j++)
                {
                    if(plat[i,j]==' ')
                    {
                        random = r.Next(65, 91);
                        plat[i, j] = Convert.ToChar(random);
                    }
                }
            }
            return plat;
        }
        public char[,] Sud(string mot, char[,] plat)
        {
            int[] pt = new int[2] { -1, -1 }; // Point d'ancrage
            int[,] lignecol = new int[plat.GetLength(0) - mot.Length+1, plat.GetLength(1)];
            for (int i = 0; i < lignecol.GetLength(0); i++)
            {
                for (int j = 0; j < lignecol.GetLength(1); j++)
                {
                    lignecol[i, j] = -1;
                }
            }
            int ligne;
            int colonne;
            bool test;//Savoir si le mot a pu s'implémeter
            do
            {
                do
                {
                    Random r = new Random();
                    ligne = r.Next(0, lignecol.GetLength(0));
                    colonne = r.Next(0, lignecol.GetLength(1));
                    if (lignecol[ligne, colonne] == -1 && (plat[ligne, colonne] == mot[0] || plat[ligne, colonne] == ' '))
                    {
                        pt[0] = ligne;
                        pt[1] = colonne;
                    }
                    else
                    {
                        lignecol[ligne, colonne] = 0;
                    }
                } while (pt[0] == -1 && SommeElementMatrice(lignecol) != 0);
                test = true;
                int compt = 0;//Compteur pour savoir si il va placer le même mot au même endroit
                if (pt[0] != -1)//Eviter les erreurs si on est sorti de la dernière boucle car la somme des élément de la patrice est egale à 0
                {
                    for (int k = 0; k < mot.Length && test; k++) //On test si le mot peut être ajouté dans le plateau
                    {
                        if (!(plat[pt[0] + k, pt[1]] == ' ' || plat[pt[0] + k, pt[1]] == mot[k]))
                        {
                            test = false;
                            lignecol[pt[0], pt[1]] = 0;
                        }
                        if (plat[pt[0] + k, pt[1]] == mot[k])
                        {
                            compt++;
                        }
                    }
                    if (compt == mot.Length)
                    {
                        test = false;
                    }
                }
            } while (!test && SommeElementMatrice(lignecol) != 0);
            if(SommeElementMatrice(lignecol)!=0)
            {
                for (int k = 0; k < mot.Length; k++) //On ajoute le mot dans le plateau
                {
                    plat[pt[0] + k, pt[1]] = mot[k];
                }
            }
            return plat;
        }
        public char[,] Est(string mot, char[,] plat)
        {
            int[] pt = new int[2] { -1, -1 }; // Point d'ancrage
            int[,] lignecol = new int[plat.GetLength(0), plat.GetLength(1) - mot.Length + 1];
            for (int i = 0; i < lignecol.GetLength(0); i++)
            {
                for (int j = 0; j < lignecol.GetLength(1); j++)
                {
                    lignecol[i, j] = -1;
                }
            }
            int ligne;
            int colonne;
            bool test = false;//Savoir si le mot a pu s'implémeter
            do
            {
                do
                {
                    Random r = new Random();
                    ligne = r.Next(0, lignecol.GetLength(0));
                    colonne = r.Next(0, lignecol.GetLength(1));
                    if (lignecol[ligne, colonne] == -1 && (plat[ligne, colonne] == mot[0] || plat[ligne, colonne] == ' '))
                    {
                        pt[0] = ligne;
                        pt[1] = colonne;
                    }
                    else
                    {
                        lignecol[ligne, colonne] = 0;
                    }
                } while (pt[0] == -1 && SommeElementMatrice(lignecol) != 0);
                test = true;
                int compt = 0;//Compteur pour savoir si il va placer le même mot au même endroit
                if (pt[0] != -1)//Eviter les erreurs si on est sorti de la dernière boucle car la somme des élément de la patrice est egale à 0
                {
                    for (int k = 0; k < mot.Length; k++) //On test si le mot peut être ajouté dans le plateau
                    {
                        if (!(plat[pt[0], pt[1]+k] == ' ' || plat[pt[0], pt[1]+k] == mot[k]))
                        {
                            test = false;
                            lignecol[pt[0], pt[1]] = 0;
                        }
                        if (plat[pt[0], pt[1]+k] == mot[k])
                        {
                            compt++;
                        }
                    }
                    if (compt == mot.Length)
                    {
                        test = false;
                    }
                }
            } while (!test && SommeElementMatrice(lignecol) != 0);
            if (SommeElementMatrice(lignecol) != 0)
            {
                for (int k = 0; k < mot.Length; k++) //On test si le mot peut être ajouté dans le plateau
                {
                    plat[pt[0], pt[1]+k] = mot[k];
                }
            }
            return plat;
        }
        public char[,] Nord(string mot, char[,] plat)
        {
            int[] pt = new int[2] { -1, -1 }; // Point d'ancrage
            int[,] lignecol = new int[plat.GetLength(0) - mot.Length + 1, plat.GetLength(1)];
             for (int i = 0; i < lignecol.GetLength(0); i++)
            {
                for (int j = 0; j < lignecol.GetLength(1); j++)
                {
                    lignecol[i, j] = -1;
                }
            }
            int ligne;
            int colonne;
            bool test = false;//Savoir si le mot a pu s'implémeter
            do
            {
                do
                {
                    Random r = new Random();
                    ligne = r.Next(0, lignecol.GetLength(0));
                    colonne = r.Next(0, lignecol.GetLength(1));
                    if (lignecol[ligne, colonne] == -1 && (plat[plat.GetLength(0) - ligne - 1, colonne] == mot[0] || plat[plat.GetLength(0) - ligne - 1, colonne] == ' '))
                    {
                        pt[0] = plat.GetLength(0) - ligne - 1;
                        pt[1] = colonne;
                    }
                    else
                    {
                        lignecol[lignecol.GetLength(0)-ligne-1, colonne] = 0;
                        //Console.WriteLine(Ecrire1(lignecol));
                    }
                } while (pt[0] == -1 && SommeElementMatrice(lignecol) != 0);
                test = true;
                int compt = 0;//Compteur pour savoir si il va placer le même mot au même endroit
                if (pt[0] != -1)//Eviter les erreurs si on est sorti de la dernière boucle car la somme des élément de la patrice est egale à 0
                {
                    for (int k = 0; k < mot.Length; k++) //On test si le mot peut être ajouté dans le plateau
                    {
                        if (!(plat[pt[0] - k , pt[1]] == ' ' || plat[pt[0] - k, pt[1]] == mot[k]))
                        {
                            test = false;
                            lignecol[pt[0]-mot.Length+1 , pt[1]] = 0;
                        }
                        if (plat[pt[0] - k, pt[1]] == mot[k])
                        {
                            compt++;
                        }
                    }
                    if (compt == mot.Length)
                    {
                        test = false;
                    }
                }
            } while (!test && SommeElementMatrice(lignecol) != 0);
            if (SommeElementMatrice(lignecol) != 0)
            {
                for (int k = 0; k < mot.Length; k++) //On test si le mot peut être ajouté dans le plateau
                {
                    plat[pt[0] - k, pt[1]] = mot[k];
                }
            }
            return plat;
        }
        public char[,] Ouest (string mot, char[,] plat)
        {
            int[] pt = new int[2] { -1, -1 }; // Point d'ancrage
            int[,] lignecol = new int[plat.GetLength(0), plat.GetLength(1) - mot.Length + 1];
            for (int i = 0; i < lignecol.GetLength(0); i++)
            {
                for (int j = 0; j < lignecol.GetLength(1); j++)
                {
                    lignecol[i, j] = -1;
                }
            }
            int ligne;
            int colonne;
            bool test = false;//Savoir si le mot a pu s'implémeter
            do
            {
                do
                {
                    Random r = new Random();
                    ligne = r.Next(0, lignecol.GetLength(0));
                    colonne = r.Next(0, lignecol.GetLength(1));
                    if (lignecol[ligne, colonne] == -1 && (plat[ligne, plat.GetLength(1) - colonne - 1] == mot[0] || plat[ligne, plat.GetLength(1) - colonne - 1] == ' '))
                    {
                        pt[0] = ligne;
                        pt[1] = plat.GetLength(1) - colonne - 1;
                    }
                    else
                    {
                        lignecol[ligne, lignecol.GetLength(1) - colonne - 1] = 0;
                    }
                } while (pt[0] == -1 && SommeElementMatrice(lignecol) != 0);
                test = true;
                int compt = 0;//Compteur pour savoir si il va placer le même mot au même endroit
                if (pt[0] != -1)//Eviter les erreurs si on est sorti de la dernière boucle car la somme des élément de la patrice est egale à 0
                {
                    for (int k = 0; k < mot.Length; k++) //On test si le mot peut être ajouté dans le plateau
                    {
                        if (!(plat[pt[0], pt[1] - k] == ' ' || plat[pt[0], pt[1] - k] == mot[k]))
                        {
                            test = false;
                            lignecol[pt[0], pt[1]-mot.Length+1] = 0;
                        }
                        if (plat[pt[0], pt[1] - k] == mot[k])
                        {
                            compt++;
                        }
                    }
                    if (compt == mot.Length)
                    {
                        test = false;
                    }
                }
            } while (!test && SommeElementMatrice(lignecol) != 0);
            if (SommeElementMatrice(lignecol) != 0)
            {
                for (int k = 0; k < mot.Length; k++) //On test si le mot peut être ajouté dans le plateau
                {
                    plat[pt[0], pt[1] - k] = mot[k];
                }
            }
            return plat;
        }
        public char[,] NE(string mot, char[,] plat)
        {
            int[] pt = new int[2] { -1, -1 }; // Point d'ancrage
            int[,] lignecol = new int[plat.GetLength(0)-mot.Length+1, plat.GetLength(0) - mot.Length + 1];
            for (int i = 0; i < lignecol.GetLength(0); i++)
            {
                for (int j = 0; j < lignecol.GetLength(1); j++)
                {
                    lignecol[i, j] = -1;
                }
            }
            int ligne;
            int colonne;
            bool test = false;//Savoir si le mot a pu s'implémeter
            do
            {
                do
                {
                    Random r = new Random();
                    ligne = r.Next(0, lignecol.GetLength(0));
                    colonne = r.Next(0, lignecol.GetLength(1));
                    if (lignecol[ligne, colonne] == -1 && (plat[plat.GetLength(0) - (lignecol.GetLength(0)-ligne), colonne] == mot[0] || plat[plat.GetLength(0) - (lignecol.GetLength(0) - ligne), colonne] == ' '))
                    {
                        pt[0] = plat.GetLength(0) - (lignecol.GetLength(0) - ligne);
                        pt[1] = colonne;
                    }
                    else
                    {
                        lignecol[ligne, colonne] = 0;
                        //Console.WriteLine(Ecrire1(lignecol));
                    }
                } while (pt[0] == -1 && SommeElementMatrice(lignecol) != 0);
                test = true;
                int compt = 0;//Compteur pour savoir si il va placer le même mot au même endroit
                if (pt[0] != -1)//Eviter les erreurs si on est sorti de la dernière boucle car la somme des élément de la patrice est egale à 0
                {
                    for (int k = 0; k < mot.Length && test; k++) //On test si le mot peut être ajouté dans le plateau
                    {
                        if (!(plat[pt[0] - k, pt[1] + k] == ' ' || plat[pt[0] - k, pt[1] + k] == mot[k]))
                        {
                            test = false;
                            lignecol[pt[0] - mot.Length + 1, pt[1]] = 0;
                        }
                        if (plat[pt[0] - k, pt[1] + k] == mot[k])
                        {
                            compt++;
                        }
                    }
                    if (compt == mot.Length)
                    {
                        test = false;
                    }
                }
            } while (!test && SommeElementMatrice(lignecol) != 0);
            if (SommeElementMatrice(lignecol) != 0)
            {
                for (int k = 0; k < mot.Length; k++) //On test si le mot peut être ajouté dans le plateau
                {
                    plat[pt[0] - k, pt[1]+k] = mot[k];
                }
            }
            return plat;
        }
        public char[,] NO(string mot, char[,] plat)
        {
            int[] pt = new int[2] { -1, -1 }; // Point d'ancrage
            int[,] lignecol = new int[plat.GetLength(0) - mot.Length + 1, plat.GetLength(0) - mot.Length + 1];
            for (int i = 0; i < lignecol.GetLength(0); i++)
            {
                for (int j = 0; j < lignecol.GetLength(1); j++)
                {
                    lignecol[i, j] = -1;
                }
            }
            int ligne;
            int colonne;
            bool test = false;//Savoir si le mot a pu s'implémeter
            do
            {
                do
                {
                    Random r = new Random();
                    ligne = r.Next(0, lignecol.GetLength(0));
                    colonne = r.Next(0, lignecol.GetLength(1));
                    if (lignecol[ligne, colonne] == -1 && (plat[plat.GetLength(0) - (lignecol.GetLength(0) - ligne), plat.GetLength(1)-(lignecol.GetLength(1)- colonne)] == mot[0] || plat[plat.GetLength(0) - (lignecol.GetLength(0) - ligne), plat.GetLength(1) - (lignecol.GetLength(1) - colonne)] == ' '))
                    {
                        pt[0] = plat.GetLength(0) - (lignecol.GetLength(0) - ligne);
                        pt[1] = plat.GetLength(1) - (lignecol.GetLength(1) - colonne);
                    }
                    else
                    {
                        lignecol[ligne, colonne] = 0;
                        //Console.WriteLine(Ecrire1(lignecol));
                    }
                } while (pt[0] == -1 && SommeElementMatrice(lignecol) != 0);
                test = true;
                int compt = 0;//Compteur pour savoir si il va placer le même mot au même endroit
                if (pt[0] != -1)//Eviter les erreurs si on est sorti de la dernière boucle car la somme des élément de la patrice est egale à 0
                {
                    for (int k = 0; k < mot.Length && test; k++) //On test si le mot peut être ajouté dans le plateau
                    {
                        if (!(plat[pt[0] - k, pt[1] -k] == ' ' || plat[pt[0] - k, pt[1] - k] == mot[k]))
                        {
                            test = false;
                            lignecol[pt[0] - mot.Length + 1, pt[1]- mot.Length +1] = 0;
                        }
                        if (plat[pt[0] - k, pt[1] - k] == mot[k])
                        {
                            compt++;
                        }
                    }
                    if (compt == mot.Length)
                    {
                        test = false;
                    }
                }
            } while (!test && SommeElementMatrice(lignecol) != 0);
            if (SommeElementMatrice(lignecol) != 0)
            {
                for (int k = 0; k < mot.Length; k++) //On test si le mot peut être ajouté dans le plateau
                {
                    plat[pt[0] - k, pt[1] - k] = mot[k];
                }
            }
            return plat;
        }
        public char[,] SE(string mot, char[,] plat)
        {
            int[] pt = new int[2] { -1, -1 }; // Point d'ancrage
            int[,] lignecol = new int[plat.GetLength(0) - mot.Length + 1, plat.GetLength(0) - mot.Length + 1];
            for (int i = 0; i < lignecol.GetLength(0); i++)
            {
                for (int j = 0; j < lignecol.GetLength(1); j++)
                {
                    lignecol[i, j] = -1;
                }
            }
            int ligne;
            int colonne;
            bool test = false;//Savoir si le mot a pu s'implémeter
            do
            {
                do
                {
                    Random r = new Random();
                    ligne = r.Next(0, lignecol.GetLength(0));
                    colonne = r.Next(0, lignecol.GetLength(1));
                    if (lignecol[ligne, colonne] == -1 && (plat[ligne,colonne] == mot[0] || plat[ligne, colonne] == ' '))
                    {
                        pt[0] = ligne;
                        pt[1] = colonne;
                    }
                    else
                    {
                        lignecol[ligne, colonne] = 0;
                        //Console.WriteLine(Ecrire1(lignecol));
                    }
                } while (pt[0] == -1 && SommeElementMatrice(lignecol) != 0);
                test = true;
                int compt = 0;//Compteur pour savoir si il va placer le même mot au même endroit
                if (pt[0] != -1)//Eviter les erreurs si on est sorti de la dernière boucle car la somme des élément de la patrice est egale à 0
                {
                    for (int k = 0; k < mot.Length && test; k++) //On test si le mot peut être ajouté dans le plateau
                    {
                        if (!(plat[pt[0] + k, pt[1] + k] == ' ' || plat[pt[0] + k, pt[1] + k] == mot[k]))
                        {
                            test = false;
                            lignecol[pt[0], pt[1]] = 0;
                        }
                        if (plat[pt[0] + k, pt[1] + k] == mot[k])
                        {
                            compt++;
                        }
                    }
                    if (compt == mot.Length)
                    {
                        test = false;
                    }
                }
            } while (!test && SommeElementMatrice(lignecol) != 0);
            if (SommeElementMatrice(lignecol) != 0)
            {
                for (int k = 0; k < mot.Length; k++) //On test si le mot peut être ajouté dans le plateau
                {
                    plat[pt[0] + k, pt[1] + k] = mot[k];
                }
            }
            return plat;
        }
        public char[,] SO(string mot, char[,] plat)
        {
            int[] pt = new int[2] { -1, -1 }; // Point d'ancrage
            int[,] lignecol = new int[plat.GetLength(0) - mot.Length + 1, plat.GetLength(0) - mot.Length + 1];
            for (int i = 0; i < lignecol.GetLength(0); i++)
            {
                for (int j = 0; j < lignecol.GetLength(1); j++)
                {
                    lignecol[i, j] = -1;
                }
            }
            int ligne;
            int colonne;
            bool test = false;//Savoir si le mot a pu s'implémeter
            do
            {
                do
                {
                    Random r = new Random();
                    ligne = r.Next(0, lignecol.GetLength(0));
                    colonne = r.Next(0, lignecol.GetLength(1));
                    if (lignecol[ligne, colonne] == -1 && (plat[ligne, plat.GetLength(1) - (lignecol.GetLength(1) - colonne)] == mot[0] || plat[ligne, plat.GetLength(1) - (lignecol.GetLength(1) - colonne)] == ' '))
                    {
                        pt[0] = ligne;
                        pt[1] = plat.GetLength(1) - (lignecol.GetLength(1) - colonne);
                    }
                    else
                    {
                        lignecol[ligne, colonne] = 0;
                        //Console.WriteLine(Ecrire1(lignecol));
                    }
                } while (pt[0] == -1 && SommeElementMatrice(lignecol) != 0);
                test = true;
                int compt = 0;//Compteur pour savoir si il va placer le même mot au même endroit
                if (pt[0] != -1)//Eviter les erreurs si on est sorti de la dernière boucle car la somme des élément de la patrice est egale à 0
                {
                    for (int k = 0; k < mot.Length && test; k++) //On test si le mot peut être ajouté dans le plateau
                    {
                        if (!(plat[pt[0] + k, pt[1] - k] == ' ' || plat[pt[0] + k, pt[1] - k] == mot[k]))
                        {
                            test = false;
                            lignecol[pt[0], pt[1] - mot.Length + 1] = 0;
                        }
                        if (plat[pt[0] + k, pt[1] - k] == mot[k])
                        {
                            compt++;
                        }
                    }
                    if (compt == mot.Length)
                    {
                        test = false;
                    }
                }
            } while (!test && SommeElementMatrice(lignecol) != 0);
            if (SommeElementMatrice(lignecol) != 0)
            {
                for (int k = 0; k < mot.Length; k++) //On test si le mot peut être ajouté dans le plateau
                {
                    plat[pt[0] + k, pt[1] - k] = mot[k];
                }
            }
            return plat;
        }

        public bool Test_Plateau(string mot, int ligne, int colonne, string direction)
        {
            mot = mot.ToUpper();
            direction = direction.ToUpper();
            bool test = false;
            switch (direction)
            {
                case "S":
                    test = true;
                    if (ligne-1 + mot.Length > plat.GetLength(0))//Vérifier que on ne va pas être hors index
                    {
                        test = false;
                    }
                    else
                    {
                        for (int k = 0; k < mot.Length && test; k++) //On ajoute le mot dans le plateau
                        {

                            if (plat[ligne - 1 + k, colonne - 1] != mot[k])
                            {
                                test = false;
                            }
                        }
                    }
                    break;
                case "N":
                    test = true;
                    if (ligne - mot.Length < 0)//Vérifier que on ne va pas être hors index
                    {
                        test = false;
                    }
                    else
                    {
                        for (int k = 0; k < mot.Length && test; k++)
                        {

                            if (plat[ligne - 1 - k, colonne - 1] != mot[k])
                            {
                                test = false;
                            }
                        }
                    }
                    break;
                case "E":
                    test = true;
                    if (colonne -1 + mot.Length > plat.GetLength(1))//Vérifier que on ne va pas être hors index
                    {
                        test = false;
                    }
                    else
                    {
                        for (int k = 0; k < mot.Length && test; k++) 
                        {

                            if (plat[ligne - 1, colonne - 1 + k] != mot[k])
                            {
                                test = false;
                            }
                        }
                    }
                    break;
                case "O":
                    test = true;
                    if (colonne - mot.Length < 0)//Vérifier que on ne va pas être hors index
                    {
                        test = false;
                    }
                    else
                    {
                        for (int k = 0; k < mot.Length && test; k++) 
                        {

                            if (plat[ligne - 1, colonne - 1 - k] != mot[k])
                            {
                                test = false;
                            }
                        }
                    }
                    break;
                case "SE":
                    test = true;
                    if (colonne - 1 + mot.Length- 1 + ligne -1  > plat.GetLength(1))//Vérifier que on ne va pas être hors index pareil que Est
                    {
                        test = false;
                    }
                    else if(colonne - 1 + mot.Length > plat.GetLength(1))
                    {
                        test = false;
                    }
                    else
                    {
                        for (int k = 0; k < mot.Length && test; k++)
                        {

                            if (plat[ligne - 1 + k, colonne - 1 + k] != mot[k])
                            {
                                test = false;
                            }
                        }
                    }
                    break;
                case "SO":
                    test = true;
                    if (colonne - mot.Length + 1 - ligne +1 < 0)//Vérifier que on ne va pas être hors index
                    {
                        test = false;
                    }
                    else if (colonne - mot.Length < 0)//Vérifier que on ne va pas être hors index
                    {
                        test = false;
                    }
                    else
                    {
                        for (int k = 0; k < mot.Length && test; k++)
                        {

                            if (plat[ligne - 1 + k, colonne - 1 - k] != mot[k])
                            {
                                test = false;
                            }
                        }
                    }
                    break;
                case "NO":
                    test = true;
                    if (colonne - mot.Length + plat.GetLength(0) - ligne - 1 < 0 && colonne > ligne)//Vérifier que on ne va pas être hors index
                    {
                        test = false;
                    }
                    else if (colonne - mot.Length < 0)
                    {
                        test = false;
                    }
                    else
                    {
                        for (int k = 0; k < mot.Length && test; k++)
                        {

                            if (plat[ligne - 1 - k, colonne - 1 - k] != mot[k])
                            {
                                test = false;
                            }
                        }
                    }
                    break;
                case "NE":
                    test = true;
                    if (colonne - 1 + mot.Length + plat.GetLength(0) - ligne - 1 > plat.GetLength(1) && ligne-1 + colonne-1< plat.GetLength(1)+plat.GetLength(0))//Vérifier que on ne va pas être hors index 
                    {
                        test = false;
                        Console.WriteLine("A");
                    }
                    else if (colonne - 1 + mot.Length > plat.GetLength(1))
                    {
                        test = false;
                        Console.WriteLine("B");
                    }
                    else
                    {
                        for (int k = 0; k < mot.Length && test; k++)
                        {

                            if (plat[ligne - 1 - k, colonne - 1 + k] != mot[k])
                            {
                                test = false;
                            }
                        }
                    }
                    break;
            }
            return test;
        }
        public string Ecrire(char[,] plat)
        {
            string a="";
            for (int k = 0; k < plat.GetLength(0); k++)
            {
                for (int j = 0; j < plat.GetLength(1); j++)
                {
                    a += plat[k, j] + " ";
                }
                a += "\n";
            }
            return a;
        }
        public string Ecrire1(int[,] plat)
        {
            string a = "";
            for (int k = 0; k < plat.GetLength(0); k++)
            {
                for (int j = 0; j < plat.GetLength(1); j++)
                {
                    a += plat[k, j] + " ";
                }
                a += "\n";
            }
            return a;
        }
    }



    
}
