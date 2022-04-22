using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class gunControl : MonoBehaviourPunCallbacks {
    [SerializeField] private GameObject firePoint;
    private float bulletForce = 0.001f, fireRate = 0.3f, nextFire = 0f;
    private Vector2 mousePosition;
    [SerializeField] private Camera cam;
    [SerializeField] public GameObject bulletPrefab;
    [SerializeField] private SpriteRenderer gunUp, gunDown, gunLeft, gunRight;
    private Transform tf;
    public AudioClip shootSound;
    healthBarControl HBControl;
    void Start(){
        HBControl = GetComponent<healthBarControl>();
        firePoint = GameObject.Find("Immue(Clone)/firepoint");
        Debug.Log("firepoint", firePoint);
        cam = Camera.main;
        //gunUp = gameObject.GetComponent<SpriteRenderer>();
        //gunDown = gameObject.GetComponent<SpriteRenderer>();
        //gunLeft = gameObject.GetComponent<SpriteRenderer>();
        //gunRight = gameObject.GetComponent<SpriteRenderer>();
        tf = firePoint.transform; 
    }

    void Update(){
        if (Input.GetMouseButton(0) && Time.time > nextFire && !Input.GetMouseButton(1))
        {
            AudioSource.PlayClipAtPoint(shootSound, transform.position);
            nextFire = Time.time + fireRate;
            StartCoroutine(shoot());
        }
    }

    IEnumerator shoot(){
        Rigidbody2D rb = firePoint.GetComponent<Rigidbody2D>();
        if (gunUp.enabled)
        {
            tf.position = this.gameObject.transform.position + new Vector3(0, 1.8f, 0);
        }
        else if (gunDown.enabled)
        {
            tf.position = this.gameObject.transform.position + new Vector3(0, -0.6f, 0);
        }
        else if (gunLeft.enabled)
        {
            tf.position = this.gameObject.transform.position + new Vector3(-0.9f, 0.5f, 0);
        }
        else if (gunRight.enabled)
        {
            tf.position = this.gameObject.transform.position + new Vector3(0.9f, 0.5f, 0);
        }
        Vector2 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 aimDirection = mousePosition - new Vector2(tf.position.x, tf.position.y);
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        rb.rotation = aimAngle;
        // Debug.Log(aimAngle);
        yield return new WaitForSeconds(0.3f);
        GameObject bullet = PhotonNetwork.Instantiate(bulletPrefab.name, tf.position, tf.rotation);
        //set the bullet damage according to our level
        bullet.GetComponent<BulletControl>().bulletDamage = HBControl.damage;

        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        bulletRb.AddForce(tf.right * bulletForce, ForceMode2D.Impulse);
    }
}
