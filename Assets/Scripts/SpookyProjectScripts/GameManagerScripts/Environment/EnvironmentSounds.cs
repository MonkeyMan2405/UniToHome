using UnityEngine;

public class EnvironmentSounds : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SoundManager.PlayLoopingSound(SoundType.Monster, 1, 1f);
    }


}
