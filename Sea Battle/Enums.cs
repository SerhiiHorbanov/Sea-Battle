using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sea_Battle
{
    enum GameEndResult
    {
        P1Win,
        P2Win,
        Draw
    }

    enum GameplayState
    {
        P1PlacingShips,
        P2PlacingShips,
        P1Move,
        P2Move,
    }
}
