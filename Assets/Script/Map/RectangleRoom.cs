using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DungeonGame
{
    public class RectangleRoom : IRoom
    {
        public RoomType RoomType { get; } = RoomType.Rectangle;
        /// <summary>
        /// 起点
        /// </summary>
        public Point Start { get; }
        /// <summary>
        /// 終点
        /// </summary>
        public Point End { get; }
        /// <summary>
        /// 幅
        /// </summary>
        public int Width { get; }
        /// <summary>
        /// 高さ
        /// </summary>
        public int Height { get; }

        private List<Point> entrances = new List<Point>();
        /// <summary>
        /// 入口
        /// </summary>
        public IEnumerable<Point> Entrances => this.entrances.AsEnumerable();

        public RectangleRoom(Point start, int width, int height)
        {
            this.Start = start;
            this.Width = width;
            this.Height = height;
            this.End = start.AddXY(width, height);
        }

        public RectangleRoom(Point start, Point end)
        {
            this.Start = start;
            this.End = end;
            this.Width = end.X - start.X;
            this.Height = end.Y - start.Y;
        }
        public RectangleRoom(int x, int y, int width, int height) : this(new Point(x, y), width, height)
        {
        }
        /// <summary>
        /// <paramref name="length"/>だけ周囲を広げた<see cref="RectangleRoom"/>を返す
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public RectangleRoom ToExpand(int length)
        {
            if (length < 0) return this;
            return new RectangleRoom(this.Start.SubXY(length), this.End.AddXY(length));
        }
        /// <summary>
        /// <paramref name="length"/>だけ右側を広げた<see cref="RectangleRoom"/>を返す
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public RectangleRoom ToExpandRight(int length)
        {
            return new RectangleRoom(this.Start, this.End.AddX(length));
        }
        /// <summary>
        /// <paramref name="length"/>だけ上側を広げた<see cref="RectangleRoom"/>を返す
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public RectangleRoom ToExpandTop(int length)
        {
            return new RectangleRoom(this.Start, this.End.AddY(length));
        }
        /// <summary>
        /// <paramref name="length"/>だけ左側を広げた<see cref="RectangleRoom"/>を返す
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public RectangleRoom ToExpandLeft(int length)
        {
            
            return new RectangleRoom(this.Start.SubX(length), this.End);
        }
        /// <summary>
        /// <paramref name="length"/>だけ下側を広げた<see cref="RectangleRoom"/>を返す
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public RectangleRoom ToExpandBottom(int length)
        {
            return new RectangleRoom(this.Start.SubY(length), this.End);
        }
        /// <summary>
        /// この<see cref="RectangleRoom"/>に含まれる全ての<see cref="Point"/>を取得する
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Point> GetPoints()
        {
            for (int y = this.Start.Y; y <= this.End.Y; y++)
            {
                for (int x = Start.X; x <= this.End.X; x++)
                {
                    yield return new Point(x, y);
                }
            }
        }
        /// <summary>
        /// 全ての入口をクリアする
        /// </summary>
        public void ClearEntrance()
        {
            this.entrances.Clear();
        }
        /// <summary>
        /// 東側へ入口を生成する
        /// </summary>
        public void CreateEntranceToEast()
        {
            var random = new Random();
            int x = this.End.X + 1;
            int y = random.Next(this.Start.Y, this.End.Y);
            this.entrances.Add(new Point(x, y));
        }
        /// <summary>
        /// 西側へ入口を生成する
        /// </summary>
        public void CreateEntranceToWest()
        {
            var random = new Random();
            int x = this.Start.X - 1;
            int y = random.Next(this.Start.Y, this.End.Y);
            this.entrances.Add(new Point(x, y));
        }
        /// <summary>
        /// 南側へ入口を生成する
        /// </summary>
        public void CreateEntranceToSouth()
        {
            var random = new Random();
            int x = random.Next(this.Start.X, this.End.X);
            int y = this.Start.Y - 1;
            this.entrances.Add(new Point(x, y));
        }
        /// <summary>
        /// 北側へ入口を生成する
        /// </summary>
        public void CreateEntranceToNorth()
        {
            var random = new Random();
            int x = random.Next(this.Start.X, this.End.X);
            int y = this.End.Y + 1;
            this.entrances.Add(new Point(x, y));
        }
        /// <summary>
        /// <paramref name="point"/>が含まれるかどうかを返す
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public bool Contains(Point point)
        {
            return (this.Start.X <= point.X && point.X <= this.End.X) && (this.Start.Y <= point.Y && point.Y <= this.End.Y);
        }

        public void Move(int x, int y)
        {
            throw new NotImplementedException();
        }

        public bool IsOverlap(IRoom room)
        {
            throw new NotImplementedException();
        }

        public IRoom Merge(IEnumerable<IRoom> rooms)
        {
            throw new NotImplementedException();
        }


        public override string ToString()
        {
            return $"{this.Start}, {this.End}";
        }
    }
}
