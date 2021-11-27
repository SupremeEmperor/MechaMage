using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    [SerializeField]
    GameObject Player;
    [SerializeField]
    Transform[] playerSpawnPoints;
    // Start is called before the first frame update
    void Start()
    {
        int sp = GameObject.FindGameObjectsWithTag("GameController")[0].GetComponent<GameController>().GetSpawnPoint();
        if(sp >= playerSpawnPoints.Length)
        {
            sp = 0;
        }
        if(playerSpawnPoints.Length == 0)
        {
            Debug.Log("playerSpawnPoints is empty");
        }
        else
        {
            GameObject.Instantiate(Player, playerSpawnPoints[sp]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
