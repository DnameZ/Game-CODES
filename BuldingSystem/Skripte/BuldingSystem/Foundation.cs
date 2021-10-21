using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foundation : BuldingsInfo, IButtonsForBulding
{

    public void BulidBulding()
    {
        clicked = true;

        StartCoroutine(CourtineObjectShow);
    }

    


}
