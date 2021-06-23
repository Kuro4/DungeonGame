using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonGame
{
    public interface IRoom
    {
        RoomType RoomType { get; }

        IEnumerable<Point> Entrances { get; }

        void Move(int x, int y);

        bool IsOverlap(IRoom room);

        IEnumerable<Point> GetPoints();

        public IRoom Merge(IEnumerable<IRoom> rooms);
    }
}
