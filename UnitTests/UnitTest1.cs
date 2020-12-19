using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GreedyMotifSearch;

namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CreateProfileForACCT()
        {
            string[] dna = new string[0];
            MotifSearcher ms = new MotifSearcher(dna, 4);
            int[,] profile = ms.CreateProfile("0113");
            string answ = "";
            for(int i = 0; i < 4; i++)
            {
                for(int j = 0; j < 4; j++)
                {
                    answ += profile[i, j].ToString();
                }
            }
            Assert.AreEqual(answ, "1000011000000001");
        }
        [TestMethod]
        public void ModifyProfileByATGT()
        {
            string[] dna = new string[0];
            MotifSearcher ms = new MotifSearcher(dna, 4);
            int[,] profile = ms.CreateProfile("0113");
            profile = ms.ModifyProfile(profile, "0323");
            string answ = "";
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    answ += profile[i, j].ToString();
                }
            }
            Assert.AreEqual(answ, "2000011000100102");
        }

        [TestMethod]
        public void RightCalculateScore()                                         //Test for AACT
                                                                                  //         ACCA  
        {                                                                         //         GTCT
            string[] dna = new string[0];                                         //         ATAT
            MotifSearcher ms = new MotifSearcher(dna,4);
            string[] motifs = new string[] { "0013", "0110", "2313", "0303" };
            int a = ms.Score(motifs);
            Assert.AreEqual(ms.Score(motifs), 5);
            
        }
        [TestMethod]
        public void Correct_Find_ACGT_in_TCGTAACGTGA()
        {
            string[] dna = new string[0];
            MotifSearcher ms = new MotifSearcher(dna, 4);
            int[,] profile = ms.CreateProfile("0113");
            profile = ms.ModifyProfile(profile, "0323");
            string answ = ms.FindMostProbableMotif(profile, "31230012320");
            Assert.AreEqual(answ, "0123");
        }
        [TestMethod]
        public void CorrectReturnFirstMotifWhenNotFindForCurrentProfile()
        {
            string[] dna = new string[0];
            MotifSearcher ms = new MotifSearcher(dna, 4);
            int[,] profile = ms.CreateProfile("0113");
            profile = ms.ModifyProfile(profile, "0323");
            string answ = ms.FindMostProbableMotif(profile, "3123002320");
            Assert.AreEqual(answ, "3123");
        }
        [TestMethod]
        public void CorrectReturnLastMotif()
        {
            string[] dna = new string[0];
            MotifSearcher ms = new MotifSearcher(dna, 4);
            int[,] profile = ms.CreateProfile("0113");
            profile = ms.ModifyProfile(profile, "0323");
            string answ = ms.FindMostProbableMotif(profile, "31230023200323");
            Assert.AreEqual(answ, "0323");
        }
    }
}
