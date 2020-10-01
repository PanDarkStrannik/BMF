using System.Collections;
using UnityEngine;

public class UIMenuController : MonoBehaviour
{
    [SerializeReference] private GameObject deathMenu;
    [SerializeField] private float toDeathTime=5f;

    [SerializeReference] private GameObject pauseMenu;
    [SerializeReference] private PlayerController playerController;
    [SerializeField] private float toPauseTime=5f;

    [SerializeReference] private GameObject winMenu;    
    [SerializeField] private float toWinTime = 5f;

    [SerializeReference] private GameObject mainMenu;
    [SerializeReference] private PlayerUI playerUI;
    [SerializeReference] private BossUI bossUI;

    [SerializeField] private float toMenuView=2f;

    private PlayerInput input;
    private bool isEscAlready=false;

    private bool isGameOver = false;

    private void Awake()
    {
        input = new PlayerInput();
        input.Enable();
    }

    private void Start()
    {
        deathMenu.SetActive(false);
        winMenu.SetActive(false);
        pauseMenu.SetActive(false);

        input.ButtonInputs.ESC.performed += context =>
        {
            if (!isEscAlready && !isGameOver)
            {
                isEscAlready = true;
                StartCoroutine(Pause());
            }
            else if(isEscAlready && !isGameOver)
            {
                isEscAlready = false;
                Resume();
            }
        };

        playerUI.OnPlayerDeathEvent += delegate
        {
            StartCoroutine(GameOver());
        };

        bossUI.OnBossDeathEvent += delegate
        {
            StartCoroutine(Win());
        };
    }

    private void OnDestroy()
    {
        input.Disable();
    }

    private IEnumerator Win()
    {
        yield return new WaitForSecondsRealtime(toWinTime);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        playerController.enabled = false;
        mainMenu.SetActive(false);
        winMenu.SetActive(true);
        isGameOver = true;
        yield return new WaitForSecondsRealtime(toMenuView);
        PauseController.Pause();
    }

    private IEnumerator GameOver()
    {
        yield return new WaitForSecondsRealtime(toDeathTime);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        playerController.enabled = false;
        mainMenu.SetActive(false);
        deathMenu.SetActive(true);
        isGameOver = true;
        yield return new WaitForSecondsRealtime(toMenuView);
        PauseController.Pause();
    }

    private IEnumerator Pause()
    {
        yield return new WaitForSecondsRealtime(toPauseTime);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        playerController.enabled = false;
        pauseMenu.SetActive(true);
        yield return new WaitForSecondsRealtime(toMenuView);
        PauseController.Pause();
    }

    public void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pauseMenu.SetActive(false);
        playerController.enabled = true;
        PauseController.Resume();
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
