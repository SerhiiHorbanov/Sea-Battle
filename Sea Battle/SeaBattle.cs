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
        private GameEndResult gameEndResult = GameEndResult.None;
        private GameplayState gameplayState = GameplayState.ChoosingGameMode;
        private bool endedPlaying = false;
        private bool isFirstPlayerAI = false;
        private bool isSecondPlayerAI = false;
        private int YInputCord = -1;
        private int XInputCord = -1;
        private int firstPlayerWins = 0;
        private int secondPlayerWins = 0;

        const int startShipCount = 2;
        const int winPointsCount = 3;

        private bool isCurrentPlayerAI 
            => (gameplayState == GameplayState.FirstPlayerMove && isFirstPlayerAI) || (gameplayState == GameplayState.SecondPlayerMove && isSecondPlayerAI);
        private bool isFirstPlayerWon
            => firstPlayerWins >= winPointsCount;
        private bool isSecondPlayerWon
            => secondPlayerWins >= winPointsCount;
        private bool isAnyoneWon
            => isFirstPlayerWon || isSecondPlayerWon;

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
        }

        private void Input()
        {
            switch (gameplayState)
            {
                case GameplayState.ChoosingGameMode:
                    ChoosingGameModeInput();
                    break;

                case GameplayState.FirstPlayerMove:
                case GameplayState.SecondPlayerMove:
                    if (!isCurrentPlayerAI)
                        MoveInput();
                    break;

                case GameplayState.GameEnd:
                    GameEndInput();
                    break;
            }
        }

        private void Update()
        {
            switch (gameplayState)
            {
                case GameplayState.FirstPlayerMove:
                case GameplayState.SecondPlayerMove:
                    if (isCurrentPlayerAI || XInputCord != -1)
                        UpdateMove();

                    break;
                case GameplayState.RestartingGame:
                    RestartGame();
                    break;

                case GameplayState.GameEnd:
                    EndGame();
                    break;
            }
        }

        private void Render()
        {
            switch (gameplayState)
            {
                case GameplayState.ChoosingGameMode:
                    RenderChoosingGameMode();
                    break;

                case GameplayState.FirstPlayerMove: 
                case GameplayState.SecondPlayerMove:
                    RenderInGame();
                    break;
                case GameplayState.GameEnd:
                    GameEndRender();
                    break;
            }
        }

        private void ChoosingGameModeInput()
        {
            string input = Console.ReadLine();
            char firstInputChar = input[0];
            char lastInputChar = input[input.Length - 1];
            isFirstPlayerAI = !(firstInputChar == 'P' || firstInputChar == 'p');
            isSecondPlayerAI = !(lastInputChar == 'P' || lastInputChar == 'p');
            gameplayState = GameplayState.FirstPlayerMove;
        }

        private void MoveInput()
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

        private void GameEndInput()
        {
            Console.ReadKey();
            if (!isAnyoneWon)
                gameplayState = GameplayState.RestartingGame;
        }

        private void UpdateMove()
        {
            ShipMap currentEnemyMap = gameplayState == GameplayState.FirstPlayerMove ? secondPlayerMap : firstPlayerMap;

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
                gameplayState = gameplayState == GameplayState.FirstPlayerMove ? GameplayState.SecondPlayerMove : GameplayState.FirstPlayerMove;
            }
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

        private void RenderInGame()
        {
            if (isCurrentPlayerAI)
                return;
            StringBuilder stringBuilder = new StringBuilder();

            (ShipMap currentPlayerMap, ShipMap currentEnemyMap) = gameplayState == GameplayState.FirstPlayerMove ? (firstPlayerMap, secondPlayerMap) : (secondPlayerMap, firstPlayerMap);

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

        private void RenderChoosingGameMode()
        {
            Console.Clear();
            Console.WriteLine("Choose game mode (PvP or PvE or EvP or EvE)");
        }
        
        private void GameEndRender()
        {
            Console.Clear();

            if (gameEndResult == GameEndResult.FirstPlayerWin)
                Console.WriteLine("first player won!");
            else if (gameEndResult == GameEndResult.SecondPlayerWin)
                Console.WriteLine("second player won!");

            if (!isAnyoneWon)
            {
                Console.WriteLine($"first player points: {firstPlayerWins}");
                Console.WriteLine($"second player points: {secondPlayerWins}");
                Console.WriteLine("press any key to play next round");
            }
            else
                Console.WriteLine("press any key to close the game");
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

        private void CheckGameEnd()
        {
            bool isFirstPlayerMoves = gameplayState == GameplayState.FirstPlayerMove;

            ShipMap mapToCheck = isFirstPlayerMoves ? secondPlayerMap : firstPlayerMap;

            bool isGameEnd = mapToCheck.IsLose();

            if (isGameEnd)
            {
                gameEndResult = isFirstPlayerMoves ? GameEndResult.FirstPlayerWin : GameEndResult.SecondPlayerWin;
                if (gameEndResult == GameEndResult.FirstPlayerWin)
                    firstPlayerWins++;
                else
                    secondPlayerWins++;
                gameplayState = GameplayState.GameEnd;
            }
        }

        private void RestartGame()
        {
            firstPlayerMap = new ShipMap(RandomMap());
            secondPlayerMap = new ShipMap(RandomMap());
            gameEndResult = GameEndResult.None;
            gameplayState = GameplayState.FirstPlayerMove;
            endedPlaying = false;
            YInputCord = -1;
            XInputCord = -1;
        }

        private void EndGame()
        {
            endedPlaying = true;
        }
    }
}
