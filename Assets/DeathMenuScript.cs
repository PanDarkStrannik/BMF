using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathMenuScript : MonoBehaviour
{
    [SerializeField] private Text score;

    private void Start()
    {
        PlayerPointsListener(0);
        PointCounter.Instance.PointEvent += PlayerPointsListener;
    }

    private void PlayerPointsListener(int value)
    {
        score.text = $"YOUR SCORE: {value}";
    }


    public void Restart()
    {
        PauseController.Restart();
        PauseController.Resume();
    }
    public void Quit()
    {
        PauseController.Quit();
    }

}
