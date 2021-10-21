using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Resource Container", menuName = "Container")]
public class ResourceContainer : ScriptableObject
{
   public enum Resource_Type { Gold,Water,Wood,Electricity};

   public Resource_Type ResourceType;

   public int max_Amount;
}
