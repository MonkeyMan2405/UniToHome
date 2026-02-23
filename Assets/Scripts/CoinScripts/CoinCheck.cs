using UnityEngine;

public class CoinCheck : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            BubbleCounter PlayerCounter = other.GetComponent<BubbleCounter>();
            if (PlayerCounter != null)
            {
                PlayerCounter.IncrementCoinCount();
                Destroy (gameObject);
            }
            else
            {
                Debug.Log("Error: Player is missing the BubbleCounter Script!");
            }

        }
    }
}

