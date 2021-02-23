using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUI : MonoBehaviour
{
    [SerializeField] private Text ammoAmout;

    private void OnEnable()
    {
        PlayerInformation.GetInstance().PlayerParamController.DamagebleParams.OnParamChanged += UpdateAmmo;
        
    }

    private void OnDisable()
    {
        PlayerInformation.GetInstance().PlayerParamController.DamagebleParams.OnParamChanged -= UpdateAmmo;
    }


    public void UpdateAmmo(DamagebleParam.ParamType paramType, float value, float maxValue)
    {

      if(paramType == DamagebleParam.ParamType.HolyWater)
        {
           ammoAmout.text = value.ToString();
        }
    }

   

}
