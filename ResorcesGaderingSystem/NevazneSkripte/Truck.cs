using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="New Truck", menuName ="Truck")]
public class Truck : ScriptableObject
{
    public GameObject TruckEngine;

    public enum Type_Of_Resource_Transporting { Gold,Water,Wood,Electricty };

    public Type_Of_Resource_Transporting TypeOfResourceTransporting;

    public int max_Amount;
}
