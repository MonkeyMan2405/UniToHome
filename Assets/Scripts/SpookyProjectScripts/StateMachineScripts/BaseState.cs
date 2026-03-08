using System;
using UnityEngine;

/// This is a base class for all states in the state machine. It defines the common interface that all states must implement.
public abstract class BaseState<EState> where EState : Enum
{
    // The constructor for the base state. It takes in the key that identifies this state and assigns it to the StateKey property.
    public BaseState(EState key)
    {
        StateKey = key;
    }
    /// The key that identifies this state. This is used to transition between states in the state machine.
    public EState StateKey { get; private set; }
    public abstract void EnterState();
    public abstract void ExitState();
    public abstract void UpdateState();
    public abstract EState GetNextState();
    public abstract void OnTriggerEnter(Collider other);
    public abstract void OnTriggerExit(Collider other);
    public abstract void OnTriggerStay(Collider other);
}
