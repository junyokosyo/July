using UnityEngine;
using System.Collections.Generic;
/// <summary>
/// 与えられたリストからランダムに要素を選択するためのユーティリティクラスです。
/// </summary>
public static class RandomPicker
{
    public static T PickRandom<T>(List<T> items)
    {
        if (items == null || items.Count == 0)
            throw new System.ArgumentException("List is null or empty");

        return items[Random.Range(0, items.Count)];
    }
}
