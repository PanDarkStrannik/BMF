using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private GameObject deathMenu;
    [SerializeField] private Image healthBarFill;
    [SerializeField] private Image damagedImage;
    [SerializeField] private float timeToGameOver = 3f;

    [SerializeField] private Text score;


    private bool alreadyDamaged = false;
    
    public delegate void OnPlayerDeathEventHelper();
    public event OnPlayerDeathEventHelper OnPlayerDeathEvent;

    private void Start()
    {
        deathMenu.SetActive(false);
        damagedImage.enabled = false;

        score.text = "0";

        PlayerInformation.GetInstance().PlayerParamController.DamagebleParams.OnParamChanged += ViewHealth;
        PlayerInformation.GetInstance().PlayerParamController.DamagebleParams.OnParamsDamaged += OnPlayerParamDamagedViewer;
        PointCounter.Instance.PointEvent += PlayerUI_PointEvent;

    }

    private void OnDestroy()
    {
        PointCounter.Instance.RefreshPoints();
        PlayerInformation.GetInstance().PlayerParamController.DamagebleParams.OnParamChanged -= ViewHealth;
        PlayerInformation.GetInstance().PlayerParamController.DamagebleParams.OnParamsDamaged -= OnPlayerParamDamagedViewer;
        PointCounter.Instance.PointEvent -= PlayerUI_PointEvent;
    }

    private void PlayerUI_PointEvent(int value)
    {
        score.text = value.ToString();
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
