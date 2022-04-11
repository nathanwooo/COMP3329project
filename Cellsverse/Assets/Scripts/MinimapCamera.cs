using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCamera : MonoBehaviour
{
    //public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Z1:    " + this.transform.position.z);
        Vector3 newP = transform.position;
        transform.position = new Vector3(newP.x, newP.y, -2);
        Debug.Log("Z:    "+this.transform.position.z);
    }

    // Update is called once per frame
    /*void LateUpdate()
    {

        Vector3 newPosition = player.position; 
        newPosition.z = this.transform.position.z; 
       
        transform.position = newPosition;
    }*/
}
