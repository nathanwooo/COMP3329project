using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class HeartLogic : MonoBehaviour
{
    public GameObject destryableMap;
    public GameObject grid;
    private int count = 0;
    private PhotonView PV;
    // Start is called before the first frame update
    void Awake()
    {
        PV = GetComponent<PhotonView>();
        if (PhotonNetwork.IsMasterClient && PV.IsMine)
        {
            PV.RPC("createTileMap", RpcTarget.AllBuffered);
        Debug.Log("213122222222223");
        Debug.Log(count++);
        } 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [PunRPC]
    void createTileMap()
    {
        var map = PhotonNetwork.Instantiate(destryableMap.name, new Vector3(0, 0, 0), Quaternion.identity);
        map.transform.SetParent(grid.transform);
    }
}
