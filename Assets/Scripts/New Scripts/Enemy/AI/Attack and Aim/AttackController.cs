using System.Collections.Generic;
using UnityEngine;
using CharacterStateMechanic;

public class AttackController : AController
{
    [SerializeField] private List<WeaponAiData> weapons;
    [SerializeField] private List<AimSpheres> aimSpheres;

    private AWeapon equipWeapon = null;

    private AttackAI.AttackStopVariants stopVariant = AttackAI.AttackStopVariants.None;

    private List<AttackAI> attackAIs=null;

    private bool alreadySubscribe = false;

    private void OnEnable()
    {
        SubscribeOnEvents();
    }

    private void OnDisable()
    {
        UnSubscribeOnEvents();
        foreach (var weapon in weapons)
            weapon.Weapon.StopAttack();
    }

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

        attackAIs = enemyAIs;

        SubscribeOnEvents();

    }

    private void SubscribeOnEvents()
    {
        if (attackAIs != null)
        {
            if (alreadySubscribe == false)
            {
                foreach (var e in attackAIs)
                {
                    e.AttackAIEvent += AttackOnStage;
                }
            }
            alreadySubscribe = true;
        }
    }

    private void UnSubscribeOnEvents()
    {
        if (attackAIs != null)
        {
            if (alreadySubscribe == true)
            {
                foreach (var e in attackAIs)
                {
                    e.AttackAIEvent -= AttackOnStage;
                }
            }
            alreadySubscribe = false;
        }
    }


    public void Deinitialize(List<AttackAI> enemyAIs)
    {
        UnSubscribeOnEvents();
        attackAIs = null;
    }


    //public void StopAttack(AttackAI.AttackStopVariants stopVariant, float stopTime)
    //{
    //    if (this.stopVariant == stopVariant)
    //    {
    //        StartCoroutine(equipWeapon.StopAttack(stopTime));
    //    }
    //}

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
                    foreach (var aimSphere in aimSpheres)
                    {
                        RaycastHit[] hits = Physics.SphereCastAll(aimSphere.Point.position, aimSphere.Radius, aimSphere.Point.forward, aimSphere.Distance, mask);
                        if (hits.Length > 0)
                        {
                            foreach (var hit in hits)
                            {
                                if (hit.transform != null)
                                {

                                    Aim(hit.transform);

                                    break;
                                }
                            }
                            break;
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


    private void Aim(Transform target)
    {
        foreach (var weapon in weapons)
        {
            foreach (var aim in weapon.ObjectAim)
            {
                NewAim.NormalAim(target, aim);
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
        if(weapon.Weapon.TryReturnNeededWeaponType<IDamagingWeapon>(out IDamagingWeapon returnedWeapon))
        {
            returnedWeapon.Attack();
        }
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



    private void OnDrawGizmosSelected()
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

        foreach (var aimSphere in aimSpheres)
        {
            if (aimSphere.GizmosColor != null && aimSphere.Point != null)
            {
                Gizmos.color = aimSphere.GizmosColor;
                Gizmos.DrawSphere(aimSphere.Point.position, aimSphere.Radius);
                Gizmos.DrawLine(aimSphere.Point.position, aimSphere.Point.position + aimSphere.Point.forward * aimSphere.Distance);
                Gizmos.DrawSphere(aimSphere.Point.position + aimSphere.Point.forward * aimSphere.Distance, aimSphere.Radius);
            }
        }
    }

    [System.Serializable]
    public struct AimSpheres
    {
        [SerializeReference] private Transform point;
        [SerializeField] private float radius;
        [SerializeField] private float distance;
        [SerializeField] private Color gizmosColor;

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
