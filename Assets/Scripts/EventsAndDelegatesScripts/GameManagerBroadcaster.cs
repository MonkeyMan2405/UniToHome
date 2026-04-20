using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class GameManagerBroadcaster : MonoBehaviour
{

    //static one shared instance, accessible from anywhere
    public static UnityEvent OnGamePaused = new UnityEvent();
    public static UnityEvent OnFClicked = new UnityEvent();

    [SerializeField]
    private int targetFPS;

     InputAction flashLightAction;

     [SerializeField]
     private TextMeshProUGUI debugText;


    void Awake()
    {
        QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = targetFPS;
    }


    void Start()
    {
        flashLightAction = InputSystem.actions.FindAction("FlashLight");
        debugText.text = Application.platform.ToString() +" & " + SystemInfo.operatingSystem +" & " + SystemInfo.deviceType + " & " + SystemInfo.deviceName;
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
