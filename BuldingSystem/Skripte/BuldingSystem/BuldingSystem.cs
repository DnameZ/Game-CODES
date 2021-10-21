using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;


public class BuldingSystem : MonoBehaviour
{
    public Camera camera;

    public RaycastHit Hit;

    public string[] TagsOfInteractAbleObjects;

    public static BuldingSystem BuldySis { get; private set; }


    private void Awake()
    {
        BuldySis = this;
    }

    // Update is called once per frame
    void Update()
    {
        RayCasting();
    }

    void RayCasting()
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray,out Hit,Mathf.Infinity))
        {
            while(Hit.collider!=null)
            {
                foreach(string Tag in TagsOfInteractAbleObjects)
                {
                    CheckObjectsTag(Tag);
                }
                break;
            }
        }
    }

    public bool CheckObjectsTag(string Tag)
    {
        bool HitedObject = Hit.collider.gameObject.CompareTag(Tag);

        return HitedObject;
    }
}
