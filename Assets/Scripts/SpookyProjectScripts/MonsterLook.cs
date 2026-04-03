using UnityEngine;

public class MonsterLook : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed = 2f;
    [SerializeField]
    private Transform currentTarget;
    private Vector3 targetDirection;
    private Quaternion targetRotation;
    public float headTilt = -45f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        targetDirection = currentTarget.position - transform.position;
        targetRotation = Quaternion.LookRotation(targetDirection);

        //Rotate towards Target
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, headTilt);
    }
}
