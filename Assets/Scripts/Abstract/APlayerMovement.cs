using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class APlayerMovement : MonoBehaviour, IMovement
{
    public PlayerMoveType moveType;
    public enum PlayerMoveType {Slow, Fast}
    public abstract void Move(Vector3 direction);

}
