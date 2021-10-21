using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : BuldingsInfo, IButtonsForBulding
{

    /// skripta sluzi za bulidanje zida na x-osi ili z-osi 

    [SerializeField] GameObject AnchorToBulit;

    public GameObject firstAnchorForWall;

    GameObject secondAnhorWall;

    private SnapAxis SnapyAxis;

    public string BuldyFirstWallAnchor = "BulidFirstAnchor";

    public int AmountOfWallAnchors = 0;

    private void Awake()
    {
        SnapyAxis = SnapAxis.X;
    }

    public void BulidBulding()
    {
        clicked = true;

        StartCoroutine(BuldyFirstWallAnchor);
    }

    public IEnumerator BulidFirstAnchor()
    {
      
        InGameObject = Instantiate(PrefabForBulding, Input.mousePosition, Quaternion.identity);

        while (clicked && BuldingSystem.BuldySis.Hit.collider != null&&firstAnchorForWall==null)
        {   
            InGameObject.transform.position = new Vector3(BuldingSystem.BuldySis.Hit.point.x, 0f, BuldingSystem.BuldySis.Hit.point.z);

            PlaceAWallAnchors(InGameObject.transform.position);

            yield return null;
        }

        while (clicked && BuldingSystem.BuldySis.Hit.collider != null && firstAnchorForWall != null)
        {
            float Distance = Vector3.Distance(firstAnchorForWall.transform.position, InGameObject.transform.position);

            switch (SnapyAxis)
            {
                case SnapAxis.X:
                    InGameObject.transform.position = new Vector3(firstAnchorForWall.transform.position.x, 0f, BuldingSystem.BuldySis.Hit.point.z);
                    if (Input.GetKeyDown(KeyCode.Z))
                        SnapyAxis = SnapAxis.Z;
                    break;

                case SnapAxis.Z:
                    InGameObject.transform.position = new Vector3(BuldingSystem.BuldySis.Hit.point.x, 0f, firstAnchorForWall.transform.position.z);
                    if (Input.GetKeyDown(KeyCode.X))
                        SnapyAxis = SnapAxis.X;
                    break;
            }

            PlaceAWallAnchors(InGameObject.transform.position);

            yield return null;
        }

        GetMeFinalWall();

        yield return new WaitForSecondsRealtime(0.1f);
    }

    public void PlaceAWallAnchors(Vector3 PosOfBuldingSite)
    {
        if(Input.GetMouseButtonDown(0))
        {
            AmountOfWallAnchors += 1;

            switch (AmountOfWallAnchors)
            {
                case 1:
                    firstAnchorForWall = Instantiate(PrefabToBulit, PosOfBuldingSite, Quaternion.identity);
                    break;
                case 2:
                    secondAnhorWall = Instantiate(PrefabToBulit, PosOfBuldingSite, Quaternion.identity);
                    break;
            }
        }
    }

    void GetMeFinalWall()
    {

        if(firstAnchorForWall!=null&&secondAnhorWall!=null)
        {
            float x1 = firstAnchorForWall.transform.position.x;

            float x2 = secondAnhorWall.transform.position.x;

            float y1 = firstAnchorForWall.transform.position.y;

            float y2 = secondAnhorWall.transform.position.y;

            float z1 = firstAnchorForWall.transform.position.z;

            float z2 = secondAnhorWall.transform.position.z;


            float Distance = Vector3.Distance(firstAnchorForWall.transform.position, secondAnhorWall.transform.position);

            switch (SnapyAxis)
            {
                case SnapAxis.X:
                    PrefabToBulit.transform.localScale = new Vector3(PrefabToBulit.transform.localScale.x, PrefabToBulit.transform.localScale.y,Distance + 0.90f);
                    break;
                case SnapAxis.Z:
                    PrefabToBulit.transform.localScale = new Vector3(Distance + 0.90f, PrefabToBulit.transform.localScale.y, PrefabToBulit.transform.localScale.z);
                    break;
            }

            float xMiddle = ReturnMiddle(x1, x2);
            float yMiddle=  ReturnMiddle(y1, y2);
            float zMiddle=  ReturnMiddle(z1, z2);

            GameObject wall=Instantiate(PrefabToBulit, new Vector3(xMiddle,yMiddle,zMiddle), Quaternion.identity);

            switch(SnapyAxis)
            {
                case SnapAxis.X:
                   GameObject firstAnchorX = Instantiate(AnchorToBulit, new Vector3(firstAnchorForWall.transform.position.x, firstAnchorForWall.transform.position.y, firstAnchorForWall.transform.position.z - 0.91f), Quaternion.identity);
                   GameObject secondAnchorX = Instantiate(AnchorToBulit, new Vector3(secondAnhorWall.transform.position.x, secondAnhorWall.transform.position.y, secondAnhorWall.transform.position.z + 0.91f), Quaternion.identity);
                   firstAnchorX.transform.parent = wall.transform;
                   secondAnchorX.transform.parent = wall.transform;
                   firstAnchorX.SetActive(false);
                   secondAnchorX.SetActive(false);
                break;
                case SnapAxis.Z:
                    GameObject firstAnchorZ=Instantiate(AnchorToBulit, new Vector3(firstAnchorForWall.transform.position.x - 0.91f, firstAnchorForWall.transform.position.y, firstAnchorForWall.transform.position.z), Quaternion.identity);
                    GameObject secondAnchorZ=Instantiate(AnchorToBulit, new Vector3(secondAnhorWall.transform.position.x+0.91f, secondAnhorWall.transform.position.y, secondAnhorWall.transform.position.z), Quaternion.identity);
                    firstAnchorZ.transform.parent = wall.transform;
                    secondAnchorZ.transform.parent = wall.transform;
                    firstAnchorZ.SetActive(false);
                    secondAnchorZ.SetActive(false);
                break;
            }

            ResetAll();
        }
    }

    float ReturnMiddle(float Num1,float Num2)
    {
        return  (Num1+Num2) / 2f;
    }

 


    void ResetAll()
    {
        PrefabToBulit.transform.localScale = new Vector3(1f, 1f, 1f);
        AmountOfWallAnchors = 0;
        DestroyObject(firstAnchorForWall);
        DestroyObject(secondAnhorWall);
    }

    enum SnapToAxis {X,Z};

   
}
