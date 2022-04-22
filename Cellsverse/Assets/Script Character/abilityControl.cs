using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
public class abilityControl : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject mp, hp; //serial this
    [SerializeField] private GameObject cdSpeedUp, cdFlash, cdHeal, cdDefense, cdAttack;
    private float currentMp, currentHp;
    private bool speedUp, healthUp;
    public AudioClip flashSound, healSound, defenseSound, speedSound, attackSound;
    public GameObject flashEffect, healEffect, defenseEffect, attackEffect;
    private Vector3 mousePosition;
    public Camera cam;
    PhotonView PV;
    healthBarControl HBControl;
    immueControl IMControl;
    void Start()
    {
        PV = GetComponent<PhotonView>();
        HBControl = GetComponent<healthBarControl>();
        IMControl = GetComponent<immueControl>();
        // hp = GameObject.Find("Canvas/Elite/Bars/Healthbar");
        // mp = GameObject.Find("Canvas/Elite/Bars/Manabar");
        // Debug.Log(mp.GetComponent<Image>().fillAmount);
        // currentHp = mp.GetComponent<Image>().fillAmount;
        // currentMp = mp.GetComponent<Image>().fillAmount;
        // cdSpeedUp = GameObject.Find("Canvas/speed/cd");
        // cdFlash = GameObject.Find("Canvas/flash/cd");
        // cdHeal = GameObject.Find("Canvas/heal/cd");
        // cdDefense = GameObject.Find("Canvas/defense/cd");
        // cdAttack = GameObject.Find("Canvas/attack/cd");
    }

    void Update(){
        if (PV.IsMine){
            ability();
        }
    }
    void ability()
    {
        if (!speedUp)
        {
            if(Input.GetKey(KeyCode.Q) && HBControl.currentMP >= 10f && cdSpeedUp.GetComponent<Text>().text == "")
            {
                HBControl.currentMP -= 10f;
                speedUp = true;
                StartCoroutine(Accelerate());
                StartCoroutine(coolDownSpeed());

            }
        }

        if (Input.GetKeyDown(KeyCode.E) && HBControl.currentMP >= 10f && cdFlash.GetComponent<Text>().text == ""){
            flash();
            HBControl.currentMP -= 10f;
            StartCoroutine(coolDownFlash());
        }

        if (Input.GetKeyDown(KeyCode.H) && HBControl.currentMP >= 30f && cdHeal.GetComponent<Text>().text == ""){
            heal();
            HBControl.currentMP -= 30f;
            StartCoroutine(coolDownHeal());
        }

        if (Input.GetKeyDown(KeyCode.R) && HBControl.currentMP >= 20f && cdDefense.GetComponent<Text>().text == ""){
            StartCoroutine(Defense());
            HBControl.currentMP -= 20f;
            StartCoroutine(coolDownDefense());
        }

        if (Input.GetKeyDown(KeyCode.T) && HBControl.currentMP >= 10f && cdAttack.GetComponent<Text>().text == ""){
            StartCoroutine(Attack());
            HBControl.currentMP -= 10f;
            StartCoroutine(coolDownAttack());
        }
    }

    IEnumerator Accelerate()
    {
        if(PV.IsMine)
        {
            PV.RPC("enemyAccelerate", RpcTarget.OthersBuffered);
            AudioSource.PlayClipAtPoint(speedSound, this.transform.position);
            IMControl.speed = 5f;
            yield return new WaitForSeconds(3f);
            IMControl.speed = 2f;
            speedUp = false;
        }
    }

    [PunRPC]
    IEnumerator enemyAccelerate()
    {
        AudioSource.PlayClipAtPoint(speedSound, this.transform.position);
        IMControl.speed = 5f;
        yield return new WaitForSeconds(3f);
        IMControl.speed = 2f;
        speedUp = false;
    }

    IEnumerator Defense(){
        if(PV.IsMine)
        {
            PV.RPC("enemyDefense", RpcTarget.OthersBuffered);
            //reduce the damage taken by a percentage for 10s, reduce 10% per level
            HBControl.defense = 1f - HBControl.lv*0.1f;
            AudioSource.PlayClipAtPoint(defenseSound, this.transform.position);
            GameObject armor = Instantiate(defenseEffect, this.transform.position + new Vector3(0,0.5f,0), this.transform.rotation);
            Destroy(armor, 0.2f);
            yield return new WaitForSeconds(5f);
            HBControl.defense = 1f;
        }
    }

    [PunRPC]
    IEnumerator enemyDefense(){
        //reduce the damage taken by a percentage for 10s, reduce 10% per level
        HBControl.defense = 1f - HBControl.lv*0.1f;
        AudioSource.PlayClipAtPoint(defenseSound, this.transform.position);
        GameObject armor = Instantiate(defenseEffect, this.transform.position + new Vector3(0,0.5f,0), this.transform.rotation);
        Destroy(armor, 0.2f);
        yield return new WaitForSeconds(5f);
        HBControl.defense = 1f;
    }
    IEnumerator Attack()
    {
        if(PV.IsMine)
        {
            PV.RPC("enemyAttack", RpcTarget.OthersBuffered);
            AudioSource.PlayClipAtPoint(attackSound, this.transform.position);
            GameObject attack = Instantiate(attackEffect, this.transform.position + new Vector3(0,2f,0), this.transform.rotation);
            Destroy(attack, 0.2f);
            HBControl.extraDamage = 1f + HBControl.lv*0.1f;
            yield return new WaitForSeconds(5f);
            HBControl.extraDamage = 1f;
        }
    }

    [PunRPC]
    IEnumerator enemyAttack()
    {
        AudioSource.PlayClipAtPoint(attackSound, this.transform.position);
        GameObject attack = Instantiate(attackEffect, this.transform.position + new Vector3(0,2f,0), this.transform.rotation);
        Destroy(attack, 0.2f);
        HBControl.extraDamage = 1f + HBControl.lv*0.1f;
        yield return new WaitForSeconds(5f);
        HBControl.extraDamage = 1f;
    }
    void flash(){
        if (PV.IsMine)
        {
            mousePosition = Input.mousePosition;
            mousePosition = cam.ScreenToWorldPoint(mousePosition);
            PV.RPC("enemyFlash", RpcTarget.OthersBuffered);
            //calculate the mouse position from the character position and normalized it to 1 max
            Vector2 result = (transform.position - mousePosition).normalized;
            //the flash effect
            GameObject flash = Instantiate(flashEffect, transform.position, Quaternion.identity);
            Destroy(flash,0.2f);
            //times 10 to increase the flash range
            this.transform.Translate(-result*10f);
            AudioSource.PlayClipAtPoint(flashSound, transform.position);
        }
    }

    [PunRPC]
    void enemyFlash()
    {
        Vector2 result = (transform.position - mousePosition).normalized;
        GameObject flash = Instantiate(flashEffect, transform.position, Quaternion.identity);
        Destroy(flash,0.2f);
        //times 10 to increase the flash range
        this.transform.Translate(-result*10f);
        AudioSource.PlayClipAtPoint(flashSound, transform.position);
    }

    void heal(){
        if(PV.IsMine)
        {
            PV.RPC("enemyheal", RpcTarget.OthersBuffered);
            //healing bases on level, 30(base) + 10 per level
            HBControl.currentHP += 30 + HBControl.lv*10;
            AudioSource.PlayClipAtPoint(healSound, this.transform.position);
            GameObject healing = Instantiate(healEffect, this.transform.position, this.transform.rotation);
            Destroy(healing, 0.5f);

            if (HBControl.currentHP > HBControl.maxHP){
                HBControl.currentHP = HBControl.maxHP;
            }
        }
    }

    [PunRPC]
    void enemyheal(){
        //healing bases on level, 30(base) + 10 per level
        HBControl.currentHP += 30 + HBControl.lv*10;
        AudioSource.PlayClipAtPoint(healSound, this.transform.position);
        GameObject healing = Instantiate(healEffect, this.transform.position, this.transform.rotation);
        Destroy(healing, 0.5f);

        if (HBControl.currentHP > HBControl.maxHP){
            HBControl.currentHP = HBControl.maxHP;
        }
    }

    IEnumerator coolDownSpeed()
    {
        for (int i = 9; i > 0; i--){
            cdSpeedUp.GetComponent<Text>().text = i.ToString();
            yield return new WaitForSeconds(1f);
        }
        cdSpeedUp.GetComponent<Text>().text = "";
    }
    IEnumerator coolDownFlash()
    {
        for (int i = 9; i > 0; i--){
            cdFlash.GetComponent<Text>().text = i.ToString();
            yield return new WaitForSeconds(1f);
        }
        cdFlash.GetComponent<Text>().text = "";
    }
    IEnumerator coolDownHeal()
    {
        for (int i = 9; i > 0; i--){
            cdHeal.GetComponent<Text>().text = i.ToString();
            yield return new WaitForSeconds(1f);
        }
        cdHeal.GetComponent<Text>().text = "";
    }
    IEnumerator coolDownDefense()
    {
        for (int i = 9; i > 0; i--){
            cdDefense.GetComponent<Text>().text = i.ToString();
            yield return new WaitForSeconds(1f);
        }
        cdDefense.GetComponent<Text>().text = "";
    }
    IEnumerator coolDownAttack()
    {
        for (int i = 9; i > 0; i--){
            cdAttack.GetComponent<Text>().text = i.ToString();
            yield return new WaitForSeconds(1f);
        }
        cdAttack.GetComponent<Text>().text = "";
    }

}


