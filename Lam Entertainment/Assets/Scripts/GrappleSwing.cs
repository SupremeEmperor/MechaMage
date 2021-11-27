using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleSwing : MonoBehaviour
{
    [SerializeField]
    float rotation1 = -90;
    [SerializeField]
    float rotation2 = 90;
    [SerializeField]
    float smooth = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion target = Quaternion.Euler(0, 0, 0);

        transform.rotation = Quaternion.Slerp(transform.rotation, target, smooth * Time.deltaTime);
    }
}
