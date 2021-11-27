using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeShader : MonoBehaviour
{
    [SerializeField]
    Material firstMaterial;
    [SerializeField]
    Material secondMaterial;
    [SerializeField]
    bool isSecondMaterial = false;
    Material previousMaterial;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isSecondMaterial)
        {
            gameObject.GetComponent<MeshRenderer>().material = secondMaterial; 
        }else 
        {
            gameObject.GetComponent<MeshRenderer>().material = firstMaterial;
        }
    }

    public void SetSecondMaterial(bool t)
    {
        isSecondMaterial = t;
    }


}
