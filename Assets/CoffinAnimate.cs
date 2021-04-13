using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;


public class CoffinAnimate : MonoBehaviour
{
    public UnityEvent OnButtonPressed;

    private PlayerInput input;

    private void Awake()
    {
        input = new PlayerInput();
        input.ButtonInputs.Heal.performed += _ => PlayAnimation();
    }

    private void OnEnable()
    {
        input.Enable();
    }
    private void OnDisable()
    {
        input.Disable();
    }

    private void PlayAnimation()
    {
        OnButtonPressed?.Invoke();
    }


}
