using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public static class NewAim
{
    public static void Aim(Transform target, ObjectAimMod aimMod)
    {
        switch (aimMod.Mod)
        {
            case ObjectAimMod.AimMod.Full:
                TargetRotationFixator.Looking(aimMod.LookingObject, target.position + aimMod.CorrectTargetPosition, TargetRotationFixator.LockRotationAngle.None);
                break;
            case ObjectAimMod.AimMod.LockYaw:
                TargetRotationFixator.Looking(aimMod.LookingObject, target.position + aimMod.CorrectTargetPosition, TargetRotationFixator.LockRotationAngle.Yaw);
                break;
            case ObjectAimMod.AimMod.LockPitch:
                TargetRotationFixator.Looking(aimMod.LookingObject, target.position + aimMod.CorrectTargetPosition, TargetRotationFixator.LockRotationAngle.Pitch);
                break;
        }
    }
    public static void NormalAim(Transform target, ObjectAimMod aimMod)
    {
        Vector3 dir = target.position - aimMod.LookingObject.position;
        float angleHorizontal = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
        float angleVertical = Mathf.Asin(dir.y / dir.magnitude) * Mathf.Rad2Deg;

        Vector3 angle = Vector3.zero;
        switch(aimMod.Mod)
        {
            case ObjectAimMod.AimMod.LockPitch:
              angle = new Vector3(angleVertical, 0, 0);
                break;
            case ObjectAimMod.AimMod.LockYaw:
                angle = new Vector3(0, angleHorizontal, 0);
                break;
            case ObjectAimMod.AimMod.Full:
                angle = new Vector3(angleVertical, angleHorizontal, 0);
                break;
        }
        aimMod.LookingObject.transform.DORotate(angle + aimMod.CorrectTargetPosition, aimMod.RotateSmoothSpeed);
    }
}
