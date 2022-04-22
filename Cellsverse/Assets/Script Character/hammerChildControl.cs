using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hammerChildControl : MonoBehaviour
{
    hammerControl HMControl;
    void Start(){
        HMControl = this.transform.parent.parent.gameObject.GetComponent<hammerControl>();
    }
    void OnTriggerEnter2D(Collider2D collision){
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.name == "PlayerBoundary")
        {
           HMControl.OnTriggerEnter36D(collision);
        }
    }
}
