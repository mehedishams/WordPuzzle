//////////////////////////////////////////////
//////////////////////////////////////////////
// Coded by Mehedi Shams Rony ////////////////
// September, October 2016 ///////////////////
//////////////////////////////////////////////
//////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Configuration;
using Microsoft.VisualBasic;

namespace WordPuzzle.Classes
{
    class ScoreEntity
    {
        public string Category { get; set; }
        public string Scorer { get; set; }
        public int Score { get; set; }
        public DateTime ScoreTime { get; set; }
        public ScoreEntity(string CategoryParam, string ScorerParam, int ScoreParam, DateTime ScoreTimeParam)
        {
            Category = CategoryParam;
            Score = ScoreParam;
            Scorer = ScorerParam;
            ScoreTime = ScoreTimeParam;
        }
        public ScoreEntity()
        {
        }
    }

    class TopScore
    {
        private const string FILE_NAME_FOR_SCORES = "Scores.dat";
        private const string ENCRYPTION_PASSWORD = "mambazamba";
        private int MAX_SCORES = Convert.ToInt16(ConfigurationManager.AppSettings["MAX_SCORES"]);

        List<ScoreEntity> Scores = new List<ScoreEntity>();
        public string CurrentCategory { get; set; }
        public string Scorer { get; set; }
        public int CurrentScore { get; set; }
        public DateTime ScoreTime { get; set; }
        public TopScore(string CategoryParam, string ScorerParam, int ScoreParam, DateTime ScoreTimeParam)
        {
            CurrentCategory = CategoryParam;
            CurrentScore = ScoreParam;
            Scorer = ScorerParam;
            ScoreTime = ScoreTimeParam;

            Scores = ReadScoresFromFile();
        }
        public TopScore()
        {
            Scores = ReadScoresFromFile();
        }

        public List<ScoreEntity> GetScores(string Category)
        {
            return Scores.Where(p => p.Category.Equals(Category)).ToList();
        }

        public bool CheckAndSaveIfTopScore()
        {
            string ScorerName = "";
            bool Added = false;
            if (CurrentScore <= 0) return false;  // No scoring for 0's.

            if (Scores.Count == 0)  // No score is written yet.
            {
                bool OptsOutSaving = GetScorerName(ref ScorerName);
                if (OptsOutSaving) return false;

                StringBuilder PipedScores = new StringBuilder();
                PipeTheScores(PipedScores, new ScoreEntity(CurrentCategory, ScorerName, CurrentScore, DateTime.Now));
                PipedScores.Remove(PipedScores.Length - 1, 1);                  // Remove the last pipe.

                string EncryptedScores = StringCipher.Encrypt(PipedScores.ToString(), ENCRYPTION_PASSWORD);
                using (StreamWriter OutputFile = new StreamWriter(FILE_NAME_FOR_SCORES))
                    OutputFile.Write(EncryptedScores);

                return true;
            }
            else
            {
                List<ScoreEntity> ScoreListForCurrentCategory = new List<ScoreEntity>();
                ScoreListForCurrentCategory = Scores.Where(q => q.Category.Equals(CurrentCategory)).ToList();   // Get subset of scores which belong to the current category.

                if (ScoreListForCurrentCategory.Count == 0) // If no score has been stored for the category yet, then just add it.
                {
                    bool OptsOutSaving = GetScorerName(ref ScorerName);
                    if (OptsOutSaving) return false;

                    Scores.Add(new ScoreEntity(CurrentCategory, ScorerName, CurrentScore, DateTime.Now));
                }
                else // If there are scores for the current category, then try to add the new high score right before the available next high score.
                {
                    foreach (ScoreEntity ScoreIter in ScoreListForCurrentCategory.OrderByDescending(p => p.Score).ToList())  // Sort the list and compare.
                        if (CurrentScore >= ScoreIter.Score)
                        {
                            bool OptsOutSaving = GetScorerName(ref ScorerName);
                            if (OptsOutSaving) return false;

                            ScoreListForCurrentCategory.Insert(ScoreListForCurrentCategory.IndexOf(ScoreIter), new ScoreEntity(CurrentCategory, ScorerName, CurrentScore, DateTime.Now));
                            Added = true;
                            break;
                        }

                    if (ScoreListForCurrentCategory.Count > MAX_SCORES)     // Maximum 14 (MAX_WORDS) scores available.
                        ScoreListForCurrentCategory.RemoveAt(MAX_SCORES);   // So if there is more (after the insertion above then break), then remove the last one.
                    else if (!Added) // Otherwise add the current score to the last.
                    {
                        bool OptsOutSaving = GetScorerName(ref ScorerName);
                        if (OptsOutSaving) return false;

                        ScoreListForCurrentCategory.Add(new ScoreEntity(CurrentCategory, ScorerName, CurrentScore, DateTime.Now));
                    }

                    Scores.RemoveAll(p => p.Category.Equals(CurrentCategory));  // First remove all the scores for current category.
                    Scores.AddRange(ScoreListForCurrentCategory);               // Then add the scores for the current category.
                }
                StringBuilder PipedScores = new StringBuilder();
                foreach (ScoreEntity ScoreIter in Scores)
                    PipeTheScores(PipedScores, ScoreIter);
                PipedScores.Remove(PipedScores.Length - 1, 1);                  // Remove the last pipe.

                string EncryptedScores = StringCipher.Encrypt(PipedScores.ToString(), ENCRYPTION_PASSWORD);
                using (StreamWriter OutputFile = new StreamWriter(FILE_NAME_FOR_SCORES))
                    OutputFile.Write(EncryptedScores);
                return true;
            }
        }

