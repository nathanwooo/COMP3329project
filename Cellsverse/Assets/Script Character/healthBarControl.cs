using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Photon.Pun;
public class healthBarControl : MonoBehaviour
{
    [SerializeField] private GameObject cv, et, nm, hpBar, mpBar, exp, immueScore, bacteriaScore;//canvas, elite, name
    [SerializeField] private GameObject lvCount;
    public float maxHP, maxMP, currentHP, currentMP, damage, extraDamage = 1, defense = 1f;
    public int lv;
    private float mpRegenRate = 1f, nextMpRegen = 0f;
    PhotonView PV;
    private bool willTP = true;
    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        maxHP = maxMP = currentHP = currentMP = 100;
        lv = 1;
        damage = lv * 8f * extraDamage;
        // cv = GameObject.Find("immue(Clone)/Canvas");
        // et = GameObject.Find("immue(Clone)/Canvas/Elite");
        // nm = GameObject.Find("immue(Clone)/Canvas/Elite/Name");
        // lvCount = GameObject.Find("immue(Clone)/Canvas/Elite/Level/Text");
        // hpBar = GameObject.Find("immue(Clone)/Canvas/Elite/Bars/Healthbar");
        // mpBar = GameObject.Find("immue(Clone)/Canvas/Elite/Bars/Manabar");
        // exp =  GameObject.Find("immue(Clone)/Canvas/Expbar/exp");
        // immueScore = GameObject.Find("immue(Clone)/Canvas/scoreboard/immueTotalScore");
        // bacteriaScore = GameObject.Find("immue(Clone)/Canvas/scoreboard/bacteriaTotalScore");
        nm.GetComponent<Text>().text = "Immune";
        exp.GetComponent<Image>().fillAmount = 0f;
        nm.GetComponent<Text>().text = this.gameObject.name;
        updateBar();
        lvCount.GetComponent<Text>().text = "1";
    }

    void Update(){
        if (PV.IsMine){
            refresh();
        }
    }

    void refresh(){
        Debug.Log(extraDamage);
        updateStats();
        updateBar();

        if (Input.GetKey(KeyCode.I) && lvCount.GetComponent<Text>().text != "6")
        {
            exp.GetComponent<Image>().fillAmount += 0.01f;
        }

        if (exp.GetComponent<Image>().fillAmount == 1f)
        {
            int newLv = int.Parse(lvCount.GetComponent<Text>().text) + 1;
            lvCount.GetComponent<Text>().text = newLv.ToString();
            exp.GetComponent<Image>().fillAmount = 0f;
            lv = int.Parse(lvCount.GetComponent<Text>().text);
            updateStats();
            currentHP = Mathf.Max(currentHP,maxHP/2);
            currentMP = Mathf.Max(currentMP,maxMP/2);
            updateBar();
        }

        if (Time.time > nextMpRegen && currentMP < maxMP){
            currentMP++;
            nextMpRegen = Time.time + mpRegenRate;
        }

        if (currentHP <= 0 && willTP)
        {
            lungLogic.hpToZero();
            willTP = false;
        }
    }
    

    void updateBar(){
        hpBar.GetComponent<Image>().fillAmount = currentHP/maxHP;
        mpBar.GetComponent<Image>().fillAmount = currentMP/maxMP;
    }



    void updateStats(){
        lv = int.Parse(lvCount.GetComponent<Text>().text);
        damage = maxHP/100 * 8f * extraDamage;
        maxHP = lv*100;
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (PV.IsMine){

            // Debug.Log(collision.gameObject.name);
            if (collision.gameObject.name == "nutrient(Clone)")
            {
                int viewID = collision.gameObject.GetComponent<PhotonView>().ViewID;
                PV.RPC("DestoryStuff", RpcTarget.AllBuffered, viewID);
                exp.GetComponent<Image>().fillAmount += 0.1f;
            }

            // // Debug.Log(collision.gameObject.GetComponent<PhotonView>().IsMine);
            // if (collision.gameObject.name == "bullets_side(Clone)" || collision.gameObject.name == "bullets_rifle(Clone)"){
            //     Debug.Log("Out");
            //     if (!collision.gameObject.GetComponent<PhotonView>().IsMine){
            //         Debug.Log("In");
            //         Debug.Log(collision.gameObject.GetComponent<BulletControl>().bulletDamage);
            //         currentHP -= collision.gameObject.GetComponent<BulletControl>().bulletDamage;
            //         int viewID = collision.gameObject.GetComponent<PhotonView>().ViewID;
            //         PV.RPC("DestoryStuff", RpcTarget.AllBuffered, viewID);
            //     }
            // }
        }
    }

    [PunRPC]
    void DestoryStuff(int viewID){
        var bullet = PhotonView.Find(viewID).gameObject;
        if (bullet.GetComponent<PhotonView>().IsMine)
        {
            PhotonNetwork.Destroy(bullet);
        }
        
    }

}