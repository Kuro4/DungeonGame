using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace DungeonGame
{
    /// <summary>
    /// 2次元座標を表す構造体
    /// </summary>
    public struct Point : IEquatable<Point>, IComparable<Point>
    {
        /// <summary>
        /// X座標
        /// </summary>
        public int X { get; }
        /// <summary>
        /// Y座標
        /// </summary>
        public int Y { get; }

        public Point(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public Point((int x, int y) point)
        {
            this.X = point.x;
            this.Y = point.y;
        }

        /// <summary>
        /// X座標に<paramref name="length"/>を加算した<see cref="Point"/>を返す
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public Point AddX(int length)
        {
            return new Point(this.X + length, this.Y);
        }
        /// <summary>
        /// Y座標に<paramref name="length"/>を加算した<see cref="Point"/>を返す
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public Point AddY(int length)
        {
            return new Point(this.X, this.Y + length);
        }
        /// <summary>
        /// X, Y座標にそれぞれ<paramref name="length"/>を加算した<see cref="Point"/>を返す
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public Point AddXY(int length)
        {
            return new Point(this.X + length, this.Y + length);
        }
        /// <summary>
        /// X, Y座標にそれぞれ<paramref name="xLength"/>, <paramref name="yLength"/>を加算した<see cref="Point"/>を返す
        /// </summary>
        /// <param name="xLength"></param>
        /// <param name="yLength"></param>
        /// <returns></returns>
        public Point AddXY(int xLength, int yLength)
        {
            return new Point(this.X + xLength, this.Y + yLength);
        }
        /// <summary>
        /// X座標から<paramref name="length"/>を減算した<see cref="Point"/>を返す
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public Point SubX(int length)
        {
            return new Point(this.X - length, this.Y);
        }
        /// <summary>
        /// Y座標から<paramref name="length"/>を減算した<see cref="Point"/>を返す
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public Point SubY(int length)
        {
            return new Point(this.X, this.Y - length);
        }
        /// <summary>
        /// X, Y座標からそれぞれ<paramref name="length"/>を減算した<see cref="Point"/>を返す
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public Point SubXY(int length)
        {
            return new Point(this.X - length, this.Y - length);
        }
        /// <summary>
        /// X, Y座標からそれぞれ<paramref name="xLength"/>, <paramref name="yLength"/>を減算した<see cref="Point"/>を返す
        /// </summary>
        /// <param name="xLength"></param>
        /// <param name="yLength"></param>
        /// <returns></returns>
        public Point SubXY(int xLength, int yLength)
        {
            return new Point(this.X - xLength, this.Y - yLength);
        }
        #region IEquatable<Point>の実装
        public override bool Equals(object obj)
        {
            return obj is Point point && this.Equals(point);
        }

        public bool Equals(Point other)
        {
            return this.X == other.X && this.Y == other.Y;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.X, this.Y);
        }

        public static bool operator ==(Point left, Point right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Point left, Point right)
        {
            return !(left == right);
        }
        #endregion
        #region Operation
        public static Point operator +(Point left, Point right)
        {
            return new Point(left.X + right.X, left.Y + right.Y);
        }
        public static Point operator -(Point left, Point right)
        {
            return new Point(left.X - right.X, left.Y - right.Y);
        }

        public static bool operator <(Point left, Point right)
        {
            return 0 < left.CompareTo(right); ;
        }
        public static bool operator >(Point left, Point right)
        {
            return left.CompareTo(right) < 0;
        }
        public static bool operator <=(Point left, Point right)
        {
            return 0 <= left.CompareTo(right);
        }
        public static bool operator >=(Point left, Point right)
        {
            return left.CompareTo(right) <= 0;
        }
        #endregion
        public int CompareTo([AllowNull] Point other)
        {
            if (this == other) return 0;
            return this.Y < other.Y ? 1 : (this.X < other.X ? 1 : -1);
        }
        public override string ToString()
        {
            return $"({this.X}, {this.Y})";
        }
    }
}
