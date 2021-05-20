using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class DissolveBody : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer render;
    [SerializeField] private string dissolveVariable;
    [SerializeField] private float dissolveDuration = 5f;
    private bool isDissoleved = false;
    private float startDissolveAmount = -1.2f;

   

    public void SetDefaultDissolveAmount()
    {
        foreach (var m in render.materials)
        {
            if (m.HasProperty(dissolveVariable))
            {
                m.SetFloat(dissolveVariable, startDissolveAmount);
            }
        }
    }

    public void Dissolve()
    {
        StartCoroutine(DissoloveTick());
    }


    private IEnumerator DissoloveTick()
    {
        isDissoleved = true;
        for (float i = startDissolveAmount; i < dissolveDuration; i+= Time.deltaTime)
        {
            var startDissolveTime = i;
            foreach (var m in render.materials)
            {
               if(m.HasProperty(dissolveVariable))
               {
                  m.SetFloat(dissolveVariable, i);
               }
            }
            

            if (!isDissoleved)
            {
                startDissolveTime = startDissolveAmount;
            }
            yield return new WaitForEndOfFrame();
        }
        isDissoleved = false;
        
    }
}
