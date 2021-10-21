using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invoker : MonoBehaviour
{
    static Queue<ICommand> KOMANDE;

    private void Awake()
    {
        KOMANDE = new Queue<ICommand>();
    }

    public static void AddComand(ICommand KOMANDA)
    {
        KOMANDE.Enqueue(KOMANDA);
    }

    private void Update()
    {
        if (KOMANDE.Count > 0)
        {
            ICommand command = KOMANDE.Dequeue();

            command.Excute();
        }
    }
}
