using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blink : AWeapon
{
    [SerializeField] private float toBlinkTime=0.2f;
    [SerializeField] private float afterBlink = 5f;
    [SerializeField] private Transform blinkBody;
    [SerializeField] private float blinkDistance=3f;
    [SerializeField] private Color gizmosColor;
    [SerializeField] private Transform blinkGun;
    [SerializeField] private float minDistanceToBlink=1f;
    [SerializeField] private float timeScaleBeforeBlink=1f;
    [SerializeField] private float timeScaleUntilLayerBlink = 0.5f;
    [SerializeField] private float secondsInLayerBlinkSloumo = 3f;
    [SerializeField] private float inLayerBlinkDistance = 1f;

    private bool isLayerBlink = false;

    public float ReloadTime
    {
        get
        {
            return afterBlink;
        }
    }

    public override void Attack()
    {
        if (!isAttack)
        {
            isAttack = true;
            StartCoroutine(Shoot());
        }
    }

    private IEnumerator Shoot()
    {
        isLayerBlink = false;
        PauseController.ChangeTime(timeScaleBeforeBlink);
        yield return new WaitForSecondsRealtime(toBlinkTime);
        Teleport();
        if (isLayerBlink)
        {
            for (int i=0; i < secondsInLayerBlinkSloumo; i++)
            {
                PauseController.ChangeTime(timeScaleUntilLayerBlink);
                yield return new WaitForSecondsRealtime(1f);
            }
        }
        //yield return new WaitForSecondsRealtime(1f);
        PauseController.ChangeTime(1f);
        yield return new WaitForSeconds(afterBlink);
        isAttack = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = gizmosColor;
        Gizmos.DrawSphere(blinkGun.position, 1f);
        Gizmos.DrawSphere(blinkGun.position + blinkGun.forward * blinkDistance, 1f);
    }

    private void Teleport()
    {
        Ray checkRay = new Ray(blinkGun.position, blinkGun.forward);
        if (Physics.Raycast(checkRay, out RaycastHit hit, blinkDistance, layer))
        {
            Debug.Log("Через препятсвие можно пройти");
            // blinkBody.transform.position = blinkGun.position + blinkGun.forward * blinkDistance;
            blinkBody.transform.position = hit.point + (hit.point - blinkGun.position).normalized * inLayerBlinkDistance;
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
            blinkBody.transform.position = blinkGun.position + blinkGun.forward * correctDistance;
        }
        else
        {
            Debug.Log("Препятствия не было");
            blinkBody.transform.position = blinkGun.position + blinkGun.forward * blinkDistance;
        }
        //blinkBody.transform.position = blinkGun.position + blinkGun.forward * blinkDistance;
    }

    private void DamageOtherObjects()
    {

    }
}
