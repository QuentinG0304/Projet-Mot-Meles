using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Projet_Algo_Mot_Meles
{
    class Jeu
    {
        Dictionnaire dico;
        public SortedList<Joueur, List<Plateau>> plateaux;
        Joueur[] joueurs;
        int nbPlateaux;

        public Jeu(Dictionnaire d, Joueur[] joueurs, int nbPlat)
        {
            dico = d;
            this.joueurs = joueurs;
            nbPlateaux = nbPlat;
            plateaux = new SortedList<Joueur, List<Plateau>>();
            List<Plateau> l1 = new List<Plateau>();
            plateaux.Add(joueurs[0], l1);
            List<Plateau> l2 = new List<Plateau>();
            plateaux.Add(joueurs[1], l2);
        }

        public void AddPlateau(Joueur jo, Plateau plat)
        {
            plateaux[jo].Append(plat);
        }

        public bool FiniPlateau(Joueur jo) //sert à savoir si le joueur a trouvé tous les mots du plateau (dans le main il faudra faire les histoires de timer)
        {
            return (plateaux[jo].Last().nbmots == jo.NbMotsTrouvés);
        }

        public Joueur VainqueurManche() //retourne le joueur vainqueur de la manche et incrémente son score, le timeSpent est changé dans le main à chaque manche
        {
            Joueur ret;
            if (joueurs[0].NbMotsTrouvés == joueurs[1].NbMotsTrouvés)
            {
                if (joueurs[0].timeSpent < joueurs[1].timeSpent)
                {
                    ret = joueurs[0];
                    joueurs[0].score += 1;
                    joueurs[0].NbMotsTrouvés = 0;
                    joueurs[1].NbMotsTrouvés = 0;
                }
                else
                {
                    ret = joueurs[1];
                    joueurs[1].score += 1;
                    joueurs[0].NbMotsTrouvés = 0;
                    joueurs[1].NbMotsTrouvés = 0;
                }
            }
            else
            {
                if (joueurs[0].NbMotsTrouvés > joueurs[1].NbMotsTrouvés)
                {
                    ret = joueurs[0];
                    joueurs[0].score += 1;
                    joueurs[0].NbMotsTrouvés = 0;
                    joueurs[1].NbMotsTrouvés = 0;
                }
                else
                {
                    ret = joueurs[1];
                    joueurs[1].score += 1;
                    joueurs[0].NbMotsTrouvés = 0;
                    joueurs[1].NbMotsTrouvés = 0;
                }

            }

            return ret;
        }

        public Joueur VainqueurPartie()
        {
            Joueur ret;
            if (joueurs[0].score > joueurs[1].score)
            {
                ret = joueurs[0];
            }
            else
            {
                ret = joueurs[1];
            }
            return ret;
        }

    }
}
