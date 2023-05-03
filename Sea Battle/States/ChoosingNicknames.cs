using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Sea_Battle.States
{
    internal class ChoosingNicknames : State
    {
        private ProfileData firstPlayerProfile;
        private ProfileData secondPlayerProfile;
        bool isFirstPlayerChoosingNickname = true;
        bool isFirstPlayerAI;
        bool isSecondPlayerAI;
        int winPointsCount;

        public ChoosingNicknames(bool isFirstPlayerAI, bool isSecondPlayerAI, int winPointsCount)
        {
            this.isFirstPlayerAI = isFirstPlayerAI;
            this.isSecondPlayerAI = isSecondPlayerAI;
            this.winPointsCount = winPointsCount;
        }

        public override void Input()
        {
            string input = Console.ReadLine();
            string path = ProfileData.PathForProfileWithNickname(input);

            if (!File.Exists(path))
                ProfileData.SaveProfile(new ProfileData(input));

            if (isFirstPlayerChoosingNickname)
                firstPlayerProfile = ProfileData.LoadProfile(path);
            else
                secondPlayerProfile = ProfileData.LoadProfile(path);
        }

        override public void Update()
        {
            if (!isFirstPlayerChoosingNickname)
            {
                SeaBattle.SetState(new PlayingGame(new MatchData(firstPlayerProfile, secondPlayerProfile, isFirstPlayerAI, isSecondPlayerAI, winPointsCount)));
                return;
            }
            isFirstPlayerChoosingNickname = false;
        }

        override public void Render()
        {
            Console.Clear();
            Console.Write("Enter your nickname\n>>>");
        }
    }
}
