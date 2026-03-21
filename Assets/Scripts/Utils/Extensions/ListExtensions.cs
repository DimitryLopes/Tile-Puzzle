using System.Collections.Generic;
using UnityEngine;

public static class ListExtensions
{
    public static T GetRandom<T>(this IList<T> list)
    {
        int randomIndex = Random.Range(0, list.Count);
        return list[randomIndex];
    }
}
