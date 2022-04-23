using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class RulePanelManager : MonoBehaviour
{
    private float shrinkTimer = 5;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        shrinkTimer -= Time.deltaTime;
        if (shrinkTimer < 0)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.LoadLevel("cellverse");
            }
        }
    }
}
