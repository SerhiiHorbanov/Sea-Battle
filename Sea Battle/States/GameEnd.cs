using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sea_Battle.States
{
    internal class GameEnd : State
    {
        override public void Input()
        {
            Console.ReadKey();
            if (!isAnyoneWon)
                gameplayState = GameplayState.RestartingGame;
        }
    }
}
