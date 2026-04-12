using UnityEngine;

public class MonsterCollision : MonoBehaviour
{
    [SerializeField]
    private bool hallway;
    [SerializeField]
    private bool wardrobe;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player has entered the monster's trigger area.");
            // You can add additional logic here, such as reducing player health or triggering an animation.

            MonsterStateMachine.triggerDoom = true;
            if (hallway)
            {
                MonsterStateMachine.hallwayDoom = true;
            }
            else if (wardrobe)
            {
                MonsterStateMachine.wardrobeDoom = true;
            }

               

        }
    }
   
  
}
