using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonGame
{
    /// <summary>
    /// 左下が原点の2次元マップインターフェイス
    /// </summary>
    public interface IMap
    {
        int Width { get; }
        int Height { get; }
        public TileType this[int x, int y] { get;set; }
    }

    public interface IMap<T>
    {
        int Width { get; }
        int Height { get; }
        public T this[int x, int y] { get;set; }
    }
}
