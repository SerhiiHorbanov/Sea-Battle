using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sea_Battle
{
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
