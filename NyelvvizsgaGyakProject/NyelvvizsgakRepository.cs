using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NyelvvizsgaGyakProject
{
    class NyelvvizsgakRepository
    {
        List<Nyelvvizsga> nyelvizsgakLista;

        public NyelvvizsgakRepository()
        {
            nyelvizsgakLista = new List<Nyelvvizsga>();
            Beolvas("sikeres.csv", true);
            Beolvas("sikertelen.csv", false);
        }

        private void Beolvas(string fileName, bool sikeres)
        {
            using (var sr = new StreamReader("forras/" + fileName))
            {
                sr.ReadLine();
                while (!sr.EndOfStream)
                {
                    var sor = sr.ReadLine().Split(';');
                    Dictionary<int, int> d = new Dictionary<int, int>();
                        d.Add(2009, Convert.ToInt32(sor[1]));
                        d.Add(2010, Convert.ToInt32(sor[2]));
                        d.Add(2011, Convert.ToInt32(sor[3]));
                        d.Add(2012, Convert.ToInt32(sor[4]));
                        d.Add(2013, Convert.ToInt32(sor[5]));
                        d.Add(2014, Convert.ToInt32(sor[6]));
                        d.Add(2015, Convert.ToInt32(sor[7]));
                        d.Add(2016, Convert.ToInt32(sor[8]));
                        d.Add(2017, Convert.ToInt32(sor[9]));
                        d.Add(2018, Convert.ToInt32(sor[10]));

                    nyelvizsgakLista.Add(new Nyelvvizsga(sor[0], d, sikeres));
                }
            }
        }

        public void Nepszeru()
        {
            var nepszeru = nyelvizsgakLista.
                                GroupBy(x => x.nyelv).
                                Select(g => new
                                {
                                    Key = g.Key,
                                    val = g.Sum(x => x.ev_VizsgazokSzama.Values.Sum())
                                }).
                                OrderByDescending(x => x.val).ToList();

            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine("\t" + nepszeru[i].Key + "\t-" + nepszeru[i].val);
            }
        }

        public void GetNyelvSikertelenVizsgaAranyaEvben(int ev)
        {
            var asdf = nyelvizsgakLista.GroupBy(x => x.nyelv).Select(g => new
                                        {
                                            Key = g.Key,
                                            Ossz = g.Sum(r => r.ev_VizsgazokSzama[ev]),
                                            Sikertelen = g.Where(r => r.sikeres.Equals(false)).Sum(x => x.ev_VizsgazokSzama[ev]),
                                        });
            double maxarany = 0;
            string maxNev = "";
            foreach (var item in asdf)
            {
                double arany = 0;
                if (item.Ossz > 0 && item.Sikertelen > 0)
                {
                    arany = (Convert.ToDouble(item.Sikertelen) / item.Ossz) * 100;
                    arany = Math.Round(arany, 2);
                }

                if (maxarany < arany)
                {
                    maxarany = arany;
                    maxNev = item.Key;
                }
            }
            Console.WriteLine("\t{0} -ben {1} nyelvből a sikertelen vizsgák aránya {2}%", ev, maxNev, maxarany);

        }

        public void VoltEVizsgazo(int ev)
        {
            var nemVolt = nyelvizsgakLista.
                                    Where(x=> x.ev_VizsgazokSzama[ev].Equals(0)).
                                    GroupBy(x=> x.nyelv).ToList();
            

            if (nemVolt.Count()>0)
            {
                nemVolt.ForEach(x => Console.WriteLine("\t{0}",x.Key));
            }
            else
            {
                Console.WriteLine("\tMinden nyelvből volt vizsgázó");
            }
        }


        public void SikeresVizsgakAranyaOsszesitettAdatokkalFileba()
        {
            var sikeresArany = nyelvizsgakLista.GroupBy(x => x.nyelv).Select(g => new
                                        {
                                            Key = g.Key,
                                            Ossz = g.Sum(r => r.ev_VizsgazokSzama.Values.Sum()),
                                            Sikeres = g.Where(r => r.sikeres.Equals(true)).Sum(x => x.ev_VizsgazokSzama.Values.Sum()),
                                        });

            using (var sw = new StreamWriter("osszesitett.csv",false,Encoding.UTF8))
            {
                foreach (var item in sikeresArany)
                {
                    sw.WriteLine("{0};{1};{2}%",
                        item.Key,
                        item.Ossz,
                        Math.Round(((Convert.ToDouble(item.Sikeres)/item.Ossz)*100),2));                    
                }
            }
            

        }
    }
}
