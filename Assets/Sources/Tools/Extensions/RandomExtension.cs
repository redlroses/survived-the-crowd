using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Sources.Tools.Extensions
{
    public static class RandomExtension
    {
        public static TElement GetRandom<TElement>(this IEnumerable<TElement> elements)
        {
            var elementsArray = elements.ToArray();
            int randomIndex = Random.Range(0, elementsArray.Length);
            return elementsArray[randomIndex];
        }
    }
}
