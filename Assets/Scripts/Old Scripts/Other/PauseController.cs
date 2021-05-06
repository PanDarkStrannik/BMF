﻿using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
public class PauseController
{
    public static bool isPaused=false;

    /// <summary>
    /// Поставить на паузу
    /// </summary>
    public static void Pause()
    {
        if (!isPaused)
        {
            DOTween.PauseAll();
            Time.timeScale = 0;
            isPaused = true;
        }
    }

    /// <summary>
    /// Снять с паузы
    /// </summary>
    public static void Resume()
    {
        if (isPaused)
        {
            DOTween.PlayAll();
            Time.timeScale = 1;
            isPaused = false;
        }
    }

    /// <summary>
    /// Выход из игры
    /// </summary>
    public static void Quit()
    {
        Application.Quit();
    }

    /// <summary>
    /// Рестарт сцены
    /// </summary>
    public static void Restart()
    {
        var scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.buildIndex);
        DOTween.Clear(true);
    }


    public static void ChangeTime(float value)
    {
        //Time.timeScale = value; эта штука меняет время на 1?? во время атаки черта
    }

}
