using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class APlayerMovement : AFaling, IMovement
{

    public enum PlayerMoveType {Slow, Fast, RangeMove}
    public PlayerMoveType moveType;

    public abstract void Move(Vector3 direction);
  //  public abstract void VerticalMove(Vector3 direction);

    public abstract IEnumerator ImpulseMove(Vector3 direction, ForceMode forceMode, float time);

}
