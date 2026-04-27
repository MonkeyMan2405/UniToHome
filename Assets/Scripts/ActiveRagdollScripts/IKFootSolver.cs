using System.Runtime.CompilerServices;
using UnityEditor.PackageManager;
using UnityEngine;

public class IKFootSolver : MonoBehaviour
{
    [SerializeField]
    private LayerMask floorLM;
    public float footSpacing;
    public Vector3 rayheightincrease;
    public Transform foot;
    private RaycastHit hitInfo;
    [SerializeField]
    private bool leftOrRight;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(leftOrRight == false)
        {
            Ray footPlacementRay = new Ray(transform.position, Vector3.right);
            if (Physics.Raycast(footPlacementRay, out RaycastHit hitInfo, 10, floorLM))
            {
                Debug.DrawRay(transform.position, Vector3.down * hitInfo.distance, Color.red);
                foot.position = hitInfo.point;
                OnDrawGizmos();
            }
        }
        else
        {
                       Ray footPlacementRay = new Ray(transform.position, Vector3.down);
            if (Physics.Raycast(footPlacementRay, out RaycastHit hitInfo, 10, floorLM))
            {
                Debug.DrawRay(transform.position, Vector3.down * hitInfo.distance, Color.red);
                foot.position = hitInfo.point;
                OnDrawGizmos();
            }
        }
       


        
      

        //if (Input.GetKeyDown(KeyCode.H))
        //{
        //    Ray footPlacementRay = new Ray(transform.position, Vector3.down);
        //    if (Physics.Raycast(footPlacementRay, out RaycastHit hitInfo, 5, floorLM))
        //    {
        //        transform.position = hitInfo.point + rayheightincrease;
        //    }
        //}
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(hitInfo.point, 1f);
    }

}
