using UnityEngine;

public class StandardState : PlayerState
{
    public PlayerStateMachine playerStateMachineRef;
    public CharacterController playerCharacterController;
    public Rigidbody playerRb;
    public GameObject playerGameObject;

    public StandardState(PlayerStateContext _pcontext, PlayerStateMachine.EPlayerState state) : base(_pcontext, state)
    {
        PlayerStateContext PContext = _pcontext;
    }

    public override void EnterState()
    {

    }



    public override void UpdateState()
    {
        CameraSetup();
        CameraTilt();
        ForwardAndBackwardTilt();
        Movement();
    }



    public override void ExitState()
    {

    }



    public override void OnTriggerEnter(Collider other)
    {

    }



    public override void OnTriggerExit(Collider other)
    {

    }



    public override void OnTriggerStay(Collider other)
    {

    }




    public override PlayerStateMachine.EPlayerState GetNextState()
    {
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
        Vector3 moveDirection = PContext.playerGameObject.transform.forward * verticalMovement + PContext.playerGameObject.transform.right * horizontalMovement;
 
        //Prevents faster diagonal movement
        moveDirection.Normalize();

        float speed = PContext.playerWalkSpeed;
        if (Input.GetAxis("Sprint") > 0)
        {
            speed *= PContext.playerRunMultiplier;
        }


        //Something
        PContext.characterController.Move(moveDirection * speed * Time.deltaTime);
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
            float mouseX = Input.GetAxis("Mouse X") * PContext.mouseSensitivityX;
            float mouseY = Input.GetAxis("Mouse Y") * PContext.mouseSensitivityY;

            PContext.verticalRotation -= mouseY;
            //Clamp the vertical rotation to prevent flipping. Clamps A by given floats
            PContext.verticalRotation = Mathf.Clamp(PContext.verticalRotation, PContext.minLookAngleY, PContext.maxLookAngleY);

            // Rotate the player horizontally and with tilt
            PContext.playerCamera.localRotation = Quaternion.Euler(PContext.verticalRotation, 0, PContext.zCurrentTilt);
            PContext.playerGameObject.transform.Rotate(Vector3.up * mouseX);
            //transform.localRotation = Quaternion.Euler(mouseX, mouseY, currentTilt);
        }
    }
}
