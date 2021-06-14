using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBlink : AWeapon, IDamagingWeapon
{
    [SerializeField] private float toBlinkTime = 0.2f;
    [SerializeField] private float afterBlink = 5f;
    [SerializeField] private Transform blinkBody;
    [SerializeField] private float blinkDistance = 3f;
    [SerializeField] private float collisionCheckDist = 0.5f;
    [SerializeField] private Color gizmosColor;
    [SerializeField] private Transform blinkGun;
    [SerializeField] private float minDistanceToBlink = 1f;
    [SerializeField] private float timeScaleBeforeBlink = 1f;
    [SerializeField] private float timeScaleUntilLayerBlink = 0.5f;
    [SerializeField] private LayerMask layer;
    [SerializeField] private float secondsInLayerBlinkSloumo = 3f;
    [SerializeField] private float inLayerBlinkDistance = 1f;

    private bool isLayerBlink = false;

    public override WeaponType WeaponType
    {
        get
        {
            return WeaponType.Blink;
        }
    }

    public float ReloadTime
    {
        get
        {
            return afterBlink;
        }
    }

    public override void UseWeapon()
    {
       if(!PauseController.isPaused)
       {
         Attack();
       }
    }

    public void Attack()
    {
        if (state == WeaponState.Serenity)
        {
            StartCoroutine(Attacking(0f));
        }
    }

    protected override IEnumerator Attacking(float time)
    {
        if(Teleport(out Vector3 blinkPos))
        {
            isLayerBlink = false;
            State = WeaponState.Attack;
            PauseController.ChangeTime(timeScaleBeforeBlink);
            yield return new WaitForSecondsRealtime(toBlinkTime);
            blinkBody.position = blinkPos;
            if (isLayerBlink)
            {
                for (int i = 0; i < secondsInLayerBlinkSloumo; i++)
                {
                    PauseController.ChangeTime(timeScaleUntilLayerBlink);
                    yield return new WaitForSecondsRealtime(1f);
                }
            }
            PauseController.ChangeTime(1f);
            StartCoroutine(Serenity(afterBlink));
        }
      
    }

    protected override IEnumerator Reload(float time)
    {
        State = WeaponState.Reload;
        yield return new WaitForSeconds(time);
        StartCoroutine(Serenity(0f));
    }

    protected override IEnumerator Serenity(float time)
    {
        yield return new WaitForSeconds(time);
        State = WeaponState.Serenity;
    }

    private bool Teleport(out Vector3 blinkPos)
    {
        blinkPos = Vector3.zero;
        Ray checkRay = new Ray(blinkGun.position, blinkGun.forward);
        if (Physics.Raycast(checkRay, out RaycastHit hit, blinkDistance, layer))
        {
            Debug.Log("Через препятсвие можно пройти");
            blinkPos = hit.point + (hit.point - blinkGun.position).normalized * inLayerBlinkDistance;
            Vector3 blinkDir = blinkBody.position - blinkPos;
            blinkDir.Normalize();

            if(Physics.Raycast(blinkPos, blinkDir, out RaycastHit hitInfo))
            {
                if((layer.value & 1 << hitInfo.collider.gameObject.layer) == 0)
                {
                    return false;
                }
            }
            
            isLayerBlink = true;
            blinkBody.transform.LookAt(hit.point);
        }
        else if (Physics.Raycast(checkRay, out RaycastHit hitInfo, blinkDistance))
        {
            Debug.Log("Через препятсвие нельзя пройти");
            var correctDistance = Vector3.Distance(hitInfo.point, blinkGun.position);
            if (correctDistance < minDistanceToBlink)
            {
                correctDistance = 0;
            }
            blinkPos = blinkGun.position + blinkGun.forward * correctDistance;
        }
        else
        {
            Debug.Log("Препятствия не было");
            blinkPos = blinkGun.position + blinkGun.forward * blinkDistance;
        }
        return true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = gizmosColor;
        Gizmos.DrawSphere(blinkGun.position, 1f);
        Gizmos.DrawSphere(blinkGun.position + blinkGun.forward * blinkDistance, 1f);
    }

}
