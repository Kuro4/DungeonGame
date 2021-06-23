using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DungeonGame
{
    /// <summary>
    /// 目的地に向かって通路が接続されるマップ
    /// </summary>
    public class DestinationMap : IMap
    {
        /// <summary>
        /// 幅
        /// </summary>
        public int Width { get; private set; }
        /// <summary>
        /// 高さ
        /// </summary>
        public int Height { get; private set; }
        /// <summary>
        /// 部屋の最小幅
        /// </summary>
        public int RoomMinWidth { get; set; } = 5;
        /// <summary>
        /// 部屋の最小高さ
        /// </summary>
        public int RoomMinHeight { get; set; } = 5;
        /// <summary>
        /// 部屋の最大幅
        /// </summary>
        public int RoomMaxWidth { get; set; } = 10;
        /// <summary>
        /// 部屋の最大高さ
        /// </summary>
        public int RoomMaxHeight { get; set; } = 10;
        /// <summary>
        /// 部屋の生成を試みる回数
        /// </summary>
        public int TryRoomCreateCount { get; set; } = 5;
        /// <summary>
        /// 目的地の数
        /// </summary>
        public int DestinationCount { get; set; } = 1;
        /// <summary>
        /// 部屋を重ねることを可能にするかどうか
        /// </summary>
        public bool CanOverlapRoom { get; set; } = false;

        private readonly List<IRoom> rooms = new List<IRoom>();
        /// <summary>
        /// 生成された部屋のリスト
        /// </summary>
        public IEnumerable<IRoom> Rooms => this.rooms.AsEnumerable();

        /// <summary>
        /// マップデータの実態
        /// </summary>
        private readonly TileType[,] map;

        public TileType this[int x, int y] 
        {
            get { return this.map[y, x]; }
            set { this.map[y, x] = value; }
        }

        public TileType this[Point point]
        {
            get { return this[point.X, point.Y]; }
            set { this[point.X, point.Y] = value; }
        }

        /// <summary>
        /// 指定した高さと幅で初期化したインスタンスを生成する
        /// </summary>
        /// <param name="height">高さ</param>
        /// <param name="width">幅</param>
        public DestinationMap(int width, int height)
        {
            this.Width = width;
            this.Height = height;
            this.map = new TileType[this.Height, this.Width];
            this.Initialize();
        }
        /// <summary>
        /// マップを初期化する
        /// </summary>
        public void Initialize()
        {
            for (int i = 0; i < this.Height; i++)
            {
                for (int j = 0; j < this.Width; j++)
                {
                    this.map[i, j] = TileType.Wall;
                }
            }
        }
        /// <summary>
        /// マップデータをランダムに生成する
        /// </summary>
        public void Next()
        {
            this.Initialize();
            var random = new Random();
            this.rooms.Clear();
            int[] destinationX = new int[this.DestinationCount];
            int[] destinationY = new int[this.DestinationCount];
            // 目的地をランダムに生成する
            for (int i = 0; i < this.DestinationCount; i++)
            {
                // TODO: margin
                destinationX[i] = random.Next(this.Width / 4, this.Width * 3 / 4);
                destinationY[i] = random.Next(this.Height / 4, this.Height * 3 / 4);
            }
            // 部屋数分部屋と道を生成する
            for (int i = 0; i < this.TryRoomCreateCount; i++)
            {
                // 部屋サイズ
                int roomWidth = random.Next(this.RoomMinWidth, this.RoomMaxWidth);
                int roomHeight = random.Next(this.RoomMinHeight, this.RoomMaxHeight);
                // 部屋の座標
                // TODO: margin
                int roomX = random.Next(2, this.Width - this.RoomMaxWidth - 2);
                int roomY = random.Next(2, this.Height - this.RoomMaxHeight - 2);
                var room = new RectangleRoom(roomX, roomY, roomWidth, roomHeight);
                var expandedRoom = room.ToExpand(1);
                // 部屋を重ねる場合
                if (this.CanOverlapRoom)
                {
                    // 重なった部屋を取得
                    var overlapRooms = this.rooms.Where(x => x.IsOverlap(room));
                    var mergedRoom = room.Merge(overlapRooms);
                    foreach (var item in overlapRooms)
                    {
                        this.rooms.Remove(item);
                    }
                    this.rooms.Add(mergedRoom);
                    // これでもいいかも
                    //this.rooms.Add(room.Merge(overlapRooms));
                    // TODO: 道の生成などをどうするか検討
                }
                // 部屋を重ねない場合は部屋が生成できるか確認
                else if (this.CanCreateRoom(room))
                {
                    continue;
                }
                this.rooms.Add(room);
                // 部屋を生成
                var points = room.GetPoints();
                foreach (var point in points)
                {
                    this[point] = TileType.Room;
                }
                // 生成範囲内に道が含まれていれば道を生成しない
                bool containsRoad = points.Where(x => this[x] == TileType.Road).Any();
                if (containsRoad) continue;
                // 目的地を取得
                var destination = new Point(destinationX.RandomAt(), destinationY.RandomAt());
                // 目的地が部屋内なら道を生成しない
                if (expandedRoom.Contains(destination)) continue;
                this[destination] = TileType.Road;
                int roadStartX;
                int roadStartY;
                // 目的地が部屋のX座標と被る場合は上下に道を作る
                if (expandedRoom.Start.X <= destination.X && destination.X <= expandedRoom.End.X)
                {
                    // 目的地のY座標に近い方に道の起点を設定
                    roadStartX = random.Next(room.Start.X, room.End.X);
                    roadStartY = (new[] { expandedRoom.Start.Y, expandedRoom.End.Y }).Nearest(destination.Y);
                    // 道を生成
                    var currentY = this.CreateRoadY(new Point(roadStartX, roadStartY), destination);
                    this.CreateRoadX(new Point(roadStartX, currentY), destination);
                }
                // それ以外は左右に道を作る
                else
                {
                    // 目的地のX座標に近い方に道の起点を設定
                    roadStartX = (new[] { expandedRoom.Start.X, expandedRoom.End.X }).Nearest(destination.X);
                    roadStartY = random.Next(room.Start.Y, room.End.Y);
                    // 道を生成
                    var currentX = this.CreateRoadX(new Point(roadStartX, roadStartY), destination);
                    this.CreateRoadY(new Point(currentX, roadStartY), destination);
                }
            }
        }
        /// <summary>
        /// 部屋が生成できるか
        /// </summary>
        /// <param name="room"></param>
        /// <returns></returns>
        private bool CanCreateRoom(RectangleRoom room)
        {
            // TODO: 目的地を含まないようにする？
            // TODO: 通路に接する場合も生成しないようにする？
            return room.ToExpand(1).GetPoints().Where(x => this[x] == TileType.Room).Any();
        }
        /// <summary>
        /// X 方向に道を生成し、生成した地点を返す
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        private int CreateRoadX(Point start, Point end)
        {
            int n = start.X < end.X ? 1 : -1;
            int currentX = start.X;
            TileType current;
            while (currentX != end.X)
            {
                current = this[currentX, start.Y];
                if (current == TileType.Room || current == TileType.Road)
                {
                    break;
                }
                this[currentX, start.Y] = TileType.Road;
                currentX += n;
            }
            return currentX;
        }
        /// <summary>
        /// Y 方向に道を生成し、生成した地点を返す
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        private int CreateRoadY(Point start, Point end)
        {
            int n = start.Y < end.Y ? 1 : -1;
            int currentY = start.Y;
            TileType current;
            while (currentY != end.Y)
            {
                current = this[start.X, currentY];
                if (current == TileType.Room || current == TileType.Road)
                {
                    break;
                }

                this[start.X, currentY] = TileType.Road;
                currentY += n;
            }
            return currentY;
        }
    }
}