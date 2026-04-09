using System.Collections.Generic;
using Tripolygon.UModeler.UI.Converters;
using UnityEngine;
using UnityEngine.Audio;

public enum CustomSoundType
{
    CLOUDS,
    RAIN,
    BUNNY,
    EATEN,
    DAYCYCLE,
    WALKING,
    GEM,
}

public class KylieSoundCollection
{
    private AudioClip[] clips;
    private int lastClipIndex;

    // if you ctor and tab twice it builds a construtor for you 
    // using params for allowing variable arguments
    public KylieSoundCollection(params string[] clipNames)
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
public class KylieSoundManager : MonoBehaviour
{
    public float mainVolume = 1.0f;
    private Dictionary<CustomSoundType, KylieSoundCollection> sounds;
    private AudioSource audioSrc;

    public static KylieSoundManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        audioSrc = GetComponent<AudioSource>();
        sounds = new()
        {
            {CustomSoundType.CLOUDS, new KylieSoundCollection("wind") },
            {CustomSoundType.RAIN, new KylieSoundCollection("rain") },
            {CustomSoundType.BUNNY, new KylieSoundCollection("rabbit") },
            {CustomSoundType.EATEN, new KylieSoundCollection("wolf") },
            {CustomSoundType.DAYCYCLE, new KylieSoundCollection("chime") },
            {CustomSoundType.WALKING, new KylieSoundCollection("grassfootstep") },
            {CustomSoundType.GEM, new KylieSoundCollection("itemtwinkle") },

        };
    }


    public static void Play(CustomSoundType type, AudioSource extAudioSource = null, float pitch = -1.0f)
    {
        if (Instance.sounds.ContainsKey(type))
        {
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
