using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class lungLogic : MonoBehaviour
{
    public float time = 5;
    private bool willTp = true;
    public static int ownGameScore = 0;
    public static int enemyGameScore = 0;
    public GameObject dmgCircle;
    public GameObject targetCircle;
    public static string currentLocation = "lung";
    static PhotonView PV1;


    // Start is called before the first frame update
    void Start()
    {
        currentLocation = "lung";
        PV1 = GetComponent<PhotonView>();
        if(PhotonNetwork.IsMasterClient){
            PhotonNetwork.Instantiate(targetCircle.name, new Vector2(2.2f, -3.4f), Quaternion.identity);
            PhotonNetwork.Instantiate(dmgCircle.name, new Vector2(2.3f, 0.5f), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static void hpToZero()
    {
        
        enemyGameScore += 1;
        currentLocation = "heart";
        PV1.RPC("enemyWin", RpcTarget.Others);            
        
        
            
        

    }
    [PunRPC]
    void enemyWin()
    {
        Debug.Log("enemyWin");
        ownGameScore += 1;
        currentLocation = "heart";
        PV1.RPC("startTp", RpcTarget.MasterClient);
        

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
                PhotonNetwork.LoadLevel("heart");
            }
        }
    }
}
