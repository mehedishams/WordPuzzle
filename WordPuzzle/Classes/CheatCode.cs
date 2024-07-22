using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordPuzzle.Classes
{
    class CheatCode
    {
        private const string CHEAT_CODE_FOR_INCREASING_TIME = "MAMBAZAMBA";
        private const string CHEAT_CODE_FOR_UNDISCOVERED_WORDS = "FLASH";

        private string CheatCodeForIncreasingTime = "";
        private string CheatCodeForFlashUndiscoveredWords = "";

        private int MAX_WORDS;

        public enum CHEAT_TYPE { INCREASE_TIME, FLASH_WORDS, NONE};

        public CheatCode(int MAX_WORDS_PARAM)
        {
            MAX_WORDS = MAX_WORDS_PARAM;
        }

        public bool CheckCheat(string CheatCode, int WordsFound, out CHEAT_TYPE CheatType)
        {
            CheatType = CHEAT_TYPE.NONE;
            CheatCodeForIncreasingTime += CheatCode;

            if (CHEAT_CODE_FOR_INCREASING_TIME.IndexOf(CheatCodeForIncreasingTime) == -1)    // Cheat code didn't match with any part of the cheat code starting from the first letter.
                CheatCodeForIncreasingTime = (CheatCode);                         // Hence erase it to start over.
            else if (CheatCodeForIncreasingTime.Equals(CHEAT_CODE_FOR_INCREASING_TIME) && WordsFound != MAX_WORDS)
            {
                CheatType = CHEAT_TYPE.INCREASE_TIME;
                return true;
            }

            CheatCodeForFlashUndiscoveredWords += CheatCode;
            if (CHEAT_CODE_FOR_UNDISCOVERED_WORDS.IndexOf(CheatCodeForFlashUndiscoveredWords) == -1)    // Cheat code didn't match with any part of the cheat code.
                CheatCodeForFlashUndiscoveredWords = (CheatCode);                         // Hence erase it to start over.
            else if (CheatCodeForFlashUndiscoveredWords.Equals(CHEAT_CODE_FOR_UNDISCOVERED_WORDS) && WordsFound != MAX_WORDS)
            {
                CheatType = CHEAT_TYPE.FLASH_WORDS;
                return true;
            }
            return false;
        }
    }
}
