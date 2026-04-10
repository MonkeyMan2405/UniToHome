using UnityEngine;
using UnityEngine.Events;

public class GameManagerBroadcaster : MonoBehaviour
{

    //static one shared instance, accessible from anywhere
    public static UnityEvent OnGamePaused = new UnityEvent();
    public static UnityEvent OnFClicked = new UnityEvent();

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
            //Flashlight On and Off
            OnFClicked?.Invoke();
        }
    }

  
}
