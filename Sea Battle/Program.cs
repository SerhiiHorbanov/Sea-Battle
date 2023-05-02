using System;

namespace Sea_Battle
{
    internal class Program
    {
        public static readonly Random random = new Random();
        static void Main(string[] args)
        {
            ProfileData.SaveProfileToFile(new ProfileData("test2", 2, 2, 1, 1));
            SeaBattle game = new SeaBattle();
            game.Start();
        }
    }
}
