
using UnityEngine;
using UnityEngine.InputSystem;

public class StandardState : PlayerState, IInteractable
{
    //Variables for this State
    private bool changeToHideState;
    private bool changeToTransitionState;
    private bool changeToBlindsState;

    private float playerResetSpeed;
    private bool hasVisitedBefore;

    InputAction mouseJoystickLook;
    InputAction interact;



    public StandardState(PlayerStateContext _pcontext, PlayerStateMachine.EPlayerState state) : base(_pcontext, state)
    {
        PlayerStateContext PContext = _pcontext;
    }


    public override void EnterState()
    {
        //enable head bobbing as it is turned off when transitioning from certain states to here
        PContext.headBobbingRef.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;

        PContext.playerSprintSpeed = PlayerStateMachine.playerMovementSpeed * PContext.playerRunMultiplier;


        PContext.transitionIdentifier = 0;

        Debug.Log("Standard");

        if (hasVisitedBefore == false)
        {
            playerResetSpeed = PlayerStateMachine.playerMovementSpeed;
        }

        mouseJoystickLook = InputSystem.actions.FindAction("Look");

        interact = InputSystem.actions.FindAction("Interact");

    }



    public override void UpdateState()
    {
        CameraSetup();
        CameraTilt();
        ForwardAndBackwardTilt();
        Movement();
        Interact();

    }
    



    public override void ExitState()
    {
        changeToHideState = false;
        changeToTransitionState = false;
        changeToBlindsState = false;

        hasVisitedBefore = true;
    }



    //Checked every frame
    public override PlayerStateMachine.EPlayerState GetNextState()
    {

        if (changeToHideState == true)
        {
            return PlayerStateMachine.EPlayerState.Hiding;
        }

        else if (changeToTransitionState == true) 
        {
            return PlayerStateMachine.EPlayerState.Transition;
        }

        else if (changeToBlindsState == true)
        {
            return PlayerStateMachine.EPlayerState.Blinds;
        }

            return StateKey;

    }



    public void CameraTilt()
    {
        bool leftStrafe = Input.GetKey(KeyCode.A);
        bool rightStrafe = Input.GetKey(KeyCode.D);
        bool forwardStrafe = Input.GetKey(KeyCode.W);
        bool backwardStrafe = Input.GetKey(KeyCode.S);

        //Left-Right Strafing Tilting
        // Determine the Ztarget tilt based on strafing input. If strafing left, set target tilt to positive value. If strafing right, set to negative value. If not strafing, set to zero
        if (leftStrafe && !rightStrafe)
        {
            PContext.zTargetTilt = PContext.zTiltAmount;    
        }
        else if (rightStrafe && !leftStrafe)
        {
            PContext.zTargetTilt = -PContext.zTiltAmount;
        }
        else
        {
            PContext.zTargetTilt = 0f;
        }

        // Smoothly interpolate current tilt to target tilt. If target tilt zero, use tilt end speed forquicker return to neutral. else, use tilt start speed for slower transition when starting to strafe.
        if (PContext.zTargetTilt == 0)
        {
            PContext.zSmoothTilt = PContext.tiltEndSpeed;
        }
        else
        {
            PContext.zSmoothTilt = PContext.tiltStartSpeed;
        }

        // Lerp the current tilt towards the target tilt using the determined smooth speed and delta time
        PContext.zCurrentTilt = Mathf.Lerp(PContext.zCurrentTilt, PContext.zTargetTilt, PContext.zSmoothTilt * Time.deltaTime);


        // Determine the Xtarget tilt based on strafing input. If strafing forward, set target tilt to positive value. If strafing backwards, set to negative value. If not strafing, set to zero
        if (forwardStrafe && !backwardStrafe)
        {
            PContext.xTargetTilt = PContext.xTiltAmount;
        }
        else if (backwardStrafe && !forwardStrafe)
        {
            PContext.xTargetTilt = -PContext.xTiltAmount;
        }
        else
        {
            PContext.xTargetTilt = 0f;
        }

        // Smoothly interpolate current tilt to target tilt. If target tilt zero, use tilt end speed for quicker return to neutral. else, use tilt start speed for slower transition when starting to strafe.
        if (PContext.xTargetTilt == 0)
        {
            PContext.xSmoothTilt = PContext.tiltEndSpeed;
        }
        else
        {
            PContext.xSmoothTilt = PContext.tiltStartSpeed;
        }

        // Lerp the current tilt towards the target tilt using the determined smooth speed and delta time
        PContext.xCurrentTilt = Mathf.Lerp(PContext.xCurrentTilt, PContext.xTargetTilt, PContext.xSmoothTilt * Time.deltaTime);
    }



