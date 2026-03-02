using UnityEngine;

public class MoveCommand : Command

{
    private Rigidbody _rb;
    private Vector3 _movement;
    public Command moveCommand;
    public float jumpForceMultiplier;


    // Constructor

    // msets up parameters
    public MoveCommand(Rigidbody rigidbody, Vector3 movement)
    {
        //sets the rigidbody attatched to the player as _rb
        //sets the Vector 3 movement from the player as vector3 _movement
        //sets ju,p force multiplier
       _rb = rigidbody;
       _movement = movement;
       jumpForceMultiplier = 5;
    }

    public void Execute()
    {
        //executes
        _rb.AddForce(_movement * jumpForceMultiplier, ForceMode.Impulse);
    }
}
