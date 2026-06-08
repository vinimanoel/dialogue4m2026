using System;

public static class PlayerOM
{
 public static object OnCoinsChanged { get; set; }
}

public static Action<int> OnCoinsChanged;

 private static void NotifyCoinCollected(int amount)
 {
  OnCoinsChanged?.Invoke(amount);
 }
