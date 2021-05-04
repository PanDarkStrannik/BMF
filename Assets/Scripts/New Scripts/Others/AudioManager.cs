using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using DG.Tweening;
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
    }

    #endregion

    [SerializeField] private List<Music> music = new List<Music>();
    [SerializeField] private List<Sound> sounds = new List<Sound>();
    private float currentTime;


    private void Start()
    {
        Init();
    }

    private void Init()
    {
        #region Sounds Init

        if (sounds.Count > 0)
        {
             foreach (var s in sounds)
             {
                 if (s.OriginOfTheSound == null)
                 {
                     s.OriginOfTheSound = this.transform;
                 }
            
                 if(s.AudioClip == null && s.AudioClips.Length == 0)
                 {
                     Debug.LogError($"{s.Name} doesn't have any clips in array");
                 }
                 else
                 {
                    s.AudioSource = s.OriginOfTheSound.gameObject.AddComponent<AudioSource>();
                    s.AudioSource.outputAudioMixerGroup = s.soundMixer;
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
        #endregion

        #region Music Init
        
        if(music.Count > 0)
        {
            foreach (var m in music)
            {
                if(m.AudioClip1 == null)
                {
                    Debug.LogError($"{m.Name} doesn't have any clips in array");
                }
                else
                {
                   m.AudioSource = gameObject.AddComponent<AudioSource>();
                    m.AudioSource.outputAudioMixerGroup = m.musicMixer;
                   m.AudioSource.clip = m.AudioClip1;
                   m.AudioSource.volume = m.Volume;
                   m.AudioSource.loop = m.isLopping;
                   m.AudioSource.playOnAwake = m.PlayOnAwake;
                }

                if (m.PlayOnAwake)
                    m.AudioSource.Play();

                if (m.PlayWithFade)
                {
                    m.AudioSource.Play();//убивает твин моментально
                    m.AudioSource.DOFade(m.Volume, 5f).From(0).Kill(true);
                }
            }
        }

        #endregion
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

    public void PlayMusicByName(string name)
    {
        PlayMusic(name);
    }

    public void PlayMusicWithFade(string name, float time)
    {
        StartCoroutine(FadeMusicAndPlayNext(name, time));
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
                    s.Volume = Random.Range(s.MinVolume, s.MaxVolume);
                    s.Pitch = Random.Range(s.MinPitch, s.MaxPitch);
                    if(s.AudioClips.Length > 0)
                    {
                        s.AudioSource.PlayOneShot(s.AudioClips[Random.Range(0, s.AudioClips.Length)]);
                        s.AudioSource.clip = s.AudioClips[Random.Range(0, s.AudioClips.Length)];
                    }
                    if(s.AudioClip != null)
                       s.AudioSource.PlayOneShot(s.AudioClip);
               }
           }
        }
    }

    private void PlayMusic(string name)
    {
        if(music.Count > 0)
        {
            foreach (var m in music)
            {
                if(m.Name == name && m.AudioClip1 != null)
                {
                    m.AudioSource.Play();
                }
            }
        }
    }


    private IEnumerator FadeMusicAndPlayNext(string name, float transitionTime = 1f)
    {
        foreach (var m in music)
        {
            if (m.AudioClip1 != null && m.AudioClip2 != null)
            {
                if (m.AudioSource.isPlaying)
                {
                   for (float i = 0; i < transitionTime; i+= Time.deltaTime)
                   {
                       m.AudioSource.volume = (m.Volume - (i / transitionTime));
                       yield return null;
                   }
                }
                m.AudioSource.Stop();
                m.AudioSource.clip = m.AudioClip2;
                m.AudioSource.Play();

                for (float i = 0; i < transitionTime; i += Time.deltaTime)
                {
                  m.AudioSource.volume =  ((i/ transitionTime) * 1);
                  yield return null;
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
    public AudioMixerGroup soundMixer;

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

[System.Serializable]
public class Music
{
    public string Name;
    public AudioMixerGroup musicMixer;

    [Header("Clip")]
    public AudioClip AudioClip1;
    public AudioClip AudioClip2;

    [Header("Audio Settings")]
    [Range(0, 1)] public float Volume;
    public bool isLopping;
    public bool PlayOnAwake;
    public bool PlayWithFade;

    [HideInInspector] public AudioSource AudioSource;




}