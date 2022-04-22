using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class swordControl : MonoBehaviour{
    public AudioClip swordSound;
    private float slashRate = 0.6f;
    private float nextSlash = 0f;
    PhotonView PV;
    healthBarControl HBControl;

    void start(){
        HBControl = GetComponent<healthBarControl>();
        PV = GetComponent<PhotonView>();
    }
    void Update(){
        if (Input.GetMouseButton(1) && Time.time > nextSlash)
        {
            AudioSource.PlayClipAtPoint(swordSound, transform.position);
            nextSlash = Time.time + slashRate;
        }
    }

    public void OnTriggerEnter36D(Collider2D collision){
        if(!PV.IsMine){
            Debug.Log("Cry to death");
            float swordDamage = HBControl.damage * 1.5f;
            int viewID = collision.gameObject.GetComponent<PhotonView>().ViewID;
            PV.RPC("enemyDamaged", RpcTarget.Others, swordDamage, viewID);
        }
    }

    [PunRPC]
    void enemyDamaged(float swordDamage, int viewID)
    {
        var player = PhotonView.Find(viewID).gameObject;
        player.GetComponent<healthBarControl>().currentHP -= HBControl.damage;
    }

    
}