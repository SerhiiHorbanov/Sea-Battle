using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        override public void Input()
        {
            string input = Console.ReadLine();
            if (isFirstPlayerChoosingNickname)
                firstPlayerProfile = ProfileData.LoadProfileFromFile(input + ".xml");
            else
                secondPlayerProfile = ProfileData.LoadProfileFromFile(input + ".xml");

        }

        override public void Update()
        {
            if (!isFirstPlayerChoosingNickname)
            {
                SeaBattle.SetState(new PlayingGame(firstPlayerProfile, secondPlayerProfile, isFirstPlayerAI, isSecondPlayerAI, winPointsCount));
                return;
            }
            isFirstPlayerChoosingNickname = false;
        }

        override public void Render()
        {
            Console.Clear();
            Console.Write("Ener your nickname\n>>>");
        }
    }
}
