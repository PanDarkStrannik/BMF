using UnityEngine;

public class WeaponAnimations : MonoBehaviour
{
    [SerializeField] private Animator weaponAnimator;
    [SerializeField] private string blendTreeFloat;
    [SerializeField] private string shiftBool;

    private void OnEnable()
    {
        PlayerInformation.GetInstance().PlayerController.OnPlayerMoved += OnPlayerMove;
        PlayerInformation.GetInstance().PlayerController.OnPlayerSprint += OnPlayerSprint;
    }
    private void OnDisable()
    {
        PlayerInformation.GetInstance().PlayerController.OnPlayerMoved -= OnPlayerMove;
        PlayerInformation.GetInstance().PlayerController.OnPlayerSprint -= OnPlayerSprint;
    }

    private void OnPlayerSprint(bool shifting)
    {
        weaponAnimator.SetBool(shiftBool, !shifting);
    }


    public virtual void OnPlayerMove(Vector3 obj)
    {
        weaponAnimator.SetFloat(blendTreeFloat, obj.magnitude, 0.1f, Time.deltaTime);
    }

}
