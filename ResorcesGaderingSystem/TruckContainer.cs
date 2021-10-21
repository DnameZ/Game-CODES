using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;
using UnityEngine.UI;

public class TruckContainer : MonoBehaviour
{
    [HideInInspector]
    public GameObject ObjectChanged;

    NavMeshAgent agent;

    [SerializeField] string[] tagsToInteractWith;

    [SerializeField] private Truck truck;

    private Container containerForResource;

    private Coroutine curr;

    private SetResources setResources;

    public int Current_Amount;

    const string Unit = "Unit";
    const string Water = "Water";
    const string Resource = "Resource";

    public bool OnPlace = false;

    private void Start() => agent = GetComponent<NavMeshAgent>();


    private void MoveTruck(Vector3 Destination,NavMeshAgent ObjectToMove)
    {
        ICommand command = new MoveObject(Destination,ObjectToMove);

        Invoker.AddComand(command);
    }

    private void Update()
    {
        DropARayCast();
    }

    Ray DropARayCast()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if(Physics.Raycast(ray,out hit,Mathf.Infinity))
        {
            if(Input.GetMouseButtonDown(0))
                MoveTruck(hit.point, agent);

            switch(hit.collider.transform.gameObject.CompareTag(Unit))
            {
                case true:
                    ObjectChanged = hit.collider.transform.gameObject;
                    ChangeShader.ChangeShaderOfObject(ChangeShader.OnHoverAndPick, ObjectChanged);

                    if (Input.GetMouseButtonDown(0))
                        SelectedUnits.Selected_Units.Add(ObjectChanged);
                        
                break;

                case false:
                    if (ObjectChanged != null)
                    {
                        ChangeShader.ChangeShaderOfObject(ChangeShader.Standard,ObjectChanged);

                        ObjectChanged = null;
                    }
                        
                break;
            }

        }

        return ray;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out setResources));
        CheckForCollisionTag(tagsToInteractWith,other);
        curr=StartCoroutine(Mine(10));
        CheckForAmount();
    }

    private void OnTriggerStay(Collider other)
    {
        CheckForCollisionTag(tagsToInteractWith, other);
    }

    private void OnTriggerExit(Collider other)
    {
        CheckForAmount().Equals(false);
        StopCoroutine(curr);
    }




    IEnumerator Mine(int DecValue)
    {
        while(CheckForAmount())
        {
            yield return new WaitForSeconds(5);
                setResources.resources.AmountOfResource -= DecValue;
                Current_Amount += DecValue;
        }
    }

    void CheckForCollisionTag(string[] tags,Collider colider)
    {
        foreach(string tag in tags)
        {
            if (colider.gameObject.CompareTag(tag))
            {
                switch(tag)
                {
                    case Resource:
                        if (agent.velocity == Vector3.zero)
                            CheckForAmount();
                    break;

                    case Water:
                        if (agent.velocity==Vector3.zero)
                            OnPlace = true;
                    break;
                }
            }
                  
        }
    }

    bool CheckForAmount()
    {
        bool isMining=false;

        if (Current_Amount < truck.max_Amount)
             isMining = true;
        else if(Current_Amount == truck.max_Amount)
        {
            isMining = false;
            StopCoroutine(curr);
            MoveTruck(FindContainer(Water).transform.position, agent);
        }

        return isMining;

    }

    GameObject FindContainer(string TypeOfResource)
    {
        int MinimalAmount;

        List<GameObject> Containers= new List<GameObject>();

        List<int> ContainersAmount = new List<int>();

        GameObject ContainerWithLowestAmount=null;

        Containers.AddRange(GameObject.FindGameObjectsWithTag(TypeOfResource));

        foreach (GameObject container in Containers)
        {
            container.TryGetComponent(out containerForResource);

            ContainersAmount.Add(containerForResource.cur_Amount);
        }

        MinimalAmount = ContainersAmount.Min();

        foreach (GameObject container in Containers)
        {
            container.TryGetComponent(out containerForResource);

            if(containerForResource.cur_Amount==MinimalAmount)
            {
                ContainerWithLowestAmount = containerForResource.transform.gameObject;
            }
        }

        return ContainerWithLowestAmount;

    }

    Transform FindClosestResource()
    {
        const string Res = Resource;

        float minDist = Mathf.Infinity;

        Vector3 CurPos = agent.transform.position;

        List<GameObject> resources = new List<GameObject>();

        Transform ClosestResource=null;

        resources.AddRange(GameObject.FindGameObjectsWithTag(Res));

        foreach (GameObject resource in resources)
        {
            float dist = Vector3.Distance(resource.transform.position, CurPos);

            if (dist < minDist)
            {
                ClosestResource = resource.transform;

                minDist = dist;
            }
        }

        return ClosestResource;
    }


    public void StartOperationMining()
    {
        MoveTruck(FindClosestResource().position, agent);
    }

}
