using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerSpwaner : MonoBehaviour
{
    //public GameObject[] playerPrefabs;
    public Transform[] spwanPoints;
    public GameObject playerToSpwan; //comment this later when change sprite

    private void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {

            Transform spawnPoint = spwanPoints[0];   //random spwan place
                                                                //GameObject PlayerToSpwan = playerPrefabs[PhotonNetwork.LocalPlayer.CustomProperties["PlayerAvatar"]];
                                                                //GameObject playerToSpwan = playerPrefabs[0];
                                                                //PhotonNetwork.Instantiate(playerToSpwan.name)
            PhotonNetwork.Instantiate(playerToSpwan.name, spawnPoint.position, Quaternion.identity);
        } else
        {
            Transform spawnPoint = spwanPoints[1];   //random spwan place
                                                     //GameObject PlayerToSpwan = playerPrefabs[PhotonNetwork.LocalPlayer.CustomProperties["PlayerAvatar"]];
                                                     //GameObject playerToSpwan = playerPrefabs[0];
                                                     //PhotonNetwork.Instantiate(playerToSpwan.name)
            PhotonNetwork.Instantiate(playerToSpwan.name, spawnPoint.position, Quaternion.identity);
        }
        
    }

}
