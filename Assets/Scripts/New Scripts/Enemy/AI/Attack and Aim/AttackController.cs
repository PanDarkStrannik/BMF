using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    [SerializeField] private List<WeaponAiData> weapons;

    private AWeapon equipWeapon = null;

    private AttackAI.AttackStopVariants stopVariant = AttackAI.AttackStopVariants.None;


    public void Initialize(List<AttackAI> enemyAIs)
    {
        if (weapons.Count > 0)
        {
            equipWeapon = weapons[0].Weapon;
        }
        else
        {
            throw new System.Exception("Нечего эккипировать!");
        }

        foreach (var e in enemyAIs)
        {
            e.AttackAIEvent += AttackOnStage;
        }
    }


    public void Deinitialize(List<AttackAI> enemyAIs)
    {
        foreach (var e in enemyAIs)
        {
            e.AttackAIEvent -= AttackOnStage;
        }
    }


    public void StopAttack(AttackAI.AttackStopVariants stopVariant, float stopTime)
    {
        if (this.stopVariant == stopVariant)
        {
            StartCoroutine(equipWeapon.StopAttack(stopTime));
        }
    }

    private void AttackOnStage(LayerMask mask, AttackAI.AttackStage stage)
    {

        stopVariant = stage.StopVariant;

        var weapons = ChooseAllWeaponsByType(stage.Weapon);
        if (weapons.Count > 0)
        {
            if (equipWeapon != weapons[0].Weapon)
            {
                SelectWeapon(weapons[0].Weapon);
            }
            foreach (var weapon in weapons)
            {
                if (stage.Aim)
                {
                    // RaycastHit[] hits = Physics.SphereCastAll(weapon.Point.position, weapon.Radius, weapon.Point.forward, weapon.Distance, mask);
                    RaycastHit[] hits = Physics.SphereCastAll(weapon.Point.position, weapon.Radius, weapon.Point.forward, weapon.Distance, mask);
                    if (hits.Length > 0)
                    {

                        foreach (var aim in weapon.ObjectAim)
                        {
                            NewAim.Aim(hits[0].transform, aim);
                        }
                    }
                }
                if (stage.Damaging)
                {
                    if(stage.AttackAnyway)
                    {
                        if(CanAttack(weapon))
                        {
                            Attack(weapon);
                        }
                    }
                    else if (CanAttack(mask, weapon))
                    {
                        Attack(weapon);
                    }
                }
            }
        }

    }


    private void SelectWeapon(AWeapon weapon)
    {
        if (weapon.State == AWeapon.WeaponState.Serenity)
        {
            equipWeapon.WeaponObject.SetActive(false);
            equipWeapon = weapon;
            equipWeapon.WeaponObject.SetActive(true);
        }
    }

    private void Attack(WeaponAiData weapon)
    {
        weapon.Weapon.Attack();
    }


    private bool CanAttack(LayerMask mask, WeaponAiData weapon)
    {
        if(CanAttack(weapon))
        {
            Ray ray = new Ray(weapon.Point.position, weapon.Point.forward);

            if (Physics.SphereCast(ray, weapon.Radius, weapon.Distance, mask.value))
            {
                return true;
            }
            
        }
        return false;
    }

    private bool CanAttack(WeaponAiData weapon)
    {
        if(weapon.Weapon.State == AWeapon.WeaponState.Serenity)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    private List<WeaponAiData> ChooseAllWeaponsByType(WeaponType weaponType)
    {
        var temp = new List<WeaponAiData>();
        foreach (var weapon in weapons)
        {
            if (weapon.Weapon.WeaponType == weaponType)
            {
                temp.Add(weapon);
            }
        }
        if (temp.Count > 0)
        {
            return temp;
        }
        else
        {
            throw new System.Exception("Не нашли подходящего оружия!");
        }
    }



    private void OnDrawGizmos()
    {
        foreach (var weapon in weapons)
        {
            if (weapon.GizmosColor != null && weapon.Point != null)
            {
                var tmpColor = weapon.GizmosColor;
                var tmpPoint = weapon.Point;
                var tmpRadius = weapon.Radius;
                var tmpDistance = weapon.Distance;
                Gizmos.color = tmpColor;
                Gizmos.DrawSphere(tmpPoint.position, tmpRadius);
                Gizmos.DrawSphere(tmpPoint.position + tmpPoint.forward * tmpDistance, tmpRadius);
            }
        }
    }

    [System.Serializable]
    public class WeaponAiData
    {
        [SerializeReference] private AWeapon weapon;
        [SerializeField] private Transform point;
        [SerializeField] private float radius = 1f;
        [SerializeField] private float distance = 1f;
        [SerializeField] private Color gizmosColor;
        [SerializeField] private List<ObjectAimMod> objectAim;


        public AWeapon Weapon
        {
            get
            {
                return weapon;
            }
        }
        public Transform Point
        {
            get
            {
                return point;
            }
        }
        public float Radius
        {
            get
            {
                return radius;
            }
        }
        public float Distance
        {
            get
            {
                return distance;
            }
        }
        public Color GizmosColor
        {
            get
            {
                return gizmosColor;
            }
        }

        public List<ObjectAimMod> ObjectAim
        {
            get
            {
                return objectAim;
            }
        }

        public WeaponAiData()
        {
            radius = 1f;
            distance = 1;
            gizmosColor = Color.black;
        }

    }
}
