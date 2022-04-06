using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCycleScript : MonoBehaviour
{
    private Transform circleTransform;
    private void Awake()
    {
        //instance = this;

        //circleShrinkSpeed = 20f;

        circleTransform = transform.Find("circle");
        if (circleTransform != null)
        {
            //Find the child named "ammo" of the gameobject "magazine" (magazine is a child of "gun").
            Debug.Log("Found");
        }
        else Debug.Log("No child with the name 'circleTransform' attached to the player");
        SetCircleSize(new Vector3(50,50));
    }
    //private void SetCircleSize(Vector3 position, Vector3 size)
    private void SetCircleSize(Vector3 size)
    {
        //circlePosition = position;
        //circleSize = size;

        //transform.position = position;

        circleTransform.localScale = size;

       
    }

}
