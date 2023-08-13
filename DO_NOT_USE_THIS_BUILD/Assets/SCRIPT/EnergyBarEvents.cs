using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
public class EnergyBarEvents : MonoBehaviour
{
    public delegate void EnergyGained();
    public static EnergyGained OnEnergyGained;

    public delegate void EnergyFull();
    public static EnergyGained OnEnergyFull;

    public delegate void SpecialShot();
    public static SpecialShot OnSpecialShot;

    [SerializeField] Image fill;

    private void OnEnable()
    {
        OnEnergyGained += EnergyPlus;
        OnSpecialShot += ShotSpecial;
    }

    private void OnDisable()
    {
        OnEnergyGained -= EnergyPlus;
        OnSpecialShot -= ShotSpecial;
    }



    void EnergyPlus()
    {
        fill.fillAmount += 0.25f;

        if (fill.fillAmount >= 0.95f)
            OnEnergyFull.Invoke();
    }

    void ShotSpecial()
    {
        fill.fillAmount = 0;
    }

    public static void CallOnEnergyGained()
    {
        OnEnergyGained.Invoke();
    }

    public static void CallOnSpecialShot()
    {
        OnSpecialShot.Invoke();
    }
}
