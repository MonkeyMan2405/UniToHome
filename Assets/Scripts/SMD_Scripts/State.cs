using Unity.VisualScripting;
using UnityEngine;

public class State : MonoBehaviour
{
    private SimpleState CurrentState;
    //changes the states when called
    public void ChangeState()
    {
        if (CurrentState == SimpleState.StateA)
        {
            CurrentState = SimpleState.StateB;
        }
        else if (CurrentState == SimpleState.StateB)
        {
            CurrentState = SimpleState.StateC;
        }
    }

    // sets up states
    private enum SimpleState
    {
        StateA,
        StateB,
        StateC
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CurrentState = SimpleState.StateA;
        Debug.Log("Current State is " +  CurrentState);
    }

    // Update is called once per frame
    void Update()
    {

        // SpaceKey = calls ChangeState function then runs console line.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangeState();
            Debug.Log("Current State is " + CurrentState);
        }
    }
}
