using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "New Audio Preset", menuName = "Audio Preset")]
public class SOAudio : ScriptableObject
{
    [SerializeField] private List<Music> music = new List<Music>();
    [SerializeField] private List<Sound> sounds = new List<Sound>();
    private float currentTime;


    public void Init(GameObject obj, AudioSource source)
    {
        #region Sounds Init

        if (sounds.Count > 0)
        {
            foreach (var s in sounds)
            {

                if (s.AudioClip == null && s.AudioClips.Length == 0)
                {
                    Debug.LogError($"{s.Name} doesn't have any clips in array");
                }
                else
                {
                    s.AudioSource = source;
                    s.AudioSource.outputAudioMixerGroup = s.soundMixer;
                    s.AudioSource.clip = s.AudioClip;
                    if (s.AudioClips.Length > 0)
                    {
                        s.AudioSource.clip = s.AudioClips[Random.Range(0, s.AudioClips.Length)];
                    }
                    s.AudioSource.volume = s.Volume;
                    s.AudioSource.pitch = s.Pitch;
                    s.AudioSource.spatialBlend = s.SpatialBlend;
                    s.AudioSource.playOnAwake = s.PlayOnAwake;
                    s.AudioSource.loop = s.Looping;
                }

            }
        }
        #endregion

        #region Music Init

        if (music.Count > 0)
        {
            foreach (var m in music)
            {
                if (m.AudioClip1 == null)
                {
                    Debug.LogError($"{m.Name} doesn't have any clips in array");
                }
                else
                {
                    m.AudioSource = source;
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
                    source.Play();
                    source.DOFade(m.Volume, m.FadeInTime).From(0f).SetAutoKill(false);
                }
            }
        }

        #endregion
    }

    #region PUBLIC METHODS
    public void PlayOneShot(string name, AudioSource source)
    {
        PlaySoundOneShot(name, source);
    }

    public void PlayOneShot(MonoBehaviour monoBehObj,string name, float delay, AudioSource source)
    {
        monoBehObj.StartCoroutine(OneShotAudioDelayed(name, delay,source));
    }

    public void PlayOneShotWithTime(MonoBehaviour monoBehObj, string name, float inBetweenTime, AudioSource source)
    {
        monoBehObj.StartCoroutine(OneShotAudioWithTime(name, inBetweenTime, source));
    }

    public void PlayMusicByName(string name, AudioSource source)
    {
        PlayMusic(name, source);
    }

    public void PlayMusicWithFade(MonoBehaviour monoBehObj, string name, AudioSource source, float transitionTime)
    {
        monoBehObj.StartCoroutine(FadeMusicAndPlayNext(name, source, transitionTime));
    }

    #endregion

    #region PRIVATE METHODS AND ENUMERATORS
    private void PlaySoundOneShot(string name, AudioSource source)
    {
        if (sounds.Count > 0)
        {
            foreach (var s in sounds)
            {
                if (s.Name == name)
                {
                    source.volume = Random.Range(s.MinVolume, s.MaxVolume);
                    source.pitch = Random.Range(s.MinPitch, s.MaxPitch);
                    if (s.AudioClips.Length > 0)
                    {
                        source.PlayOneShot(s.AudioClips[Random.Range(0, s.AudioClips.Length)]);
                        source.clip = s.AudioClips[Random.Range(0, s.AudioClips.Length)];
                    }
                    if (s.AudioClip != null)
                    {
                        source.clip = s.AudioClip;
                        source.PlayOneShot(s.AudioClip);
                    }
                }
            }
        }
    }

    private void PlayMusic(string name, AudioSource source)
    {
        if (music.Count > 0)
        {
            foreach (var m in music)
            {
                if (m.Name == name && m.AudioClip1 != null)
                {
                    source.Play();
                }
            }
        }
    }

    private IEnumerator FadeMusicAndPlayNext(string name, AudioSource source, float transitionTime = 1f)
    {
        foreach (var m in music)
        {
            if (m.AudioClip1 != null && m.AudioClip2 != null)
            {
                if (source.isPlaying)
                {
                    for (float i = 0; i < transitionTime; i += Time.deltaTime)
                    {
                        source.volume = (m.Volume - (i / transitionTime));
                        yield return null;
                    }
                }
                source.Stop();
                source.clip = m.AudioClip2;
                source.Play();

                for (float i = 0; i < transitionTime; i += Time.deltaTime)
                {
                    source.volume = ((i / transitionTime) * 1);
                    yield return null;
                }
            }

        }
    }




    private IEnumerator OneShotAudioDelayed(string name, float delay, AudioSource source)
    {
        yield return new WaitForSeconds(delay);
        PlaySoundOneShot(name,source);
    }

    private IEnumerator OneShotAudioWithTime(string name, float inBetweenTime, AudioSource source)
    {
        currentTime -= Time.deltaTime;
        if (currentTime <= 0)
        {
            PlaySoundOneShot(name,source);
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
    [Range(0, 1)] public float Volume;
    [Range(0.5f, 3)] public float Pitch;
    [Range(0, 1)] public float SpatialBlend;
    public bool PlayOnAwake;
    public bool Looping;

    [Header("Random Params")]
    [Range(0.01f, 1)] public float MinVolume;
    [Range(0.01f, 1)] public float MaxVolume;
    [Range(0.5f, 3)] public float MinPitch;
    [Range(0.5f, 3)] public float MaxPitch;

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
    [Range(0, 50)] public float FadeInTime;
    public bool isLopping;
    public bool PlayOnAwake;
    public bool PlayWithFade;

    [HideInInspector] public AudioSource AudioSource;
}

