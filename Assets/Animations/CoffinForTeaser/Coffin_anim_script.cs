
using UnityEngine;

public class Coffin_anim_script : MonoBehaviour
{
    private Animator _anim;
    private PlayerInput _input;

    private void Awake()
    {
        _input = new PlayerInput();
        _input.InteractionForTeaser.TurnOnAnimation.performed += context => StartAnimation(); // KeyCode = "Enter"
        
        _anim = GetComponent<Animator>();
        _anim.enabled = false;
    }

    private void OnEnable() => _input.Enable();
    private void OnDisable() => _input.Disable();

    public void NextAnimationCoffin() => _anim.SetBool("NextAnim", true);

    private void StartAnimation() => _anim.enabled = true;

}
