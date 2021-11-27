using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOnDeath : MonoBehaviour
{
    [SerializeField]
    GameObject lastChild;

    

    private void OnDestroy()
    {
        Instantiate(lastChild, gameObject.transform.position, gameObject.transform.rotation);
    }
}
