using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        //need to make my own
        SoundManager.PlaySound(SoundType.TensionRiser, 1f, 1f);
        SoundManager.PlaySound(SoundType.MonsterJumpscare, 0.3f, 1f);
        SoundManager.PlaySound(SoundType.MonsterShock, 0.5f, 0.6f);
        //yield return new WaitForSeconds(3.5f);
        yield return new WaitForSeconds(4);
        SoundManager.PlaySound(SoundType.Stab, 1f, 1f);
        yield return new WaitForSeconds(0.5f);
        StopAllCoroutines();
        SceneManager.LoadScene("MonsterDeathMenu", LoadSceneMode.Single);
        SceneManager.SetActiveScene(SceneManager.GetActiveScene());
    }
}
