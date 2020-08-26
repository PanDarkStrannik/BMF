using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageblePlace : ADamageble
{

    [SerializeField] private GameObject popup;

    [SerializeField] private float popupDestroyTime = 2f;

    [SerializeField] private float popupRangeTime=1f;




    public override void ApplyDamage(DamageByType weapon)
    {
        var allWeak = datas.FindAllByWeak(weapon.DamageType);

        if (allWeak != null)
        {

            foreach (var weak in allWeak)
            {
                float damage = weapon.Value;
                foreach (var strongData in weak.Strongs)
                {
                    if (strongData.DamageType == weapon.DamageType)
                    {
                        damage -= strongData.Value;
                    }
                }
                foreach(var weakData in weak.Weakneses)
                {
                    if(weakData.DamageType == weapon.DamageType)
                    {
                        damage += weakData.Value;
                    }
                }
                if (damage < 0)
                {
                    damage = 0;
                }
                weak.ApplyDamage(damage);

                Debug.Log(weak.Type + " " + weapon.DamageType + " " + damage);

                PopupCreate(weak, weapon, damage);
            }

            
            DamageEvent();
        }
    }


    


    private void PopupCreate(DamagebleParam param, DamageByType weapon, float damage)
    {

        var tmpPopup = Instantiate(popup, transform.position, Quaternion.identity);
        tmpPopup.GetComponentInChildren<TextMesh>().text = $"{param.Type}: {weapon.Value} : {damage}";
        Destroy(tmpPopup, popupDestroyTime);
        Debug.Log("Слабость у " + param.Type + " " + weapon.DamageType + " " + damage);

    }
}
