using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.Text;
using System.Configuration;

namespace WordPuzzle.Classes
{
    public class GameEngine
    {
        CheatCode CheatCodeObj;
        public const int REMAINING_TIME_BONUS_FACTOR = 10;
        private const int PENALTY_SCORE = 50;
        private const int NUM_ORIENTATIONS = 4;                     // Orientations - we have four as listed below in the enum.
        private int MAX_WORDS = Convert.ToInt16(ConfigurationManager.AppSettings["MAX_WORDS"]);
        private int MAX_SECONDS;
        public List<string> WORDS_FOUND = new List<string>();      // These arrays are needed to store all the words that the player found.
        public List<WordPosition> WordPositions = new List<WordPosition>();
        private Random Rnd;
        private string CurrentCategory;
        private int GridSize;

        public string[] WORD_ARRAY { get; set; }                    // These arrays are needed to store all the available words.
        public char[,] WORDS_IN_BOARD;                             // This is a word matrix to mimic the board.
        public enum Direction { Down = 1, Right, DownLeft, DownRight, Up, Left, UpLeft, UpRight, None };
        List<Direction> ChosenDirections = new List<Direction>();
        public int CurrentScore { get; set; }
        public string StatusForDisplay { get; set; }
        public List<Point> ColouredRectangles = new List<Point>();
        public int RemainingTime;

        public GameEngine(string[] Words, string CurrentCategoryParam, int GridSizeParam, int MAX_SECONDS_PARAM, List<Direction> ChosenDirectionsParam)
        {
            ChosenDirections = ChosenDirectionsParam;
            WORD_ARRAY = Words;
            GridSize = GridSizeParam;
            MAX_SECONDS = MAX_SECONDS_PARAM;
            CurrentCategory = CurrentCategoryParam;
            CheatCodeObj = new CheatCode(MAX_WORDS);
        }

        public void InitializeBoard(int GridSize)
        {
            try
            {
                WORDS_IN_BOARD = new char[GridSize, GridSize];
                Rnd = new Random(DateTime.Now.Millisecond);
                Direction OrientationDecision;
                int PlacementIndex_X, PlacementIndex_Y;
                bool PlacementSuccessful;

                for (int i = 0; i <= WORD_ARRAY.GetUpperBound(0); i++)   // For all the words in the list, try to place them on the board.
                {
                    PlacementSuccessful = false;
                    do
                    {
                        // Total number of elements in an enum: http://stackoverflow.com/questions/856154/total-number-of-items-defined-in-an-enum
                        OrientationDecision = GetOrientation(Rnd, Enum.GetNames(typeof(Direction)).Length);   // From randomizer, orientation of the string to put should be either of the eight orientations.
                        PlacementIndex_X = GetRandomNumber(Rnd, GridSize);    // Get the X-coordinate for the string to place.
                        PlacementIndex_Y = GetRandomNumber(Rnd, GridSize);    // Get the Y-coordinate for the string to place.
                        PlacementSuccessful = PlaceTheWords(OrientationDecision, PlacementIndex_X, PlacementIndex_Y, WORD_ARRAY[i]);
                    }
                    while (!PlacementSuccessful);
                }
                FillInTheGaps();    // Fill the empty squares with random letters.
            }
            catch (Exception Ex)
            {
                MessageBox.Show("An error occurred in 'InitializeBoard' method of the 'GameEngine' class. Error msg: " + Ex.Message);
            }
        }

