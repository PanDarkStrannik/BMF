using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    #region SINGLETON
    public static AudioManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(this);
    }

    #endregion

    [SerializeField] private List<Sound> sounds = new List<Sound>();
    private float currentTime;


    private void Start()
    {
        Init();
    }

    private void Init()
    {
        if(sounds.Count > 0)
        {
             foreach (var s in sounds)
             {
                 if (s.OriginOfTheSound == null)
                 {
                     s.OriginOfTheSound = this.transform;
                 }
            
                 if(s.AudioClip == null && s.AudioClips.Length == 0)
                 {
                     Debug.LogError($"{s.Name} DOESN'T HAVE ANY SOUNDS IN ARRAY");
                 }
                 else
                 {
                    s.AudioSource = s.OriginOfTheSound.gameObject.AddComponent<AudioSource>();
                    s.AudioSource.clip = s.AudioClip;
                     if(s.AudioClips.Length > 0)
                     {
                         s.AudioSource.clip = s.AudioClips[Random.Range(0, s.AudioClips.Length)];
                     }
                    s.AudioSource.volume = s.Volume;
                    s.AudioSource.pitch = s.Pitch;
                    s.AudioSource.loop = s.isLooping;
                    s.AudioSource.playOnAwake = s.PlayOnAwake;
                    s.AudioSource.spatialBlend = s.SpatialBlend;
                 }
            
             }
        }
    }


    #region PUBLIC METHODS
    public void PlayOneShot(string name)
    {
        PlaySoundOneShot(name);
    }

    public void PlayOneShot(string name, float delay)
    {
        StartCoroutine(OneShotAudioDelayed(name, delay));
    }

    public void PlayOneShotWithTime(string name, float inBetweenTime)
    {
        StartCoroutine(OneShotAudioWithTime(name, inBetweenTime));
    }

    #endregion

    #region PRIVATE METHODS AND ENUMERATORS
    private void PlaySoundOneShot(string name)
    {
        if(sounds.Count > 0)
        {
           foreach (var s in sounds)
           {
               if (s.Name == name)
               {
                    s.AudioSource.PlayOneShot(s.AudioClip);
                    s.Volume = Random.Range(s.MinVolume, s.MaxVolume);
                    s.Pitch = Random.Range(s.MinPitch, s.MaxPitch);
                    if(s.AudioClips.Length > 0)
                    {
                        s.AudioSource.PlayOneShot(s.AudioClips[Random.Range(0, s.AudioClips.Length)]);
                        s.AudioSource.clip = s.AudioClips[Random.Range(0, s.AudioClips.Length)];
                    }
               }
           }
        }
    }
    private IEnumerator OneShotAudioDelayed(string name, float delay)
    {
        yield return new WaitForSeconds(delay);
        PlaySoundOneShot(name);
    }

    private IEnumerator OneShotAudioWithTime(string name, float inBetweenTime)
    {
        currentTime -= Time.deltaTime;
        if(currentTime <= 0)
        {
            PlaySoundOneShot(name);
            currentTime = inBetweenTime;
        }
        yield return new WaitForEndOfFrame();
    }



    #endregion


}

[System.Serializable]
public class Sound
{

    public string Name;

    [Header("Clips")]
    public AudioClip[] AudioClips;
    public AudioClip AudioClip;

    [Header("Audio Settings")]
    [Range(0,1)] public float Volume;
    [Range(0.5f,3)] public float Pitch;
    [Range(0, 1)] public float SpatialBlend;


    [Header("Random Params")]

    [Range(0.01f, 1)] public float MinVolume;
    [Range(0.01f, 1)] public float MaxVolume;
    [Range(0.5f, 3)] public float MinPitch;
    [Range(0.5f, 3)] public float MaxPitch;

    [Header("Other")]
    public Transform OriginOfTheSound;
    public bool isLooping;
    public bool PlayOnAwake;

   [HideInInspector] public AudioSource AudioSource;

}