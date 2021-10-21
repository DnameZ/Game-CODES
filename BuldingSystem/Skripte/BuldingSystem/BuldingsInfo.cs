using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuldingsInfo:MonoBehaviour
{
    public GameObject PrefabForBulding;

    public GameObject PrefabToBulit;

    public GameObject Flyer;

    public GameObject InGameObject;

    public GameObject SelectedObject;

    public bool clicked;

    public bool clickedForDestroy;

    public string CourtineObjectShow = "ObjectShow";

    [SerializeField] Material SelectedGrid;
    [SerializeField] Material DefaultGrid;


    private void Update()
    { 
        UnSelectObject();

       

    }

    public IEnumerator ObjectShow()
    {
        clicked = true;

        InGameObject = Instantiate(PrefabForBulding, Input.mousePosition, Quaternion.identity);

        while (clicked && BuldingSystem.BuldySis.Hit.collider != null)
        {
            InGameObject.transform.position = new Vector3(BuldingSystem.BuldySis.Hit.point.x, 0f, BuldingSystem.BuldySis.Hit.point.z);

            PlaceAObject();

            yield return null;
        }

        yield return new WaitForSeconds(0f);
    }

    void UnSelectObject()
    {
        if(Input.GetMouseButtonDown(1)&&clicked)
        {
            clicked = false;
            StopCoroutine(CourtineObjectShow);
            DestroyObject(InGameObject);
        }
    }

    void PlaceAObject()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Instantiate(Flyer, InGameObject.transform.position, Quaternion.identity);
        }
    }

    public void DestroyBulding()
    {
        clickedForDestroy = true;

        StartCoroutine(SelectToDestroy());
    }

    IEnumerator SelectToDestroy()
    {   
        while (clickedForDestroy)
        {

            if (SelectedObject != null)
            {
                ChangeMaterial(SelectedObject, DefaultGrid);

                SelectedObject = null;
            }


            if (BuldingSystem.BuldySis.Hit.collider.gameObject.CompareTag("Wall"))
            {
                SelectedObject = BuldingSystem.BuldySis.Hit.collider.gameObject;
            }

            if(SelectedObject!=null)
            {
                ChangeMaterial(SelectedObject, SelectedGrid);             

                if(Input.GetMouseButtonDown(0))
                {
                    DestroyObject(SelectedObject);
                }              
            }
            SelectedObject = SelectedObject;

            

            yield return null;
        }

       
    }

    Material ChangeMaterial(GameObject Object, Material mat) => Object.GetComponent<Renderer>().material = mat;
}

