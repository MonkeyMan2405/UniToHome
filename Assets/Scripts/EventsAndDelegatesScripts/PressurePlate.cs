using UnityEngine;
using UnityEngine.Events; // Required for using UnityEvents

public class PressurePlate : MonoBehaviour
{

    //Declare Event. Initialised so never null.
    [SerializeField]
    public UnityEvent OnActivated = new UnityEvent();


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //? is a Null Conditional Operator
        OnActivated?.Invoke();
    }
}
