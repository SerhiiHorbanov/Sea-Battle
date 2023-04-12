using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sea_Battle
{
    class SeaBattle
    {
        ShipMap firstPlayer;
        ShipMap secondPlayer;
        GameEndResult gameEndResult = GameEndResult.Draw;
        GameplayState gameplayState = GameplayState.P1PlacingShips;
        bool isSecondPlayerAI;
        private bool endedPlaying = false;
        public void Start()
        {
            SetupConsole();

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
            string[] words = input.Split(' ');
            if (words.Length == 2 && int.TryParse(words[0], out int y))
            {

            }
        }

        private void Update()
        {

        }

        private void Render()
        {

        }

        private void SetupConsole()
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
    }
}