        private Direction GetOrientation(Random Rnd, int Max)
        {
            switch (Rnd.Next(1, Max))   // Generate a random number between 1 and Max - 1; So if Max = 9, it will generate a random direction between 1 and 8.
            {
                case 1: if (ChosenDirections.Find(p => p.Equals(Direction.Down)) == Direction.Down) return Direction.Down; break;
                case 2: if (ChosenDirections.Find(p => p.Equals(Direction.Right)) == Direction.Right) return Direction.Right; break;
                case 3: if (ChosenDirections.Find(p => p.Equals(Direction.DownLeft)) == Direction.DownLeft) return Direction.DownLeft; break;
                case 4: if (ChosenDirections.Find(p => p.Equals(Direction.DownRight)) == Direction.DownRight) return Direction.DownRight; break;
                case 5: if (ChosenDirections.Find(p => p.Equals(Direction.Up)) == Direction.Up) return Direction.Up; break;
                case 6: if (ChosenDirections.Find(p => p.Equals(Direction.Left)) == Direction.Left) return Direction.Left; break;
                case 7: if (ChosenDirections.Find(p => p.Equals(Direction.UpLeft)) == Direction.UpLeft) return Direction.UpLeft; break;
                case 8: if (ChosenDirections.Find(p => p.Equals(Direction.UpRight)) == Direction.UpRight) return Direction.UpRight; break;
                default: return Direction.None;
            }
            return Direction.None;
        }

        private int GetRandomNumber(Random Rnd, int Max)
        {
            return Rnd.Next(Max);   // Generates a number from 0 up to the grid size.
        }

