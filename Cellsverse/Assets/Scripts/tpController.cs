using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public  class tpController : PhotonView
{
    public static int testvar = 0;
    public static string[] tpArea = new string[2] { "heart", "liver" };
    public static int currentTpIndex = 0;
    public static int ownGameScore = 0;
    public static int enemyGameScore = 0;
    public void  hpToZero()
    {

        enemyGameScore += 1;

        this.RPC("enemyWin", RpcTarget.Others);





    }
    [PunRPC]
    void enemyWin()
    {
        Debug.Log("enemyWin");
        ownGameScore += 1;
        this.RPC("startTp", RpcTarget.MasterClient);


    }
    [PunRPC]
    void startTp()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (enemyGameScore == 2 || ownGameScore == 2)
            {
                PhotonNetwork.LoadLevel("End Game");
            }
            else
            {
                PhotonNetwork.LoadLevel(tpArea[currentTpIndex++]);
            }
        }
    }

}
