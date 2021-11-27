using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField]
    GameObject bullet;
    [SerializeField]
    GameObject startPoint;
    [SerializeField]
    Transform center;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("The fire key is editable in the Gun.cs Script");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            GameObject Temp = Instantiate(bullet, startPoint.transform.position, center.rotation);
            //Temp.transform.rotation = center.rotation;
        }
    }
}
