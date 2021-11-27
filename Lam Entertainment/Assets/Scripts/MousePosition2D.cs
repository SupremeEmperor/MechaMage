using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePosition2D : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    [SerializeField] LayerMask layerMask;
    [SerializeField] GameObject player;
    [SerializeField] bool useController = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        if (!useController)
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, layerMask))
            {
                transform.position = raycastHit.point;
            }
        }
        else
        {
            float H = Input.GetAxisRaw("H_Aim") * 1000;
            float V = Input.GetAxisRaw("V_Aim") * 1000;

            if (player.GetComponent<TwoDMovement>())
            {
                if (player.GetComponent<TwoDMovement>().GetLookRight())
                {
                    H = H + 100;
                }
                else
                {
                    H = H - 100;
                }
                
            }

            transform.position = new Vector3(player.transform.position.x + H, player.transform.position.y - V + .5f, 0);
        }
            
        
        
    }
}
