using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sea_Battle
{
    struct ShipMap
    {
        public const char shotShipChar = 'X';
        public const char shipChar = 'O';
        public const char unknownChar = '#';
        public const char waterChar = '~';
        public const char shotWaterChar = '-';

        public bool[,] shipMap { private set; get; }
        public bool[,] shotTilesMap { private set; get; }

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

        public void ShootTile(int x, int y)
        {
            shotTilesMap[y, x] = true;
        }

        public void RenderMap(StringBuilder stringBuilder, bool showFullMap)
        {
            stringBuilder.AppendLine(" |0|1|2|3|4|5|6|7|8|9|");
            for (int y = 0; y < 10; y++)
            {
                stringBuilder.Append((char)('A' + y));

                RenderMapLine(stringBuilder, y, showFullMap);

                stringBuilder.Append("\n");
            }
        }

        private void RenderMapLine(StringBuilder stringBuilder, int y, bool showFullMap)
        {
            stringBuilder.Append('|');
            for (int x = 0; x < 10; x++)
            {
                char charToAdd = '_';
                bool isShotTile = shotTilesMap[y, x];
                bool isShipTile = shipMap[y, x];

                if (isShotTile)
                {
                    charToAdd = isShipTile ? shotShipChar : shotWaterChar;
                }
                else if (showFullMap)
                {
                    if (isShipTile)
                        charToAdd = shipChar;
                    else
                        charToAdd = waterChar;
                }
                stringBuilder.Append(charToAdd);
                stringBuilder.Append('|');
            }
        }

    }
}