        private bool PlaceTheWords(Direction OrientationDecision, int PlacementIndex_X, int PlacementIndex_Y, string Word)
        {
            try
            {
                bool PlaceAvailable = true;

                switch (OrientationDecision)
                {
                    case Direction.Down:
                        for (int i = 0, j = PlacementIndex_Y; i < Word.Length; i++, j++)               // First we check if the word can be placed in the array. For this it needs blanks there.
                        {
                            if (j >= GridSize) return false; // Falling outside the grid. Hence placement unavailable.
                            if (WORDS_IN_BOARD[PlacementIndex_X, j] != '\0')
                                if (WORDS_IN_BOARD[PlacementIndex_X, j] != Word[i])   // If there is an overlap, then we see if the characters match. If matches, then it can still go there.
                                {
                                    PlaceAvailable = false;
                                    break;
                                }
                        }
                        if (PlaceAvailable)
                        {   // If all the cells are blank, or a non-conflicting overlap is available, then this word can be placed there. So place it.
                            for (int i = 0, j = PlacementIndex_Y; i < Word.Length; i++, j++)
                                WORDS_IN_BOARD[PlacementIndex_X, j] = Word[i];
                            StoreWordPosition(Word, PlacementIndex_X, PlacementIndex_Y, OrientationDecision);
                            return true;
                        }
                        break;
                    case Direction.Up:
                        for (int i = 0, j = PlacementIndex_Y; i < Word.Length; i++, j--)               // First we check if the word can be placed in the array. For this it needs blanks there.
                        {
                            if (j < 0) return false; // Falling outside the grid. Hence placement unavailable.
                            if (WORDS_IN_BOARD[PlacementIndex_X, j] != '\0')
                                if (WORDS_IN_BOARD[PlacementIndex_X, j] != Word[i])   // If there is an overlap, then we see if the characters match. If matches, then it can still go there.
                                {
                                    PlaceAvailable = false;
                                    break;
                                }
                        }
                        if (PlaceAvailable)
                        {   // If all the cells are blank, or a non-conflicting overlap is available, then this word can be placed there. So place it.
                            for (int i = 0, j = PlacementIndex_Y; i < Word.Length; i++, j--)
                                WORDS_IN_BOARD[PlacementIndex_X, j] = Word[i];
                            StoreWordPosition(Word, PlacementIndex_X, PlacementIndex_Y, OrientationDecision);
                            return true;
                        }
                        break;
                    case Direction.Right:
                        for (int i = 0, j = PlacementIndex_X; i < Word.Length; i++, j++)               // First we check if the word can be placed in the array. For this it needs blanks there.
                        {
                            if (j >= GridSize) return false; // Falling outside the grid. Hence placement unavailable.
                            if (WORDS_IN_BOARD[j, PlacementIndex_Y] != '\0')
                                if (WORDS_IN_BOARD[j, PlacementIndex_Y] != Word[i])   // If there is an overlap, then we see if the characters match. If matches, then it can still go there.
                                {
                                    PlaceAvailable = false;
                                    break;
                                }
                        }
                        if (PlaceAvailable)
                        {   // If all the cells are blank, or a non-conflicting overlap is available, then this word can be placed there. So place it.
                            for (int i = 0, j = PlacementIndex_X; i < Word.Length; i++, j++)
                                WORDS_IN_BOARD[j, PlacementIndex_Y] = Word[i];
                            StoreWordPosition(Word, PlacementIndex_X, PlacementIndex_Y, OrientationDecision);
                            return true;
                        }
                        break;
                    case Direction.Left:
                        for (int i = 0, j = PlacementIndex_X; i < Word.Length; i++, j--)               // First we check if the word can be placed in the array. For this it needs blanks there.
                        {
                            if (j < 0) return false; // Falling outside the grid. Hence placement unavailable.
                            if (WORDS_IN_BOARD[j, PlacementIndex_Y] != '\0')
                                if (WORDS_IN_BOARD[j, PlacementIndex_Y] != Word[i])   // If there is an overlap, then we see if the characters match. If matches, then it can still go there.
                                {
                                    PlaceAvailable = false;
                                    break;
                                }
                        }
                        if (PlaceAvailable)
                        {   // If all the cells are blank, or a non-conflicting overlap is available, then this word can be placed there. So place it.
                            for (int i = 0, j = PlacementIndex_X; i < Word.Length; i++, j--)
                                WORDS_IN_BOARD[j, PlacementIndex_Y] = Word[i];
                            StoreWordPosition(Word, PlacementIndex_X, PlacementIndex_Y, OrientationDecision);
                            return true;
                        }
                        break;
                    case Direction.DownLeft:
                        for (int i = 0, j = PlacementIndex_X, k = PlacementIndex_Y; i < Word.Length; i++, j++, k--)  // First we check if the word can be placed in the array. For this it needs blanks there.
                        {
                            if (j >= GridSize || k < 0) return false;   // Falling outside the grid. Hence placement unavailable.
                            if (WORDS_IN_BOARD[k, j] != '\0')
                                if (WORDS_IN_BOARD[k, j] != Word[i])   // If there is an overlap, then we see if the characters match. If matches, then it can still go there.
                                {
                                    PlaceAvailable = false;
                                    break;
                                }
                        }
                        if (PlaceAvailable)
                        {   // If all the cells are blank, then this word can be placed there. So place it.
                            for (int i = 0, j = PlacementIndex_X, k = PlacementIndex_Y; i < Word.Length; i++, j++, k--)
                                WORDS_IN_BOARD[k, j] = Word[i];
                            StoreWordPosition(Word, PlacementIndex_X, PlacementIndex_Y, OrientationDecision);
                            return true;
                        }
                        break;
                    case Direction.UpLeft:
                        for (int i = 0, j = PlacementIndex_X, k = PlacementIndex_Y; i < Word.Length; i++, j--, k--)  // First we check if the word can be placed in the array. For this it needs blanks there.
                        {
                            if (j < 0 || k < 0) return false;   // Falling outside the grid. Hence placement unavailable.
                            if (WORDS_IN_BOARD[k, j] != '\0')
                                if (WORDS_IN_BOARD[k, j] != Word[i])   // If there is an overlap, then we see if the characters match. If matches, then it can still go there.
                                {
                                    PlaceAvailable = false;
                                    break;
                                }
                        }
                        if (PlaceAvailable)
                        {   // If all the cells are blank, then this word can be placed there. So place it.
                            for (int i = 0, j = PlacementIndex_X, k = PlacementIndex_Y; i < Word.Length; i++, j--, k--)
                                WORDS_IN_BOARD[k, j] = Word[i];
                            StoreWordPosition(Word, PlacementIndex_X, PlacementIndex_Y, OrientationDecision);
                            return true;
                        }
                        break;
                    case Direction.DownRight:
                        for (int i = 0, j = PlacementIndex_X, k = PlacementIndex_Y; i < Word.Length; i++, j++, k++)  // First we check if the word can be placed in the array. For this it needs blanks there.
                        {
                            if (j >= GridSize || k >= GridSize) return false;   // Falling outside the grid. Hence placement unavailable.
                            if (WORDS_IN_BOARD[j, k] != '\0')
                                if (WORDS_IN_BOARD[j, k] != Word[i])   // If there is an overlap, then we see if the characters match. If matches, then it can still go there.
                                {
                                    PlaceAvailable = false;
                                    break;
                                }
                        }
                        if (PlaceAvailable)
                        {   // If all the cells are blank, then this word can be placed there. So place it.
                            for (int i = 0, j = PlacementIndex_X, k = PlacementIndex_Y; i < Word.Length; i++, j++, k++)
                                WORDS_IN_BOARD[j, k] = Word[i];
                            StoreWordPosition(Word, PlacementIndex_X, PlacementIndex_Y, OrientationDecision);
                            return true;
                        }
                        break;
                    case Direction.UpRight:
                        for (int i = 0, j = PlacementIndex_X, k = PlacementIndex_Y; i < Word.Length; i++, j++, k--)  // First we check if the word can be placed in the array. For this it needs blanks there.
                        {
                            if (j >= GridSize || k < 0) return false;   // Falling outside the grid. Hence placement unavailable.
                            if (WORDS_IN_BOARD[j, k] != '\0')
                                if (WORDS_IN_BOARD[j, k] != Word[i])   // If there is an overlap, then we see if the characters match. If matches, then it can still go there.
                                {
                                    PlaceAvailable = false;
                                    break;
                                }
                        }
                        if (PlaceAvailable)
                        {   // If all the cells are blank, then this word can be placed there. So place it.
                            for (int i = 0, j = PlacementIndex_X, k = PlacementIndex_Y; i < Word.Length; i++, j++, k--)
                                WORDS_IN_BOARD[j, k] = Word[i];
                            StoreWordPosition(Word, PlacementIndex_X, PlacementIndex_Y, OrientationDecision);
                            return true;
                        }
                        break;
                }
                return false;   // Otherwise continue with a different place and index.
            }
            catch (Exception Ex)
            {
                MessageBox.Show("An error occurred in 'PlaceTheWords' method of the 'GameEngine' class. Error msg: " + Ex.Message);
                return false;   // Otherwise continue with a different place and index.
            }
        }

