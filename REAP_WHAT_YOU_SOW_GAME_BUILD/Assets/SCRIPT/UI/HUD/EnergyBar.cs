using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class EnergyBar : MonoBehaviour
{
    // Start is called before the first frame update
    public  static RectTransform bar;
   
    void Start()
    {
        bar = GetComponent<RectTransform>();
        SetSize(Energy.EnergyPoints);
    }
    public static void Energy_Collected(float EnergyHarvested)
    {
        if ((Energy.EnergyPoints += EnergyHarvested) <= 0f)
        {
            Energy.EnergyPoints += EnergyHarvested;
        }
      
     
        SetSize(Energy.EnergyPoints);
    }

    public static void SetSize(float size)
    {
        bar.localScale = new Vector3(size, 1);
    }


}