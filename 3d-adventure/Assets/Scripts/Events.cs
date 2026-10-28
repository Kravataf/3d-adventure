using UnityEngine;
using System;

public static class Events
{
    public static event Action<string, int> onCollected;

    public static void RaiseCollected(string item, int amount) => onCollected?.Invoke(item, amount);
}
