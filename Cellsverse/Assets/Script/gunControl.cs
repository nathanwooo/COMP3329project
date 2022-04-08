using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunControl : MonoBehaviour{
    private GameObject firePoint;
    private float bulletForce = 10f;
    private float fireRate = 0.3f;
    private float nextFire = 0f;
    private Vector2 mousePosition;
    public GameObject bulletPrefab;
    private Camera cam;
    private SpriteRenderer gunUp;
    private SpriteRenderer gunDown;
    private SpriteRenderer gunLeft;
    private SpriteRenderer gunRight;
    private Transform tf;
    public AudioClip shootSound;
    void Start(){
        /* firePoint = GameObject.Find("Immue/firepoint");
        Debug.Log("firepoint", firePoint);
        Rigidbody2D rb = firePoint.AddComponent<Rigidbody2D>();
        rb.gravityScale = 0f; */
        cam = Camera.main;
        /* gunUp = GameObject.Find("Immue/gun_up/gun_up").GetComponent<SpriteRenderer>();
        gunDown = GameObject.Find("Immue/gun_down/gun_down").GetComponent<SpriteRenderer>();
        gunLeft = GameObject.Find("Immue/gun_left/gun_side").GetComponent<SpriteRenderer>();
        gunRight = GameObject.Find("Immue/gun_right/gun_side").GetComponent<SpriteRenderer>();
        tf = GameObject.Find("Immue/firepoint").transform; */
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
        if (gunUp.enabled)
        {
            tf.position = this.gameObject.transform.position + new Vector3(0, 2f, 0);
        }
        else if (gunDown.enabled)
        {
            tf.position = this.gameObject.transform.position + new Vector3(0, -1, 0);
        }
        else if (gunLeft.enabled)
        {
            tf.position = this.gameObject.transform.position + new Vector3(-1.5f, 0.5f, 0);
        }
        else if (gunRight.enabled)
        {
            tf.position = this.gameObject.transform.position + new Vector3(1.5f, 0.5f, 0);
        }
        Vector2 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 aimDirection = mousePosition - new Vector2(tf.position.x, tf.position.y);
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        rb.rotation = aimAngle;
        Debug.Log(aimAngle);

        yield return new WaitForSeconds(0.2f);
        GameObject bullet = Instantiate(bulletPrefab, tf.position, tf.rotation);
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        bulletRb.AddForce(tf.right * bulletForce, ForceMode2D.Impulse);
    }
}
