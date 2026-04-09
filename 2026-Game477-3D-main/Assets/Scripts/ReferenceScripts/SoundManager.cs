using System.Collections.Generic;
using Tripolygon.UModeler.UI.Converters;
using UnityEngine;
using UnityEngine.Audio;

public enum SoundType
{
    TUMBLE,
    WALKING,
    PICKUP, // trailing commas are allowed in C# 
}

// you can have several helper classes but they 
// cannot be attached to game objects without inheriting from MonoBehavior 
public class SoundCollection
{
    private AudioClip[] clips;
    private int lastClipIndex;

    // if you ctor and tab twice it builds a construtor for you 
    // using params for allowing variable arguments
    public SoundCollection(params string[] clipNames)
    {
        this.clips = new AudioClip[clipNames.Length];
        for (int i = 0; i < clipNames.Length; i++)
        {
            // unity goes through folder named specifically Resources 
            // to be able to dynamically load files of certain names 
            clips[i] = Resources.Load<AudioClip>(clipNames[i]);
            if (clips[i] == null)
            {
                // print is an alias for Debug.Log that is only available 
                // in scripts that inherit from MonoBehavior 
                // Debug.Log is available anywhere 
                Debug.LogError("dynamically loaded clip is null"); // note self reported errors won't crash the game 
            }
        }
        lastClipIndex = -1;
    }

    public AudioClip GetRandomClip()
    {
        if (clips.Length == 0)
        {
            Debug.LogWarning("Must have at least one clip");
            return null; // <- don't do this. This is bad. 
        }

        else if (clips.Length == 1)
        {
            return clips[0];
        }

        else
        {
            int index = lastClipIndex;
            while (index == lastClipIndex)
            {
                index = Random.Range(0, clips.Length);
            }
            lastClipIndex = index;
            return clips[index];
        }
    }
}



// you can have several public classes, only the one inhereiting from
// MonoBehavior needs to match the file name
// inheriting from MonoBehavior lets you attach it to a game object

// forces an audio source component onto the sound manager 
[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    public float mainVolume = 1.0f;
    private Dictionary<SoundType, SoundCollection> sounds;
    private AudioSource audioSrc;

    // making singleton so it can be accessed from anywhere directly 
    public static SoundManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        audioSrc = GetComponent<AudioSource>();
        // you don't have to put anything after new in C#
        sounds = new()
        {
            {SoundType.PICKUP, new SoundCollection("hitShield") },
            {SoundType.TUMBLE, new SoundCollection("superLaserShoot") }, // have multiple here 
            {SoundType.WALKING, new SoundCollection("explosion") },
        };
    }


    public static void Play(SoundType type, AudioSource extAudioSource = null, float pitch = -1.0f)
    {
        if (Instance.sounds.ContainsKey(type))
        {
            //var audioSrcToPlayFrom = extAudioSrc == null ? Instance.audioSrc : extAudioSource; 
            //var audioSrcToPlayFrom = extAudioSource ?? Instance.audioSrc; 
            extAudioSource ??= Instance.audioSrc; // called the null propagation operator 
            extAudioSource.volume = Random.Range(0.7f, 1.0f) * Instance.mainVolume;
            extAudioSource.pitch = pitch >= 0 ? pitch : Random.Range(0.75f, 1.25f);
            extAudioSource.clip = Instance.sounds[type].GetRandomClip();
            // debug 
            print("played sound");
            extAudioSource.Play(); 

        }
    }
}
