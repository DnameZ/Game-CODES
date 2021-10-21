using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoundationStone : BuldingsInfo, IButtonsForBulding
{
    public void BulidBulding()
    {
        clicked = true;

        StartCoroutine(BulidFoundationStone());
    }

    IEnumerator BulidFoundationStone()
    {

        if (clicked&&InGameObject==null)
        {
            InGameObject = Instantiate(PrefabToBulit, BuldingSystem.BuldySis.Hit.point, Quaternion.identity);
        }
       while(InGameObject!=null)
        {
            Vector3 PosOFFoundation = new Vector3(BuldingSystem.BuldySis.Hit.point.x, BuldingSystem.BuldySis.Hit.point.y, BuldingSystem.BuldySis.Hit.point.z);

            InGameObject.transform.position = PosOFFoundation;

            yield return null;
        }

        yield return new WaitForSeconds(0f);
    }
}
