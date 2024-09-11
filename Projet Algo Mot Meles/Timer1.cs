using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Projet_Algo_Mot_Meles
{
    class Timer
    {
        DateTime intervalle;
        public Timer(int intervalle)
        {
            this.intervalle = DateTime.Now.AddSeconds(intervalle);
        }

        public bool Start()
        {
            return DateTime.Compare(DateTime.Now,intervalle)<1;
        }
    }

}
