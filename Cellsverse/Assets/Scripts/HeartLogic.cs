using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class HeartLogic : MonoBehaviour
{
    public GameObject destryableMap;
    // Start is called before the first frame update
    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.Instantiate(destryableMap.name, new Vector3(0, 0, 0), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
