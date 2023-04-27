using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sea_Battle.States
{
    internal class ChoosingRoundCount : State
    {
        bool isFirstPlayerAI;
        bool isSecondPlayerAI;
        int matchCount = 0;
        bool canParseInput;

        public ChoosingRoundCount(bool isFirstPlayerAI, bool isSecondPlayerAI)
        {
            this.isFirstPlayerAI = isFirstPlayerAI;
            this.isSecondPlayerAI = isSecondPlayerAI;
        }

        override public void Input()
        {
            string input = Console.ReadLine();
            if (int.TryParse(input, out matchCount))
            {
                canParseInput = true;
            }
        }

        override public void Update()
        {
            if (canParseInput)
                SeaBattle.SetState(new PlayingGame(isFirstPlayerAI, isSecondPlayerAI, matchCount));
        }

        override public void Render()
        {
            Console.Clear();
            Console.Write("Choose number of points needed to win match\n>>>");
        }
    }
}
