using UnityEngine;

public class BubbleCheck : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            BubbleCounter PlayerCounter = other.GetComponent<BubbleCounter>();
            if (PlayerCounter != null)
            {
                PlayerCounter.IncrementBubbleCount();
            }
            else
            {
                Debug.Log("Error: Player is missing the BubbleCounter Script!");
            }

        }
    }
}
