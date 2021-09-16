using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BossUI : MonoBehaviour
{
    [SerializeField] private GameObject currentBossUI;
    [SerializeField] private Image bossIcon;
    [SerializeField] private Text bossName;
    [SerializeField] private Text textBossMaxHP;
    [SerializeField] private Text textBossCurrentHP;
    [SerializeField] private Image bossBar;
    [SerializeField] private EnemyDetectionController detection;

    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject winMenu;
    [SerializeField] private List<MonoBehaviour> deactiveScripts;
    [SerializeField] private float timeToDeactive=5f;

    private float maxBossHP=0f;

    public delegate void OnBossDeathEventHelper();
    public event OnBossDeathEventHelper OnBossDeathEvent;

    private void Start()
    {
        currentBossUI.SetActive(false);
        winMenu.SetActive(false);
        detection.DetectingEvent += delegate
        {
            currentBossUI.SetActive(true);
        };
    }


    private void OnDisable()
    {
        detection.DetectingEvent -= delegate
        {
            currentBossUI.SetActive(true);
        };
    }


    public void InitializeBossView(string bossName, Sprite bossIcon, float maxBossHP)
    {
        this.bossName.text = bossName;
        this.bossIcon.sprite = bossIcon;
        this.maxBossHP = maxBossHP;
        textBossMaxHP.text = maxBossHP.ToString();
        textBossCurrentHP.text = maxBossHP.ToString();
    }

    public void ViewHealth(float currentHP)
    {
        textBossCurrentHP.text = currentHP.ToString();
        bossBar.fillAmount = currentHP / maxBossHP;
        if(currentHP<=0)
        {
            currentBossUI.SetActive(false);
            OnBossDeathEvent?.Invoke();
        }
    }

    private IEnumerator Win()
    {
        yield return new WaitForSeconds(timeToDeactive);
        winMenu.SetActive(true);
        mainMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        foreach(var e in deactiveScripts)
        {
            e.enabled = false;
        }
        yield return new WaitForSeconds(2f);
        PauseController.Pause();
    }

}
