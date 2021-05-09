using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class DissolveBody : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer render;
   // [SerializeField] private List<Material> dissolveMaterials = new List<Material>();
    [SerializeField] private float dissolveDuration = 5f;
    private bool isDissoleved = false;
    private float startDissolveAmount = -1.2f;
    private float endDissolveAmount;

    private void Start()
    {
        foreach (var m in render.materials)
        {
           if(m.HasProperty("DissolveAmount"))
           {
               startDissolveAmount = m.GetFloat("DissolveAmount");
           }
        }

       
    }

    [ContextMenu("Dissolve")]
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
               if(m.HasProperty("DissolveAmount"))
               {
                  m.SetFloat("DissolveAmount", i);
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
