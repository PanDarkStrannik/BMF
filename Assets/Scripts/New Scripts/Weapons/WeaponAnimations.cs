using UnityEngine;

public class WeaponAnimations : MonoBehaviour
{
    [SerializeField] protected Animator weaponAnimator;
    [SerializeField] protected string blendTreeFloat;


    private void OnEnable()
    {
        PlayerInformation.GetInstance().PlayerController.OnPlayerMoved += OnPlayerMove;
    }

    public virtual void OnPlayerMove(Vector3 obj)
    {
        weaponAnimator.SetFloat(blendTreeFloat, obj.magnitude, 0.1f, Time.deltaTime);
    }

}
