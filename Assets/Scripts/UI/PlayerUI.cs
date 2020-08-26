using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private GameObject deathMenu;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private Text textPlayerMaxHP;
    [SerializeField] private Text textPlayerHP;
    [SerializeField] private Image playerBar;

    [SerializeField] private Text HealCDtext;
    [SerializeField] private Image HealCDimage;

    [SerializeField] private Text TPCDtext;
    [SerializeField] private Image TPCDimage;

    /*if(спелл на кулдауне) HealCDtext.enabled = true;
     * else HealCDtext.enabled = false;
     * HealCDtext.text = (время перезарядки скилла).ToString();
     * HealCDimage.fillAmount = (оставшееся время перезарядки) / (время перезарядки);
     * то же самое и для тп
     */

    private float maxPlayerHealth;


    private void Start()
    {
        TPCDimage.enabled = false;
        TPCDtext.enabled = false;
        deathMenu.SetActive(false);
    }

    public void InitializePlayerView(float maxPlayerHP)
    {
        maxPlayerHealth = maxPlayerHP;
        textPlayerMaxHP.text = maxPlayerHP.ToString();
        textPlayerHP.text = maxPlayerHP.ToString();
    }

    public IEnumerator ReloadTP(float value)
    {
        TPCDtext.enabled = true;
        TPCDimage.enabled = true;
        var tmp = value;
        for (float i = 0; i < value; i += Time.fixedDeltaTime)
        {
            TPCDtext.text =  ((int)tmp).ToString();
            TPCDimage.fillAmount = tmp / value;
            yield return new WaitForFixedUpdate();
            tmp -= Time.fixedDeltaTime;
        }
        TPCDtext.enabled = false;
        TPCDimage.enabled = false;
    }

    public void ViewHealth(float currentHP)
    {
        textPlayerHP.text = currentHP.ToString();
        playerBar.fillAmount = currentHP / maxPlayerHealth;
        if (currentHP <= 0)
        {
            StartCoroutine(GameOver());
        }
    }




    #region Управление состоянием игры
    private IEnumerator GameOver()
    {
        deathMenu.SetActive(true);
        mainMenu.SetActive(false);
        yield return new WaitForSeconds(2f);
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
