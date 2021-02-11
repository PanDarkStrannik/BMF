using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class FlyCamera : MonoBehaviour
{
    [Range(0,2)]
    [SerializeField] private float sensY=0.3f;
    [Range(0,2)]
    [SerializeField] private float sensX=0.3f;
    [Range(0,2)]
    [SerializeField] private float speedMove=0.3f;


    private PlayerInput input;
    private Camera currentCamera;

    private Vector2 rotateVector;

    private void Awake()
    {
        Cursor.visible = false;
        input = new PlayerInput();
        currentCamera = GetComponent<Camera>();

        rotateVector = new Vector2(currentCamera.transform.rotation.eulerAngles.x, currentCamera.transform.rotation.eulerAngles.y);
    }

    private void OnEnable()
    {
        input.Enable();
        input.RotationInput.GetRotation.performed += _ => RotationInput();
        input.MovementInput.GetDirection.performed += _ => MovementInput();
    }

    private void OnDisable()
    {
        input.Disable();
        input.RotationInput.GetRotation.performed -= _ => RotationInput();
        input.MovementInput.GetDirection.performed -= _ => MovementInput();
    }

    private void Update()
    {
        MovementInput();
    }

    private void MovementInput()
    {
        var moveDirection = input.MovementInput.GetDirection.ReadValue<Vector2>();
        var correctMove = new Vector3(moveDirection.x, 0, moveDirection.y) * speedMove;

        currentCamera.transform.Translate(correctMove);

    }

    private void RotationInput()
    {
        var rotationInput = input.RotationInput.GetRotation.ReadValue<Vector2>();

        rotateVector.y += rotationInput.x * sensY;
        rotateVector.x -= rotationInput.y * sensX;
        rotateVector.x = ClampAngle(rotateVector.x, -90, 90);

        currentCamera.transform.rotation = Quaternion.Euler(rotateVector.x, rotateVector.y, 0);

    }


    private float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F) angle -= 360F;
        if (angle > 360F) angle += 360F;
        return Mathf.Clamp(angle, min, max);
    }
}
