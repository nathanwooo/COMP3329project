using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class lungLogic : MonoBehaviour
{
    public float time = 5;
    private bool willTp = true;
    public static int ownGameScore = 6;
    public static int enemyGameScore = 9;
    public GameObject dmgCircle;
    public GameObject targetCircle;
    static PhotonView PV1;
    private static string[] tpArea= new string[2] {"heart","liver"};
    private static int currentTpIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        PV1 = GetComponent<PhotonView>();
        if(PhotonNetwork.IsMasterClient){
            PhotonNetwork.Instantiate(targetCircle.name, new Vector2(2.2f, -3.4f), Quaternion.identity);
            PhotonNetwork.Instantiate(dmgCircle.name, new Vector2(2.3f, 0.5f), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        if (time < 0 && willTp)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.LoadLevel("heart");
                willTp = false;
            }
        }
    }
    public static void hpToZero()
    {
        
        enemyGameScore += 1;
        if (enemyGameScore == 2)
        {
            PV1.RPC("enemyWin", RpcTarget.Others);
            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.LoadLevel("End Game");

            }
        }
        else
        {
            PV1.RPC("enemyWin", RpcTarget.Others);
            if (PhotonNetwork.IsMasterClient)
            {
                
                PhotonNetwork.LoadLevel(tpArea[currentTpIndex++]);


            }
        }
        
            
        

    }
    [PunRPC]
    static void enemyWin()
    {
        ownGameScore += 1;
        
        if (ownGameScore ==2 && PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel("End Game");

        }
        else if(PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel(tpArea[currentTpIndex++]);
        }

    }
}
