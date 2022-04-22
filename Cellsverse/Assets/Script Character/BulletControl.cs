using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BulletControl : MonoBehaviour{
    public float bulletDamage;
    public GameObject hitEffect;
    public AudioClip explosionSound;
    healthBarControl HBControl;
    PhotonView PV;

    void Start(){
        PV = GetComponent<PhotonView>();
        HBControl = GetComponent<healthBarControl>();
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (PV.IsMine)
        {
            PV.RPC("enemyBulletCollision", RpcTarget.OthersBuffered);
            // if (name == "Immue(Clone)")
            // {//when more characters need change
            //     HBControl.currentHP = HBControl.currentHP - damage;
            // }
            GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(explosionSound, transform.position);
            Destroy(effect, 0.3f);
            PhotonNetwork.Destroy(gameObject);
        }

    }

    [PunRPC]
    void enemyBulletCollision()
    {
        GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        AudioSource.PlayClipAtPoint(explosionSound, transform.position);
        Destroy(effect, 0.3f);
    }

}