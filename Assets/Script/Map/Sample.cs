
using System.Data;
namespace DungeonGame
{
    public class Sample
    {
        public Sample()
        {
            int width = 100;
            int height = 100;
            var map = new DestinationMap(width, height)
            {
                // 部屋生成の試行回数
                TryRoomCreateCount = 20,
                // 交点(目的地)の数
                DestinationCount = 1,
            };
            map.Next();
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    // tile に応じてマップにオブジェクト配置
                    switch (map[x, y])
                    {
                        case TileType.Wall:
                            break;
                        case TileType.Room:
                            break;
                        case TileType.Road:
                            break;
                        case TileType.Entrance:
                            break;
                        default:
                            break;
                    }
                }   
            }
        }
    }   
}