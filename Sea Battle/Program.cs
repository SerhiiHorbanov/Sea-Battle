using System;

namespace Sea_Battle
{
    internal class Program
    {
        public static readonly Random random = new Random();
        static void Main(string[] args)
        {
            SeaBattle game = new SeaBattle();
            game.Start();
        }
    }
}
