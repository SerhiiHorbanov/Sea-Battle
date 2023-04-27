using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sea_Battle.Enums;

namespace Sea_Battle.States
{
    class GameEnd : State
    {
        private GameEndResult gameEndResult;
        private int firstPlayerWins;
        private int secondPlayerWins;
        private int WinPointsCount;
        private bool isFirstPlayerAI;
        private bool isSecondPlayerAI;
        private ConsoleKey input;
        private bool isAnyoneWon
            => firstPlayerWins >= WinPointsCount || secondPlayerWins >= WinPointsCount;

        public GameEnd(GameEndResult gameEndResult, int firstPlayerWins, int secondPlayerWins, int winPointsCount, bool isFirstPlayerAI, bool isSecondPlayerAI)
        {
            this.gameEndResult = gameEndResult;
            this.firstPlayerWins = firstPlayerWins;
            this.secondPlayerWins = secondPlayerWins;
            this.WinPointsCount = winPointsCount;
            this.isFirstPlayerAI = isFirstPlayerAI;
            this.isSecondPlayerAI = isSecondPlayerAI;
        }

        override public void Input()
        {
            input = Console.ReadKey().Key;
        }

        public override void Update()
        {
            if (input == ConsoleKey.R)
            {
                SeaBattle.SetState(new ChoosingGameMode());
                return;
            }

            else if (!isAnyoneWon)
            {
                SeaBattle.SetState(new PlayingGame(isFirstPlayerAI, isSecondPlayerAI, firstPlayerWins, secondPlayerWins));
            }

            else
            {
                SeaBattle.endedPlaying = true;
            }

        }

        override public void Render()
        {
            Console.Clear();

            if (gameEndResult == GameEndResult.FirstPlayerWin)
                Console.WriteLine("first player won!");
            else
                Console.WriteLine("second player won!");

            Console.WriteLine($"first player wins: {firstPlayerWins}");
            Console.WriteLine($"second player wins: {secondPlayerWins}");

            if (!isAnyoneWon)
                Console.WriteLine("press any key to play again");
            Console.WriteLine("press R to play another match");
        }
    }
}
