using System;
using UnityEngine;

public static class GameEvents
{
    public enum PlayerEvents
    {
        Shoot
    }
    public enum EnemyEvents
    {
        None
    }

    public static Action<PlayerEvents> PlayerAction;
    public static Action<EnemyEvents> EnemyAction;

    public static Action<GameObject> onBulletDie;
}
