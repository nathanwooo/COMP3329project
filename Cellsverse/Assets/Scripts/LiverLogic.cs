using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class LiverLogic : MonoBehaviour
{
    static PhotonView PV;
    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static void hpToZero()
    {

        lungLogic.enemyGameScore += 1;
        lungLogic.currentLocation = "End Game";
        PV.RPC("enemyWin", RpcTarget.Others);





    }
    [PunRPC]
    void enemyWin()
    {
        Debug.Log("enemyWin");
        lungLogic.ownGameScore += 1;
        lungLogic.currentLocation = "End Game";
        PV.RPC("startTp", RpcTarget.MasterClient);


    }
    [PunRPC]
    void startTp()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (lungLogic.enemyGameScore == 2 || lungLogic.ownGameScore == 2)
            {
                PhotonNetwork.LoadLevel("End Game");
            }
            else
            {
                PhotonNetwork.LoadLevel("lung");
            }
        }
    }
}
