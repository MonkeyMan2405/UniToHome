using System.Runtime.CompilerServices;
using UnityEditor.PackageManager;
using UnityEngine;

public class IKFootSolver2 : MonoBehaviour
{
    [SerializeField]
    private LayerMask floorLM;
    [SerializeField]
    private float stepDistance;

    public Vector3 rayheightincrease;
    public Transform rightFoot;
    public Transform pelvisPos;
    public Rigidbody rightFootRB;
    public Vector3 pelvisToFloorPos;

    private bool shouldCheck;

    private RaycastHit hitInfo;
    private RaycastHit hitInfo2;
    [SerializeField]

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        shouldCheck = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       
        if(shouldCheck)
        {
            CheckRay();
        }

        rightFoot.position = hitInfo.point + rayheightincrease;

        CheckDistanceRay();





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
        Gizmos.DrawSphere(hitInfo.point, 0.1f);
    }


    public void CheckRay()
    {
        Ray footPlacementRay = new Ray(rightFoot.position, Vector3.down);

        if (Physics.Raycast(transform.position, Vector3.down, out hitInfo, 3))
        {
            shouldCheck = false;
            Debug.DrawRay(rightFoot.position, Vector3.down * 3f, Color.red);
            //foot.position = hitInfo.point + rayheightincrease;
            rightFootRB.constraints = RigidbodyConstraints.FreezeAll;
            Debug.Log(rightFoot.position);


        }
    }

    public void CheckDistanceRay()
    {

        if (Physics.Raycast(pelvisPos.position, Vector3.down, out hitInfo2, 5.75f, floorLM))
        {
            pelvisToFloorPos = hitInfo2.point;

            if(Vector3.Distance(pelvisToFloorPos, rightFoot.position) > stepDistance)
            {
                Debug.Log("yes");
            }
        }

    }

}
