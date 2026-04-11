using System.Collections;
using UnityEngine;

public class PlayerDoom : MonoBehaviour
{

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            StartCoroutine(DoomPlayer());
        }
      
    }

    private void OnEnable()
    {
        MonsterDoomState.PlayerDoom.AddListener(StartDoom);
    }

    private void OnDisable()
    {
        MonsterDoomState.PlayerDoom.RemoveListener(StartDoom);
    }


    public void StartDoom()
    {
       StartCoroutine(DoomPlayer());
    }

    public IEnumerator DoomPlayer()
    {
        SoundManager.PlaySound(SoundType.TensionRiser, 1f, 1f);
        yield return new WaitForSeconds(1f);
        //SoundManager.PlaySound(SoundType.MonsterJumpscare, 0.6f, 1f);
        SoundManager.PlaySound(SoundType.MonsterShock, 1f, 0.6f);
        yield return new WaitForSeconds(3.5f);
        //SoundManager.PlaySound(SoundType.Stab, 0.6f, 1f);
        StopAllCoroutines();
    }
}
