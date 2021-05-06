using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class DeathMenuScript : MonoBehaviour
{
    [SerializeField] private Text score;
    [SerializeField] private Text restartButton;
    [SerializeField] private Image backGround;
    [SerializeField] private Image circle;
    [SerializeField] private Image blood;


    private void OnEnable()
    {
        var uiAnimSeq = DOTween.Sequence();
        uiAnimSeq.Append(backGround.DOFade(1f, 5f).From(0f));
        uiAnimSeq.Join(restartButton.DOFade(1f, 5f));
        uiAnimSeq.Join(score.DOFade(1f, 5f).From(0f));
        uiAnimSeq.Join(circle.DOFillAmount(1f, 5f).From(0));
        uiAnimSeq.Join(blood.DOFade(1f, 3f).SetEase(Ease.Linear).From(0f));
        TotalScore(PointCounter.Instance.Points);
    }


    private void TotalScore(int value)
    {
        score.text = "Ваша святость : " + value;
    }


    public void Restart()
    {
        PauseController.Restart();
    }
}
