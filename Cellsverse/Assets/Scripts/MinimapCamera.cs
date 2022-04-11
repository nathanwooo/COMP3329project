using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCamera : MonoBehaviour
{
    //public Transform player;
    
    void Start()
    {
        
        Vector3 newP = transform.localPosition;
        transform.localPosition = new Vector3(newP.x, newP.y, -2);
        
    }

    
    void LateUpdate()
    {

        
    }
}
