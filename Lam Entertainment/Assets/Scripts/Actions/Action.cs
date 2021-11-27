using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action : MonoBehaviour
{
    bool performAction = false;
    

    public void StartAction(bool B)
    {
        performAction = B;
    }

    public bool isActionActive()
    {
        return performAction;
    }
}
