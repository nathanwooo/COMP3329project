using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class LungRule : MonoBehaviour
{
    private float shrinkTimer = 5;
    private bool willTP = true;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        shrinkTimer -= Time.deltaTime;
        if (shrinkTimer < 0 && willTP)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.LoadLevel("cellverse");
                willTP = false;
            }
        }
    }
}
