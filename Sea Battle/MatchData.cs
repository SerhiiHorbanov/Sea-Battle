using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sea_Battle
{
    struct MatchData
    {
        public ProfileData firstPlayerProfile { get; private set; }
        public ProfileData secondPlayerProfile { get; private set; }
        public bool isFirstPlayerAI { get; private set; }
        public bool isSecondPlayerAI { get; private set; }
        public int winPointsCount { get; private set; }
        public int firstPlayerWins { get; private set; }
        public int secondPlayerWins { get; private set; }

        public MatchData(ProfileData firstPlayerProfile, ProfileData secondPlayerProfile, bool isFirstPlayerAI = false, bool isSecondPlayerAI = false, int winPointsCount = 1, int firstPlayerWins = 0, int secondPlayerWins = 0)
        {
            this.firstPlayerProfile = firstPlayerProfile;
            this.secondPlayerProfile = secondPlayerProfile;
            this.isFirstPlayerAI = isFirstPlayerAI;
            this.isSecondPlayerAI = isSecondPlayerAI;
            this.winPointsCount = winPointsCount;
            this.firstPlayerWins = firstPlayerWins;
            this.secondPlayerWins = secondPlayerWins;
        }

        public void FirstPlayerWon()
        {
            firstPlayerWins++;

            firstPlayerProfile.WinRound();
            secondPlayerProfile.LoseRound();

            if (firstPlayerWins >= winPointsCount)
            {
                firstPlayerProfile.WinMatch();
                secondPlayerProfile.LoseMatch();
            }
        }

        public void SecondPlayerWon()
        {
            secondPlayerWins++;

            secondPlayerProfile.WinRound();
            firstPlayerProfile.LoseRound();

            if (secondPlayerWins >= winPointsCount)
            {
                secondPlayerProfile.WinMatch();
                firstPlayerProfile.LoseMatch();
            }
        }
    }
}
