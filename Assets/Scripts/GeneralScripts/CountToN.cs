using UnityEngine;

public class CountToN : MonoBehaviour
{
    public int TargetNumber = 10; // can change to a different value
    private int CurrentNumber = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        while (CurrentNumber <= TargetNumber)
        {
            Debug.Log("Number: " + CurrentNumber); // display on console
            CurrentNumber++; // increment for next iteration
        }
    }
}
