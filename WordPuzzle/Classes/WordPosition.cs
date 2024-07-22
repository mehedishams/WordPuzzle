//////////////////////////////////////////////
//////////////////////////////////////////////
// Coded by Mehedi Shams Rony ////////////////
// September, October 2016 ///////////////////
//////////////////////////////////////////////
//////////////////////////////////////////////
namespace WordPuzzle.Classes
{
    public class WordPosition
    {
        public string Word { get; set; }
        public int PlacementIndex_X { get; set; }
        public int PlacementIndex_Y { get; set; }
        public GameEngine.Direction Direction { get; set; }
        public int ScoreAugmenter { get; set; }
    }
}