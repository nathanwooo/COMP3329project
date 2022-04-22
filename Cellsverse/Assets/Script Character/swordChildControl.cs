using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swordChildControl : MonoBehaviour
{
    swordControl SWControl;
    void Start(){
        SWControl = this.transform.parent.parent.gameObject.GetComponent<swordControl>();
    }
    void OnTriggerEnter2D(Collider2D collision){
        SWControl.OnTriggerEnter36D(collision);
    }
}
