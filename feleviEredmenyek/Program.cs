using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace feleviEredmenyek
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Tanulo> tanulok = new();

            using StreamReader sr = new("../../../src/adatok.txt");

            string[] tantargyak = sr.ReadLine().Split("\t");

            for (int i = 2; !sr.EndOfStream; i++)
            {
                string vizsgaltSor = sr.ReadLine();
                if (!string.IsNullOrWhiteSpace(vizsgaltSor)) tanulok.Add(new(vizsgaltSor, tantargyak));
            }

            Console.WriteLine("2. feladat: Programozásból megbukottak:");
            var programozasbolBukottak = tanulok
                .Where(t => t.Jegyek["Programozás"] == 1)
                .ToList();

            foreach (var p in programozasbolBukottak)
            {
                Console.WriteLine(p);
            }

            Console.WriteLine("3. feladat");

            var _3asAngolTanulo = harmasAngol(tanulok);

            if (_3asAngolTanulo == null) Console.WriteLine("Senkinek nem volt hármasa angolból");
            else Console.WriteLine(_3asAngolTanulo);

            Console.Write("4. feladat: Add meg a tanuló nevét: ");
            string nev = Console.ReadLine();

            if (!(tanulok.Exists(t => t.Nev == nev)))
            {
                Console.WriteLine("Nincs ilyen nevű ember!");
            }
            else
            {
                var bekertTanulo = tanulok.Single(t => t.Nev == nev);
                var legjobbJegy = bekertTanulo.Jegyek.MaxBy(kvp => kvp.Value);

                Console.WriteLine($"A tanuló legjobb tantárgya a {legjobbJegy.Key}, amelyre érdemjegye {legjobbJegy.Value}");

                using StreamWriter sw = new("../../../src/legjobbJegyesAdatai.txt", false, Encoding.UTF8);

                sw.WriteLine($"{bekertTanulo.Nev}; {bekertTanulo.Azonosito}");
            }

            Console.ReadKey();
        }

        public static List<double> tanuloAtlag(List<Tanulo> tanulok)
        {
            List<double> tanuloAtlagok = new();
            foreach (var t in tanulok)
            {
                tanuloAtlagok.Add(t.Jegyek.Values.Average());
            }
            return tanuloAtlagok;
        }

        public static double osztalyAtlag(List<Tanulo> tanulok) => tanuloAtlag(tanulok).Average();
        public static List<double> tantargyAtlagok(List<Tanulo> tanulok)
        {
            List<double> tantargyAtlagok = tanulok
                .SelectMany(t => t.Jegyek)
                .GroupBy(kvp => kvp.Key)
                .Select(x => x.Average(kvp => kvp.Value))
                .ToList();

            return tantargyAtlagok;
        }

        public static Tanulo harmasAngol(List<Tanulo> tanulok)
        {
            return tanulok.FirstOrDefault(t => t.Jegyek["Angol nyelv"] == 3);
        }

    }
}