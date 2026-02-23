using UnityEngine;

public class EvenOddCheckerScript : MonoBehaviour
{
    //Can change to test other values.
    public int NumberToCheck = 5;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {// check if number is even or odd.
        //% means Factor Of.
        if (NumberToCheck % 2 == 0)
        {
            Debug.Log("Is Even");
        }
        else
        {
            Debug.Log("Is Odd");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
