using UnityEngine;

public class WAnimationRandomizer : MonoBehaviour
{
    [SerializeField] private string[] triggers;
    [SerializeField] private Animator weaponAnimator;

    public void RandomizeAnimation()
    {
        int index = Random.Range(0, triggers.Length);
        string triggerName = triggers[index];
        weaponAnimator.SetTrigger(triggerName);
    }
}
