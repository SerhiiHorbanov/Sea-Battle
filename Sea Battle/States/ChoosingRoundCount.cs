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
        int winPointsCount = 0;
        bool canParseInput;

        public ChoosingRoundCount(bool isFirstPlayerAI, bool isSecondPlayerAI)
        {
            this.isFirstPlayerAI = isFirstPlayerAI;
            this.isSecondPlayerAI = isSecondPlayerAI;
        }

        public override void Input()
        {
            string input = Console.ReadLine();
            if (int.TryParse(input, out winPointsCount))
            {
                canParseInput = true;
            }
        }

        override public void Update()
        {
            if (canParseInput)
                SeaBattle.SetState(new ChoosingNicknames(isFirstPlayerAI, isSecondPlayerAI, winPointsCount));
        }

        override public void Render()
        {
            Console.Clear();
            Console.Write("Choose number of points needed to win match\n>>>");
        }
    }
}
