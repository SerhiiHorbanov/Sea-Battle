using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sea_Battle.Enums;
using Sea_Battle.States;

namespace Sea_Battle
{
    class SeaBattle
    {
        const int winPointsCount = 3;
        static public bool endedPlaying = false;
        static public State currentState = new ChoosingGameMode();

        public void Start()
        {
            Console.Title = "Sea Battle";

            while (!endedPlaying)
            {
                Render();
                Input();
                Update();
            }
        }

        public static void SetState(State state)
        {
            currentState = state;
        }//я спочатку хотів зробити як тут нижче, але так не можна

        /*public void SetState<State>()
        {
            currentState = new State();
        }*/

        private void Input()
        {
            currentState.Input();
        }

        private void Update()
        {
            currentState.Update();
        }

        private void Render()
        {
            currentState.Render();
        }




        
        


        /*private void RestartGame()
        {
            firstPlayerMap = new ShipMap(RandomMap());
            secondPlayerMap = new ShipMap(RandomMap());
            gameEndResult = GameEndResult.None;
            gameplayState = GameplayState.FirstPlayerMove;
            endedPlaying = false;
            YInputCord = -1;
            XInputCord = -1;
        }*/
    }
}
