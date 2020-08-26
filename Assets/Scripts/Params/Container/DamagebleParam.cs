using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DamagebleParam
{
    [SerializeField] private ParamType type;

    [Range(0,1000)]
    [SerializeField] private float value;
    [Range(0,1000)]
    [SerializeField] private float maxValue;

   
    [SerializeField] private List<DamageByType> weakneses;
    [SerializeField] private List<DamageByType> strongs;

    public ParamType Type
    {
        get
        {
            return type;
        }
    }

    public float Value
    {
        get
        {
            return value;
        }
    }

    public float MaxValue
    {
        get
        {
            return maxValue;
        }
    }

    public List<DamageByType> Weakneses
    {
        get
        {
            return weakneses;
        }
    }


    public List<DamageByType> Strongs
    {
        get
        {
            return strongs;
        }
    }


    public DamagebleParam(DamagebleParam damagebleParam)
    {
        type = damagebleParam.Type;
        value = damagebleParam.Value;
        maxValue = damagebleParam.MaxValue;
        weakneses = damagebleParam.Weakneses;
        strongs = damagebleParam.Strongs;
    }

    public DamagebleParam()
    {
        type = ParamType.Health;
        value = 3f;
        maxValue = value;
    }

    #region Проверки на слабую и сильную стороны

    /// <summary>
    /// Проверка на слабость
    /// </summary>
    /// <param name="weak"> Возможная слабость параметра</param>
    /// <returns></returns>

    public bool IsMyWeaknese(DamageType weak)
    {
        foreach(var type in weakneses)
        {
            if (weak == type.DamageType)
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Проверка на сильную сторону
    /// </summary>
    /// <param name="strong"> Возможный сильный параметр </param>
    /// <returns></returns>
    public bool IsMyStrong(DamageType strong)
    {
        foreach (var type in strongs)
        {
            if (strong == type.DamageType)
            {
                return true;
            }
        }
        return false;
    }

    #endregion


    #region Функции нанесения чистого урона параметру

    /// <summary>
    /// Нанесение урона обычного
    /// </summary>
    /// <param name="damage">величина урона</param>
    public void ApplyDamage(float damage)
    {
        value -= damage;
    }


    /// <summary>
    /// Нанесение урона не только параметру, но и его максимуму
    /// </summary>
    /// <param name="damage">величина урона</param>
    public void ApplyDamageWithMax(float damage)
    {
        value -= damage;
        maxValue -= damage;
    }


    /// <summary>
    /// Нанесение урона параметру в процентах от максимального значения
    /// </summary>
    /// <param name="proc_damage">Процент от 0 до 1</param>
    public void ApplyProcentDamage(float proc_damage)
    {
        value -= maxValue * proc_damage;
    }



    /// <summary>
    /// Нанесение урона параметру и его максимуму в процентах от максимального значения
    /// </summary>
    /// <param name="proc_damage">Процент от 0 до 1</param>
    public void ApplyProcentDamageWithMax(float proc_damage)
    {
        value -= maxValue * proc_damage;
        maxValue -= maxValue * proc_damage;
    }


    #endregion



    #region Функция для повышения параметра

    /// <summary>
    /// Увеличение параметра не превышая максимального значения
    /// </summary>
    /// <param name="enlarge"> величина увеличения</param>
    public void Enalrge(float enlarge)
    {
        value += enlarge;

        if (value > maxValue)
        {
            value = maxValue;
        }
    }

    /// <summary>
    /// Увеличить параметр вместе с его максимумом
    /// </summary>
    /// <param name="enlarge"> величина увеличения</param>
    public void EnlargeWithMax(float enlarge)
    {
        value += enlarge;
        maxValue += enlarge;
    }

    /// <summary>
    /// Установить максимальное значение параметра
    /// </summary>
    public void Maximize()
    {
        value = maxValue;
    }

    #endregion

    public enum ParamType
    {
        Health, Test
    }

}

