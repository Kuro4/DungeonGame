using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DungeonGame
{
    public static class RandomExtensions
    {
        /// <summary>
        /// 指定した確率でランダムな<see cref="bool"/>値を返す
        /// </summary>
        /// <param name="self"></param>
        /// <param name="percentage">true になる確率(0～1)</param>
        /// <returns></returns>
        public static bool NextBool(this Random self, double percentage = 0.5)
        {
            return self.NextDouble() < percentage;
        }
    }
}
