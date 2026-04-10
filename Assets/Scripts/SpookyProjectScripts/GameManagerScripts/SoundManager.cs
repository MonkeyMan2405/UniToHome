using UnityEngine;
using System;

public enum SoundType
{
    Monster,
    Environment,
    Player,

}

[RequireComponent(typeof(AudioSource)), ExecuteInEditMode]
public class SoundManager : MonoBehaviour
{

    [SerializeField]
    private SoundList[] soundList;
    private static SoundManager instance;
    private AudioSource audioSource;

    [SerializeField]
    private AudioSource loopingAudioSource;


    private void Awake()
    {
        instance = this;
    }


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

#if UNITY_EDITOR
    private void OnEnable()
    {
        //searches enum array
        string[] names = Enum.GetNames(typeof(SoundType));
        Array.Resize(ref soundList, names.Length);

        for (int i = 0; i < soundList.Length; i++)
        {
            soundList[i].name = names[i];
        }
    }
#endif


    public static void PlaySound(SoundType sound, float volume = 1, float pitch = 1)
    {
        //parameters passed through ideally are above,

        AudioClip[] clips = instance.soundList[(int)sound].Sounds;
        AudioClip soundToPlay = clips[0];
        instance.audioSource.pitch = pitch;

        //Implement this is wanting randomness
        //AudioClip randomClip = clips[UnityEngine.Random.Range(0, clips.Length)];

        instance.audioSource.PlayOneShot(soundToPlay, volume);
    }


    public static void PlaySoundAt(SoundType sound, float volume = 1, float pitch = 1, Transform whereToPlay = null)
    {
        AudioClip[] clips = instance.soundList[(int)sound].Sounds;
        AudioClip soundToPlay = clips[0];
        instance.audioSource.pitch = pitch;

       AudioSource.PlayClipAtPoint(soundToPlay, whereToPlay.position);
    }


    public static void PlayLoopingSound(SoundType sound, float volume = 1, float pitch = 1)
    {

        AudioClip[] clips = instance.soundList[(int)sound].Sounds;
        AudioClip soundToPlay = clips[0];

        //ensure spatial blend of audio source is zero

        instance.loopingAudioSource.clip = soundToPlay;

        instance.loopingAudioSource.Play();

    }


}


[Serializable]
public struct SoundList
{
    //is returned when calling sound array
    public AudioClip[] Sounds { get => sounds; }
    //allows to set the group bname of the sounds in the inspector
    [HideInInspector]
    public string name;

    [SerializeField]
    private AudioClip[] sounds;

    
}
