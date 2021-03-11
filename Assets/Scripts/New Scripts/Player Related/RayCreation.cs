using UnityEngine;
using UnityEngine.InputSystem;

public interface IRay
{
    Ray CreateRayFromPlayer(PlayerInput input);
    float rayDist { get; }
}

public class RayCreation : MonoBehaviour, IRay
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float rayDistance;

    public float rayDist { get => rayDistance; }

    public Ray CreateRayFromPlayer(PlayerInput input)
    {
        return playerCamera.ScreenPointToRay(input.RotationInput.MouseDelta.ReadValue<Vector2>());
    }


}
