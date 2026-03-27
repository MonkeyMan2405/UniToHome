using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UIElements;

public interface IInteractable
{
    public void Interact();
}

public class Interactor : MonoBehaviour
{
    public Transform interactorSource;
    public float interactionRange;
    public LayerMask interactionMask;

    //Interactee inherits this so interacter can invoke behaviours
    //Put this on interactable objects.
   

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray interactRay = new Ray(interactorSource.position, interactorSource.forward);
           
            if (Physics.Raycast(interactRay, out RaycastHit interactRayHitInfo, interactionRange))
            {
                Debug.DrawRay(interactorSource.position, interactorSource.forward * interactRayHitInfo.distance, Color.green, interactionMask);
                if (interactRayHitInfo.collider.gameObject.TryGetComponent(out IInteractable interactableObject))
                {
                    interactableObject.Interact();
                    Debug.Log("interacted");
                }
            }
        }
    }


}
