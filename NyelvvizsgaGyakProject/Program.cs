using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NyelvvizsgaGyakProject
{
    class Program
    {
        static void Main(string[] args)
        {
            //1. feladat
            NyelvvizsgakRepository repo = new NyelvvizsgakRepository();

            //2. feladat
            Console.WriteLine("2. feladat: A legnépszerűbb nyelvek:");
            repo.Nepszeru();

            //3. feladat
            Console.Write("3. feladat: ");
            int evszam = 0;
            do
            {
                Console.Write("\tAdjon meg egy évszámot: ");
                string be = Console.ReadLine();
                int.TryParse(be, out evszam);
                
            } while (!(evszam >= 2009 && evszam <= 2017));
            Console.WriteLine("\tA vizsgált év: " + evszam);

            //4. feladat
            Console.WriteLine("4. feladat: ");
            repo.GetNyelvSikertelenVizsgaAranyaEvben(evszam);

            //5. feladat
            Console.WriteLine("5. feladat: ");
            repo.VoltEVizsgazo(evszam);

            //6. feladat
            repo.SikeresVizsgakAranyaOsszesitettAdatokkalFileba();

            Console.ReadKey();
        }
    }
}
