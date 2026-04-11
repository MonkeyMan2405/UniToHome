using System.Collections;
using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{
    //this script will be innacurate if PlayerMovementSettings are Changed. Update Accordingly

    private float timeToWait;
    private float volume;
    private float pitch;

    public void Start()
    {
        StartCoroutine(PlayFootsteps());
    }

    public IEnumerator PlayFootsteps()
    {
        while (true)
        {
            if (PlayerStateMachine.playerMovementDirection.magnitude != 0)
            {

                //check if player was Sprinting
                if (PlayerStateMachine.playerMovementSpeed >= 4f)
                {
                    timeToWait = 0.35f;
                    //volume = Random.Range(0.8f, 1f);
                    pitch = Random.Range(0.6f, 1f);
                }

                else
                {
                    timeToWait = 0.7f;
                    //volume = Random.Range(0.1f, 1f);
                    pitch = Random.Range(0.1f, 0.4f);
                }

                SoundManager.PlaySoundAt(SoundType.PlayerStep, volume, pitch, gameObject.transform);

            }

            else
            {
                timeToWait = 0.1f;
            }

                yield return new WaitForSeconds(timeToWait);
            

        }
    }

}
