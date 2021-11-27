using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorScript : MonoBehaviour
{
    [SerializeField]
    int nextScene;
    [SerializeField]
    int nextSceneSpawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("WhatIsPlayer"))
        {
            GameObject.FindGameObjectsWithTag("GameController")[0].GetComponent<GameController>().
                SetSpawnPoint(nextSceneSpawnPoint);
            SceneManager.LoadScene(nextScene);
        }
    }
}
