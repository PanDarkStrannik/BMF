using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class PlayerUI : MonoBehaviour
{
    [SerializeField] private GameObject deathMenu;
    [SerializeField] private Image healthBarFill;
    [SerializeField] private Image damagedImage;
    [SerializeField] private Image blackScreen;
    [SerializeField] private float timeToGameOver = 3f;
    [SerializeField] private Text score;
    [SerializeField] private Text viyAwakingText;
    [SerializeField] private Transform scoreEndPos;


    private bool alreadyDamaged = false;
    
    public delegate void OnPlayerDeathEventHelper();
    public event OnPlayerDeathEventHelper OnPlayerDeathEvent;

    private void OnEnable()
    {
        deathMenu.SetActive(false);
        damagedImage.enabled = false;
        blackScreen.DOFade(0f, 5f).OnComplete(() => blackScreen.enabled = false);
        PlayerUI_PointEvent(0);

        PlayerInformation.GetInstance().PlayerParamController.DamagebleParams.OnParamChanged += ViewHealth;
        PlayerInformation.GetInstance().PlayerParamController.DamagebleParams.OnParamsDamaged += OnPlayerParamDamagedViewer;
        PointCounter.Instance.PointEvent += PlayerUI_PointEvent;

    }

    private void OnDisable()
    {
        PointCounter.Instance.RefreshPoints();
        PlayerInformation.GetInstance().PlayerParamController.DamagebleParams.OnParamChanged -= ViewHealth;
        PlayerInformation.GetInstance().PlayerParamController.DamagebleParams.OnParamsDamaged -= OnPlayerParamDamagedViewer;
        PointCounter.Instance.PointEvent -= PlayerUI_PointEvent;
    }

    private void PlayerUI_PointEvent(int value)
    {
        score.text =  value + "/" + GlobalGameEvents.Instance.PointsToOver;
        if(value >= GlobalGameEvents.Instance.PointsToOver)
        {
            GameOverFade();
        }
    }

    //for pitch
    public void GameOverFade()
    {
        blackScreen.enabled = true;
        var fadeInSeq = DOTween.Sequence();
        fadeInSeq.Append(blackScreen.DOFade(1f, 3f).OnComplete(() => GlobalGameEvents.Instance.SwitchState(GameState.Over)));
        fadeInSeq.Append(blackScreen.DOFade(0f, 5f));
        fadeInSeq.AppendInterval(10f);
        fadeInSeq.Append(blackScreen.DOFade(1f, 5f).OnComplete(() => DOTween.Clear(true)).OnComplete(() => SceneManager.LoadSceneAsync(0)));

    }

    //for pitch
    public void ScoreAnimate()
    {
        var scoreSeq = DOTween.Sequence();
        scoreSeq.AppendInterval(3f);
        scoreSeq.Append(score.DOFade(1f, 4f).From(0f));
        scoreSeq.Append(score.transform.DOMove(scoreEndPos.position, 3f));
    }

    //for pitch
    public void GameOverText()
    {
        string text = "";
        DOTween.To(() => text, x => text = x, "Вий пробуждается", 8f).OnUpdate(() => viyAwakingText.text = text).OnComplete(() => viyAwakingText.DOFade(0f, 5f));
    }

    private void ViewHealth(DamagebleParam.ParamType paramType, float value, float maxValue)
    {
        if (paramType == DamagebleParam.ParamType.Health)
        {
            if (value <= 0)
            {
                OnPlayerDeathEvent?.Invoke();
                StartCoroutine(GameOver());
            }
            
             healthBarFill.fillAmount = value / maxValue;
        }
    }


    private void OnPlayerParamDamagedViewer()
    {
        if (!alreadyDamaged)
        {
            alreadyDamaged = true;
            damagedImage.enabled = true;
            StartCoroutine(ToDamagedEffectDisable());
        }
    }

    private IEnumerator ToDamagedEffectDisable()
    {
        damagedImage.DOFade(0, 1f);
        yield return new WaitForSeconds(1f);
        damagedImage.enabled = false;
        damagedImage.DOFade(1, 0);
        alreadyDamaged = false;
    }


    #region Управление состоянием игры
    private IEnumerator GameOver()
    {
        deathMenu.SetActive(true);
        //mainMenu.SetActive(false);
        yield return new WaitForSeconds(timeToGameOver);
        PauseController.Pause();
    }

   

    public void Resume()
    {
        PauseController.Resume();
        // menu.SetActive(false);
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

    #endregion


}
