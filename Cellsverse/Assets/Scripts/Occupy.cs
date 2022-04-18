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
    private float passedTime;
    private bool countTime = true;
    void Start()
    {
        
    }

    void Update()
    {
        if (countTime)
        {
            Debug.Log("Check length ..............................");
            Debug.Log(occupyingPlayers.Count);
            passedTime += Time.deltaTime;
            Debug.Log(passedTime);
        }
    }

    void OnTriggerEnter2D(Collider2D obj)
    {
        string name = obj.gameObject.name;

        // if collided with bullet
        // Debug.Log("Enter " + name);
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
            if (occupyingPlayers.Count == 1)
            {
                passedTime = 0f;
                countTime = true;
            } else
            {
                countTime = false;
            }
            printList();

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
    void printList()
    {
        string playerinside = "";
        foreach (string name in occupyingPlayers)
        {
            playerinside += name+" ";
        }
        Debug.Log(playerinside);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        string name = collision.gameObject.name;
        if (name == playerBoundingName)
        {
            var photonView = collision.gameObject.transform.parent.gameObject.GetComponent<PhotonView>();
            Debug.Log("EXIT "+ photonView.Owner.NickName);
            occupyingPlayers.Remove(photonView.Owner.NickName);
            printList();
            if (occupyingPlayers.Count == 1)
            {
                passedTime = 0f;
                countTime = true;
            }
            else
            {
                countTime = false;
            }
        }

    }
}
