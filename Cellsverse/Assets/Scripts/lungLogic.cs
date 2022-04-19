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
    // Start is called before the first frame update
    void Start()
    {
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
}
