using UnityEngine;

public class DestroyableObjectScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerStay (Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy (gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
