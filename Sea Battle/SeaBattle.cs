using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sea_Battle.Enums;

namespace Sea_Battle
{
    class SeaBattle
    {
        private ShipMap firstPlayerMap = new ShipMap(new bool[10, 10]);
        private ShipMap secondPlayerMap = new ShipMap(new bool[10, 10]);
        private GameEndResult gameEndResult = GameEndResult.Draw;
        private GameplayState gameplayState = GameplayState.FirstPlayerMove;
        private bool isSecondPlayerAI;
        private bool endedPlaying = false;
        private int YInputCord;
        private int XInputCord;
        private bool isStreak;

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

            if (gameEndResult == GameEndResult.P1Win)
                Console.WriteLine("first player won!");
            else if (gameEndResult == GameEndResult.P2Win)
                Console.WriteLine("second player won!");

            Console.WriteLine("press any button to close the game");
            Console.ReadKey();
        }

        private void Input()
        {
            string input = Console.ReadLine();

            if (input.Length == 2 || input.Length == 3)
            {
                char firstInputChar = input[input.Length - 1];
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
                    RenderMove();
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
        }

        private void UpdateMove()
        {
            ShipMap currentEnemyMap = gameplayState == GameplayState.FirstPlayerMove ? secondPlayerMap : firstPlayerMap;
            currentEnemyMap.ShootTile(XInputCord, YInputCord);
            switch (currentEnemyMap.shipMap[YInputCord, XInputCord])
            {
                
            }
        }

        private void RenderMove()
        {
            StringBuilder stringBuilder = new StringBuilder();

            //ShipMap currentPlayerMap = gameplayState == GameplayState.FirstPlayerMove ? firstPlayerMap : secondPlayerMap;
            //ShipMap currentEnemyMap = gameplayState == GameplayState.FirstPlayerMove ? secondPlayerMap : firstPlayerMap;

            //спочатку я хотів зробити так як згори але зрозумів що так як нижче буде краще

            (ShipMap currentPlayerMap, ShipMap currentEnemyMap) = gameplayState == GameplayState.FirstPlayerMove ? (secondPlayerMap, firstPlayerMap) : (firstPlayerMap, secondPlayerMap);
            
            stringBuilder.Append("this is your map:\n");
            currentPlayerMap.RenderMap(stringBuilder, true);

            stringBuilder.Append("this is your enemy's map:\n");
            currentEnemyMap.RenderMap(stringBuilder, false);

            Console.WriteLine(stringBuilder.ToString());
        }
    }
}
