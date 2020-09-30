using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
