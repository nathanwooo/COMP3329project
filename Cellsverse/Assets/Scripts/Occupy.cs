using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class Occupy : MonoBehaviour
{
    public string playerBoundingName = "PlayerBoundary";
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D obj)
    {
        string name = obj.gameObject.name;

        // if collided with bullet
        Debug.Log("Enter " + name);
        /*
        Debug.Log("Player Enters");
        Debug.Log(obj);
        */
        if (name == playerBoundingName)
        {
            Debug.Log("Player Enters");
            Debug.Log(obj);
            var photonView = obj.gameObject.transform.parent.gameObject.GetComponent<PhotonView>();
            Debug.Log(photonView.Owner.NickName);

            /* Debug.Log(PhotonNetwork.CurrentRoom.Players);
            Debug.Log(photonId);
            foreach (KeyValuePair<int, Player> player in PhotonNetwork.CurrentRoom.Players)
            {
                Debug.Log(player.Key);
            }
            var playerName = PhotonView.Find(photonId).playerName;
            */
            // var playerName = PhotonNetwork.CurrentRoom.Players[photonId];


        }
    }
}
