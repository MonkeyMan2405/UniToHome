using UnityEngine;

public class BlindsState : PlayerState
{

    private Vector3 oldBlindsPos;
    private Vector3 newBlindsPos;
    private float blindsDownSpeed = 3f;
    private float blindsUpSpeed = 6f;

    public bool blindsClosed = false;

    private bool ableToClose;

    private float distanceToBlinds;
    private float blindsCheckDistance = 2.5f;
    private float blindsClosingDistance = 1.2f;

    private bool changeToStandardState;

    public BlindsState(PlayerStateContext _pcontext, PlayerStateMachine.EPlayerState state) : base(_pcontext, state)
    {
        PlayerStateContext PContext = _pcontext;
    }

    public override void EnterState()
    {
        oldBlindsPos = PContext.blinds.transform.position;
        newBlindsPos = oldBlindsPos + new Vector3(0, -1.1f, 0f);
    }



    public override void UpdateState()
    {
        CameraSetup();
        MoveBlinds();
        Movement();
        ForwardAndBackwardTilt();
        CameraTilt();
        checkDistance();
    }



    public override void ExitState()
    {
        changeToStandardState = false;
        ableToClose = true;
        PContext.blinds.transform.position = oldBlindsPos;
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



    //Checked every frame
    public override PlayerStateMachine.EPlayerState GetNextState()
    {
        if (changeToStandardState == true)
        {
            return PlayerStateMachine.EPlayerState.Standard;
        }
        return StateKey;
    }



    public void MoveBlinds()
    {
        Ray interactRay = new Ray(PContext.interactorSource.position, PContext.interactorSource.forward);

        //while pressing lmb, sends raycasts, if hits blinds, move them down, set blindsclosed to true, else, move back up and sewt blindsclosed false

        if (Input.GetMouseButton(0) && ableToClose == true)
        {
            if (Physics.Raycast(interactRay, out RaycastHit interactRayHitInfo, PContext.interactionRange))
            {
                if (interactRayHitInfo.collider.CompareTag("InteractableBlinds"))
                {
                    PContext.blinds.transform.position = Vector3.Lerp(PContext.blinds.transform.position, newBlindsPos, blindsDownSpeed * Time.deltaTime);
                    blindsClosed = true;
                }
                else
                {
                    PContext.blinds.transform.position = Vector3.Lerp(PContext.blinds.transform.position, oldBlindsPos, blindsUpSpeed * Time.deltaTime);
                    blindsClosed = false;
                }
            }
            else
            {
                PContext.blinds.transform.position = Vector3.Lerp(PContext.blinds.transform.position, oldBlindsPos, blindsUpSpeed * Time.deltaTime);
                blindsClosed = false;
            }
        }
        else
        {
            PContext.blinds.transform.position = Vector3.Lerp(PContext.blinds.transform.position, oldBlindsPos, blindsUpSpeed * Time.deltaTime);
            blindsClosed = false;
        }
    }



    public void checkDistance()
    {
        distanceToBlinds = Vector3.Distance(PContext.playerGameObject.transform.position, newBlindsPos);
        if (distanceToBlinds <= blindsClosingDistance)
        {
            ableToClose = true;
        }
        else if (distanceToBlinds >= blindsCheckDistance)
        {
            changeToStandardState = true;
        }
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


}
