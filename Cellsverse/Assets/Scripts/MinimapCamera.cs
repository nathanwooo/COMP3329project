using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCamera : MonoBehaviour
{
    //public Transform player;
    
    void Start()
    {
        Debug.Log("Z1:    " + transform.localPosition.z);
        Vector3 newP = transform.localPosition;
        transform.localPosition = new Vector3(newP.x, newP.y, -2);
        Debug.Log("Z:    "+transform.localPosition.z);
    }

    
    void LateUpdate()
    {

        Debug.Log("Z:    " + transform.localPosition.z);
    }
}
