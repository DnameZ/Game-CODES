using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SnapyScript : BuldingsInfo
{
    /// skripta sluzi za snapanje zida uz zid kako bi izgledalo simetricno

    Wall Wally;

    public List<Transform> ANCHORS = new List<Transform>();

    public bool Wall = false;
    bool Hited = false;

    string ClickedOnObject = "ClickedOnSnapAbleObject";
    string Clone = "(Clone)";
    
    GameObject ObjectClickedOn;
    public GameObject[] Test;
    

    private void Awake()
    {
        Wally = FindObjectOfType<Wall>();
    }

    private void Update()
    {
        ShowAnchors();
        BulidFoundation();
    }

    IEnumerator ClickedOnSnapAbleObject()
    {
        ObjectClickedOn = BuldingSystem.BuldySis.Hit.collider.gameObject;
        FindInactiveChildren(false);
        yield return new WaitForSecondsRealtime(0.1f);
    }

    void BulidFoundation()
    {
        if(Input.GetMouseButtonDown(0))
        {           
            foreach (string Tag in BuldingSystem.BuldySis.TagsOfInteractAbleObjects)
            {
                if(BuldingSystem.BuldySis.CheckObjectsTag(Tag))
                {
                    Hited = true;

                   foreach(GameObject gameObject in Test)
                    {
                        if(gameObject.tag==Tag&&BuldingSystem.BuldySis.Hit.collider.gameObject.name==gameObject.tag + Clone)
                        {                           
                            GameObject ObjectToBulit = gameObject;
                            GameObject Anchor = BuldingSystem.BuldySis.Hit.collider.gameObject;
                            clicked = true;
                            Wally.firstAnchorForWall = Instantiate(ObjectToBulit, Anchor.transform.position, Quaternion.identity);
                            Wally.BulidBulding();
                            DestroyObject(Anchor);
                        }
                    }
                }
            }
        }

        
    }

     void ShowAnchors()
    {
        if(Input.GetMouseButtonDown(0)&&Hited)
        {
            StartCoroutine(ClickedOnObject);
        }

        if (Input.GetMouseButtonDown(1) && ANCHORS.Count > 0)
        {
            FindInactiveChildren(true);
            StopCoroutine(ClickedOnObject);
        }
    }

    void FindInactiveChildren(bool isActive)
    {
        foreach(Transform obj in ObjectClickedOn.transform)
        {
            ANCHORS.Add(obj);
            obj.transform.gameObject.SetActive(!isActive);
        }
    }
}
