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



    //Implementation of the Event system from 30/3/2026's workshop. Not the best implementation as the if statement checking complexity it tecyhnically the same as is currently,
    // I just wanted to put it into practise further
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
            SoundManager.PlaySound(SoundType.PlayerFlashlight, Random.Range(0.6f, 1f), Random.Range(0.4f, 0.6f));
        }
        else
        {
            flashlightLight.enabled = true;
            SoundManager.PlaySound(SoundType.PlayerFlashlight, Random.Range(0.6f, 1f), Random.Range(0.4f, 0.6f));
        }
    }




}
