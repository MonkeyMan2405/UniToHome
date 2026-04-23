using UnityEngine;

public class RagLimbCollision : MonoBehaviour
{
    public RagPlayerController playerControllerRef;


    private void Start()
    {
        playerControllerRef = GameObject.FindObjectOfType<RagPlayerController>().GetComponent<RagPlayerController>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        playerControllerRef.isGrounded = true;
    }

}
