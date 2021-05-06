using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAI : AEnemyAI
{
    [SerializeField] private List<AttackStage> attackStages;

    private AttackStage currentStage;

    private float timer = 0f;
    private int currentNumber;

    public delegate void AttackAIEventHelper(LayerMask mask, AttackStage stage);
    public event AttackAIEventHelper AttackAIEvent;

    protected override void InStateStart()
    {
        base.InStateStart();
        if (attackStages.Count > 0)
        {
            currentNumber = 0;
            currentStage = attackStages[currentNumber];
            AttackAIEvent?.Invoke(layerMask, currentStage);
        }
    }

    protected override void InStateUpdate()
    {
        if(!PauseController.isPaused)
        {
             base.InStateUpdate();
             if (attackStages.Count > 0)
             {
                 if (timer < currentStage.During)
                 {
                     AttackAIEvent?.Invoke(layerMask, currentStage);
                     isRigidbodyKinematick = currentStage.IsRigidbodyKinematick;
                     isNavMeshAgentActive = currentStage.IsNavMeshAgentActive;
                    // Debug.Log($"{currentStage.During} : {timer}");
                 }
                 else
                 {
                     if (currentNumber < attackStages.Count - 1)
                     {
                         currentNumber++;
                      //   Debug.Log(currentNumber);
                     }
                     currentStage = attackStages[currentNumber];
                     timer = 0f;
                 }
                 timer += Time.deltaTime;
             }
        }

    }

    protected override void InStateExit()
    {
        base.InStateExit();
        if (attackStages.Count > 0)
        {
            AttackAIEvent?.Invoke(layerMask, attackStages[currentNumber]);
        }
    }

    public enum AttackStopVariants
    {
        None, StopOnDamage
    }

    [System.Serializable]
    public class AttackStage
    {
        [SerializeField] private string stageName = "None";
        [SerializeField] private float during = 1f;
        [SerializeField] private WeaponType weapon = WeaponType.Mili;
        [SerializeField] private AttackStopVariants stopVariant = AttackStopVariants.None;
        [SerializeField] private bool aim = false;
        [SerializeField] private bool damaging = false;
        [SerializeField] private bool isRigidBodyKinematick = false;
        [SerializeField] private bool isNavMeshAgentActive = false;
        [SerializeField] private bool attackAnyway = true;

        public bool IsRigidbodyKinematick
        {
            get
            {
                return isRigidBodyKinematick;
            }
        }

        public bool IsNavMeshAgentActive
        {
            get
            {
                return isNavMeshAgentActive;
            }
        }


        public WeaponType Weapon
        {
            get
            {
                return weapon;
            }
        }

        public AttackStopVariants StopVariant
        {
            get
            {
                return stopVariant;
            }
        }

        public bool Aim
        {
            get
            {
                return aim;
            }
        }


        public bool Damaging
        {
            get
            {
                return damaging;
            }
        }

        public bool AttackAnyway
        {
            get
            {
                return attackAnyway;
            }
        }

        public float During
        {
            get
            {
                return during;
            }
        }

        public string Name
        {
            get
            {
                return stageName;
            }
        }
    }
}