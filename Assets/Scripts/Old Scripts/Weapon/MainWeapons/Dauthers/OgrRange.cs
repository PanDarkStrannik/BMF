using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OgrRange : RangeWeapon
{
    [SerializeReference] private EnemyWeaponLogic weaponLogic;

    protected override void InShoot()
    {
        CorrectGunPosition();
    }

    private void CorrectGunPosition()
    {
        TargetRotationFixator.Looking(gunPosition, weaponLogic.WantDamagedObject.transform, TargetRotationFixator.LockRotationAngle.Yaw);
        //gunPosition.LookAt(weaponLogic.WantDamagedObject.transform);
        //TargetRotationFixator.RotateYaw(weaponLogic.WantDamagedObject.transform.position - gunPosition.position, gunPosition, gunPosition);
    }
}
