using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDeath : MonoBehaviour
{
    [SerializeField]
    GameObject boss;
    [SerializeField]
    GameObject deathSpawn;
    [SerializeField]
    BulletDoorInteractor door;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (boss == null)
        {
            Debug.Log("The Boss is Dead");
            GameObject.Instantiate(deathSpawn);
            door.SetLock(false);
            Destroy(this.gameObject);
        }
    }
}
