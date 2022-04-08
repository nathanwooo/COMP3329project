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
        PhotonNetwork.JoinRandomRoom();
        Debug.Log("Connected to photon!");
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
        PhotonNetwork.CreateRoom("My First Room");
    }
    public override void OnJoinedRoom()
    {
        PhotonNetwork.Instantiate(playerPrefab.name,
new Vector3(0, 3.0f, 0), Quaternion.identity);
        Debug.Log($"{PhotonNetwork.CurrentRoom.Name} joined!");
    }
    #endregion
}