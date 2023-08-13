using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectEnergy : MonoBehaviour
{


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            EnergyBarEvents.CallOnEnergyGained();
            Destroy(this.gameObject);
        }
    }




}
