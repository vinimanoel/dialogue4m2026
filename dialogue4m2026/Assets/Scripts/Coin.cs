using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerOM.NotifyCoinCollected(1);
            Destroy(gameObject);

        }

    }
}