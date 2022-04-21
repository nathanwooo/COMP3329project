using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BulletControl : MonoBehaviour{
    public static float damage;
    public GameObject hitEffect;
    public AudioClip explosionSound;
    healthBarControl HBControl;

    void Start(){
        HBControl = GetComponent<healthBarControl>();
        UpdateDamage();
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        AudioSource.PlayClipAtPoint(explosionSound, transform.position);
        Destroy(effect, 0.3f);
        PhotonNetwork.Destroy(gameObject);
        if (collision.gameObject.name == "Immue(Clone)"){//when more characters need change
            HBControl.currentHP = HBControl.currentHP - damage;
        }
    }

    void Update(){
        UpdateDamage();
    }

    void UpdateDamage(){
        //update damage base on level and maxHP
        damage = HBControl.maxHP/100 * HBControl.lv * 1.5f;
    }
}