        private List<ScoreEntity> ReadScoresFromFile()
        {
            List<ScoreEntity> ScoreList = new List<ScoreEntity>();
            if (!File.Exists(FILE_NAME_FOR_SCORES))
                return ScoreList;
            try
            {
                string Str = File.ReadAllText(FILE_NAME_FOR_SCORES);
                string[] DecryptedWords = StringCipher.Decrypt(Str, ENCRYPTION_PASSWORD).Split('|');

                if (DecryptedWords[0].Equals(""))  // This means the file was tampered.
                {
                    MessageBox.Show("The score file was tampered. Any scores saved by the player will be lost.");
                    File.Delete(FILE_NAME_FOR_SCORES);
                    return null;
                }

                for (int i = 0; i < DecryptedWords.GetUpperBound(0); i += 4)
                {
                    ScoreEntity Score = new ScoreEntity();
                    Score.Category = DecryptedWords[i];
                    Score.Scorer = DecryptedWords[i + 1];
                    Score.Score = Convert.ToInt16(DecryptedWords[i + 2]);
                    Score.ScoreTime = Convert.ToDateTime(DecryptedWords[i + 3]);
                    ScoreList.Add(Score);
                }
                return ScoreList;
            }
            catch (Exception Ex)
            {
                MessageBox.Show("An error occurred in 'ReadScoresFromFile' method of 'GameBoard' form. Error msg: " + Ex.Message);
                return ScoreList;
            }
        }

        private static void PipeTheScores(StringBuilder PipedScores, ScoreEntity ScoreIter)
        {
            PipedScores.Append(ScoreIter.Category);
            PipedScores.Append("|");
            PipedScores.Append(ScoreIter.Scorer);
            PipedScores.Append("|");
            PipedScores.Append(ScoreIter.Score);
            PipedScores.Append("|");
            PipedScores.Append(ScoreIter.ScoreTime);
            PipedScores.Append("|");
        }

        private bool GetScorerName(ref string ScorerName)
        {
            ScorerName = Interaction.InputBox("Congratulations, you are a top scorer. Please enter your name: ", "Word Puzzle", "", -1, -1);
            bool OptsOutSaving = false;

            do  // Loop until the player enters a name or s/he opts for not saving the score.
            {
                if (string.IsNullOrEmpty(ScorerName))
                {
                    DialogResult Result = MessageBox.Show("No name was given. Score will not be saved. Are you sure?", "Word Puzzle", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (Result == DialogResult.Yes)
                    {
                        OptsOutSaving = true;
                        break;
                    }
                    else
                        ScorerName = Interaction.InputBox("Please enter your name: ", "Word Puzzle", "", -1, -1);
                }
                else break;
            }
            while (true);
            return OptsOutSaving;
        }
    }
}