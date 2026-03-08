using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateManager<EState> : MonoBehaviour where EState : Enum
{
    // The current state of the state machine.
    protected Dictionary <EState, BaseState<EState>> States = new Dictionary<EState, BaseState<EState>>();

    protected BaseState<EState> CurrentState;

    protected bool isTransitioningState = false;






    private void Start()
    {
        // Initialize the current state to the first state in the dictionary.
        CurrentState.EnterState();
    }

    private void Update()
    {
        // Get next state key from current state and check if different from current state key.
        EState nextStateKey = CurrentState.GetNextState();

        // If next state key is same as current state key, update current state. Otherwise, transition to next state.
        if (!isTransitioningState && nextStateKey.Equals(CurrentState.StateKey))
        {
            CurrentState.UpdateState();
        }

        else if (!isTransitioningState)
        {
            TransitionToState(nextStateKey);
        }
    }
   



    public void TransitionToState(EState nextStateKey)
    {
        // Check if the next state exists in the dictionary before transitioning.
        if (States.ContainsKey(nextStateKey))
        {
            // Transition to the next state.
            isTransitioningState = true;
            CurrentState.ExitState();
            CurrentState = States[nextStateKey];
            CurrentState.EnterState();
            isTransitioningState = false;
        }
        else
        {
            Debug.LogError($"State {nextStateKey} does not exist in the state machine.");
        }
    }




    private void OnTriggerEnter(Collider other)
    {
        CurrentState.OnTriggerEnter(other);
    }

    private void OnTriggerExit(Collider other)
    {
        CurrentState.OnTriggerExit(other);
    }

    private void OnTriggerStay(Collider other)
    {
        CurrentState.OnTriggerStay(other);   
    }















}
