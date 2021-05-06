using UnityEngine;
using UnityEngine.SceneManagement;
using System;
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

   


    private void OnEnable()
    {
        _input.Enable();
    }
    private void OnDisable()
    {
        _input.Disable();
    }


    public void Pause()
    {
        isGamePaused = !isGamePaused;
        PauseMenu.SetActive(isGamePaused);
        PauseCheck();
    }

    private void PauseCheck()
    {
        if (isGamePaused)
        {
            PauseController.Pause();
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            PauseController.Resume();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
