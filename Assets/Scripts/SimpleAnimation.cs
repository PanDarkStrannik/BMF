using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SimpleAnimation : MonoBehaviour
{
    [SerializeField] private List<AnimateClip> animations;

    private bool animateWorking = false;

    public void OnEnable()
    {
        foreach(var e in animations)
        {
            e.OnMovingComplete += AnimateNotWorking;
        }
    }

    public void OnDisable()
    {
        foreach (var e in animations)
        {
            e.OnMovingComplete -= AnimateNotWorking;
        }
    }

    private void AnimateNotWorking()
    {
        animateWorking = false;
    }

    public void StartAnimation(int animNum)
    {
        if (!animateWorking)
        {
            if (animNum < animations.Count)
            {
                animateWorking = true;
                animations[animNum].StartAnimation();
            }
        }
    }

    [System.Serializable]
    private class AnimateClip
    {
        [SerializeReference] private Transform animatingObject;
        [SerializeReference] private List<Transform> animatingPositions;
        [Min(0f)]
        [SerializeField] private float movingSpeed = 1f;
        [Min(0f)]
        [SerializeField] private float rotationSpeed = 1f;
        [SerializeField] private UnityEvent onEndEvent;

        public UnityAction OnMovingComplete
        { get; set; }

        public void StartAnimation()
        {
            var Seq = DOTween.Sequence();
            foreach(var e in animatingPositions)
            {
                Seq.Append(animatingObject.DOMove(e.position, movingSpeed, false));
                Seq.Join(animatingObject.DORotateQuaternion(e.rotation, rotationSpeed));
            }
            Seq.OnComplete(MovingComplete);
        }

        private void MovingComplete()
        {
            OnMovingComplete?.Invoke();
            onEndEvent?.Invoke();
        }
    }
}
