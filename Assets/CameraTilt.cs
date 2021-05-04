using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraTilt : MonoBehaviour
{
    [SerializeField] private Transform cam;

    private Vector3 defaultCamLocalRotation;

    private void Start()
    {
        defaultCamLocalRotation = new Vector3(cam.localRotation.x, cam.localRotation.y, cam.localRotation.z);
    }

  

    #region CameraTilting
    public void TiltByY(float angle)
    {
        var rotationAngle = new Vector3(angle, 0f, 0f);
        cam.DOLocalRotate(rotationAngle, 0.2f);
    }
    
    public void TiltByX(float angle)
    {
        var rotationAngle = new Vector3(0f, angle, 0f);
        cam.DOLocalRotate(rotationAngle, 0.2f);
    }

    public void TiltByYFromTo(float angle)
    {
        var rotationAngle = new Vector3(angle, 0f, 0f);
        cam.DOLocalRotate(rotationAngle, 0.1f).From(-rotationAngle, false);
       
    }

    public void DefaultTilt()
    {
        cam.DOLocalRotate(defaultCamLocalRotation, 0.15f);
    }
    #endregion



}
