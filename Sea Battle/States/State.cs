using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sea_Battle.States
{
    abstract class State
    {
        public abstract void Render();
        public abstract void Input();
        public abstract void Update();
    }
}
