﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMObjectToTarget : MonoBehaviour
{
    [SerializeField] private List<ObjectAimMod> objectsAims;
    [SerializeReference] private MainEvents mainEvents;

    private void Start()
    {
        mainEvents.OnDetectedObject += Aim;
    }

    private void Aim(Transform target)
    {
        foreach(var e in objectsAims)
        {
            switch (e.Mod)
            {
                case ObjectAimMod.AimMod.Full:
                    TargetRotationFixator.Looking(e.LookingObject, target.position + e.CorrectTargetPosition, TargetRotationFixator.LockRotationAngle.None);
                    break;
                case ObjectAimMod.AimMod.LockYaw:
                    TargetRotationFixator.Looking(e.LookingObject, target.position + e.CorrectTargetPosition, TargetRotationFixator.LockRotationAngle.Yaw);
                    break;
                case ObjectAimMod.AimMod.LockPitch:
                    TargetRotationFixator.Looking(e.LookingObject, target.position + e.CorrectTargetPosition, TargetRotationFixator.LockRotationAngle.Pitch);
                    break;
            }
        }
    }

}

[System.Serializable]
public struct ObjectAimMod
{
    [SerializeField] private Transform lookingObject;
    [SerializeField] private AimMod aimMod;
    [SerializeField] private Vector3 correctTargetPosition;

    public Transform LookingObject
    {
        get
        {
            return lookingObject;
        }
    }

    public AimMod Mod
    {
        get
        {
            return aimMod;
        }
    }

    public Vector3 CorrectTargetPosition
    {
        get
        {
            return correctTargetPosition;
        }
    }

    public enum AimMod
    {
        Full, LockYaw, LockPitch
    }
}