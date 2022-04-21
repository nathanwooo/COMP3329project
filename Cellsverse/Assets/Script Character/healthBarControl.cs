using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class healthBarControl : MonoBehaviour
{
    private GameObject cv;//canvas
    private GameObject et;//elite
    private GameObject nm;//name
    private GameObject hpBar;
    private GameObject mpBar;
    private GameObject exp;
    private static GameObject lvCount;
    public static float maxHP;
    static float maxMP;
    public static float currentHP;
    public static float currentMP;
    public static int lv;
    public static float damage, extraDamage = 1;
    public static float defense = 1f;
    // Start is called before the first frame update
    void Start()
    {
        maxHP = maxMP = currentHP = currentMP = 100;
        lv = 1;
        damage = maxHP/100 * 8f * extraDamage;
        cv = GameObject.Find("immue(Clone)/Canvas");
        et = GameObject.Find("immue(Clone)/Canvas/Elite");
        nm = GameObject.Find("immue(Clone)/Canvas/Elite/Name");
        lvCount = GameObject.Find("immue(Clone)/Canvas/Elite/Level/Text");
        hpBar = GameObject.Find("immue(Clone)/Canvas/Elite/Bars/Healthbar");
        mpBar = GameObject.Find("immue(Clone)/Canvas/Elite/Bars/Manabar");
        exp =  GameObject.Find("immue(Clone)/Canvas/Expbar/exp");
        nm.GetComponent<Text>().text = "Immune";
        exp.GetComponent<Image>().fillAmount = 0f;
        nm.GetComponent<Text>().text = this.gameObject.name;
        updateBar();
        lvCount.GetComponent<Text>().text = "1";
    }

    // Update is called once per frame
    void Update(){
        // Debug.Log(damage);
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
    }

    void updateBar(){
        hpBar.GetComponent<Image>().fillAmount = currentHP/maxHP;
        mpBar.GetComponent<Image>().fillAmount = currentMP/maxMP;
    }

    void updateStats(){
        lv = int.Parse(lvCount.GetComponent<Text>().text);
        damage = maxHP/100 * 8f * extraDamage;
        maxHP = lv*100;
        maxMP = lv*100;
    }

}