    public void ForwardAndBackwardTilt()
    {
        PContext.camPivotRef.transform.localRotation = Quaternion.Euler(PContext.xCurrentTilt, 0, 0);
    }



    public void Movement()
    {
        //Maps WASD to horizontal and vertical movement
        float horizontalMovement = Input.GetAxisRaw("Horizontal");
        float verticalMovement = Input.GetAxisRaw("Vertical");

        //Movement
        PlayerStateMachine.playerMovementDirection = PContext.playerGameObject.transform.forward * verticalMovement + PContext.playerGameObject.transform.right * horizontalMovement;

        //Prevents faster diagonal movement
        PlayerStateMachine.playerMovementDirection.Normalize();


        if (Input.GetButton("Sprint"))
        {
            PlayerStateMachine.playerMovementSpeed = PContext.playerSprintSpeed;

        }
        else
        {
            PlayerStateMachine.playerMovementSpeed = playerResetSpeed;
        }


        //Move direction and speed
        PContext.characterController.Move(PlayerStateMachine.playerMovementDirection * PlayerStateMachine.playerMovementSpeed * Time.deltaTime);

        //Gravity
        PContext.characterController.Move(Vector3.down * PContext.gravity * Time.deltaTime);

        //Move the character controller using inbuilt function
        PContext.characterController.Move(PContext.velocity * Time.deltaTime);
    }


    public void CameraSetup()
    {
        //Camera set up
        if (PContext.playerCamera != null)
        {
            //float mouseX = Input.GetAxis("Mouse X") * PContext.mouseSensitivityX;
            //float mouseY = Input.GetAxis("Mouse Y") * PContext.mouseSensitivityY;

            Vector2 mouseX = mouseJoystickLook.ReadValue<Vector2>();
            Vector2 mouseY = mouseJoystickLook.ReadValue<Vector2>();

            float floatMouseX = mouseX.x * PContext.mouseSensitivityX * Time.deltaTime;

            float floatMouseY = mouseY.y * PContext.mouseSensitivityY * Time.deltaTime;


            PContext.verticalRotation -= floatMouseY;
            //Clamp the vertical rotation to prevent flipping. Clamps A by given floats
            PContext.verticalRotation = Mathf.Clamp(PContext.verticalRotation, PContext.minLookAngleY, PContext.maxLookAngleY);

            // Rotate the player horizontally and with tilt
            PContext.playerCamera.localRotation = Quaternion.Euler(PContext.verticalRotation, 0, PContext.zCurrentTilt);
            PContext.playerGameObject.transform.Rotate(Vector3.up * floatMouseX);
            //transform.localRotation = Quaternion.Euler(mouseX, mouseY, currentTilt);
        }
    }


    public void Interact()
    {
        if (interact.WasPressedThisFrame())
        {
            Ray interactRay = new Ray(PContext.interactorSource.position, PContext.interactorSource.forward);

            if (Physics.Raycast(interactRay, out RaycastHit interactRayHitInfo, PContext.interactionRange))
            {
                Debug.DrawRay(PContext.interactorSource.position, PContext.interactorSource.forward * interactRayHitInfo.distance, Color.green, PContext.interactionMask);

                //check if the ray hits specific object with the tag below. in this instance, this is the computer.
                if (interactRayHitInfo.collider.CompareTag("InteractableWork"))
                {
                    changeToTransitionState = true;
                    Debug.Log("Work Interacted");
                }
                else if (interactRayHitInfo.collider.CompareTag("InteractableBlinds"))
                {
                    changeToBlindsState = true;
                    Debug.Log("Blinds Interacted");
                }
                //or any othjer interactable object that implements the IInteractable interface, in which case it will call the Interact function on that object
                else if (interactRayHitInfo.collider.gameObject.TryGetComponent(out IInteractable interactableObject))
                {
                    interactableObject.Interact();
                }
            }
        }
    }
          
}

