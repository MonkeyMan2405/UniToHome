using UnityEngine;
using System;

public enum SoundType
{
    PlayerStep,
    PlayerFlashlight,

    AirAmbience,
    HorrorAmbience,
    HorrorStinger,
    HorrorRiser,

    MonsterJumpscare,
    MonsterLeave,
    MonsterClimbWindow,
    Stab,
    HeavyFootsteps,
    MonsterSee,

    DoorOpen,
    DoorClose,
    ClosetOpen,
    ClosetClose,
    DoorSlam,
    Blinds,

    MonitorRight,
    MonitorWrong,
    LightFlick,
    MonitorPattern,

    MonsterShock,
    TensionRiser,

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


    public static void PlaySound(SoundType sound, float volume, float pitch)
    {
        //parameters passed through ideally are above,

        AudioClip[] clips = instance.soundList[(int)sound].Sounds;
        AudioClip soundToPlay = clips[0];
        instance.audioSource.pitch = pitch;

        //Implement this is if wanting randomness
        //AudioClip randomClip = clips[UnityEngine.Random.Range(0, clips.Length)];

        instance.audioSource.PlayOneShot(soundToPlay, volume);
    }


    public static void PlaySoundAt(SoundType sound, float volume, float pitch, Transform whereToPlay = null)
    {
        AudioClip[] clips = instance.soundList[(int)sound].Sounds;
        AudioClip soundToPlay = clips[0];

        AudioSource.PlayClipAtPoint(soundToPlay, whereToPlay.position, pitch);

    }


    public static void PlayLoopingSound(SoundType sound, float volume, float pitch)
    {

        AudioClip[] clips = instance.soundList[(int)sound].Sounds;
        AudioClip soundToPlay = clips[0];

        //ensure spatial blend of audio source is zero

        instance.loopingAudioSource.clip = soundToPlay;
        instance.loopingAudioSource.volume = volume;
        instance.loopingAudioSource.pitch = pitch;

        //different audio source than the rest. don't conmfuse it
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
