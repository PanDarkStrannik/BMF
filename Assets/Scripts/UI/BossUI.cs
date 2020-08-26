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
    [SerializeField] private EnemyDetection detection;

    private float maxBossHP=0f;


    private void Start()
    {
        currentBossUI.SetActive(false);
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
        }
    }

}
