using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sea_Battle
{
    struct ShipMap
    {
        public static readonly Random random = new Random();
        public const char shotShipChar = 'X';
        public const char shipChar = 'O';
        public const char unknownChar = 'X';
        public const string waterChars = "~-";

        private bool[,] shipMap;
        private bool[,] shotTilesMap;

        public ShipMap(bool[,] shipMap)
        {
            this.shipMap = shipMap;
            shotTilesMap = new bool[10, 10]
            {
                {false, false, false, false, false, false, false, false, false, false},
                {false, false, false, false, false, false, false, false, false, false},
                {false, false, false, false, false, false, false, false, false, false},
                {false, false, false, false, false, false, false, false, false, false},
                {false, false, false, false, false, false, false, false, false, false},
                {false, false, false, false, false, false, false, false, false, false},
                {false, false, false, false, false, false, false, false, false, false},
                {false, false, false, false, false, false, false, false, false, false},
                {false, false, false, false, false, false, false, false, false, false},
                {false, false, false, false, false, false, false, false, false, false}
            };
        }

        public void RenderMap(StringBuilder stringBuilder, bool showFullMap)
        {
            stringBuilder.AppendLine(" 0123456789");
            for (int y = 0; y < 10; y++)
            {
                stringBuilder.Append((char)('A' + y));

                RenderMapLine(stringBuilder, y, showFullMap);

                stringBuilder.Append("\n");
            }
        }

        private void RenderMapLine(StringBuilder stringBuilder, int y, bool showFullMap)
        {
            for (int x = 0; x < 10; x++)
            {
                char charToAdd = '#';
                bool isShotTile = shotTilesMap[y, x];
                bool isShipTile = shipMap[y, x];

                if (isShotTile)
                {
                    charToAdd = isShipTile ? shotShipChar : waterChars[random.Next(0, waterChars.Length)];
                }
                else if (showFullMap)
                {
                    if (isShipTile)
                        charToAdd = shipChar;
                    else
                        charToAdd = waterChars[random.Next(0, waterChars.Length)];
                }
                stringBuilder.Append(charToAdd);
            }
        }

    }
}
