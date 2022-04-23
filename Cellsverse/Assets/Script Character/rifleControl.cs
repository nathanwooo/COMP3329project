using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class rifleControl : MonoBehaviourPunCallbacks {
    [SerializeField] private GameObject firePoint;
    private float bulletForce = 0.001f, fireRate = 0.1f, nextFire = 0f;
    private Vector2 mousePosition;
    [SerializeField] private Camera cam;
    [SerializeField] public GameObject bulletPrefab;
    [SerializeField] private SpriteRenderer rifleUp, rifleDown, rifleLeft, rifleRight;
    private Transform tf;
    public AudioClip shootSound;
    healthBarControl HBControl;
    PhotonView PV;
    void Start(){
        PV = GetComponent<PhotonView>();
        HBControl = GetComponent<healthBarControl>();
        // firePoint = this.transform.GetChild(4).gameObject;
        Debug.Log("firepoint", firePoint);
        cam = Camera.main;
        //gunUp = gameObject.GetComponent<SpriteRenderer>();
        //gunDown = gameObject.GetComponent<SpriteRenderer>();
        //gunLeft = gameObject.GetComponent<SpriteRenderer>();
        //gunRight = gameObject.GetComponent<SpriteRenderer>();
        tf = firePoint.transform; 
    }

    void Update(){
        if (PV.IsMine){
            Shooting();
        }
    }
    void Shooting(){
        if (Input.GetMouseButton(0) && Time.time > nextFire && !Input.GetMouseButton(1))
        {
            PV.RPC("enemyShooting",RpcTarget.Others);
            AudioSource.PlayClipAtPoint(shootSound, transform.position);
            nextFire = Time.time + fireRate;
            StartCoroutine(shoot());
        }
    }

    [PunRPC]
    void enemyShooting(){
        AudioSource.PlayClipAtPoint(shootSound, transform.position);
        nextFire = Time.time + fireRate;
        StartCoroutine(shoot());
    }

    IEnumerator shoot(){
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
        object[] arr = new object[1];

        arr[0] = HBControl.damage;
        GameObject bullet = PhotonNetwork.Instantiate(bulletPrefab.name, tf.position, tf.rotation,0, arr);
        
        //set the bullet damage according to our level
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        bulletRb.AddForce(tf.right * bulletForce, ForceMode2D.Impulse);
    }


}
