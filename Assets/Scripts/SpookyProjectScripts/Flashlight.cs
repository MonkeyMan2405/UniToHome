using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public Transform playerTransform;
    public float zFlashlightOffset;
    public float catchingSpeed;
    public Light flashlightLight;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        flashlightLight.enabled = false;
        transform.position = playerTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = playerTransform.position + new Vector3(0, 0, zFlashlightOffset);
        transform.rotation = Quaternion.Lerp(transform.rotation, playerTransform.rotation, catchingSpeed * Time.deltaTime);
    }

    private void OnEnable()
    {
        GameManagerBroadcaster.OnFClicked.AddListener(ToggleFlashlight);
    }

     private void OnDisable()
    {
        GameManagerBroadcaster.OnFClicked.RemoveListener(ToggleFlashlight);
    }

    private void ToggleFlashlight()
    {
        if (flashlightLight.enabled)
        {
            flashlightLight.enabled = false;
        }
        else
        {
            flashlightLight.enabled = true;
        }
    }




}
