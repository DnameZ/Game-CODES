using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class DefensiveStructure
{
    public enum TypeOfDefensiveStructure { Tower, Artillery };

    public TypeOfDefensiveStructure Type_Of_DefensiveStructure;

    public int Hp;

    public bool atacking = false;

    public float maxTemperatureOfBulding;

    public float curTemperatureOfBulding;

}
