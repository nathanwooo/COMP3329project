using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rifleControl : MonoBehaviour{
    public GameObject firePoint;
    private float bulletForce = 10f, fireRate = 0.3f, nextFire = 0f;
    private Vector2 mousePosition;
    private Camera cam;
    [SerializeField] public GameObject bulletPrefab;
    [SerializeField] private SpriteRenderer rifleUp, rifleDown, rifleLeft, rifleRight;
    private Transform tf;
    public AudioClip shootSound;
    void Start(){
        firePoint = GameObject.Find("Immue(Clone)/firepoint");
        Debug.Log("firepoint", firePoint);
        Rigidbody2D rb = firePoint.AddComponent<Rigidbody2D>();
        rb.gravityScale = 0f; 
        cam = Camera.main;
        // rifleUp = GameObject.Find("Bacteria/rifle_up/rifle_up").GetComponent<SpriteRenderer>();
        // rifleDown = GameObject.Find("Bacteria/rifle_down/rifle_down").GetComponent<SpriteRenderer>();
        // rifleLeft = GameObject.Find("Bacteria/rifle_left/rifle_side").GetComponent<SpriteRenderer>();
        // rifleRight = GameObject.Find("Bacteria/rifle_right/rifle_side").GetComponent<SpriteRenderer>();
        tf = firePoint.transform; 
    }

    void Update(){
        if (Input.GetMouseButton(0) && Time.time > nextFire)
        {
            AudioSource.PlayClipAtPoint(shootSound, transform.position);
            nextFire = Time.time + fireRate;
            StartCoroutine(shoot());
        }
    }
    
    IEnumerator shoot(){
        //SpriteRenderer sr = GetComponent<SpriteRenderer>();
        Rigidbody2D rb = firePoint.GetComponent<Rigidbody2D>();
        if (rifleUp.enabled)
        {
            tf.position = this.gameObject.transform.position + new Vector3(0, 2.5f, 0);
        }
        else if (rifleDown.enabled)
        {
            tf.position = this.gameObject.transform.position + new Vector3(0, -1f, 0);
        }
        else if (rifleLeft.enabled)
        {
            tf.position = this.gameObject.transform.position + new Vector3(-1.7f, 0.5f, 0);
        }
        else if (rifleRight.enabled)
        {
            tf.position = this.gameObject.transform.position + new Vector3(1.7f, 0.5f, 0);
        }
        Vector2 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 aimDirection = mousePosition - new Vector2(tf.position.x, tf.position.y);
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        rb.rotation = aimAngle;
        // Debug.Log(aimAngle);
        yield return new WaitForSeconds(0.3f);
        GameObject bullet = Instantiate(bulletPrefab, tf.position, tf.rotation);
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        bulletRb.AddForce(tf.right * bulletForce, ForceMode2D.Impulse);
    }
}
