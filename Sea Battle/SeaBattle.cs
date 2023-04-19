using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sea_Battle.Enums;

namespace Sea_Battle
{
    class SeaBattle
    {
        private ShipMap firstPlayerMap;
        private ShipMap secondPlayerMap;
        private GameEndResult gameEndResult = GameEndResult.Draw;
        private GameplayState gameplayState = GameplayState.FirstPlayerMove;
        private bool endedPlaying = false;
        private bool isFirstPlayerAI = false;
        private bool isSecondPlayerAI = true;
        private int YInputCord = -1;
        private int XInputCord = -1;
        
        const int startShipCount = 16;

        public void Start()
        {
            Console.Title = "Sea Battle";
            RandomizeMaps();

            while (!endedPlaying)
            {
                Render();
                Input();
                Update();
            }
            EndGame();
        }

        private void Input()
        {
            if (gameplayState == GameplayState.FirstPlayerMove && isFirstPlayerAI || gameplayState == GameplayState.SecondPlayerMove && isSecondPlayerAI)
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

        private void Update()
        {
            switch (gameplayState)
            {
                case GameplayState.FirstPlayerMove:
                case GameplayState.SecondPlayerMove:
                    if (XInputCord != -1)
                        UpdateMove();
                    break;
            }
        }

        private void Render()
        {
            switch (gameplayState)
            {
                case GameplayState.FirstPlayerMove: case GameplayState.SecondPlayerMove:
                    RenderInGame();
                    break;
            }
        }

        private void UpdateMove()
        {
            Console.WriteLine("update started");
            ShipMap currentEnemyMap = gameplayState == GameplayState.FirstPlayerMove ? firstPlayerMap : secondPlayerMap;
            
            bool isCurrentPlayerAI = gameplayState == (GameplayState.FirstPlayerMove && isFirstPlayerAI) || (gameplayState == GameplayState.SecondPlayerMove && isSecondPlayerAI);

            int shootX = XInputCord;
            int shootY = YInputCord;
            if (isCurrentPlayerAI)
            {
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
                            break;
                    }
                    if (shootIndexOutOfUnshotTiles == 0)
                        break;
                }
            }

            currentEnemyMap.ShootTile(shootX, shootY);

            gameplayState = gameplayState == GameplayState.FirstPlayerMove ? GameplayState.SecondPlayerMove : GameplayState.FirstPlayerMove;
            if (currentEnemyMap.shipMap[YInputCord, XInputCord])
            {
                CheckGameEnd();
                return;
            }
        }

        private void CheckGameEnd()
        {
            bool isFirstPlayerMoves = gameplayState == GameplayState.FirstPlayerMove;
            bool isGameEnd = true;

            ShipMap mapToCheck = isFirstPlayerMoves ? firstPlayerMap : secondPlayerMap;

            for (int y = 0; y < 10; y++)
            {
                for (int x = 0; x < 10; x++)
                {
                    bool isShotTile = mapToCheck.shotTilesMap[y, x];
                    bool isShipTile = mapToCheck.shipMap[y, x];
                    isGameEnd = isGameEnd && (!isShipTile || (isShipTile && isShotTile));
                }

                if (!isGameEnd)
                    break;
            }

            if (isGameEnd)
            {
                gameEndResult = isFirstPlayerMoves ? GameEndResult.SecondPlayerWin : GameEndResult.FirstPlayerWin;
                endedPlaying = isGameEnd;
            }

        }

        private void RenderInGame()
        {
            if (gameplayState == GameplayState.FirstPlayerMove && isFirstPlayerAI || gameplayState == GameplayState.SecondPlayerMove && isSecondPlayerAI)
                return;
            StringBuilder stringBuilder = new StringBuilder();

            (ShipMap currentPlayerMap, ShipMap currentEnemyMap) = gameplayState == GameplayState.FirstPlayerMove ? (secondPlayerMap, firstPlayerMap) : (firstPlayerMap, secondPlayerMap);

            stringBuilder.Append("you are player ");
            stringBuilder.Append(gameplayState == GameplayState.FirstPlayerMove ? "1" : "2");
            stringBuilder.Append("\n");

            stringBuilder.Append("this is your map:\n");
            currentPlayerMap.RenderMap(stringBuilder, true);

            stringBuilder.Append("this is your enemy's map:\n");
            currentEnemyMap.RenderMap(stringBuilder, false);

            Console.Clear();
            Console.WriteLine(stringBuilder.ToString());
        }

        private bool[,] RandomMap()
        {
            bool[,] map = new bool[10, 10];
            for (int i = 0; i < startShipCount; i++)
            {
                while (true)
                {
                    int x = Program.random.Next(0, 10);
                    int y = Program.random.Next(0, 10);
                    if (!map[y, x])
                    {
                        map[y, x] = true;
                        break;
                    }
                }
            }
            return map;
        }

        private void RandomizeMaps()
        {
            firstPlayerMap = new ShipMap(RandomMap());
            secondPlayerMap = new ShipMap(RandomMap());
        }

        private void EndGame()
        {
            Console.Clear();

            if (gameEndResult == GameEndResult.FirstPlayerWin)
                Console.WriteLine("first player won!");
            else if (gameEndResult == GameEndResult.SecondPlayerWin)
                Console.WriteLine("second player won!");

            Console.WriteLine("press any button to close the game");
            Console.ReadKey();
        }
    }
}
