using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sea_Battle.Enums;

namespace Sea_Battle.States
{
    class PlayingGame : State
    {
        private ShipMap firstPlayerMap = ShipMap.RandomMap(startShipCount);
        private ShipMap secondPlayerMap = ShipMap.RandomMap(startShipCount);
        private GameEndResult gameEndResult = GameEndResult.None;
        private bool isFirstPlayerAI = false;
        private bool isSecondPlayerAI = false;
        private bool isFirstPlayerMoves = true;
        private int YInputCord = -1;
        private int XInputCord = -1;
        const int startShipCount = 2;
        static public int firstPlayerWins { get; private set; }
        static public int secondPlayerWins { get; private set; }

        private bool isCurrentPlayerAI
            => isFirstPlayerMoves ? isFirstPlayerAI : isSecondPlayerAI;

        public PlayingGame(bool isFirstPlayerAI, bool isSecondPlayerAI, int _firstPlayerWins = 0, int _secondPlayerWins = 0)
        {
            this.isFirstPlayerAI = isFirstPlayerAI;
            this.isSecondPlayerAI = isSecondPlayerAI;
            firstPlayerWins = _firstPlayerWins;
            secondPlayerWins = _secondPlayerWins;
        }

        override public void Input()
        {
            if (isCurrentPlayerAI)
                return;
            string input = Console.ReadLine();

            if (input.Length == 2 || input.Length == 3)
            {
                char firstInputChar = input[0];
                char lastInputChar = input[input.Length - 1];

                bool isFirstYCord = (firstInputChar <= 'J' && firstInputChar >= 'A');
                bool isLastYCord = (lastInputChar <= 'J' && lastInputChar >= 'A');
                bool isFirstXCord = (firstInputChar <= '9' && firstInputChar >= '0');
                bool isLastXCord = (lastInputChar <= '9' && lastInputChar >= '0');
                if ((isFirstYCord || isLastYCord) && (isFirstXCord || isLastXCord))
                {
                    if (isFirstYCord)
                    {
                        YInputCord = firstInputChar - 'A';
                        XInputCord = lastInputChar - '0';
                        return;
                    }
                    XInputCord = firstInputChar - '0';
                    YInputCord = lastInputChar - 'A';
                    return;
                }
                XInputCord = -1;
            }
        }

        override public void Update()
        {
            ShipMap currentEnemyMap = isFirstPlayerMoves ? secondPlayerMap : firstPlayerMap;

            int shootX = XInputCord;
            int shootY = YInputCord;
            if (isCurrentPlayerAI)
            {
                AIChooseWhereToShoot(currentEnemyMap, out shootX, out shootY);
            }

            currentEnemyMap.ShootTile(shootX, shootY);

            if (currentEnemyMap.shipMap[shootY, shootX])
            {
                CheckGameEnd();
            }
            else
            {
                isFirstPlayerMoves = !isFirstPlayerMoves;
            }
        }

        override public void Render()
        {
            if (isCurrentPlayerAI)
                return;
            StringBuilder stringBuilder = new StringBuilder();

            (ShipMap currentPlayerMap, ShipMap currentEnemyMap) = isFirstPlayerMoves ? (firstPlayerMap, secondPlayerMap) : (secondPlayerMap, firstPlayerMap);

            stringBuilder.Append("you are player ");
            stringBuilder.Append(isFirstPlayerMoves ? "1" : "2");
            stringBuilder.Append("\n");

            stringBuilder.Append("this is your map:\n");
            currentPlayerMap.RenderMap(stringBuilder, true);

            stringBuilder.Append("this is your enemy's map:\n");
            currentEnemyMap.RenderMap(stringBuilder, false);

            Console.Clear();
            Console.WriteLine(stringBuilder.ToString());
        }

        private void AIChooseWhereToShoot(ShipMap currentEnemyMap, out int shootX, out int shootY)
        {
            shootX = Program.random.Next(0, 10);
            shootY = Program.random.Next(0, 10);
            int unshotTiles = 0;
            foreach (bool isShot in currentEnemyMap.shotTilesMap)
            {
                if (!isShot)
                    unshotTiles++;
            }
            int shootIndexOutOfUnshotTiles = Program.random.Next(0, unshotTiles);
            for (int y = 0; y < 10; y++)
            {
                for (int x = 0; x < 10; x++)
                {
                    if (!currentEnemyMap.shotTilesMap[y, x])
                        shootIndexOutOfUnshotTiles--;
                    if (shootIndexOutOfUnshotTiles == 0)
                    {
                        shootX = x;
                        break;
                    }
                }
                if (shootIndexOutOfUnshotTiles == 0)
                {
                    shootY = y;
                    break;
                }
            }
        }

        private void CheckGameEnd()
        {
            ShipMap mapToCheck = isFirstPlayerMoves ? secondPlayerMap : firstPlayerMap;

            bool isGameEnd = mapToCheck.IsLose();

            if (isGameEnd)
            {
                gameEndResult = isFirstPlayerMoves ? GameEndResult.FirstPlayerWin : GameEndResult.SecondPlayerWin;
                if (gameEndResult == GameEndResult.FirstPlayerWin)
                    firstPlayerWins++;
                else
                    secondPlayerWins++;
                SeaBattle.SetState(new GameEnd(gameEndResult, firstPlayerWins, secondPlayerWins, isFirstPlayerAI, isSecondPlayerAI));
            }
        }
    }
}
