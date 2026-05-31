using UnityEngine;

public class PlayerCoins : MonoBehaviour
{
    private int coins = 0;
    
    public void CollectCoin()
    {
        coins++;
        
        PlayerOM.OnCoinsChanged?.Invoke(coins);
    }
}