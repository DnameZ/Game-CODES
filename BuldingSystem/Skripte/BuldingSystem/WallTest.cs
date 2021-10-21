using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTest : BuldingsInfo , IButtonsForBulding
{
    [SerializeField] GameObject GameObjectToDrag;
    [SerializeField] GameObject WallToBulit;
    const string WallSys = "SpawnWall";
    Vector3 truePos;
    public float gridSize;

    private SnapAxis SnapyAxis;

    private void Awake()
    {
        SnapyAxis = SnapAxis.X;
    }

    public void BulidBulding()
    {
        clicked = true;

        StartCoroutine(WallSys);
    }

    IEnumerator SpawnWall()
    {
        if(clicked&&InGameObject==null)
        {
            InGameObject = Instantiate(GameObjectToDrag, BuldingSystem.BuldySis.Hit.point, Quaternion.identity);
        }

        while (InGameObject != null)
        {
            SetWallPos();

            switch (SnapyAxis)
            {
                case SnapAxis.X:
                   
                    if (Input.GetKeyDown(KeyCode.Z))
                    {
                        SnapyAxis = SnapAxis.Z;
                        StartCoroutine(RotateWall(0.5f,90f));
                                       
                    }
                       
                    break;

                case SnapAxis.Z:                    
                    if (Input.GetKeyDown(KeyCode.X))
                    {
                        SnapyAxis = SnapAxis.X;
                        StartCoroutine(RotateWall(0.5f,180));                                      
                    }
                    break;            
            }

            BulidAWall();

            yield return null;
        }
    }

    Vector3 SetWallPos()
    {
        truePos.x = Mathf.Floor(BuldingSystem.BuldySis.Hit.point.x / gridSize) * gridSize;
        truePos.z = Mathf.Floor(BuldingSystem.BuldySis.Hit.point.z / gridSize) * gridSize;

        truePos.y = 0.525f;

       return InGameObject.transform.position = new Vector3(truePos.x,truePos.y,truePos.z);
    }

    void BulidAWall()
    {
        if(clicked&&Input.GetMouseButtonDown(0))
        {
            Instantiate(WallToBulit, InGameObject.transform.position, InGameObject.transform.rotation);
        }
    }


    IEnumerator RotateWall(float Times,float reqRotation)
    {
        Quaternion fromAngleZ = Quaternion.Euler(new Vector3(InGameObject.transform.rotation.x,InGameObject.transform.rotation.y,InGameObject.transform.rotation.z));
        Quaternion toAngleZ = Quaternion.Euler(new Vector3(0,90,0));

        Quaternion fromAngleX = Quaternion.Euler(new Vector3(InGameObject.transform.rotation.x, InGameObject.transform.rotation.y , InGameObject.transform.rotation.z));
        Quaternion toAngleX = Quaternion.Euler(new Vector3(0,180,0));


        for (float t = InGameObject.transform.rotation.y; t != reqRotation ; t += Time.deltaTime / Times)
        { 
                switch (SnapyAxis)
                {

                    case SnapAxis.X:
                        InGameObject.transform.rotation = Quaternion.Slerp(fromAngleX, toAngleX, t);                                                                                                                                                                                                                            
                    break;

                    case SnapAxis.Z:
                         InGameObject.transform.rotation = Quaternion.Slerp(fromAngleZ, toAngleZ, t);
                    break;

                    default:
                         InGameObject.transform.rotation = InGameObject.transform.rotation;
                    break;

                }

            yield return null;
        }

    }

    enum SnapToAxis { X, Z };
}
