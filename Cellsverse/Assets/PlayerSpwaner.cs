using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerSpwaner : MonoBehaviour
{
    // public GameObject[] playerPrefabs;
    public Transform[] spwanPoints;
    public GameObject playerToSpwan; //comment this later when change sprite


    private void Start()
    {
        var index = 0;
        if (PhotonNetwork.IsMasterClient)
        {
            index = 0;
            //GameObject PlayerToSpwan = playerPrefabs[PhotonNetwork.LocalPlayer.CustomProperties["PlayerAvatar"]];
            //GameObject playerToSpwan = playerPrefabs[0];
            //PhotonNetwork.Instantiate(playerToSpwan.name)
            // PhotonNetwork.Instantiate(playerToSpwan.name, spawnPoint.position, Quaternion.identity);
        }
        else
        {
            index = 1;
        }
        Transform spawnPoint = spwanPoints[index];   //random spwan place
        // GameObject playerToSpwan = playerPrefabs[(int)PhotonNetwork.LocalPlayer.CustomProperties["playerAvatar"]];
        PhotonNetwork.Instantiate(playerToSpwan.name, spawnPoint.position, Quaternion.identity);

    }

}
