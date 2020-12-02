using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class APlayerMovement : AFaling, IMovement
{

    public PlayerMoveType moveType;
    public enum PlayerMoveType {Slow, Fast, RangeMove}
    public abstract void Move(Vector3 direction);

    public abstract IEnumerator ImpulseMove(Vector3 direction, ForceMode forceMode, float time);

}
