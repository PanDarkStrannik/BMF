using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private GameObject deathMenu;
    //[SerializeField] private GameObject mainMenu;
    //[SerializeField] private Text textPlayerMaxHP;
    //[SerializeField] private Text textPlayerHP;
    [SerializeField] private Image playerBar;
    [SerializeField] private GameObject damagedEffect;
    [SerializeField] private Animator damagedEffectAnim;
    //[SerializeField] private Text HealCDtext;
    //[SerializeField] private Image HealCDimage;

    //[SerializeField] private Text TPCDtext;
    //[SerializeField] private Image TPCDimage;

    [SerializeField] private float timeToGameOver = 3f;
    [SerializeField] private float timeDamagedAnim;

    [SerializeField] private Text score;

    /*if(спелл на кулдауне) HealCDtext.enabled = true;
     * else HealCDtext.enabled = false;
     * HealCDtext.text = (время перезарядки скилла).ToString();
     * HealCDimage.fillAmount = (оставшееся время перезарядки) / (время перезарядки);
     * то же самое и для тп
     */

    private bool alreadyDamaged = false;

    //private float maxPlayerHealth;

    public delegate void OnPlayerDeathEventHelper();
    public event OnPlayerDeathEventHelper OnPlayerDeathEvent;

    private void Start()
    {
        //TPCDimage.enabled = false;
        //TPCDtext.enabled = false;
        deathMenu.SetActive(false);
        damagedEffect.SetActive(false);

        score.text = "0";

        //maxPlayerHealth = PlayerInformation.GetInstance().PlayerParamController.MaxHealth;
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

    //public void InitializePlayerView(float maxPlayerHP)
    //{
    //    maxPlayerHealth = maxPlayerHP;
    //    textPlayerMaxHP.text = maxPlayerHP.ToString();
    //    textPlayerHP.text = maxPlayerHP.ToString();
    //}

    //public IEnumerator ReloadTP(float value)
    //{
    //    TPCDtext.enabled = true;
    //    TPCDimage.enabled = true;
    //    var tmp = value;
    //    for (float i = 0; i < value; i += Time.fixedDeltaTime)
    //    {
    //        TPCDtext.text =  ((int)tmp).ToString();
    //        TPCDimage.fillAmount = tmp / value;
    //        yield return new WaitForFixedUpdate();
    //        tmp -= Time.fixedDeltaTime;
    //    }
    //    TPCDtext.enabled = false;
    //    TPCDimage.enabled = false;
    //}

    private void ViewHealth(DamagebleParam.ParamType paramType, float value, float maxValue)
    {
        //textPlayerHP.text = currentHP.ToString();

        if (paramType == DamagebleParam.ParamType.Health)
        {
            if (value <= 0)
            {
                OnPlayerDeathEvent?.Invoke();
                StartCoroutine(GameOver());
            }
            //else
            //{
            //    if (!alreadyDamaged)
            //    {
            //        alreadyDamaged = true;
            //        damagedEffect.SetActive(true);
            //        damagedEffectAnim.SetTrigger("Damaged");
            //        StartCoroutine(ToDamagedEffectDisable());
            //    }
            //}
             playerBar.fillAmount = value / maxValue;
        }
    }

    private void OnPlayerParamDamagedViewer()
    {
        if (!alreadyDamaged)
        {
            alreadyDamaged = true;
            damagedEffect.SetActive(true);
            damagedEffectAnim.SetTrigger("Damaged");
            StartCoroutine(ToDamagedEffectDisable());
        }
    }

    private IEnumerator ToDamagedEffectDisable()
    {
        yield return new WaitForSeconds(timeDamagedAnim);
        alreadyDamaged = false;
        damagedEffect.SetActive(false);
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
