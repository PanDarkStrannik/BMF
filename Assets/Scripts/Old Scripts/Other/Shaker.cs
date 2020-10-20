using UnityEngine;
using DG.Tweening;

public class Shaker
{

    private Transform shakeObject;

    /// <summary>
    /// Тряска из стороны в сторону
    /// </summary>
    /// <param name="duration">Продолжительность тряски</param>
    public void ShakePosition(Transform shakeObject, float duration, float strength)
    {
        this.shakeObject = shakeObject;

        this.shakeObject.DOShakePosition(duration, strength);

    }


    /// <summary>
    /// Тряска кручением камеры
    /// </summary>
    /// <param name="duration">Продолжительность тряски</param>
    public void ShakeRotation(Transform shakeObject, float duration, float strength)
    {
        this.shakeObject = shakeObject;

        this.shakeObject.DOShakeRotation(duration, strength);

    }

    public void KillAllTweens()
    {
        shakeObject.DOKill();
    }
}

[System.Serializable]
public class ShakingParams
{
    [SerializeField] private float duration;
    [SerializeField] private float strength;

    public delegate void ShakeEventHelper(float duration, float strngth);
    public event ShakeEventHelper ShakeEvent;

    public void ShakeEventInvoke()
    {
        ShakeEvent?.Invoke(duration, strength);
    }

}