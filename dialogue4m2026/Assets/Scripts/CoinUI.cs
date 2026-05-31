using TMPro;
using UnityEngine;

public class CoinUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinText;

    private void OnEnable()
    {
        PlayerOM.OnCoinsChanged += UpdateCoins;
    }

    private void OnDisable()
    {
        PlayerOM.OnCoinsChanged -= UpdateCoins;
    }

    private void UpdateCoins(int value)
    {
        coinText.text = "Moedas: " + value;
    }
}