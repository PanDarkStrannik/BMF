using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using System;
using UnityEngine.Events;
using DG.Tweening;
public class SimpleMenuController : MonoBehaviour
{
    [SerializeField] private AudioMixer mainMixer;
    [SerializeField] private UnityEvent OnLoadingEnter;

    public event Action<float> OnProgressChanged;

    private void Awake()
    {
        PauseController.Resume();
    }

    public void SelectSceneAsync(int levelIndex)
    {
        DOTween.Clear(true);
        StartCoroutine(LoadingScene(levelIndex));
    }

    public void SelectScene(int levelIndex)
    {
        DOTween.Clear(true);
        SceneManager.LoadScene(levelIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }


    private IEnumerator LoadingScene(int levelIndex)
    {
        var loadedLevel = SceneManager.LoadSceneAsync(levelIndex);

        OnLoadingEnter?.Invoke();
        while(!loadedLevel.isDone)
        {
            float loadedProgress = Mathf.Clamp01(loadedLevel.progress / 0.9f);
            OnProgressChanged?.Invoke(loadedProgress);
            yield return null;
        }
    }

    #region Settings
    public void SetMusicVolume(float volume)
    {
        mainMixer.SetFloat("MusicVolume", volume);
    }

    public void SetSoundVomule(float volume)
    {
        mainMixer.SetFloat("SoundVolume", volume);
    }

    public void SetFullScreen(bool fullScreen)
    {
        Screen.fullScreen = !fullScreen;
    }

    #endregion
}
