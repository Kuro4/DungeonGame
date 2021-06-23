using System;
using System.Collections.Generic;
using System.Linq;

namespace DungeonGame
{
    public static class IEnumerableExtensions
    {
        private static readonly Random random = new Random();

        /// <summary>
        /// <paramref name="source"/>の中から<paramref name="value"/>に最も近い値を取得する
        /// </summary>
        /// <param name="source"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int Nearest(this IEnumerable<int> source, int value)
        {
            return source.OrderBy(x => Math.Abs(x - value)).First();
        }
        /// <summary>
        /// <paramref name="source"/>の中から<paramref name="value"/>に最も近い値を取得する
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="value"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static T Nearest<T>(this IEnumerable<T> source, T value, Func<T, int> selector)
        {
            int v = selector(value);
            return source.OrderBy(x => Math.Abs(selector(x) - v)).First();
        }
        /// <summary>
        /// ランダムに要素を取得する
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static T RandomAt<T>(this IEnumerable<T> source)
        {
            return source.ElementAt(random.Next(source.Count()));
        }
        /// <summary>
        /// ランダムに要素を指定範囲内のランダムな数分取得する
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static IEnumerable<T> RandomPicks<T>(this IEnumerable<T> source, int min, int max)
        {
            var buf = source.ToList();
            var res = new List<T>();
            T value;
            for (int i = 0; i < max; i++)
            {
                if (min <= i && random.NextBool())
                {
                    break;
                }
                value = source.ElementAt(random.Next(source.Count()));
                res.Add(value);
                buf.Remove(value);
            }
            return res;
        }
        /// <summary>
        /// ランダムに要素を指定範囲内のランダムな数分取得する
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static IEnumerable<T> RandomPicks<T>(this IEnumerable<T> source, int max)
        {
            return source.RandomPicks(1, max);
        }
    }
}
