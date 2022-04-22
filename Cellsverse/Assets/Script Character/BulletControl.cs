using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BulletControl : MonoBehaviour{
    public float damage;
    public GameObject hitEffect;
    public AudioClip explosionSound;
    healthBarControl HBControl;
    PhotonView PV;

    void Start(){
        PV = GetComponent<PhotonView>();
        HBControl = GetComponent<healthBarControl>();
        UpdateDamage();
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

    void Update(){
        UpdateDamage();
    }

    void UpdateDamage(){
        //update damage base on level and maxHP
        damage = HBControl.maxHP/100 * HBControl.lv * 1.5f;
    }
}