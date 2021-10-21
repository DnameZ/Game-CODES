using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SelectedUnits : MonoBehaviour
{
    public static List<GameObject> Selected_Units = new List<GameObject>();
    [SerializeField] Animator SetButtons;


    private void Start()
    {
        StartCoroutine(ChangeShaderOfSelectedUnits());
    }


    IEnumerator ChangeShaderOfSelectedUnits()
    {
        while(Selected_Units.Count==0)
        {
            SetButtons.SetBool("isOpen", false);
            yield return new WaitForEndOfFrame();

            while(Selected_Units.Count!=0)
            {
                SetButtons.SetBool("isOpen", true);

                foreach (GameObject unit in Selected_Units)
                {
                    ChangeShader.ChangeShaderOfObject(ChangeShader.OnHoverAndPick, unit);
                }

                yield return new WaitForSeconds(0f);
            }
        }
    }
}
