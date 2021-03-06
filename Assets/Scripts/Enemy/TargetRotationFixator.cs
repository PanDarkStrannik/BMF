﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TargetRotationFixator
{
    public static void Looking(Transform lookObject, Transform lookTarget,LockRotationAngle rotationAngle)
    {
      
        var temp = lookTarget.position;
        if (rotationAngle != LockRotationAngle.None)
        {
            switch (rotationAngle)
            {
                case LockRotationAngle.Pitch:
                    RotateYaw(lookTarget.position - lookObject.position, lookObject, lookObject);
                    break;
                case LockRotationAngle.Yaw:
                    temp.y = lookObject.position.y;
                    break;
            }
        }
        lookObject.LookAt(temp);



    }

    public static void Looking(Transform lookObject, Vector3 lookTarget, LockRotationAngle rotationAngle)
    {
        var temp = lookTarget;
        if (rotationAngle != LockRotationAngle.None)
        {
            switch (rotationAngle)
            {
                case LockRotationAngle.Pitch:
                    RotateYaw(lookTarget - lookObject.position, lookObject, lookObject);
                    break;
                case LockRotationAngle.Yaw:
                    temp.y = lookObject.position.y;
                    break;
            }
        }
        lookObject.LookAt(temp);
    }

    private static void RotateYaw(Vector3 _direction, Transform yaw, Transform pitch)
    {
        pitch.rotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(_direction, yaw.right), Vector3.up);
        yaw.rotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(_direction, yaw.up), Vector3.up);
    }


    public static void Looking(Transform lookObject, Transform lookTarget, LockRotationAngle rotationAngle1, LockRotationAngle rotationAngle2)
    {
        var temp = lookTarget.position;
        if (rotationAngle1 != LockRotationAngle.None || rotationAngle2 != LockRotationAngle.None)
        {
            switch (rotationAngle1)
            {
                case LockRotationAngle.Pitch:
                    temp.y = lookObject.position.y;
                    break;
                case LockRotationAngle.Yaw:
                    temp.x = lookObject.position.x;
                    temp.z = lookObject.position.z;
                    break;
            }
            switch (rotationAngle2)
            {
                case LockRotationAngle.Pitch:
                    temp.y = lookObject.position.y;
                    break;
                case LockRotationAngle.Yaw:
                    temp.x = lookObject.position.x;
                    temp.z = lookObject.position.z;
                    break;
            }
        }
        lookObject.LookAt(temp);
    }

    public enum LockRotationAngle
    {
        Pitch,Yaw, None
    }
}


