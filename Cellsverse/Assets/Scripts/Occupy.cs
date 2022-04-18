using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class Occupy : MonoBehaviour
{
    public string playerBoundingName = "PlayerBoundary";
    private List<string> occupyingPlayers = new List<string>();
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

            occupyingPlayers.Add(photonView.Owner.NickName);
            Debug.Log(occupyingPlayers);

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
    private void OnTriggerExit2D(Collider2D collision)
    {
        string name = collision.gameObject.name;
        if (name == playerBoundingName)
        {
            var photonView = collision.gameObject.transform.parent.gameObject.GetComponent<PhotonView>();
            Debug.Log(photonView.Owner.NickName);
            occupyingPlayers.Remove(photonView.Owner.NickName);
            Debug.Log(occupyingPlayers);
        }

    }
}
