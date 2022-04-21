using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swordControl : MonoBehaviour{
    public AudioClip swordSound;
    private float slashRate = 0.6f;
    private float nextSlash = 0f;
    void Update(){
        if (Input.GetMouseButton(1) && Time.time > nextSlash)
        {
            AudioSource.PlayClipAtPoint(swordSound, transform.position);
            nextSlash = Time.time + slashRate;
        }
    }

    void OnTriggerEnter2D(Collider2D collider){
        if (collider.tag == "enemy"){
            Debug.Log("enemy detected");
        }
    }

}