using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Resorces
{
    public enum TypeOfResource { Water,Gold,Wood,Electricity };

    public TypeOfResource Type_Of_Resource;

    public int AmountOfResource;
}
