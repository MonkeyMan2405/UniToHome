using Unity.VisualScripting;
using UnityEngine;

public class CommandPlayerController : MonoBehaviour
{
    private MoveCommand _jumpCommand;
    private MoveCommand _moveForwardCommand;
    private MoveCommand _moveLeftCommand;
    private MoveCommand _moveBackwardCommand;
     private MoveCommand _moveRightCommand;

    public Rigidbody rb;
    Vector3 forwardMovement;
    Vector3 backwardMovement;
    
    Vector3 leftMovement;
    
    Vector3 rightMovement;
    Vector3 jumpMovement;
    



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //set rigidbody as rb. if null, add rigidbody component.
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
           rb= gameObject.AddComponent<Rigidbody>();
        }

        //set up the movement vectors properly. set up the movement commands with correct parameters.
        jumpMovement = new Vector3(0,1,0);
        _jumpCommand = new MoveCommand(rb,jumpMovement);


        forwardMovement = new Vector3(0,0,1);
        _moveForwardCommand = new MoveCommand(rb,forwardMovement);


        backwardMovement = new Vector3(0,0,-1);
        _moveBackwardCommand = new MoveCommand(rb, backwardMovement);

        leftMovement = new Vector3(-1,0,0);
        _moveLeftCommand = new MoveCommand(rb, leftMovement);

        rightMovement = new Vector3(1,0,0);
        _moveRightCommand = new MoveCommand(rb, rightMovement);

        
    }

    
    
    // Update is called once per frame
    void Update()
    {
        //press button to execute command.
        if (Input.GetKeyDown(KeyCode.Space))
        {
           _jumpCommand.Execute();
        }


        if (Input.GetKeyDown(KeyCode.W))
        {
           _moveForwardCommand.Execute();
        }


        if (Input.GetKeyDown(KeyCode.A))
        {
            _moveLeftCommand.Execute();
        }


        if (Input.GetKeyDown(KeyCode.S))
        {
            _moveBackwardCommand.Execute();
        }


        if (Input.GetKeyDown(KeyCode.D))
        {
            _moveRightCommand.Execute();
        }

    }

    
// ask shurray how this executes. across scripts.
}
