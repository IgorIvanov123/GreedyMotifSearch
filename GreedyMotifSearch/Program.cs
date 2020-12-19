using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreedyMotifSearch
{
    class Program
    {
        static void Main(string[] args)
        {
            int k, t;
            List<string> motifs = new List<string>();
            using (StreamReader sr = new StreamReader("test.txt"))
            {
                string[] kt = sr.ReadLine().Split(' ');
                k = int.Parse(kt[0]);
                t = int.Parse(kt[1]);
                for(int i = 0; i < t; i++)
                {
                    motifs.Add(sr.ReadLine());
                }
            }
            for(int i = 0; i < motifs.Count; i++)
            {
                motifs[i] = motifs[i].Replace('A', '0').Replace('C', '1').Replace('G', '2').Replace('T', '3');
            }
            MotifSearcher ms = new MotifSearcher(motifs.ToArray(), k);
            string[] answ = ms.GreedyMotifSearch();
            for (int i = 0; i < answ.Length; i++)
            {
                answ[i] = answ[i].Replace('0', 'A').Replace('1', 'C').Replace('2', 'G').Replace('3', 'T');
                Console.WriteLine(answ[i]);
            }
            Console.ReadKey();
        }
    }
}
