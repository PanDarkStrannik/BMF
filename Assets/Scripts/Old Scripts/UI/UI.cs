using System.Collections;
using UnityEngine.UI;
using UnityEngine;


/// <summary>
/// Игровое меню и отображение параметров
/// </summary>
public class UI : MonoBehaviour
{
    [SerializeField] private GameObject deathMenu;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private Text textPlayerMaxHP;
    [SerializeField] private Text textPlayerHP;
    [SerializeField] private Image imagePlayerHP;
    [SerializeField] private Image imagePlayerMaxHP;

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

    public static float playerHealth=100f;
    public static float bossHealth=100f;
    private float maxPlayerHealth = 100f;
    private float maxBossHealth = 100f;

    private void Start()
    {
        maxPlayerHealth = playerHealth;

        textPlayerMaxHP.text = maxPlayerHealth.ToString();
        textPlayerHP.text = playerHealth.ToString();

        imagePlayerMaxHP.fillAmount = maxPlayerHealth;
        imagePlayerHP.fillAmount = playerHealth;
        //maxBossHealth = bossHealth;
        deathMenu.SetActive(false);
    }



   

    private void LateUpdate()
    {
        HealthView();
    }

    // #region Отображение в UI

    /// <summary>
    /// Функция отображения здоровья
    /// </summary>
    public void HealthView()
    {
        if(playerHealth<=0)
        {
            playerHealth = 0;
            StartCoroutine(GameOver());
        }
        textPlayerHP.text = playerHealth.ToString();
        imagePlayerHP.fillAmount = playerHealth/maxPlayerHealth;
    
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
        maxPlayerHealth = playerHealth;
    }
    public void Quit()
    {
        PauseController.Quit();
    }

    #endregion
   
}


