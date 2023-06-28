using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sea_Battle
{
    struct MatchData
    {
        public bool isFirstPlayerAI { get; private set; }
        public bool isSecondPlayerAI { get; private set; }
        public int winPointsCount { get; private set; }
        public int firstPlayerWins { get; private set; }
        public int secondPlayerWins { get; private set; }

        public static MatchData standartData = new MatchData(false, true, 3);

        public MatchData(bool isFirstPlayerAI = false, bool isSecondPlayerAI = false, int winPointsCount = 1, int firstPlayerWins = 0, int secondPlayerWins = 0)
        {
            this.isFirstPlayerAI = isFirstPlayerAI;
            this.isSecondPlayerAI = isSecondPlayerAI;
            this.winPointsCount = winPointsCount;
            this.firstPlayerWins = firstPlayerWins;
            this.secondPlayerWins = secondPlayerWins;
        }
    }
}
