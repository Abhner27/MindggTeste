using System.Collections.Generic;
using UnityEngine;
using System;

namespace Abhner.List
{
    public static class ExtensionLists
    {
        public static T GetRandomElement<T>(this IList<T> list)
        {
            return list[UnityEngine.Random.Range(0, list.Count)];
        }

        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = UnityEngine.Random.Range(0, n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}

namespace Abhner.Numbers
{
    public static class ExtensionNumbers
    {
        public static T Clamp<T>(this T value, T min, T max) where T : IComparable<T>
        {
            if (value.CompareTo(min) < 0) return min;
            if (value.CompareTo(max) > 0) return max;
            return value;
        }

    }
}

namespace Abhner.Color
{
    public static class ExtensionColor
    {
        public static UnityEngine.Color HexToColor(this string hex)
        {
            if (ColorUtility.TryParseHtmlString(hex, out UnityEngine.Color color))
            {
                return color;
            }
            return UnityEngine.Color.white; // Valor padrão em caso de falha na conversão
        }

    }
}