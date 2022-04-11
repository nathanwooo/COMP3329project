using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class liverLogic : MonoBehaviour
{
    private float time = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        if (time < 0)
        {

            PhotonNetwork.LoadLevel("heart");
        }
    }
}
