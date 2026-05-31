using UnityEngine;


public class PlayerController : MonoBehaviour
{
    private int coins = 0;

    public void CollectCoin()
    {
        coins++;

        PlayerOM.CoinCollected(coins);
    }
}
