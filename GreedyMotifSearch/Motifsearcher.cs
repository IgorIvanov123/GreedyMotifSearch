using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreedyMotifSearch
{
    public class MotifSearcher
    {
        private string[] _dna;
        private int _k;
        public MotifSearcher(string[] dna, int k)
        {
            _dna = dna;
            _k = k;

        }

        public int[,] CreateProfile(string motif)
        {
            int[,] profile = new int[4, _k];
            for (int i = 0; i < motif.Length; i++)
            {
                profile[int.Parse(motif[i].ToString()), i]++;
            }
            return profile;
        }

        public int[,] ModifyProfile(int[,] profile, string motif)
        {
            for (int i = 0; i < motif.Length; i++)
            {
                profile[int.Parse(motif[i].ToString()), i]++;
            }
            return profile;
        }

        public string FindMostProbableMotif(int[,] profile, string motif)
        {
            double probability = 0;
            string mostProbableMotif = motif.Substring(0, _k);
            for (int i = 0; i < motif.Length - _k + 1; i++)
            {
                string currentMotif = motif.Substring(i, _k);
                double currentProbability = 1;
                for (int j = 0; j < _k; j++)
                {
                    currentProbability *= profile[int.Parse(currentMotif[j].ToString()),j];
                }
                if(currentProbability > probability)
                {
                    mostProbableMotif = currentMotif;
                }
            }
            return mostProbableMotif;
        }
        public int Score(string[] motifs)
        {
            string consensus = "";
            for(int i = 0; i < motifs[0].Length; i++)
            {
                int[] countsOfACGT = new int[4];
                foreach(var motif in motifs)
                {
                    countsOfACGT[int.Parse(motif[i].ToString())]++;
                }
                consensus += Array.IndexOf(countsOfACGT, countsOfACGT.Max()).ToString();
            }

            int score = 0;
            for(int i = 0; i < motifs.Length; i++)
            {
                for(int j = 0; j < motifs[0].Length; j++)
                {
                    if(motifs[i][j] != consensus[j])
                    {
                        score++;
                    }
                }
            }
            return score;
        }

        public string[] GreedyMotifSearch()
        {
            List<string> bestMotifs = new List<string>();
            for(int i = 0; i < _dna.Length; i++)
            {
                bestMotifs.Add(_dna[i].Substring(0, _k));
            }
            for(int i = 0; i < _dna[0].Length - _k + 1; i++)
            {
                List<string> motifs = new List<string>();
                string currentMotif = _dna[0].Substring(i, _k);
                motifs.Add(currentMotif);
                int[,] profile = CreateProfile(currentMotif);
                for (int j = 1; j < _dna.Length; j++)
                {
                    string motif = FindMostProbableMotif(profile, _dna[j]);
                    profile = ModifyProfile(profile, motif);
                    motifs.Add(motif);
                }
                if(Score(motifs.ToArray()) < Score(bestMotifs.ToArray()))
                {
                    bestMotifs = motifs;
                }              
            }
            return bestMotifs.ToArray();
        }
    }
}
