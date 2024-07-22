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
using System.Threading.Tasks;

namespace WordPuzzle.Classes
{
    class CountDownClock
    {
        // This integer variable keeps track of the 
        // remaining time.
        public int TimeLeft { get; set; }

        public CountDownClock(int MaxSeconds)
        {
            TimeLeft = MaxSeconds;
        }
        public void UpdateTimer()
        {
            TimeLeft--;
        }
        public bool TimeOut()
        {
            return TimeLeft == 0;
        }
    }
}
