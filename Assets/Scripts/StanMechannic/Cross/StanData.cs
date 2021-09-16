using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StanMechannic;

[CreateAssetMenu(fileName = "New Stan Data", menuName = "Data/StanData")]
public class StanData : ScriptableObject, IStanData
{
    [SerializeField, Min(0f)] private float _stanTime=0f;

    public float StanTime
    {
        get => _stanTime;
    }
}
