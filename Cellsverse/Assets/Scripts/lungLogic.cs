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
        
    }
    public static void hpToZero()
    {
        
        enemyGameScore += 1;
        
        PV1.RPC("enemyWin", RpcTarget.Others);            
        
        
            
        

    }
    [PunRPC]
    void enemyWin()
    {
        Debug.Log("enemyWin");
        ownGameScore += 1;
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
                PhotonNetwork.LoadLevel(tpArea[currentTpIndex++]);
            }
        }
    }
}
