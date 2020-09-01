using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageblePlace : ADamageble
{

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

                DamageEventWithValue(damage, this);
            }
            DamageEvent();
        }
    }


    


    //private void PopupCreate(DamagebleParam param, DamageByType weapon, float damage)
    //{

    //    var tmpPopup = Instantiate(popup, transform.position, Quaternion.identity);
    //    if (damage <= 3f)
    //    {
    //        tmpPopup.GetComponentInChildren<TextMesh>().color = Color.white;
    //    }
    //    else if (damage <= 7f)
    //    {
    //        tmpPopup.GetComponentInChildren<TextMesh>().color = Color.yellow;
    //    }
    //    else if (damage <= 10f)
    //    {
    //        tmpPopup.GetComponentInChildren<TextMesh>().color = Color.green;
    //    }
    //    tmpPopup.GetComponentInChildren<TextMesh>().text = $"{damage}";
    //    Destroy(tmpPopup, popupDestroyTime);
    //    Debug.Log("Слабость у " + param.Type + " " + weapon.DamageType + " " + damage);

    //}
}
