using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sea_Battle.States
{
    internal class ChoosingGameMode : State
    {
        bool isFirstPlayerAI;
        bool isSecondPlayerAI;
        public override void Input()
        {
            string input = Console.ReadLine();
            char firstInputChar = input[0];
            char lastInputChar = input[input.Length - 1];
            isFirstPlayerAI = !(firstInputChar == 'P' || firstInputChar == 'p');
            isSecondPlayerAI = !(lastInputChar == 'P' || lastInputChar == 'p');
        }

        public override void Update()
        {
            SeaBattle.SetState(new ChoosingRoundCount(isFirstPlayerAI, isSecondPlayerAI));
        }

        public override void Render()
        {
            Console.Clear();
            Console.Write("Choose game mode (PvP or PvE or EvP or EvE)\n>>>");
        }
    }
}
