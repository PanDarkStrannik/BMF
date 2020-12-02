using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathMenuScript : MonoBehaviour
{
    [SerializeField] private Text score;

    private void Start()
    {
        PlayerDamagedListener();
        PlayerInformation.GetInstance().PlayerParamController.PlayerDamaged += delegate { PlayerDamagedListener(); };     
    }

    private void PlayerDamagedListener()
    {
        score.text = $"YOUR SCORE: {PointCounter.GetPointCounter().Points}";
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
