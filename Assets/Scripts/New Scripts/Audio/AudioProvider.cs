using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioProvider : MonoBehaviour
{
    [SerializeField] private SOAudio audioPreset;
    [SerializeField] private AudioSource source;

    private void Awake()
    {
        audioPreset.Init(this.gameObject, source);
    }

    #region Sounds

    public void PlayOneShot(string name)
    {
        audioPreset.PlayOneShot(name, source);
    }

    public void PlayOneShot(string name, float delay)
    {
        audioPreset.PlayOneShot(this, name, delay, source);
    }

    public void PlayOneShotWithTime(string name, float timeInBetween)
    {
        audioPreset.PlayOneShotWithTime(this, name, timeInBetween, source);
    }

    #endregion

    #region Music

    public void PlayMusicByName(string name)
    {
        audioPreset.PlayMusicByName(name, source);
    }

    public void PlayMusicWithFade(string name, float transitionTime)
    {
        audioPreset.PlayMusicWithFade(this, name,source, transitionTime);
    }

    #endregion
}
