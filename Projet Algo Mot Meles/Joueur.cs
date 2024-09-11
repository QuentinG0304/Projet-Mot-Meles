using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Algo_Mot_Meles
{
    class Joueur : IComparable<Joueur> //Pour pouvoir Add des plateaux dans la class Jeu
    {
        string nom;
        public string Nom { get { return nom; } }
        string[] mots_trouvés;
        public DateTime timeSpent;
        public int score; //Je ne sais pas si ça doit être un tableau ou pas car dans les docs ils nous disent de l'initialiser à 0
        int nbmotstrouvés;
        public int NbMotsTrouvés { get { return nbmotstrouvés; } set { nbmotstrouvés = value; } }

        public Joueur (string nom)
        {
            this.nom = nom;
            this.mots_trouvés = null;
            this.score = 0;
            this.nbmotstrouvés = 0;
            timeSpent = new DateTime();
        }


        public int CompareTo(Joueur other)// Va avec IComparable
        {
            if (other == null) return 1;
            return nom.CompareTo(other.Nom);
        }

        
        public void Add_Mot(string mot)
        {
            mots_trouvés[nbmotstrouvés] = mot;
            nbmotstrouvés++;
        }
        public string toString()
        {
            string a = "Nom du joueur: "+nom+ "\nMots trouvés:\n";
            for(int i=0; i<nbmotstrouvés;i++)
            {
                a += mots_trouvés[i] + "\n";
            }
            a += "Score: " + score;
            return a;
        }
        public void Add_Score(int val)
        {
            score += val;
        }
    }
}
