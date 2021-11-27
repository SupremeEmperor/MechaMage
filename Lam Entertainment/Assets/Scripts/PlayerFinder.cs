using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFinder : MonoBehaviour
{
    
    [SerializeField]
    LayerMask layer;
    GameObject player;
    bool seenPlayer;
    bool hasCollided;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<Player>())
        {
            seenPlayer = true;
            player = other.gameObject;
        }

        hasCollided = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.Equals(player))
        {

            player = null;
            seenPlayer = false;
        }
    }

    public bool GetSeenPlayer()
    {
        return seenPlayer;
    }

    public GameObject GetPlayer()
    {
        return player;
    }

    public bool GetHasCollided()
    {
        return hasCollided;
    }
}
