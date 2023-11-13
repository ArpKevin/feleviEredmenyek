using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace feleviEredmenyek
{
    class Tanulo
    {
        public string Nev { get; set; }
        public long Azonosito { get; set; }
        public Dictionary<string, int> Jegyek { get; set; }

        public Tanulo(string sor, string[] tantargyak)
        {
            var t = sor.Split("\t");
            Nev = t[0];
            Azonosito = long.Parse(t[1]);
            Jegyek = new Dictionary<string, int>();
            for (int i = 2; i < t.Length; i++)
            {
                Jegyek.Add(tantargyak[i], int.Parse(t[i]));
            }
        }

        public override string ToString() => $"{Nev}, {Azonosito}, {string.Join(", ", Jegyek.Values)}";
    }
}