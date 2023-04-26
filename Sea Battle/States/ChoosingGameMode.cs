using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sea_Battle.States
{
    internal class ChoosingGameMode : State
    {
        override public void Input()
        {
            string input = Console.ReadLine();
            char firstInputChar = input[0];
            char lastInputChar = input[input.Length - 1];
            bool isFirstPlayerAI = !(firstInputChar == 'P' || firstInputChar == 'p');
            bool  isSecondPlayerAI = !(lastInputChar == 'P' || lastInputChar == 'p');
            SeaBattle.SetState(new PlayersMove(isFirstPlayerAI, isSecondPlayerAI));
        }

        override public void Update()
        {

        }

        override public void Render()
        {
            Console.Clear();
            Console.WriteLine("Choose game mode (PvP or PvE or EvP or EvE)\n>>>");
        }
    }
}
