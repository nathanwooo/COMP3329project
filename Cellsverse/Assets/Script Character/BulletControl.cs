using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour{
    public static float damage;
    public GameObject hitEffect;
    public AudioClip explosionSound;

    void Start(){
        UpdateDamage();
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        AudioSource.PlayClipAtPoint(explosionSound, transform.position);
        Destroy(effect, 0.3f);
        Destroy(gameObject);
        if (collision.gameObject.name == "Immue(Clone)"){//when more characters need change
            healthBarControl.currentHP = healthBarControl.currentHP - damage;
        }
    }

    void Update(){
        UpdateDamage();
    }

    void UpdateDamage(){
        //update damage base on level and maxHP
        damage = healthBarControl.maxHP/100 * healthBarControl.lv * 1.5f;
    }
}
