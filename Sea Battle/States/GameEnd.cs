using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sea_Battle.States
{
    class GameEnd : State
    {
        private bool isFirstPlayerWon;
        private int firstPlayerWins;
        private int secondPlayerWins;

        public GameEnd(bool isFirstPlayerWon, int firstPlayerWins, int secondPlayerWins)
        {
            this.isFirstPlayerWon = isFirstPlayerWon;
            this.firstPlayerWins = firstPlayerWins;
            this.secondPlayerWins = secondPlayerWins;
        }

        override public void Input()
        {
            Console.ReadKey();
            SeaBattle.SetState(new ChoosingGameMode());
        }

        public override void Update()
        {
            
        }

        override public void Render()
        {
            Console.Clear();

            if (isFirstPlayerWon)
                Console.WriteLine("first player won!");
            else
                Console.WriteLine("second player won!");

            Console.WriteLine("press any key to play again");
        }
    }
}
