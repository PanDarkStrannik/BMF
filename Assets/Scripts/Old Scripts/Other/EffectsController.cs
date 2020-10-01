using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsController : MonoBehaviour
{
    [SerializeField] private List<Effects> effects;
    [SerializeReference] private MainEvents mainEvents;

    private void Start()
    {
        mainEvents.EffectTypeEvent += Checker;
    }

    private void Checker(EffectType effectType, bool activate)
    {
        foreach(var effect in effects)
        {
            if(effect.CheckType(effectType))
            {
                effect.EnableOrDisable(activate);
            }
        }
    }

    public enum EffectType
    {
        Melle, Range
    }

}

[System.Serializable]
public struct Effects
{
    [SerializeField] private ParticleSystem particleSystem;
    [SerializeField] private EffectsController.EffectType effectType;

    public void StartSystem()
    {
        particleSystem.Play();
    }

    public void StopSystem()
    {
        particleSystem.Stop();
    }

    public void EnableOrDisable(bool activate)
    {
        if(activate && !particleSystem.isPlaying)
        {
            particleSystem.Play();
        }
        else if(!activate && particleSystem.isPlaying)
        {
            particleSystem.Stop();
        }
    }

    public bool CheckType(EffectsController.EffectType type)
    {
        if(effectType==type)
        {
            return true;
        }
        return false;
    }
}
