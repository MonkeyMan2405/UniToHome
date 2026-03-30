using UnityEngine;
using UnityEngine.Events;

public class GameManagerBroadcaster : MonoBehaviour
{

    //static one shared instance, accessible from anywhere
    public static UnityEvent OnGamePaused = new UnityEvent();
    public static UnityEvent OnFClicked = new UnityEvent();



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //Invoke any subscriber anywhere in the scene. 
            OnGamePaused?.Invoke();
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            OnFClicked?.Invoke();
        }
    }
}