        private void FillInTheGaps()
        {
            try
            {
                for (int i = 0; i < GridSize; i++)
                    for (int j = 0; j < GridSize; j++)
                        if (WORDS_IN_BOARD[i, j] == '\0')
                            WORDS_IN_BOARD[i, j] = (char)(65 + GetRandomNumber(Rnd, 26));
            }
            catch (Exception Ex)
            {
                MessageBox.Show("An error occurred in 'FillInTheGaps' method of the 'GameEngine' class. Error msg: " + Ex.Message);
            }
        }

        private void StoreWordPosition(string Word, int PlacementIndex_X, int PlacementIndex_Y, Direction OrientationDecision)
        {
            WordPosition Pos = new WordPosition();
            Pos.Word = Word;
            Pos.PlacementIndex_X = PlacementIndex_X;
            Pos.PlacementIndex_Y = PlacementIndex_Y;
            Pos.Direction = OrientationDecision;

            switch (OrientationDecision)
            {
                case Direction.Down: Pos.ScoreAugmenter = 10; break;
                case Direction.Up: Pos.ScoreAugmenter = 20; break;
                case Direction.Right: Pos.ScoreAugmenter = 10; break;
                case Direction.Left: Pos.ScoreAugmenter = 20; break;
                case Direction.DownLeft: Pos.ScoreAugmenter = 20; break;
                case Direction.DownRight: Pos.ScoreAugmenter = 20; break;
                case Direction.UpLeft: Pos.ScoreAugmenter = 30; break;
                case Direction.UpRight: Pos.ScoreAugmenter = 30; break;
                case Direction.None: Pos.ScoreAugmenter = 0; break;
            }
            WordPositions.Add(Pos);
        }

