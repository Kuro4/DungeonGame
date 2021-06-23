using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonGame
{
    public enum Direction
    {
        /// <summary>
        /// 東
        /// </summary>
        East,
        /// <summary>
        /// 西
        /// </summary>
        West,
        /// <summary>
        /// 南
        /// </summary>
        South,
        /// <summary>
        /// 北
        /// </summary>
        North,
    }

    public static class DirectionExtensions
    {
        public static Direction[] GetDirections()
        {
            return new Direction[] 
            {
                Direction.East,
                Direction.West,
                Direction.South,
                Direction.North
            };
        }
    }
}
