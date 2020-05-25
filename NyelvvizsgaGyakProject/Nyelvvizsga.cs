using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NyelvvizsgaGyakProject
{
    class Nyelvvizsga
    {
        //nyelv;2009;2010;2011;2012;2013;2014;2015;2016;2017;2018
        //angol;116;441;709;620;656;445;471;480;695;554
        public string nyelv { get; set; }
        public Dictionary<int, int> ev_VizsgazokSzama { get; set; }
        public bool sikeres { get; set; }

        public Nyelvvizsga(string nyelv, Dictionary<int, int> ev_VizsgazokSzama, bool sikeres)
        {
            this.nyelv = nyelv;
            this.ev_VizsgazokSzama = ev_VizsgazokSzama;
            this.sikeres = sikeres;
        }
    }
}
