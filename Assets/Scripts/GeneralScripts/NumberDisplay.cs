using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    int i;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        // use a For Loop to display numbers from 1 to 10
        for (int i = 1; i <= 10; i++) 

        // Display current number in console
        Debug.Log("The Current Number Is " + i);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
