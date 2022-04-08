using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class NetworkConnector : MonoBehaviourPunCallbacks
{
    public GameObject playerPrefab;
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }
    #region Pun Callbacks
    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to photon!");
        // Try to join a random room
        PhotonNetwork.JoinRandomRoom();
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.LogWarning($"Failed to connect: {cause}");
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        // Failed to connect to random, probably because none exist
        Debug.Log(message);
        // Create a new room
        PhotonNetwork.CreateRoom("Nat13213 Room");
    }
    public override void OnJoinedRoom()
    {
        Debug.Log($"{PhotonNetwork.CurrentRoom.Name} joined!");
        PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(0, 3.0f, 0), Quaternion.identity);

    }
    #endregion

}