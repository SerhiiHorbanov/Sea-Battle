﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sea_Battle.Enums;

namespace Sea_Battle
{
    class SeaBattle
    {
        private ShipMap firstPlayerMap = new ShipMap(new bool[10, 10]
            {
                {true, true, false, false, false, false, true, false, false, false},
                {true, false, false, false, false, false, false, false, false, false},
                {false, false, false, false, false, false, false, false, false, false},
                {false, false, false, false, false, false, false, false, false, false},
                {false, false, false, false, false, false, false, false, false, false},
                {false, false, false, false, false, false, false, false, false, false},
                {false, false, false, false, false, false, false, false, false, false},
                {false, false, false, false, false, false, false, false, false, false},
                {false, false, false, false, false, false, false, false, false, false},
                {false, false, false, false, false, false, false, false, false, false}
            });
        private ShipMap secondPlayerMap = new ShipMap(new bool[10, 10]
            {
                {true, true, false, false, false, false, false, false, false, false},
                {true, false, false, false, false, true, false, false, false, false},
                {false, false, false, false, true, false, true, false, false, false},
                {false, false, false, true, false, false, false, true, false, false},
                {false, false, false, false, false, false, true, false, false, false},
                {false, false, false, false, false, true, false, false, false, false},
                {false, false, false, false, true, false, false, false, false, false},
                {false, false, false, false, true, false, false, false, false, false},
                {false, false, false, false, true, true, true, true, false, false},
                {false, false, false, false, false, false, false, false, false, false}
            });
        private GameEndResult gameEndResult = GameEndResult.Draw;
        private GameplayState gameplayState = GameplayState.FirstPlayerMove;
        private bool endedPlaying = false;
        private int YInputCord = -1;
        private int XInputCord = -1;



        public void Start()
        {
            Console.Title = "Sea Battle";

            while (!endedPlaying)
            {
                Render();
                Input();
                Update();
            }

            Console.Clear();

            if (gameEndResult == GameEndResult.FirstPlayerWin)
                Console.WriteLine("first player won!");
            else if (gameEndResult == GameEndResult.SecondPlayerWin)
                Console.WriteLine("second player won!");

            Console.WriteLine("press any button to close the game");
            Console.ReadKey();
        }

        private void Input()
        {
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
                }
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
                    RenderMove();
                    break;
            }
            Console.WriteLine(XInputCord);
            Console.WriteLine(YInputCord);
        }

        private void UpdateMove()
        {
            ShipMap currentEnemyMap = gameplayState == GameplayState.FirstPlayerMove ? firstPlayerMap : secondPlayerMap;
            currentEnemyMap.ShootTile(XInputCord, YInputCord);
            
            if (currentEnemyMap.shipMap[YInputCord, XInputCord])
            {
                CheckGameEnd();
                return;
            }
            gameplayState = gameplayState == GameplayState.FirstPlayerMove ? GameplayState.SecondPlayerMove : GameplayState.FirstPlayerMove;
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

                if (isGameEnd)
                    break;
            }

            if (isGameEnd)
            {
                gameEndResult = isFirstPlayerMoves ? GameEndResult.SecondPlayerWin : GameEndResult.FirstPlayerWin;
                endedPlaying = isGameEnd;
            }

        }

        private void RenderMove()
        {
            StringBuilder stringBuilder = new StringBuilder();

            (ShipMap currentPlayerMap, ShipMap currentEnemyMap) = gameplayState == GameplayState.FirstPlayerMove ? (secondPlayerMap, firstPlayerMap) : (firstPlayerMap, secondPlayerMap);
            
            stringBuilder.Append("this is your map:\n");
            currentPlayerMap.RenderMap(stringBuilder, true);

            stringBuilder.Append("this is your enemy's map:\n");
            currentEnemyMap.RenderMap(stringBuilder, false);

            Console.Clear();
            Console.WriteLine(stringBuilder.ToString());
        }
    }
}
