using Unity.Transforms;
using UnityEngine;

public class RagPlayerController : MonoBehaviour
{

    public float speed;
    public float strafeSpeed;

    public float jumpForce;

    public Rigidbody hipsRb;

    public bool isGrounded;

    private RaycastHit rayHitInfo;

    [SerializeField]
    private LayerMask ragdollLayerMask;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //douible check later if this script isn't on the hips, as this would result wrong.
        hipsRb = GetComponent<Rigidbody>();

        isGrounded = true;
    }

    private void FixedUpdate()
    {
        if(Input.GetKey(KeyCode.W))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                hipsRb.AddForce(hipsRb.transform.right * speed * 1.5f);
            }
            else
            {
                hipsRb.AddForce(hipsRb.transform.right * speed);
            }
               
        }

        else if (Input.GetKey(KeyCode.S))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                hipsRb.AddForce(hipsRb.transform.right * -speed * 1.5f);
            }
            else
            {
                hipsRb.AddForce(hipsRb.transform.right * -speed);
            }

        }

        else if (Input.GetKey(KeyCode.A))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                hipsRb.AddForce(hipsRb.transform.up * -speed * 1.5f);
            }
            else
            {
                hipsRb.AddForce(hipsRb.transform.up * -speed);
            }
          
        }

        else if (Input.GetKey(KeyCode.D))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                hipsRb.AddForce(hipsRb.transform.up * speed * 1.5f);
            }
            else
            {
                hipsRb.AddForce(hipsRb.transform.up * speed);
            }

        }



        if (Input.GetKey(KeyCode.Space))
        {

            Ray ragdollRay = new Ray(hipsRb.transform.position, new Vector3(0, -1, 0));

            //5.75 seems right
            if (Physics.Raycast(hipsRb.transform.position, new Vector3 (0, -5.75f, 0), out rayHitInfo, 5.75f, ragdollLayerMask))
            {
               
                Debug.DrawRay(hipsRb.transform.position, new Vector3(0, -5.75f, 0), Color.red);

                if (rayHitInfo.collider.CompareTag("Jumpable"))
                {
                    hipsRb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
                

                }
            }
        }
     

    }

}
