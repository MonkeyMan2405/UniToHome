using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class GameManagerBroadcaster : MonoBehaviour
{

    //static one shared instance, accessible from anywhere
    public static UnityEvent OnGamePaused = new UnityEvent();
    public static UnityEvent OnFClicked = new UnityEvent();


     InputAction flashLightAction;

    void Start()
    {
        flashLightAction = InputSystem.actions.FindAction("FlashLight");
    }
    // Update is called once per frame
    void Update()
    {
       
        if (flashLightAction.WasPressedThisFrame())
        {
            //Invoke any subscriber anywhere in the scene. 
            OnGamePaused?.Invoke();
            OnFClicked?.Invoke();
        }
        
    }

  
}
