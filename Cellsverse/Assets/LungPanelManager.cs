using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class RulePanelManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ExampleCoroutine());
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel("cellverse");
        }
    }

    IEnumerator ExampleCoroutine()
    {
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
