using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DStructure : MonoBehaviour
{
    public DefensiveStructure DefensiveStructure;

    private Coroutine curr;

    IEnumerator RaisingTemperature()
    { 
        while (!DefensiveStructure.atacking)
        {
            yield return new WaitForFixedUpdate();

            while (DefensiveStructure.atacking)
            { 
                yield return new WaitForSeconds(2);

                DefensiveStructure.curTemperatureOfBulding += 2;
            }
        }        
    }

    private void Start()
    {
        curr = StartCoroutine(RaisingTemperature());
    }

    
}






