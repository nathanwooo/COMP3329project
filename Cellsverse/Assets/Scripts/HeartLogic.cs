using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class HeartLogic : MonoBehaviour
{
    public GameObject destryableMap;
    public GameObject grid;
    // Start is called before the first frame update
    void Awake()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            var map = PhotonNetwork.Instantiate(destryableMap.name, new Vector3(0, 0, 0), Quaternion.identity);
            map.transform.SetParent(grid.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
