using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Stan Data", menuName = "Data/StanData")]
public class StanData : ScriptableObject
{
    [SerializeField, Min(0f)] private float _stanTime=0f;

    public float StanTime
    {
        get => _stanTime;
    }
}
