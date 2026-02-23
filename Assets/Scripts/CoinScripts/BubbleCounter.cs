using UnityEngine;

public class BubbleCounter : MonoBehaviour
{
    private int BubblesCollected = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Bubble Count has Begun. Start Count: " + BubblesCollected);
    }

    public void IncrementBubbleCount()
    {
        BubblesCollected = BubblesCollected + 2;

        Debug.Log("Bubble Collected! New Total: " + BubblesCollected);


    }

    public void IncrementCoinCount()
    {
        BubblesCollected = BubblesCollected + 1;

        Debug.Log("Bubble Collected! New Total: " + BubblesCollected);


    }


}
