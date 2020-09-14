using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPS : MonoBehaviour
{
    [SerializeField] private ParticleSystem rocks;
    [SerializeField] private float rockTime;

    private void Update()
    {
        if (rockTime <= 0)
        {
            var main = rocks.main;
            main.gravityModifier = 4;
        }
        else rockTime -= Time.deltaTime;
    }
}