        internal void ObtainFailedWords(ref List<string> FailedWords)
        {
            foreach (string Word in WORD_ARRAY)
                if (WORDS_FOUND.IndexOf(Word) == -1)
                    FailedWords.Add(Word);
        }

        internal WordPosition ObtainFailedWordPosition(string WordParam)
        {
            return WordPositions.Find(p => p.Word.Equals(WordParam));
        }
        
        public void CheckValidityAndUpdateScore(Stack<Point> Points, int SizeFactor, ref string TheWordIntendedParam, int RemainingTimeParam)
        {
            try
            {
                RemainingTime = RemainingTimeParam;
                if (Points.Count == 1) return; // This was a doble click, no dragging, hence return.
                int StartX = Points.ToArray()[1].X / SizeFactor;    // Retrieve the starting position of the line.
                int StartY = Points.ToArray()[1].Y / SizeFactor;

                int EndX = Points.ToArray()[0].X / SizeFactor;      // Retrieve the ending position of the line.
                int EndY = Points.ToArray()[0].Y / SizeFactor;

                if (StartX > GridSize || EndX > GridSize || StartY > GridSize || EndY > GridSize || // Boundary checks.
                    StartX <= 0 || EndX <= 0 || StartY <= 0 || EndY <= 0)
                    StatusForDisplay ="Nope!";

                StringBuilder TheWordIntended = new StringBuilder();
                List<Point> TempRectangles = new List<Point>();
                TheWordIntended.Clear();
                if (StartX < EndX && StartY == EndY) // Right line drawn.
                    for (int i = StartX; i <= EndX; i++)
                    {
                        TheWordIntended.Append(WORDS_IN_BOARD[i - 1, StartY - 1].ToString());
                        TempRectangles.Add(new Point(i * SizeFactor, StartY * SizeFactor));
                    }
                else if (StartX > EndX && StartY == EndY) // Left line drawn.
                    for (int i = StartX; i >= EndX; i--)
                    {
                        TheWordIntended.Append(WORDS_IN_BOARD[i - 1, StartY - 1].ToString());
                        TempRectangles.Add(new Point(i * SizeFactor, StartY * SizeFactor));
                    }
                else if (StartX == EndX && StartY < EndY) // Down line drawn.
                    for (int i = StartY; i <= EndY; i++)
                    {
                        TheWordIntended.Append(WORDS_IN_BOARD[StartX - 1, i - 1].ToString());
                        TempRectangles.Add(new Point(StartX * SizeFactor, i * SizeFactor));
                    }
                else if (StartX == EndX && StartY > EndY) // Up line drawn.
                    for (int i = StartY; i >= EndY; i--)
                    {
                        TheWordIntended.Append(WORDS_IN_BOARD[StartX - 1, i - 1].ToString());
                        TempRectangles.Add(new Point(StartX * SizeFactor, i * SizeFactor));
                    }
                else if (StartX > EndX && EndY > StartY) // Down Left line drawn.
                    for (int i = StartX, j = StartY; i >= EndX; i--, j++)
                    {
                        TheWordIntended.Append(WORDS_IN_BOARD[i - 1, j - 1].ToString());
                        TempRectangles.Add(new Point(i * SizeFactor, j * SizeFactor));
                    }
                else if (StartX > EndX && EndY < StartY) // Up Left line drawn.
                    for (int i = StartX, j = StartY; i >= EndX; i--, j--)
                    {
                        TheWordIntended.Append(WORDS_IN_BOARD[i - 1, j - 1].ToString());
                        TempRectangles.Add(new Point(i * SizeFactor, j * SizeFactor));
                    }
                else if (EndX > StartX && EndY > StartY) // Down Right line drawn.
                    for (int i = StartX, j = StartY; i <= EndX; i++, j++)
                    {
                        TheWordIntended.Append(WORDS_IN_BOARD[i - 1, j - 1].ToString());
                        TempRectangles.Add(new Point(i * SizeFactor, j * SizeFactor));
                    }
                else if (EndX > StartX && EndY < StartY) // Up Right line drawn.
                    for (int i = StartX, j = StartY; i <= EndX; i++, j--)
                    {
                        TheWordIntended.Append(WORDS_IN_BOARD[i - 1, j - 1].ToString());
                        TempRectangles.Add(new Point(i * SizeFactor, j * SizeFactor));
                    }

                if (FoundAWord(TheWordIntended.ToString())) // Checking if the word the player made is available in the words list.
                {
                    if (WORDS_FOUND.FindIndex(p => p.Equals(TheWordIntended.ToString())) == -1)  // Checking if the word was already found before or it is a new finding. It is -1 when the word is not found.
                    {
                        WORDS_FOUND.Add(TheWordIntended.ToString()); // If it is a new finding, then add the word in the 'found' list.
                        TheWordIntendedParam = TheWordIntended.ToString();
                        AddCoordinates(TempRectangles);
                        UpdateScore(TheWordIntended.ToString(), RemainingTimeParam);
                        StatusForDisplay = "Bingo!!!";
                    }
                    else
                        StatusForDisplay = "Already found! Why wasting time?";
                }
                else  // This is not a legitimate word in the word list.
                    StatusForDisplay = "Nope!";
            }
            catch (Exception Ex)
            {
                MessageBox.Show("An error occurred in 'CheckValidityAndUpdateScore' method of the 'GameEngine' class. Error msg: " + Ex.Message);
            }
        }

