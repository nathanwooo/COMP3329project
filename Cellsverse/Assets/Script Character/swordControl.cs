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

    void Start(){
        HBControl = GetComponent<healthBarControl>();
        PV = GetComponent<PhotonView>();

    }
    void Update(){
        if(PV.IsMine){
            Sword();
        }
    }
    void Sword(){
        if (Input.GetMouseButton(1) && Time.time > nextSlash)
        {
            PV.RPC("enemySword",RpcTarget.Others);
            AudioSource.PlayClipAtPoint(swordSound, transform.position);
            nextSlash = Time.time + slashRate;
        }
    }

    [PunRPC]
    void enemySword(){
        AudioSource.PlayClipAtPoint(swordSound, transform.position);
        nextSlash = Time.time + slashRate;
    }


    public void OnTriggerEnter36D(Collider2D collision){
        Debug.Log("Yes");
   
        float swordDamage = HBControl.damage * 1.5f;
        Debug.Log("damage:" + swordDamage);
        int viewID = collision.gameObject.GetComponentInParent<PhotonView>().ViewID;
        Debug.Log("ID:"+ viewID);
        PV.RPC("enemyDamaged", RpcTarget.Others, swordDamage, viewID);

    }

    [PunRPC]
    void enemyDamaged(float swordDamage, int viewID)
    {
        var player = PhotonView.Find(viewID).gameObject;
        player.GetComponent<healthBarControl>().currentHP -= swordDamage* player.GetComponent<healthBarControl>().defense;
    }

}