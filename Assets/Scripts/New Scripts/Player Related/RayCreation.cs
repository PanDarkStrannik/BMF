using UnityEngine;
using UnityEngine.InputSystem;

public interface IRay
{
    Ray CreateRayFromPlayer(PlayerInput input);
}

public class RayCreation : MonoBehaviour, IRay
{
    [SerializeField] private Camera playerCamera;

    public Ray CreateRayFromPlayer(PlayerInput input)
    {
        return playerCamera.ScreenPointToRay(input.RotationInput.MouseDelta.ReadValue<Vector2>());
    }
}
