using UnityEngine;
using UnityEngine.SceneManagement;

public class SimplePauseController : MonoBehaviour
{
    private PlayerInput _input;
    private bool isGamePaused;

    [SerializeField] private GameObject PauseMenu;

    private void Awake()
    {
        _input = new PlayerInput();
        _input.ButtonInputs.ESC.performed += ctx => Pause();

        isGamePaused = false;
        PauseMenu.SetActive(false);
    }

    private void Update()
    {
        PauseCheck();
    }


    private void OnEnable() => _input.Enable();
    private void OnDisable() => _input.Disable();

    public void GoToMainMenu() => SceneManager.LoadScene(0);

    public void Pause()
    {
        isGamePaused = !isGamePaused;
        PauseMenu.SetActive(isGamePaused);
    }

    private void PauseCheck()
    {
        if (isGamePaused)
        {
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
