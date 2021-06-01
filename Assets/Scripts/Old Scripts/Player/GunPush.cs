using UnityEngine;

public partial class PlayerController
{
    [System.Serializable]
    public class GunPush
    {
        [SerializeField] private WeaponType weaponType;
        [SerializeField] private Vector3 pushForce;
        [SerializeField] private ForceMode forceMode;
        [SerializeField] private float timeToPush;
        [SerializeField] private ShakingParams shakingParams;

        public WeaponType WeaponType
        {
            get
            {
                
                return weaponType;
            }
        }

        public Vector3 PushForce
        {
            get
            {
                return pushForce;
            }
        }

        public ForceMode ForceMode
        {
            get
            {
                return forceMode;
            }
        }

        public float TimeToPush
        {
            get
            {
                return timeToPush;
            }
        }

        public ShakingParams ShakingParams
        {
            get
            {
                return shakingParams;
            }
        }
    }

}