        private void UpdateScore(string TheWordIntended, int TimeLeft)
        {
            //CurrentScore += TheWordIntended.Length * 10;
            CurrentScore += (TheWordIntended.Length * WordPositions.Find(p => p.Word.Equals(TheWordIntended)).ScoreAugmenter);
            if (WORDS_FOUND.Count == MAX_WORDS)  // Found all the words; add the remaining time bonus.
            {
                CurrentScore += (TimeLeft * REMAINING_TIME_BONUS_FACTOR);   // Remaining seconds are added as bonus.
                StatusForDisplay = "Found All! Congrats!!";
            }
        }

        internal bool SavedTopScoreSuccessfully()
        {
            TopScore Score = new TopScore();
            Score.CurrentCategory = CurrentCategory;
            Score.CurrentScore = CurrentScore;
            return Score.CheckAndSaveIfTopScore();
        }

        private void AddCoordinates(List<Point> TempRectangles)
        {
            foreach (Point Pt in TempRectangles)
                if (ColouredRectangles.IndexOf(Pt) == -1)   // If the point was not added in the coloured rectangles' list, then add it.
                    ColouredRectangles.Add(Pt);
        }

        private bool FoundAWord(string TheWordIntended)
        {
            foreach (string Str in WORD_ARRAY)
                if (TheWordIntended.Equals(Str))
                    return true;
            return false;
        }

        internal bool FoundAllWords()
        {
            return WORDS_FOUND.Count == MAX_WORDS;
        }

        internal bool CheckCheat(string EnteredCheatKey, out CheatCode.CHEAT_TYPE CheatType)
        {
            bool Cheated = CheatCodeObj.CheckCheat(EnteredCheatKey, WORDS_FOUND.Count, out CheatType);
            if (Cheated)
            {   // Apply penalty on cheating.
                CurrentScore -= PENALTY_SCORE;
                StatusForDisplay = "Cheated! Penalised -" + PENALTY_SCORE.ToString() + "!!";
            }
            return Cheated;
        }
    }
}
