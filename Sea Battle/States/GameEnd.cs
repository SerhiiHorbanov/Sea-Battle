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
        private MatchData matchData;
        private ConsoleKey input;
        private bool isAnyoneWon
            => matchData.firstPlayerWins >= matchData.winPointsCount || matchData.secondPlayerWins >= matchData.winPointsCount;

        public GameEnd(MatchData matchData, GameEndResult gameEndResult)
        {
            this.matchData = matchData;
            this.gameEndResult = gameEndResult;
        }

        public override void Input()
        {
            input = Console.ReadKey().Key;
        }

        public override void Update()
        {

            if (input == ConsoleKey.R)
            {
                SeaBattle.SetState(new PlayingGame(MatchData.standartData));
                return;
            }

            else if (!isAnyoneWon)
            {
                SeaBattle.SetState(new PlayingGame(matchData));
            }

            else
            {
                SeaBattle.endedPlaying = true;
            }

        }

        public override void Render()
        {
            Console.Clear();

            if (gameEndResult == GameEndResult.FirstPlayerWin)
                Console.WriteLine("first player won!");
            else
                Console.WriteLine("second player won!");

            if (!isAnyoneWon)
                Console.WriteLine("press any key to play again");
            Console.WriteLine("press R to play another match");
        }
    }
}
