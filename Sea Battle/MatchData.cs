using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sea_Battle
{
    struct MatchData
    {
        private ProfileData firstPlayerProfile;
        private ProfileData secondPlayerProfile;
        private bool isFirstPlayerAI;
        private bool isSecondPlayerAI;
        private int winPointsCount;
        private int firstPlayerWins;
        private int secondPlayerWins;
    }
}
