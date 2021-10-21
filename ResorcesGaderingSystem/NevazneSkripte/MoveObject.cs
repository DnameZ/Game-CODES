using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class MoveObject : ICommand
{
   
    [SerializeField]
    private Vector3 direction;
    private NavMeshAgent ObjectToMove;

    public MoveObject(Vector3 Direction,NavMeshAgent ObjectToMove)
    {
        this.direction = Direction;
        this.ObjectToMove = ObjectToMove;
    }

    void ICommand.Excute()
    {
        ObjectToMove.SetDestination(direction);
    }
}
