using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sea_Battle
{
    class SeaBattle
    {
        private ShipMap firstPlayerMap = new ShipMap(new bool[10,10]);
        private ShipMap secondPlayerMap;
        private GameEndResult gameEndResult = GameEndResult.Draw;
        private GameplayState gameplayState = GameplayState.P1PlacingShips;
        private bool isSecondPlayerAI;
        private bool endedPlaying = false;
        private int YInputCord;
        private int XInputCord;

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

        }

        private void Render()
        {

        }

    }

    struct ShipMap
    {
        private bool[,] shipMap;
        private bool[,] shotTilesMap;
        
        public ShipMap(bool[,] shipMap)
        {
            this.shipMap = shipMap;
            shotTilesMap = new bool[10, 10];
        }

        public void RenderMapForEnemy(StringBuilder stringBuilder)
        {
            stringBuilder.AppendLine(" 0123456789");
            for (int y = 0; y < 10; y++)
            {
                stringBuilder.Append('A' + y);
                for (int x = 0; x < 10; x++)
                {
                    char charToAdd = '#';
                    bool isShotTile = shotTilesMap[y, x];
                    bool isShipTile = shipMap[y, x];
                    if (isShotTile)
                    {
                        if (isShipTile)
                            charToAdd = 'X';
                        else
                            charToAdd = '~';
                    }
                    stringBuilder.Append(charToAdd);
                }
                stringBuilder.Append("\n");
            }
        }
    }
}
