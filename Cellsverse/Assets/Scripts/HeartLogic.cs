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
    void Start()
    {
        PV = GetComponent<PhotonView>();
        Debug.Log(PV.IsMine);
        if (PhotonNetwork.IsMasterClient && PV.IsMine)
        {
        Debug.Log("Creatttttttttttttttttttttttttttttttttttttttttttttttttttttttt");
            var map = PhotonNetwork.Instantiate(destryableMap.name, new Vector3(0, 0, 0), Quaternion.identity);
            PV.RPC("createTileMap", RpcTarget.AllBufferedViaServer, map);
        Debug.Log("213122222222223");
        Debug.Log(count++);
        } 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [PunRPC]
    void createTileMap(GameObject map)
    {
        
        map.transform.SetParent(grid.transform);
    }
}
