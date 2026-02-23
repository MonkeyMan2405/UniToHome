using UnityEngine;

public class CoinSpin : MonoBehaviour
{
    public float RotationSpeed = 50.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
  
    
        
  

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.left, RotationSpeed * Time.deltaTime);
    }
}
