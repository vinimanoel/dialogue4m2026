using TMPro;
using UnityEngine;

public class CoinUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinText;
    private int totalCoins = 0;
    private void OnEnable()
    {
        PlayerOM.OnCoinsChanged += OnCoinsChanged;
    }

    private void OnDisable()
    {
        PlayerOM.OnCoinsChanged -= OnCoinsChanged;
    }

    private void OnCoinsChanged(int delta)
    {
        totalCoins += delta;
        if (coinText != null)
        coinText.text = "Moedas: " + totalCoins;
    }
}