using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    //an int that tells the levels which spawn point to load the character at.
    int spawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        Object.DontDestroyOnLoad(this.gameObject);  
        spawnPoint = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetSpawnPoint(int sp)
    {
        spawnPoint = sp;
    }

    public int GetSpawnPoint()
    {
        return spawnPoint;
    }
}
